using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace SeleniumUtils.PageObjects
{
    /// <summary>
    /// Covers desktop only, for smaller screens locators will break
    /// </summary>
    public class ProfilePage : BasePage
    {
        public override string Url => "https://www.instagram.com/{0}";

        public ProfilePage(IWebDriver driver) : base(driver)
        {
        }

        protected override By LoadIndicatingElementLocator { get; set; }


        public override bool Load(params string[] urlParams)
        {
            if (urlParams.Length != 1) 
                throw new ArgumentException("UserName param is expected.");

            LoadIndicatingElementLocator = By.XPath($"//header//*[contains(text(),'{urlParams[0]}')]");
            return base.Load(urlParams);
        }

        

        public ReadOnlyCollection<IWebElement> DescriptionParts =>
            _driver.FindElements(By.XPath("//header/section/div[3]/*[text()]"));

        public string GetDescriptionText()
        {
            var textParts = DescriptionParts.Select(p => p.Text).ToArray();
            if (textParts.Length < 1) textParts = new string[] { "placeholder" };
            return string.Join("\r\n", textParts);
        }

        public IWebElement FollowersNumElement =>
            _driver.FindElement(By.XPath(".//section/ul/li[2]/*/div/span/span"));

        public IWebElement FollowingsNumElement =>
            _driver.FindElement(By.XPath(".//section/ul/li[3]/*/div/span/span"));

        public ReadOnlyCollection<IWebElement> PostLinks =>
            _driver.FindElements(By.XPath(".//article/div/div//a"));

        public bool OpenPost(out Post postObj, int postNumFromTopLeft = 0)
        {
            if (PostLinks.Count < 1)
            {
                postObj = default;
                return false;
            }
            
            PostLinks[postNumFromTopLeft].Click();
            postObj = new Post(_driver);
            return true;
        }

        public IList<Post> GetLastPosts(int limit = 4)
        {
            var opened = OpenPost(out Post current);
            var result = new List<Post>();
            var count = 1;

            while (opened && count <= limit)
            {
                result.Add(current);
                count++;
                opened = current.TrySwitchToNext(out current);
            }

            return result;
        }
    }
}
