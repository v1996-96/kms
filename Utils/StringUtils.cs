using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace kms.Utils
{
    public static class StringUtils
    {
        private const string SEPARATOR = "-";
        private const int LENGTH = 50;

        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.RemoveDiacritics().ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Substring(0, str.Length <= LENGTH ? str.Length : LENGTH).Trim();
            str = Regex.Replace(str, @"\s", SEPARATOR);
            str += SEPARATOR + Guid.NewGuid().ToString("n").Substring(0, 8);
            return str;
        }

        public static string RemoveDiacritics(this string text)
        {
            var s = new string(text.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());

            return s.Normalize(NormalizationForm.FormC);
        }

        public static bool IsValidQuery(this string query) => query != null && query != "";
    }
}
