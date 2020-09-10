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
        private string contactName, contactAlias, contactCompanyName;
        #endregion

        #region Properties
        public ImageSource ContactImageSource
        {
            get
            {
                if (ContactImage != null)
                    contactImageSource =
                        ImageSource.FromStream(() => new MemoryStream(this.ContactImage));
                else contactImageSource =/* Device.RuntimePlatform == Device.UWP ? "Assets/profileImage.png" : */"profileImage";
                return contactImageSource;
            }
            set 
            {
                if (contactImageSource == value)
                    return;
                contactImageSource = value;
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
