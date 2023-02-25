using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
namespace SeleniumUtils
{
    public class Scroll
    {
        private readonly IWebDriver _driver;
        private readonly string _jsGetByXpathFunc = "function getElementByXpath(path) { return document.evaluate(path, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue; }";

        public Scroll(IWebDriver driver)
        {
            _driver = driver;
        }

        public void IntoView(string xpath)
        {
            _driver.ExecuteJavaScript($$"""
                {{_jsGetByXpathFunc}}
                let element = getElementByXpath("{{xpath}}");
                element.scrollIntoView()
                """);
        }

        public void ToBottom(string xpath)
        {
            var js = $$"""
                {{_jsGetByXpathFunc}}
                let scrollableArea = getElementByXpath("{{xpath}}");
                scrollableArea.scroll(0, scrollableArea.scrollHeight);
                """;

            _driver.ExecuteJavaScript(js);
        }
    }
}
