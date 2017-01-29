using Microsoft.VisualStudio.TestTools.UnitTesting;
using WydatkiDomowe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WydatkiDomowe.Tests
{
    [TestClass()]
    public class CorrectDataTests
    {
        [TestMethod()]
        public void containsLettersTest()
        {
            string text = "44";
            bool expected = false;
            bool actual;

            actual = CorrectData.containsLetters(text);

            Assert.AreEqual(expected, actual);
        }
    }
}