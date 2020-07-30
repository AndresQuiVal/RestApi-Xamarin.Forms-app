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
    public partial class PasswordRecoverPage : ContentPage
    {
        public PasswordRecoverPage(string email)
        {
            this.BindingContext = new PasswordRecoverViewModel(email);
            InitializeComponent();
        }
    }
}