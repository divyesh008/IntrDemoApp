using System;
using System.Text.RegularExpressions;

namespace DemoApp.Helpers
{
    public static class Validator
    {
        public static bool IsValidEmail(string email)
        {
            string expresion = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            //string expresion = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Regex reg = new Regex(expresion, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(email);
        }

        public static bool IsValidPassword(string password)
        {
            //string regex = @"^(?=.{ 8,})[a-zA - Z][\w](?= (.*[\W])).*$";
            string regex = @"^[a-zA - Z][\w](?=(.*[\W])).*$";
            if (Regex.Match(password, regex).Success)
                return true;
            else
                return false;
        }
    }
}
