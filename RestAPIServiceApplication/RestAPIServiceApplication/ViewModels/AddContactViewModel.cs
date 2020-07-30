using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public class AddContactViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Class fields
        private string name, alias, companyName;
        private long phoneNumber;
        private bool isRunning;
        private ImageSource contactImage;
        private MediaFile file;
        #endregion

        #region Properties
        public string Name 
        {
            get { return this.name; }
            set
            {
                if (this.name == value)
                    return;
                this.name = value;
                OnPropertyChanged();
            }
        }
        public string Alias
        {
            get { return this.alias; }
            set
            {
                if (this.alias == value)
                    return;
                this.alias = value;
                OnPropertyChanged();
            }
        }
        public string CompanyName
        {
            get { return this.companyName; }
            set
            {
                if (this.companyName == value)
                    return;
                this.companyName = value;
                OnPropertyChanged();
            }
        }
        public long PhoneNumber
        {
            get { return this.phoneNumber; }
            set
            {
                if (this.phoneNumber == value)
                    return;
                this.phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public bool IsRunning 
        {
            get { return this.isRunning; }
            set
            {
                if (this.isRunning == value)
                    return;
                this.isRunning = value;
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
        public ICommand AddContactCommand 
        {
            get { return new Command(this.AddContact); }
        }

        public ICommand ChangeImageCommand 
        {
            get { return new Command(this.ChangeImage); } 
        }
        #endregion

        #region Contructors
        public AddContactViewModel()
        {
            apiService = new ApiService();
            ContactImage = Device.RuntimePlatform == Device.UWP ? "Assets/profileImage.png" : "profileImage";
        }
        #endregion

        #region Methods
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

                if (!string.IsNullOrWhiteSpace(actionSheetResponse))
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

        public async void AddContact()
        {
            IsRunning = true;

            ContactsModel contactModel = new ContactsModel()
            {
                ContactName = this.Name,
                ContactAlias = this.Alias,
                ContactCompanyName = this.CompanyName,
                ContactNumber = this.PhoneNumber
            };

            if (file != null)
            {
                var ms = new MemoryStream();
                file.GetStream().CopyTo(ms);
                file.Dispose();
                contactModel.ContactImage = ms.ToArray();
            }

            bool isSuccess = (await apiService.PostAsync(
                "ContactsModel", 
                model: contactModel,
                auth: new AuthenticationHeaderValue(
                    "Bearer", MemoryDataAcess.UserProfile.Token.AccessToken))).IsSuccessStatusCode;

            IsRunning = false;

            if (isSuccess)
                await Application.Current.MainPage.DisplayAlert(
                    "Sucess",
                    "Your contact has been added!",
                    "OK");
            else
                await Application.Current.MainPage.DisplayAlert(
                    "Failure",
                    "Our systems have crashed, try later",
                    "OK");

            await App.Navigation.PopAsync();
        }
        #endregion
    }
}
