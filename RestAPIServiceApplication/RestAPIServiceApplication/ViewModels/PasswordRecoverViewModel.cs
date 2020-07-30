using RestAPIServiceApplication.Models;
using RestAPIServiceApplication.Services;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RestAPIServiceApplication.ViewModels
{
    public class PasswordRecoverViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Class fields
        private bool isRunning;
        private string password, confirmPassword, email;
        #endregion

        #region Properties
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

        public string ConfirmPassword
        {
            get { return this.confirmPassword; }
            set
            {
                if (this.confirmPassword == value)
                    return;
                this.confirmPassword = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand RecoverPasswordCommand 
        {
            get { return new Command(this.RecoverPassword); }
        }
        #endregion

        #region Constructors
        public PasswordRecoverViewModel(string email)
        {
            this.email = email;
            this.apiService = new ApiService();
        }
        #endregion

        #region Methods
        public async void RecoverPassword()
        {
            IsRunning = true;
            bool isSuccess = (await this.apiService.PostAsync(
                "Account", 
                new PasswordChangeModel() 
                {
                    Email = this.email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                },
                $"/SendPasswordRecover")).IsSuccessStatusCode;

            string title = "Success",
                message = $"Your new password has been set";
            if (!isSuccess)
            {
                title = "Error";
                message = "Something happened while changing your password, try later";
            }

            await Application.Current.MainPage.DisplayAlert(
                title, message, "OK");

            IsRunning = false;
        }
        #endregion
    }
}
