using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestAPIServiceApplication.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundedEntry : ContentView
    {
        #region BindableProperties
        public static readonly BindableProperty PlaceHolderProperty =
            BindableProperty.Create("PlaceHolder", typeof(string), typeof(RoundedEntry), default(string));

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(RoundedEntry), default(string));

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create("CornerRadius", typeof(float), typeof(RoundedEntry), default(float));

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create("IsPassword", typeof(bool), typeof(RoundedEntry), false);
        #endregion

        #region Properties
        public string PlaceHolder 
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        public float CornerRadius
        {
            get { return (float)GetValue(CornerRadiusProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        public RoundedEntry()
        {
            InitializeComponent();

            CustomEntry.SetBinding(Entry.PlaceholderProperty, new Binding("PlaceHolder", source: this));
            CustomEntry.SetBinding(Entry.TextProperty, new Binding("Text", source: this));
            CustomEntry.SetBinding(Entry.IsPasswordProperty, new Binding("IsPassword", source:this));

            CustomFrame.SetBinding(Frame.CornerRadiusProperty, new Binding("CornerRadius", source: this));

            var gr = new TapGestureRecognizer()
            {
                Command = new Command(() =>
                { 
                    
                })
            };
        }
    }
}