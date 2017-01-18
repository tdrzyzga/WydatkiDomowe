using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WydatkiDomowe
{
    static class StringUppercaseFirst
    {
        public static string UppercaseFirst(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            text = text.ToLower();
            text = char.ToUpper(text[0]) + text.Substring(1);
            return text;
        }
    }
}
