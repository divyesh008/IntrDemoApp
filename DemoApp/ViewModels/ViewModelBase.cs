using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DemoApp.Helpers;
using DemoApp.Views;
using Xamarin.Forms;

namespace DemoApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        string CardNumber, CVVCode;
        int ExpiryMonth, ExpiryYear;

        public ViewModelBase()
        {
            ResetProps();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


        private ViewFlipper _cardSimulationInfo;
        public ViewFlipper CardSimulationInfo
        {
            get { return _cardSimulationInfo; }
            set { _cardSimulationInfo = value; OnPropertyChanged(nameof(CardSimulationInfo)); }
        }

        private EntryPage _number;
        public EntryPage Number
        {
            get { return _number; }
            set { _number = value; OnPropertyChanged(nameof(Number)); }
        }

        private Label _buttonText;
        public Label ButtonText
        {
            get { return _buttonText; }
            set { _buttonText = value; OnPropertyChanged(nameof(ButtonText)); }
        }

        private EntryPage _name;
        public EntryPage Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        private EntryPage _valid;
        public EntryPage Valid
        {
            get { return _valid; }
            set { _valid = value; OnPropertyChanged(nameof(Valid)); }
        }

        private EntryPage _cvv;
        public EntryPage Cvv
        {
            get { return _cvv; }
            set { _cvv = value; OnPropertyChanged(nameof(Cvv)); }
        }

        private Image _logoCreditCard;
        public Image LogoCreditCard
        {
            get { return _logoCreditCard; }
            set { _logoCreditCard = value; OnPropertyChanged(nameof(LogoCreditCard)); }
        }

        private string _displayName = "Name Surname";
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; OnPropertyChanged(nameof(DisplayName)); }
        }

        [Obsolete]
        public Command NextCommand => new Command(() =>
        {
            if (Number.Panel.IsVisible)
            {
                if (!ValidatedNumberNext())
                    return;

                CardNumber = Number.Entry.Text.Replace(" ", string.Empty);
                Number.Panel.IsVisible = false;
                Number.BorderSpot.BorderColor = Color.Transparent;
                DisplayName = "Amul Patel";
                Name.Panel.IsVisible = !Number.Panel.IsVisible;
                Name.BorderSpot.BorderColor = Color.Yellow;
                return;
            }

            if (Name.Panel.IsVisible)
            {
                //if (!ValidatedNameNext())
                //    return;

                Name.Panel.IsVisible = false;
                Name.BorderSpot.BorderColor = Color.Transparent;
                Valid.Panel.IsVisible = !Name.Panel.IsVisible;
                Valid.BorderSpot.BorderColor = Color.Yellow;
                return;
            }

            if (Valid.Panel.IsVisible)
            {
                if (!ValidatedValidNext())
                    return;

                ExpiryMonth = Int32.Parse(Valid.Entry.Text.Substring(0, 2));
                ExpiryYear = Int32.Parse(Valid.Entry.Text.Substring(3, 2));
                Valid.Panel.IsVisible = false;
                Valid.BorderSpot.BorderColor = Color.Transparent;
                Cvv.Panel.IsVisible = !Valid.Panel.IsVisible;
                Cvv.BorderSpot.BorderColor = Color.Yellow;
                CardSimulationInfo.FlipState = FlipState.Back;

                ButtonText.Text = "Done";

                return;
            }

            if (Cvv.Panel.IsVisible)
            {
                if (!ValidatedCvvNext())
                    return;

                CVVCode = Cvv.Entry.Text;
                Cvv.Panel.IsVisible = false;
                Cvv.BorderSpot.BorderColor = Color.Transparent;
                Number.Panel.IsVisible = !Cvv.Panel.IsVisible;
                Number.BorderSpot.BorderColor = Color.Yellow;
                CardSimulationInfo.FlipState = FlipState.Front;

                ButtonText.Text = "Next";

                Number.Entry.Unfocus();

                //Save();
            }
        });


        private void ResetProps()
        {
            Number = new EntryPage
            {
                Panel = new StackLayout
                {
                    IsVisible = true
                },
                BorderSpot = new Frame
                {
                    BorderColor = Color.Yellow
                }
            };

            Name = new EntryPage
            {
                Panel = new StackLayout
                {
                    IsVisible = false
                }
            };

            Valid = new EntryPage
            {
                Panel = new StackLayout
                {
                    IsVisible = false
                }
            };

            Cvv = new EntryPage
            {
                Panel = new StackLayout
                {
                    IsVisible = false
                }
            };

            ButtonText = new Label
            {
                Text = "Next"
            };

            CardSimulationInfo = new ViewFlipper();

            LogoCreditCard = new Image();
        }

        private bool ValidatedNumberNext()
        {
            //#if DEBUG
            //            return true;
            //#endif

            Number.ErrorMsg.IsVisible = false;

            if (string.IsNullOrEmpty(Number?.Entry?.Text) || !Number.Entry.Text.IsACreditCardValid())
            {
                Number.ErrorMsg.Text = "Incorrect card numnber";
                Number.ErrorMsg.IsVisible = true;
                return false;
            }

            return true;
        }

        private bool ValidatedNameNext()
        {
            return true;
        }

        private bool ValidatedValidNext()
        {
            Valid.ErrorMsg.IsVisible = false;

            if (string.IsNullOrEmpty(Valid?.Entry?.Text) || !Valid.Entry.Text.Length.Equals(5))
            {
                Valid.ErrorMsg.Text = "Invalid expiry date";
                Valid.ErrorMsg.IsVisible = true;
                return false;
            }

            var invalidDateMsg = "";

            var monthValid = new List<string> { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };

            var yearValid = new List<string>();

            for (int i = DateTime.UtcNow.Year; i < DateTime.UtcNow.AddYears(15).Year; i++)
            {
                yearValid.Add(i.ToString().Substring(2, 2));
            }

            if (!monthValid.Contains(Valid.Entry.Text.Substring(0, 2)))
                invalidDateMsg += $"Invalid Month\n";

            if (!yearValid.Contains(Valid.Entry.Text.Substring(3, 2)))
                invalidDateMsg += "Invalid Year";

            if (!string.IsNullOrEmpty(invalidDateMsg))
            {
                Valid.ErrorMsg.Text = invalidDateMsg;
                Valid.ErrorMsg.IsVisible = true;
                return false;
            }

            return true;
        }

        private bool ValidatedCvvNext()
        {
            //#if DEBUG
            //            return true;
            //#endif

            Cvv.ErrorMsg.IsVisible = false;

            if (string.IsNullOrEmpty(Cvv?.Entry?.Text) || !Cvv.Entry.Text.IsAValidCVVCode(Number.Entry.Text))
            {
                Cvv.ErrorMsg.Text = "Incorrect CVV";
                Cvv.ErrorMsg.IsVisible = true;
                return false;
            }

            return true;
        }

        private string _labelHome { get; set; } = "HOME";
        public string LabelHome { get { return _labelHome = "HOME"; } set { _labelHome = value; OnPropertyChanged("LabelHome"); } }

        private string _labelCall { get; set; } = "CALL";
        public string LabelCall { get { return _labelCall = "CALL"; } set { _labelCall = value; OnPropertyChanged("LabelCall"); } }

        private string _labelFile { get; set; } = "FILE";
        public string LabelFile { get { return _labelFile = "FILE"; } set { _labelFile = value; OnPropertyChanged("LabelFile"); } }


        public ICommand NavigationCommand { get { return new Command<string>(NavigationToPage); } }
        public async void NavigationToPage(string title)
        {
            try
            {
                if (title == "HOME")
                {
                    await App.Current.MainPage.Navigation.PopToRootAsync();
                }
                else if (title == "CALL")
                {
                    CallPage callPage = new CallPage();
                    CallPageViewModel callPageVM = new CallPageViewModel();
                    callPage.BindingContext = callPageVM;
                    await App.Current.MainPage.Navigation.PushAsync(callPage, false);
                }
                else if (title == "FILE")
                {
                    FilePage filePage = new FilePage();
                    FilePageViewModel filePageVM = new FilePageViewModel();
                    filePage.BindingContext = filePageVM;

                    await App.Current.MainPage.Navigation.PushAsync(filePage, false);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }

    public class EntryPage
    {
        public EntryPage()
        {
            Panel = new StackLayout();
            Entry = new Entry();
            ErrorMsg = new Label();
            BorderSpot = new Frame();
        }

        public StackLayout Panel { get; set; }

        public Entry Entry { get; set; }

        public Label ErrorMsg { get; set; }

        public Frame BorderSpot { get; set; }
    }

   
}
