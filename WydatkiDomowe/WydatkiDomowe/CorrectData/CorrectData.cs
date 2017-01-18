using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WydatkiDomowe
{
    static class CorrectData
    {
        static int correctAccountLength = 26;

        static public bool containsLetters(string text)
        {
            if (text.Count(char.IsLetter) != 0)
                return true;
            else
                return false;
        }

        static public bool containsNumbers(string text)
        {
            if (text.Count(char.IsNumber) != 0)
                return true;
            else
                return false;
        }

        static public bool isTooShort(string text)
        {
            if (text.Length < correctAccountLength)
                return true;
            else
                return false;
        }

        static public bool isEpmty(string text)
        {
            if (String.IsNullOrEmpty(text) || String.IsNullOrWhiteSpace(text))
                return true;
            else
                return false;
        }

        static public bool isString(string text)
        {
            int temp;
            if (!Int32.TryParse(text, out temp) || !(temp>= 0))
                return true;
            else
                return false;
        }
    }
}
