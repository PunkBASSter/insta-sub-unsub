using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System.Collections.ObjectModel;

namespace SeleniumUtils.Helpers
{
    /// <summary>
    /// The class is to track the changes of the page's title while it's loading.
    /// Why? Because:
    /// Instagram is showing "User unavailable" page to struggle with bots but
    /// the real page shows the propper title immediatly but the fake page
    /// displays the original user page's title containing username for a moment.
    /// So if the page is anti-bot fake then GetTitleChanges' output will contain
    /// the username of the requested page.
    /// </summary>
    public class TitleObserver
    {
        private readonly IWebDriver _driver;
        public TitleObserver(IWebDriver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// Invoked right after Driver.Navigate().GoToUrl(...)
        /// </summary>
        public void SetObserver()
        {
            _driver.ExecuteJavaScript("""
                globalPageTitleValuesStorage = [];
                globalPageTitleValuesStorage.push(document.title);

                function observeTitleChanges(){
                    // select the target node
                    let target = document.querySelector('title');
                    
                    // create an observer instance
                    let observer = new MutationObserver(function(mutations) {
                        // Take only first event and only new value of the title
                        globalPageTitleValuesStorage.push(mutations[0].target.nodeValue);
                    });
                    
                    // configuration of the observer:
                    let config = { subtree: true, characterData: true, childList: true };
                    
                    // pass in the target node, as well as the observer options
                    observer.observe(target, config);
                }

                observeTitleChanges();
               
                """
                );
        }

        /// <summary>
        /// Invoked after page is loaded.
        /// </summary>
        /// <returns>Array of page title values observed.</returns>
        public string[] GetTitleChanges()
        {
            var result = _driver.ExecuteJavaScript<ReadOnlyCollection<object>>("""
                return globalPageTitleValuesStorage;
                """
                );

            if (result == null)
                return Array.Empty<string>();

#pragma warning disable CS8619 // Допустимость значения NULL для ссылочных типов в значении не соответствует целевому типу.
            return result.Where(o => o != null).Select(o => o.ToString()).ToArray();
#pragma warning restore CS8619 // Допустимость значения NULL для ссылочных типов в значении не соответствует целевому типу.
        }
    }
}
