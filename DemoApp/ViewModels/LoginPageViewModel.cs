using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Input;
using DemoApp.Helpers;
using DemoApp.LangResources;
using Xamarin.Forms;

namespace DemoApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel()
        {
        }

        #region Private Properties

        private string _email;
        private string _password;
        private string _emailErrorMsg;
        private string _passwordErrorMsg;
        private string _loginErrorMsg;
        private bool _isVisibleLoginError;
        private double _errorLabelHeight;

        #endregion

        #region Public Properties

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public string EmailErrorMsg
        {
            get { return _emailErrorMsg; }
            set { _emailErrorMsg = value; OnPropertyChanged(nameof(EmailErrorMsg)); }
        }

        public string PasswordErrorMsg
        {
            get { return _passwordErrorMsg; }
            set { _passwordErrorMsg = value; OnPropertyChanged(nameof(PasswordErrorMsg)); }
        }

        public string LoginErrorMsg
        {
            get { return _loginErrorMsg; }
            set { _loginErrorMsg = value; OnPropertyChanged(nameof(LoginErrorMsg)); }
        }

        public bool IsVisibleLoginError
        {
            get { return _isVisibleLoginError; }
            set { _isVisibleLoginError = value; OnPropertyChanged(nameof(IsVisibleLoginError)); }
        }

        public double ErrorLabelHeight
        {
            get { return _errorLabelHeight; }
            set { _errorLabelHeight = value; OnPropertyChanged(nameof(ErrorLabelHeight)); }
        }

        #endregion

        #region Commands

        public ICommand LoginCommand { get { return new Command(() => LoginClick()); } }

        #endregion

        #region Private Methods

        private async void LoginClick()
        {
            try
            {
                if (!string.IsNullOrEmpty(Email))
                {
                    EmailErrorMsg = Validator.IsValidEmail(Email) ? string.Empty : AppResources.InvalidEmail;
                }
                else
                {
                    EmailErrorMsg = AppResources.EmailRequired;
                }

                PasswordErrorMsg = string.IsNullOrEmpty(Password) ? AppResources.PasswordRequired : string.Empty;

                if (!string.IsNullOrEmpty(Password))
                {
                    PasswordErrorMsg = Validator.IsValidPassword(Password) ? string.Empty : "Password must be 8 character long";
                }

                if (string.IsNullOrEmpty(EmailErrorMsg) && string.IsNullOrEmpty(PasswordErrorMsg))
                {
                    ErrorLabelHeight = 0;

                    /* Test Api call */
                    /*

                    var keyValues = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("UserName","abc@gmail.com"),
                        new KeyValuePair<string, string>("Password","abc@12345"),
                        new KeyValuePair<string, string>("PrimaryRole","User"),
                        new KeyValuePair<string, string>("UserProfile",null),
                    };


                    HttpClient _httpClient = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "API_URL");
                    request.Content = new FormUrlEncodedContent(keyValues);
                    var response = await _httpClient.SendAsync(request);
                    var content = await response.Content.ReadAsStringAsync();
                    if (content == "")
                    {
                        //Api call failed
                    }
                    else
                    {
                        //Api call passed
                    }*/

                    MainPage mainPage = new MainPage();
                    MainPageViewModel mainPageViewModel = new MainPageViewModel();

                    await App.Current.MainPage.Navigation.PushAsync(mainPage, false);
                }
                else
                {
                    ErrorLabelHeight = 15;
                }
            }
            catch (Exception ex)
            {
                IsVisibleLoginError = false;
                Debug.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}
