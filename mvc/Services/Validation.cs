using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace mvc.Services
{
    public class Validation
    {
        private static readonly string PhonePattern = "^((8|\\+374|\\+994|\\+995|\\+375|\\+7|\\+380|\\+38|\\+996|\\+998|\\+993)[\\- ]?)?\\(?\\d{3,5}\\)?[\\- ]?\\d{1}[\\- ]?\\d{1}[\\- ]?\\d{1}[\\- ]?\\d{1}[\\- ]?\\d{1}(([\\- ]?\\d{1})?[\\- ]?\\d{1})?$";
        private static readonly string EmailPattern = "\\b\\w+([\\.\\w]+)*\\w@\\w((\\.\\w)*\\w+)*\\.\\w{2,3}\\b";
        private static readonly string NumberPattern = "[0-9]{5}";
        public static bool IsTelValid(string phone)
        {
            Match isMatch = Regex.Match(phone, PhonePattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
        public static bool IsEmailValid(string email)
        {
            Match isMatch = Regex.Match(email, EmailPattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
        public static bool IsPriceValid(string price)
        {
            Match isMatch = Regex.Match(price, NumberPattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
    }
}
