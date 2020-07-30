using RestAPIServiceApplication.Models;
using RestAPIServiceApplication.Services;
using RestAPIServiceApplication.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Net.Http.Headers;
using System.Linq;
using RestAPIServiceApplication.Helpers;
using System.Threading.Tasks;

namespace RestAPIServiceApplication.ViewModels
{
    public class ContactsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion
        
        #region Class fields
        private ObservableCollection<ContactViewModel> contactsCollection;
        private ObservableCollection<ContactViewModel> contactsDefaultCollection;
        private ContactViewModel selectedContanct;
        private string searchText;
        private bool isRefreshing;
        #endregion

        #region Properties
        public ContactViewModel SelectedContact 
        {
            get { return this.selectedContanct; }
            set 
            {
                if (this.selectedContanct == value)
                    return;
                this.selectedContanct = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ContactViewModel> ContactsCollection
        {
            get { return this.contactsCollection; }
            set 
            {
                if (this.contactsCollection == value)
                    return;
                this.contactsCollection = value;
                OnPropertyChanged();
            }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set
            {
                if (this.isRefreshing == value)
                    return;
                this.isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get { return this.searchText; }
            set
            {
                if (this.searchText == value)
                    return;
                this.searchText = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand NewContactCommand
        {
            get
            {
                return new Command(async () =>
                    await App.Navigation.PushAsync(new AddContactPage()));
            }
        }

        public ICommand RefreshContactsCommand
        {
            get { return new Command(this.RefreshContacts); }
        }

        public ICommand DeleteContactCommand 
        {
            get { return new Command(this.DeleteContact); }
        }

        public ICommand SearchCommand 
        {
            get { return new Command<string>(this.Search); }
        }
        #endregion

        #region Constructors
        public ContactsViewModel()
        {
            PropertyChanged += ContactsViewModel_PropertyChanged;
            this.apiService = new ApiService();
            RefreshContacts();
        }

        private void ContactsViewModel_PropertyChanged(
            object sender, 
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchText))
                this.Search(SearchText);
        }

        #endregion

        #region Methods
        public async void RefreshContacts()
        {
            IsRefreshing = true;

            var contacts =
                (await this.apiService.GetAsync<IEnumerable<ContactViewModel>>(
                    "ContactsModel",
                    auth: new AuthenticationHeaderValue(
                        "Bearer", MemoryDataAcess.UserProfile.Token.AccessToken)))
                        .Select(e => ConverterHelper.ToContactViewModel(e));

            contactsDefaultCollection = new ObservableCollection<ContactViewModel>(contacts);
            ContactsCollection = contactsDefaultCollection;
            IsRefreshing = false;
        }

        public void DeleteContact()
        {
            //this.RefreshContacts();
        }

        public void Search(string reference)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                ContactsCollection = contactsDefaultCollection;
                return;
            }

            ContactsCollection = new ObservableCollection<ContactViewModel>
                (ContactsCollection.Where(e =>
                    e.ContactName.Contains(reference) ||
                    e.ContactNumber.ToString().Contains(reference) ||
                    e.ContactCompanyName.Contains(reference) ||
                    e.ContactAlias.Contains(reference) ||
                    e.UserId.Contains(reference)));
        }
        #endregion
    }
}
