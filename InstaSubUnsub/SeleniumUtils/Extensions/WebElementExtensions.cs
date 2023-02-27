using OpenQA.Selenium;

namespace SeleniumUtils.Extensions
{
    public static class WebElementExtensions
    {
        public static int GetInstaSubNumber(this IWebElement sourceElem)
        {
            var txt = sourceElem.Text;
            var mul = 1;
            if (txt.Contains("млн")) mul = 1000000;
            if (txt.Contains("тыс.")) mul = 1000;
            txt = txt.Replace(" ", string.Empty);
            txt = txt.Replace("млн", string.Empty);
            txt = txt.Replace("тыс.", string.Empty);
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
