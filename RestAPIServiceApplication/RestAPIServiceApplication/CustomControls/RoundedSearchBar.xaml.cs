using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestAPIServiceApplication.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundedSearchBar : ContentView
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(RoundedSearchBar), default(string));

        public static readonly BindableProperty SearchCommandProperty =
            BindableProperty.Create(nameof(SearchCommand), typeof(ICommand), typeof(RoundedSearchBar), null);

        public static readonly BindableProperty PlaceHolderProperty =
            BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(RoundedSearchBar), default(string));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); } 
        }

        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        public ICommand SearchCommand
        {
            get { return (ICommand)GetValue(SearchCommandProperty); }
            set { SetValue(SearchCommandProperty, value); }
        }
        public RoundedSearchBar()
        {
            InitializeComponent();
            CustomSearchBar.SetBinding(SearchBar.TextProperty, new Binding(nameof(Text), source: this));
            CustomSearchBar.SetBinding(SearchBar.PlaceholderProperty, new Binding(nameof(PlaceHolder), source: this));
            CustomSearchBar.SetBinding(SearchBar.SearchCommandProperty, new Binding(nameof(this.SearchCommand), source: this));
        }
    }
}