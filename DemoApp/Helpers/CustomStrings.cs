using System;
using DemoApp.LangResources;

namespace DemoApp.Helpers
{
    public class CustomStrings
    {
        public static string LoginText
        {
            get { return AppResources.Login.ToUpper(); }
        }

        public static string LoginFailText
        {
            get { return AppResources.LoginFail; }
        }
    }
}
