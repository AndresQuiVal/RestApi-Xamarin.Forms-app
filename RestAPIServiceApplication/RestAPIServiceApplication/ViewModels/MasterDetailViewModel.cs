using RestAPIServiceApplication.Helpers;
using RestAPIServiceApplication.Models;
using RestAPIServiceApplication.Services;
using RestAPIServiceApplication.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;

namespace RestAPIServiceApplication.ViewModels
{
    public class MasterDetailViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Class fields
        private ObservableCollection<Module> modulesCollection;
        private string userName;
        #endregion

        #region Properties
        public ObservableCollection<Module> ModulesCollection 
        {
            get { return this.modulesCollection; }
            set
            {
                if (this.modulesCollection == value)
                    return;
                this.modulesCollection = value;
                OnPropertyChanged();
            }
        }

        public ImageSource ProfileImageSource { get; set; } = MemoryDataAcess.UserProfile.UserProfileImageSource;

        public string UserName
        {
            get { return $"Bienvenido {userName}"; }
            set
            {
                if (this.userName == value)
                    return;
                this.userName = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Constructors
        public MasterDetailViewModel()
        {
            this.apiService = new ApiService();
            this.UserName = MemoryDataAcess.UserProfile.FirstName;

            ModulesCollection = new ObservableCollection<Module>
            {
                new Module()
                {
                    ModuleIcon =
                        /*Device.RuntimePlatform == Device.UWP ? "Assets/logoutIcon.png" : */"logoutIcon.png",
                    ModuleTitle = "Logout",
                    ModuleCommand = new Command(this.Logout)
                },
                new Module()
                {
                    ModuleIcon =
                        /*Device.RuntimePlatform == Device.UWP ? "Assets/profileModuleIcon.png" : */"profileModuleIcon",
                    ModuleTitle = "My profile",
                    ModuleCommand = new Command(async () =>
                    {
                        await App.Navigation.PushAsync(new ProfilePage());
                        MasterDetailPage mpage = Application.Current.MainPage as MasterDetailPage;
                        mpage.IsPresented = false;
                    })
                }
            };

            MessagingCenter.Subscribe<ProfileViewModel>(
                this, Consts.NameUpdateConst, (sender) =>
                {
                    UserName = MemoryDataAcess.UserProfile.FirstName;
                });
        }
        #endregion

        #region Methods
        public async void Logout()
        {
            bool isSuccess = (await this.apiService.PostAsync<string>(
                "Account", 
                method: "/Logout",
                auth: new AuthenticationHeaderValue(
                    "Bearer", MemoryDataAcess.UserProfile.Token.AccessToken))).IsSuccessStatusCode;

            if (isSuccess)
            {
                Settings.UserName = string.Empty;
                Settings.Password = string.Empty;
                Application.Current.MainPage = new NavigationPage(new LoginPage())
                { BarBackgroundColor = Color.FromHex("3f48cc") };
            }
                
        }
	    #endregion
    }
}
