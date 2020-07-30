using RestAPIServiceApplication.Helpers;
using RestAPIServiceApplication.Models;
using RestAPIServiceApplication.Services;
using RestAPIServiceApplication.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestAPIServiceApplication
{
    public partial class App : Application
    {
        public static NavigationPage Navigation { get; set; }

        public App()
        {
            InitializeComponent();

            Settings.UserName = string.Empty;
            Settings.Password = string.Empty;

            this.VerifyLogin();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public async void VerifyLogin()
        {
            if (string.IsNullOrEmpty(Settings.UserName) &&
                string.IsNullOrEmpty(Settings.Password))
                MainPage = new NavigationPage(new LoginPage())
                    { BarBackgroundColor = Color.FromHex("3f48cc") };    
            else
            {
                var apiService = new ApiService();
                var userModel = await
                    apiService.LoginAsync(Settings.UserName, Settings.Password);

                if (!string.IsNullOrEmpty(userModel.Token.Error))
                {
                    MainPage = new NavigationPage(new LoginPage());
                    return;
                }

                MemoryDataAcess.UserProfile = userModel;

                Navigation = new NavigationPage(new ContactsPage())
                { Title = "Contacts" };
                MainPage = new MasterDetail();
            }
        }
    }
}
