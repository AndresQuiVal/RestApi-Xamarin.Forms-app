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
    public class ProfileViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Class fields
        private string firstName, lastName;
        private ImageSource profileImage;
        private MediaFile file;
        private bool canExecute, isRunning;
        #endregion

        #region Properties
        public UserProfileModel UserProfile { get; set; }
        public string FirstName 
        {
            get { return MemoryDataAcess.UserProfile.FirstName; }
            set
            {
                //if (this.firstName == value)
                //    return;
                //this.firstName = value;
                MemoryDataAcess.UserProfile.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return MemoryDataAcess.UserProfile.LastName; }
            set
            {
                //if (this.lastName == value)
                //    return;
                //this.lastName = value;
                MemoryDataAcess.UserProfile.LastName = value;
                OnPropertyChanged();
            }
        }

        public ImageSource ProfileImage 
        {
            get { return this.profileImage; }
            set
            {
                if (this.profileImage == value)
                    return;
                this.profileImage = value;
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
        #endregion

        #region Commands
        public ICommand ChangeImageCommand 
        {
            get { return new Command(this.ChangeImage); }
        }

        public ICommand SaveProfileChangesCommand
        {
            get { return new Command(this.SaveProfileChanges, () => canExecute); }
        }
        #endregion

        #region Constructors
        public ProfileViewModel()
        {
            PropertyChanged += ProfileViewModel_PropertyChanged;
            this.apiService = new ApiService();
            this.ProfileImage = MemoryDataAcess.UserProfile.UserProfileImageSource;
        }
        #endregion

        #region Methods
        private void ProfileViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            canExecute = true;
        }

        public async void SaveProfileChanges()
        {
            IsRunning = true;
            if (this.file != null)
            {
                var ms = new MemoryStream();
                file.GetStream().CopyTo(ms);
                file.Dispose();
                MemoryDataAcess.UserProfile.UserProfileImage = ms.ToArray();
            }

            bool isSuccess = await apiService.PutAsync(
               "UserProfileModels",
               model: ConverterHelper.ToUserProfileModel(MemoryDataAcess.UserProfile),
               auth: new AuthenticationHeaderValue(
                   "Bearer", MemoryDataAcess.UserProfile.Token.AccessToken));

            IsRunning = false;
            string title = "Sucess", message = "Your contact has been added!";

            if (!isSuccess)
            {
                title = "Failure";
                message = "Our systems have crashed, try later";
            }
                
            await Application.Current.MainPage.DisplayAlert(
                title,
                message,
                "OK");

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
                        this.ProfileImage = ImageSource.FromStream(() =>
                        {
                            var stream = file.GetStream();
                            return stream;
                        });
                    }
                }
            }
        }
        #endregion
    }
}
