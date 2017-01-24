using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WydatkiDomowe
{
    public static class StringUppercaseFirst
    {
        

        public static string UppercaseFirstInWords(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            text = text.ToLower();

            text = removeCharacters(text);

            string[] tempWords = text.Split(' ');
            text = "";

            foreach (string i in tempWords)
            {
                text += uppercaseFirstInWord(i) + ' ';
            }

            text = text.Trim();
            return text;
        }

        private static string uppercaseFirstInWord(string text)
        {
            text = char.ToUpper(text[0]) + text.Substring(1);
            return text;
        }

        private static string removeCharacters(string text)
        {
            string pattern = "[@,;\"\'$]";
            Regex rgx = new Regex(pattern);
            text = rgx.Replace(text, "");
            
            pattern = "[ ]{2,}";

            rgx = new Regex(pattern, RegexOptions.None);
            text = rgx.Replace(text, " ");
            text = text.Trim();

            return text;
        }
    }
}
