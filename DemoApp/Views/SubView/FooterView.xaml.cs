using System;
using System.Collections.Generic;
using DemoApp.ViewModels;
using Xamarin.Forms;

namespace DemoApp.Views.SubView
{
    public partial class FooterView : ContentView
    {
        public FooterView()
        {
            InitializeComponent();
        }

        public static BindableProperty SelectedPageProperty =
            BindableProperty.Create(nameof(SelectedPage), typeof(string), typeof(FooterView),
                                    default(string), BindingMode.TwoWay, propertyChanged: OnSelectedPageChanged);

        public string SelectedPage
        {
            get
            {
                return (string)this.GetValue(SelectedPageProperty);
            }
            set
            {
                this.SetValue(SelectedPageProperty, value);
            }
        }

        private static void OnSelectedPageChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var picker = bindable as FooterView;
            picker.SelectedPage = (string)newvalue;

            picker.SetTheme();
        }

        public void SetTheme()
        {
            Tab1.BackgroundColor = Color.Transparent;
            Tab2.BackgroundColor = Color.Transparent;
            Tab3.BackgroundColor = Color.Transparent;


            Tab1Text.TextColor = Color.Black;
            Tab2Text.TextColor = Color.Black;
            Tab3Text.TextColor = Color.Black;

            if (SelectedPage.ToString() == "HOME")
            {
                Tab1.BackgroundColor = Color.Orange;
                Tab1Text.TextColor = Color.White;
                //Tab1Image.Source = (string)App.Current.Resources["SelectedTab1Image"];
            }
            else if (SelectedPage.ToString() == "CALL")
            {
                Tab2.BackgroundColor = Color.Orange;
                Tab2Text.TextColor = Color.White;
                //Tab2Image.Source = (string)App.Current.Resources["SelectedTab2Image"];
            }
            else if (SelectedPage.ToString() == "FILE")
            {
                Tab3.BackgroundColor = Color.Orange;
                Tab3Text.TextColor = Color.White;
                //Tab3Image.Source = (string)App.Current.Resources["SelectedTab3Image"];
            }
        }

        void NavigationTapped(object sender, EventArgs e)
        {
            var args = e as TappedEventArgs;
            var context = ((ViewModelBase)(this.BindingContext));
            context.NavigationToPage(args.Parameter.ToString());
        }
    }
}
