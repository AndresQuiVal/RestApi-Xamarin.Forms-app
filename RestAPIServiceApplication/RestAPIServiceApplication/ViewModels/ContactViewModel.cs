using RestAPIServiceApplication.Interfaces;
using RestAPIServiceApplication.Models;
using RestAPIServiceApplication.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RestAPIServiceApplication.ViewModels
{
    public class ContactViewModel : ContactsModel, IBaseViewModel
    {
        #region ViewModelServices
        private IBaseViewModel baseViewModel = new BaseViewModel();
        #endregion

        #region Class fields
        private ImageSource contactImageSource;
        #endregion

        #region Properties
        public ImageSource ContactImageSource
        {
            get
            {
                if (this.ContactImage != null)
                    this.contactImageSource =
                        ImageSource.FromStream(() => new MemoryStream(this.ContactImage));
                else this.contactImageSource =/* Device.RuntimePlatform == Device.UWP ? "Assets/profileImage.png" : */"profileImage";
                return this.contactImageSource;
            }
            set 
            {
                if (this.contactImageSource == value)
                    return;
                this.contactImageSource = value;
                OnPropertyChanged();
            }
        }

        public ICommand ViewContactInfo
        {
            get
            {
                return new Command(async () =>
                {
                    await App.Navigation.PushAsync(
                        new ContactDetailsPage(this));
                });
            }
        }
        #endregion

        #region Methods
        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            baseViewModel.OnPropertyChanged(propertyName);
        #endregion

    }
}
