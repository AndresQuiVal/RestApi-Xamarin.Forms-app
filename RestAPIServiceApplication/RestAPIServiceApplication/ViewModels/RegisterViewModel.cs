using RestAPIServiceApplication.Models;
using RestAPIServiceApplication.Services;
using RestAPIServiceApplication.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RestAPIServiceApplication.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Class fields
        private string firstName, lastName, email, password, confirmPassword;
        private bool isRunning;
        #endregion

        #region Properties
        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                if (this.firstName == value)
                    return;
                this.firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (this.lastName == value)
                    return;
                this.lastName = value;
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
        public ICommand RegisterCommand { get { return new Command(this.Register); } }
        #endregion

        #region Constructors
        public RegisterViewModel()
        {
            apiService = new ApiService();
        }
        #endregion

        #region Methods
        public async void Register()
        {
            this.IsRunning = true;

            var registerResponse = await apiService.RegisterUserAsync(
                new UserProfileModel
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                });

            this.IsRunning = false;

            string title = "User registered!", message = $"{Email} has been registered";
            if (registerResponse != null)
            {
                title = "Error";
                message = ((string[])
                    typeof(ModelState).GetRuntimeFields().First(
                        e => ((string[]) e.GetValue(registerResponse.ModelState)) != null).GetValue(registerResponse.ModelState))[0];
            }

            await Application.Current.MainPage.DisplayAlert(
                title, message, "OK");
        }
        #endregion
    }
}
