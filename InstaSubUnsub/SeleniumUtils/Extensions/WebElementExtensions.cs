﻿using InstaCommon;
using OpenQA.Selenium;

namespace SeleniumUtils.Extensions
{
    public static class WebElementExtensions
    {
        public static int GetInstaSubNumber(this IWebElement sourceElem)
        {
            //todo consider parsing title attribute 
            var txt = sourceElem.Text;
            var mul = 1;
            if (txt.Contains("млн") || txt.Contains("M")) mul = 1000000;
            if (txt.Contains("тыс.") || txt.Contains("K")) mul = 1000;
            txt = txt.Replace(" ", string.Empty);
            txt = txt.Replace("млн", string.Empty);
            txt = txt.Replace("тыс.", string.Empty);
            txt = txt.Replace("M", string.Empty);
            txt = txt.Replace("K", string.Empty);

            var dbl = Convert.ToDouble(txt) * mul;
            return Convert.ToInt32(dbl);
        }

        public static void SlowSendKeys(this IWebElement target, string str)
        {
            var delay = new Delay(200, 600);
            foreach (var c in str)
            {
                delay.Random(() => target.SendKeys(c.ToString()));
            }
        }
    }
}
