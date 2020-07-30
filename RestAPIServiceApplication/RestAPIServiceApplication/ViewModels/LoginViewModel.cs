using RestAPIServiceApplication.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using RestAPIServiceApplication.Services;
using RestAPIServiceApplication.Models;
using RestAPIServiceApplication.Helpers;
using System.Net.Http.Headers;

namespace RestAPIServiceApplication.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Class fields
        private string email, password;
        private bool isRunning;
        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set
            {
                if (this.email == value)
                    return;
                this.email = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return this.password; }
            set
            {
                if (this.password == value)
                    return;
                this.password = value;
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
        public ICommand LoginCommand
        {
            get { return new Command(this.Login); }
        }
        public ICommand RegisterViewCommand
        {
            get
            {
                return new Command(async () =>
              await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage()));
            }
        }

        public ICommand RecoverPasswordCommand 
        {
            get 
            {
                return new Command(async () => 
                    await Application.Current.MainPage.Navigation.PushAsync(new EmailVerificationPage())); 
            }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            apiService = new ApiService();
        }
        #endregion

        #region Methods
        public async void Login()
        {
            IsRunning = true;

            MemoryDataAcess.UserProfile = 
                await apiService.LoginAsync(Email, Password);

            if (string.IsNullOrEmpty(MemoryDataAcess.UserProfile.Token.AccessToken))
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    MemoryDataAcess.UserProfile.Token.ErrorDescription,
                    "OK");
            else
            {
                App.Navigation = new NavigationPage(new ContactsPage()) 
                    { Title = "Contacts", BarBackgroundColor = Color.FromHex("3f48cc") };
                Application.Current.MainPage = new MasterDetail();

                Settings.UserName = this.Email;
                Settings.Password = this.Password;
            }

            IsRunning = false;
        }
        #endregion
    }
}
