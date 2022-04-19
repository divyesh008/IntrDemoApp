using System;
namespace DemoApp.Helpers
{
    public static class CreditCardInfoExtention
    {
        public static string GetLogo(this CardType cardType)
        {
            switch (cardType)
            {
                case CardType.MasterCard:
                    return "\uf1f1";
                case CardType.Visa:
                    return "\uf1f0";
                case CardType.AmericanExpress:
                    return "\uf1f3";
                case CardType.Discover:
                    return "\uf1f2";
                case CardType.JCB:
                    return "\uf24b";
                case CardType.Diners:
                    return "\uf24c";
            }

            return "";
        }
    }

    public enum CardType
    {
        Undefined, MasterCard, Visa, AmericanExpress, Discover, JCB, Diners
    };
}
