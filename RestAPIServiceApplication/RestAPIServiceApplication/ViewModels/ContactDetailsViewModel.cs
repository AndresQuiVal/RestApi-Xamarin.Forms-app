using Plugin.Media;
using Plugin.Media.Abstractions;
using RestAPIServiceApplication.Helpers;
using RestAPIServiceApplication.Models;
using RestAPIServiceApplication.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RestAPIServiceApplication.ViewModels
{
    public class ContactDetailsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Class fields
        private MediaFile file;
        private ImageSource contactImage;
        private ContactViewModel contact;
        #endregion

        #region Properties
        public ContactViewModel Contact 
        {
            get { return this.contact; }
            set
            {
                if (this.contact == value)
                    return;
                this.contact = value;
                OnPropertyChanged();
            }
        }

        public ImageSource ContactImage
        {
            get { return this.contactImage; }
            set
            {
                if (this.contactImage == value)
                    return;
                this.contactImage = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand DeleteContactCommand 
        {
            get { return new Command(this.DeleteContact); }
        }

        public ICommand EditContactCommand
        {
            get { return new Command(this.EditContact); }
        }

        public ICommand ChangeImageCommand
        {
            get { return new Command(this.ChangeImage); }
        }
        #endregion

        #region Constructors
        public ContactDetailsViewModel(ContactViewModel contact)
        {
            this.apiService = new ApiService();
            this.ContactImage = Device.RuntimePlatform == Device.UWP ? "Assets/profileImage.png" : "profileImage";
            this.Contact = contact;
            this.ContactImage = this.Contact.ContactImageSource;
        }
        #endregion

        #region Methods
        public async void DeleteContact()
        {
            bool isSuccess =
                await this.apiService.DeleteAsync(
                    "ContactsModel",
                    Contact.Id,
                    new AuthenticationHeaderValue(
                        "Bearer", MemoryDataAcess.UserProfile.Token.AccessToken));
            string title = "Sucess", message = "Your contact has been deleted!";
            if (!isSuccess)
            {
                title = "Error";
                message = "Something ocurred, try later!";
            }
            await Application.Current.MainPage.DisplayAlert(
                title, message, "OK");

            await App.Navigation.PopAsync();
        }

        public async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsPickPhotoSupported)
            {
                string actionSheetResponse = await Application.Current.MainPage.DisplayActionSheet(
                    "From where to select the image?",
                    "Cancel",
                    null,
                    "From gallery",
                    "Take snap");

                if (!string.IsNullOrWhiteSpace(actionSheetResponse) 
                    && actionSheetResponse != "Cancel")
                {
                    if (actionSheetResponse == "Take snap")
                    {
                        this.file = await CrossMedia.Current.TakePhotoAsync(
                         new StoreCameraMediaOptions
                         {
                             Directory = "TestImages", //<---- Directory name
                             Name = "test.png",
                             PhotoSize = PhotoSize.Small
                         });
                    }
                    else
                        this.file = await CrossMedia.Current.PickPhotoAsync();

                    if (this.file != null)
                    {
                        this.ContactImage = ImageSource.FromStream(() =>
                        {
                            var stream = file.GetStream();
                            return stream;
                        });
                    }
                }
            }
        }

        public async void EditContact()
        {
            if (file != null)
            {
                var ms = new MemoryStream();
                file.GetStream().CopyTo(ms);
                file.Dispose();
                Contact.ContactImage = ms.ToArray();
            }

            bool isSuccess =
                await this.apiService.PutAsync(
                    "ContactsModel",
                    ConverterHelper.ToContactModel(Contact),
                    new AuthenticationHeaderValue(
                        "Bearer", MemoryDataAcess.UserProfile.Token.AccessToken),
                    $"/{Contact.Id}");
            
            string title = "Sucess", message = "Your contact has been edited!";
            if (!isSuccess)
            {
                title = "Error";
                message = "Something ocurred, try later!";
            }
            await Application.Current.MainPage.DisplayAlert(
                title, message, "OK");

            await App.Navigation.PopAsync();
        }
        #endregion
    }
}
