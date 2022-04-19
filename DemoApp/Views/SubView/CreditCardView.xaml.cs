using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DemoApp.Helpers;
using DemoApp.LangResources;
using DemoApp.ViewModels;
using Xamarin.Forms;

namespace DemoApp.Views.SubView
{
    public partial class CreditCardView : ContentView
    {
        public Color PlaceholderValueColor { get { return (Color)App.Current.Resources.FirstOrDefault(x => x.Key.Equals("#ecf0f1")).Value; } }

        public uint FadeCreditCardLogo { get { return 1000; } }

        public ViewModelBase Vm { get { return (ViewModelBase)BindingContext; } }

        public CreditCardView()
        {
            InitializeComponent();
            entryNumber.Focus();
        }

        void imgLogoCard_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Image.Source)))
            {
                var img = (Image)sender;
                img.Opacity = 0;
                img.FadeTo(1, FadeCreditCardLogo);
            }
        }

        void StackLayout_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(StackLayout.IsVisible)) && ((StackLayout)sender).IsVisible)
            {
                entryNumber.Focus();
            }
        }

        void entryNumber_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e?.NewTextValue))
                {
                    lbNumberValue.Text = "XXXX XXXX XXXX XXXX";
                    lbNumberValue.TextColor = PlaceholderValueColor;

                    //imgLogoCard.Opacity = 1;
                    imgLogoCard.FadeTo(0, FadeCreditCardLogo);
                }
                else
                {
                    lbNumberValue.Text = e.NewTextValue;
                    lbNumberValue.TextColor = Color.White;

                    if (lbNumberValue.Text.Length.Equals(4)) //&& !string.IsNullOrEmpty(e?.OldTextValue)
                    {
                        Task.Run(() =>
                        {
                            var cardType = e.NewTextValue.GetCardType();

                            if (cardType != CardType.Undefined)
                            {
                                Vm.LogoCreditCard.Source = new FontImageSource
                                {
                                    FontFamily = "FA_Brands",
                                    Glyph = cardType.GetLogo(),
                                    Size = 40,
                                    Color = Color.White
                                };
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
        }

        void StackLayout_PropertyChanged_1(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                if (e.PropertyName.Equals(nameof(StackLayout.IsVisible)) && ((StackLayout)sender).IsVisible)
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(200);
                        //entryName.Focus();
                    });
                }
            }
            else
            {
                // In iOS above condition not let the keyboard open in Real device.
                if (e.PropertyName.Equals(nameof(StackLayout.IsVisible)) && ((StackLayout)sender).IsVisible)
                {
                    if (!string.IsNullOrEmpty(lbNumberValue.Text) && lbNumberValue.Text != "XXXX XXXX XXXX XXXX")
                    {
                        //entryName.Focus();
                    }
                }
            }
        }

        void entryName_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e?.NewTextValue))
                {
                    lbNameValue.Text = "Name Surname";
                    lbNameValue.TextColor = PlaceholderValueColor;
                }
                else
                {
                    lbNameValue.Text = e.NewTextValue.ToUpper();
                    lbNameValue.TextColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
        }

        void StackLayout_PropertyChanged_2(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                if (e.PropertyName.Equals(nameof(StackLayout.IsVisible)) && ((StackLayout)sender).IsVisible)
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(200);
                        entryValid.Focus();
                    });
                }
            }
            else
            {
                if (e.PropertyName.Equals(nameof(StackLayout.IsVisible)) && ((StackLayout)sender).IsVisible)
                {
                    entryValid.Focus();
                }
            }
        }

        void entryValid_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e?.NewTextValue))
                {
                    lbValidValue.Text = "Expiry Date";
                    lbValidValue.TextColor = PlaceholderValueColor;
                }
                else
                {
                    lbValidValue.Text = e.NewTextValue;
                    lbValidValue.TextColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        void StackLayout_PropertyChanged_3(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                if (e.PropertyName.Equals(nameof(StackLayout.IsVisible)) && ((StackLayout)sender).IsVisible)
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(200);
                        entryCvv.Focus();
                    });
                }
            }
            else
            {
                if (e.PropertyName.Equals(nameof(StackLayout.IsVisible)) && ((StackLayout)sender).IsVisible)
                {
                    entryCvv.Focus();
                }
            }
        }

        void entryCvv_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e?.NewTextValue))
                lbCvvValue.Text = "CVV";
            else
                lbCvvValue.Text = e.NewTextValue;
        }

    }
}
