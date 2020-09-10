using RestAPIServiceApplication.Services;
using RestAPIServiceApplication.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RestAPIServiceApplication.ViewModels
{
    public class EmailVerificationViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Class fields
        private bool isRunning;
        private string email, verificationCode, emailVerificationCode;
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

        public string VerificationCode
        {
            get { return this.verificationCode; }
            set
            {
                if (this.verificationCode == value)
                    return;
                this.verificationCode = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand SendVerificationCodeCommand
        {
            get { return new Command(this.SendVerificationCode); }
        }

        public ICommand VerifyCodeCommand
        {
            get { return new Command(this.VerifyCode); }
        }
        #endregion

        #region Constructors
        public EmailVerificationViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Methods
        public async void SendVerificationCode()
        {
            var response = await this.apiService.PostAsync(
                "Account", (string)null, $"/SendVerificationCode?email={Email}");

            this.emailVerificationCode = await response.Content.ReadAsStringAsync();

            string title = "Success",
                message = $"The verification code has been sent to the email: {Email}";
            
            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(emailVerificationCode))
            {
                title = "Error";
                message = "Something happened while making requests to our servers...";
            }

            await Application.Current.MainPage.DisplayAlert(
                title, message, "OK");
        }

        public async void VerifyCode()
        {
            if (string.Equals(this.VerificationCode, emailVerificationCode))
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PasswordRecoverPage(this.Email));
                return;
            }

            await Application.Current.MainPage.DisplayAlert(
                "Code unequal",
                "The verification code is not the same, try again please",
                "OK");
        }
        #endregion
    }
}
