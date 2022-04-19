using System;
using System.ComponentModel;
using DemoApp.CustomControls;
using DemoApp.iOS.CustomControls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace DemoApp.iOS.CustomControls
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (Entry)Element;

            if (view != null && Control != null)
            {
                UIView paddingView = new UIView(new CoreGraphics.CGRect(0, 0, 0, 0));
                Control.LeftView = paddingView;
                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.TintColor = Color.Blue.ToUIColor();
                Control.AutocapitalizationType = UITextAutocapitalizationType.None;
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
                if (view.IsPassword)
                {
                    Control.TextContentType = UITextContentType.Password;
                }
            }
        }
    }
}
