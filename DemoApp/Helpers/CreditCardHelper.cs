using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DemoApp.Helpers
{
    public static class CreditCardHelper
    {
        public static bool Mod10Check(string creditCardNumber)
        {
            // check whether input string is null or empty
            if (string.IsNullOrEmpty(creditCardNumber))
            {
                return false;
            }

            /* 1 Starting with the check digit double the value of every other digit 
               2 If doubling of a number results in a two digits number, add up the digits to get a single digit number.
                  This will results in eight single digit numbers                    
               3 Get the sum of the digits */

            int sumOfDigits = creditCardNumber.Where((e) => e >= '0' && e <= '9')
                            .Reverse()
                            .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                            .Sum((e) => e / 10 + e % 10);

            /* If the final sum is divisible by 10, then the credit card number is valid. If it is not divisible by 10, 
               the number is invalid. */
            return sumOfDigits % 10 == 0;
        }

        public static bool IsACreditCardValid(this string cardNumber)
        {
            return cardNumber.GetCardTypeFromRegularExpression() != CardType.Undefined;
        }

        public static CardType GetCardTypeFromRegularExpression(this string cardNumber)
        {
            if (Mod10Check(cardNumber))
            {
                //https://www.regular-expressions.info/creditcard.html

                var tempCardNo = cardNumber.Replace(" ", string.Empty);

                if (Regex.Match(tempCardNo, @"^4[0-9]{12}(?:[0-9]{3})?$").Success)
                {
                    return CardType.Visa;
                }

                if (Regex.Match(tempCardNo, @"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$").Success)
                {
                    return CardType.MasterCard;
                }

                if (Regex.Match(tempCardNo, @"^3[47][0-9]{13}$").Success)
                {
                    return CardType.AmericanExpress;
                }

                if (Regex.Match(tempCardNo, @"^6(?:011|5[0-9]{2})[0-9]{12}$").Success)
                {
                    return CardType.Discover;
                }

                if (Regex.Match(tempCardNo, @"^(?:2131|1800|35\d{3})\d{11}$").Success)
                {
                    return CardType.JCB;
                }

                // Regex for 14 Digits number Diners Card 
                if (Regex.Match(tempCardNo, @"^3(?:0[0-5]|[68][0-9])[0-9]{11}$").Success)
                {
                    return CardType.Diners;
                }

                // Regex for 16 Digits number Diners Card 
                if (Regex.Match(tempCardNo, @"^(?:5[45]|36|30[0-5]|3095|3[8-9])\d+$").Success)
                {
                    return CardType.Diners;
                }
            }
            return CardType.Undefined;
        }

        public static CardType GetCardType(this string cardNumber)
        {
            // All Visa card numbers start with a 4. New cards have 16 digits. Old cards have 13.
            if (cardNumber.StartsWith("4"))
            {
                return CardType.Visa;
            }

            /* MasterCard numbers either start with the numbers 51 through 55 or with the numbers 2221 through 2720.
               All have 16 digits. */
            if (cardNumber.StartsWith("5"))
            {
                return CardType.MasterCard;
            }

            // American Express card numbers start with 34 or 37 and have 15 digits.
            if (cardNumber.StartsWith("34") || cardNumber.StartsWith("37"))
            {
                return CardType.AmericanExpress;
            }

            // Discover card numbers begin with 6011 or 65. All have 16 digits.
            if (cardNumber.StartsWith("65") || cardNumber.StartsWith("6011"))
            {
                return CardType.Discover;
            }

            // JCB cards beginning with 2131 or 1800 have 15 digits. JCB cards beginning with 35 have 16 digits.
            if (cardNumber.StartsWith("35") || cardNumber.StartsWith("1800") || cardNumber.StartsWith("2131"))
            {
                return CardType.JCB;
            }

            // Diners Club card numbers begin with 300 through 305, 36 or 38. All have 14 digits. 
            if (cardNumber.StartsWith("30") || cardNumber.StartsWith("36") || cardNumber.StartsWith("38"))
            {
                return CardType.Diners;
            }

            return CardType.Undefined;
        }

        /// <summary>
        /// Card Holder Name Validator
        /// </summary>
        /// <param name="cardHolderName">Card Holder Name</param>
        /// <returns>true/false</returns>
        public static bool IsAValidHolderName(this string cardHolderName)
        {
            Match match = Regex.Match(cardHolderName.Trim(), @"^((?:[A-Za-z]+ ?){1,3})$", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                string key = match.Groups[1].Value;
                Console.WriteLine("valid:" + key);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Credit Card CVV Code Validator
        /// </summary>
        /// <param name="cvvCode">CVV Code</param>
        /// <param name="cardNumber">Card Number</param>
        /// <returns>true/false</returns>
        public static bool IsAValidCVVCode(this string cvvCode, string cardNumber)
        {
            var cardType = GetCardType(cardNumber).ToString();

            var digits = 0;
            switch (cardType.ToUpper())
            {
                case "MASTERCARD":
                case "EUROCARD":
                case "EUROCARD/MASTERCARD":
                case "VISA":
                case "DISCOVER":
                case "JCB":
                case "DINERS":
                    digits = 3;
                    break;
                case "AMEX":
                case "AMERICANEXPRESS":
                case "AMERICAN EXPRESS":
                    digits = 4;
                    break;
                default:
                    return false;
            }

            Regex regEx = new Regex("[0-9]{" + digits + "}");
            return (cvvCode.Length == digits && regEx.Match(cvvCode).Success);
        }
    }
}
