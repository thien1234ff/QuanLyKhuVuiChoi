using System;
using System.Text.RegularExpressions;

namespace QuanLyKhuVuiChoi.Services
{
    public static class ValidationHelper
    {
        private static readonly Regex PhoneRegex = new Regex(@"^(0|\+84)[3|5|7|8|9][0-9]{8}$", RegexOptions.Compiled);
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static bool IsValidPhoneNumber(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            var cleanPhone = phone.Replace(" ", "").Replace(".", "").Replace("-", "");
            return PhoneRegex.IsMatch(cleanPhone) || (cleanPhone.Length >= 10 && cleanPhone.Length <= 11 && cleanPhone.StartsWith("0"));
        }

        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return true; // Email can be optional
            return EmailRegex.IsMatch(email.Trim());
        }

        public static bool IsValidPrice(decimal price)
        {
            return price >= 0;
        }

        public static bool IsValidQuantity(int qty)
        {
            return qty >= 0;
        }

        public static bool IsNotEmpty(string? value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
