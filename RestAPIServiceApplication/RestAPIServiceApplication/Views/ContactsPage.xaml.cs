using RestAPIServiceApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestAPIServiceApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsPage : ContentPage
    {
        public ContactsViewModel Contacts { get; set; }
        public ContactsPage()
        {
            this.Contacts = new ContactsViewModel();
            this.BindingContext = this.Contacts;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.Contacts.RefreshContacts();
        }
    }
}