using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestAPIServiceApplication.CustomControls;
using RestAPIServiceApplication.Droid.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(ShadowEntry), typeof(ShadowEntryAndroid))]
namespace RestAPIServiceApplication.Droid.CustomControls
{
    public class ShadowEntryAndroid : EntryRenderer // Custom renderer
    {
        public ShadowEntryAndroid(Context context) : base(context)
        {
        //    this.SetWillNotDraw(false);
        }

        //public override void Draw(Canvas canvas)
        //{
        //    base.Draw(canvas);
        //    var element = (ShadowEntry)this.Element;
        //    var rect = new Rect();

        //    GetDrawingRect(rect);

        //    var rectInterior = rect;

        //    var paint = new Paint()
        //    {
        //        Color = element.BackgroundColor.ToAndroid(),
        //        AntiAlias = true
        //    };

        //    canvas.DrawRoundRect(
        //        new RectF(rectInterior), (float)element.CornerRadius,
        //        (float)element.CornerRadius, paint);

        //    canvas.DrawRoundRect(
        //        new RectF(rect), (float)element.CornerRadius,
        //        (float) element.CornerRadius, paint);
        //    //var rectInterior = rect;


        //}

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
                return;

            var element = (ShadowEntry) this.Element;

            GradientDrawable gd = new GradientDrawable();
            gd.SetStroke(5, element.BackgroundColor.ToAndroid());
            gd.SetCornerRadius((float)element.CornerRadius);
            gd.SetColor(element.BackgroundColor.ToAndroid());

            Control.SetBackground(gd);

            Control.SetPadding(
                50, Control.PaddingTop, 
                Control.PaddingRight, Control.PaddingBottom);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            //this.Invalidate();
        }
    }
}