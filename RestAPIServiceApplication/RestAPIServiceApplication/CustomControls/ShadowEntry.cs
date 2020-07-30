using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RestAPIServiceApplication.CustomControls
{
    public class ShadowEntry : Entry
    {
        #region Bindable Properties
        public static readonly BindableProperty HasShadowProperty =
            BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(ShadowEntry), false);

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(ShadowEntry), default(double));
        #endregion

        #region Properties
        public bool HasShadow 
        {
            get { return (bool) GetValue(HasShadowProperty); }
            set { SetValue(HasShadowProperty, value); }
        }

        public double CornerRadius
        {
            get { return (double) GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        #endregion
    }
}
