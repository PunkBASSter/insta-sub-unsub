using InstaCommon.Exceptions;
using OpenQA.Selenium;
using SeleniumUtils.Helpers;
using System.Collections.ObjectModel;

namespace SeleniumUtils.PageObjects
{
    /// <summary>
    /// Covers desktop only, for smaller screens locators will break
    /// </summary>
    public class ProfilePage : BasePage
    {
        private readonly TitleObserver _titleObserver;
        private readonly string _userName;

        public override string Url => "https://www.instagram.com/{0}";

        public ProfilePage(IWebDriver driver, string username) : base(driver)
        {
            _titleObserver = new TitleObserver(driver);
            _userName = username;
        }

        protected override By LoadIndicatingElementLocator { get; set; } = By.XPath("");


        public override bool Load(params string[] urlParams)
        {
            LoadIndicatingElementLocator = By.XPath($"//header//*[contains(text(),'{_userName}')]");
            return base.Load(_userName);
        }

        protected override void HandlePageLoading()
        {
            _titleObserver.SetObserver();
            base.HandlePageLoading();
        }

        protected override bool HandleLoadErrors()
        {
            //Instagram error is displayed on the page (anti-bot or something like that).
            if (ElementIsVisible(LoadErrorElementLocator)(_driver))
            {
                if (_titleObserver.GetTitleChanges().FirstOrDefault(title => title.Contains(_userName)) != null)
                    throw new InstaAntiBotException($"Detected: К сожалению, эта страница недоступна." +
                        $" However page title contained {_userName} which indicates that the page exists but Instagram hides it for the current user agent.");
                else
                    throw new UserPageUnavailable("Detected: К сожалению, эта страница недоступна. But there's no sign of anti-scrapping measures.");
            }
            return base.HandleLoadErrors();
        }

        public bool CheckHasStory()
        {
            return new Wait(_driver).TryFindElement(By.XPath("//section//header//div[@style='cursor: pointer;']"), out _);
        }

        public bool CheckClosedAccount()
        {
            return new Wait(_driver).TryFindElement(By.XPath(".//article/div//*[contains(text(),'закрытый аккаунт')]"), out _);
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
            _driver.FindElement(By.XPath(".//section/ul/li[2]//div/span/span"));

        public IWebElement FollowingsNumElement =>
            _driver.FindElement(By.XPath(".//section/ul/li[3]//div/span/span"));

        public ReadOnlyCollection<IWebElement> PostLinks =>
            _driver.FindElements(By.XPath(".//article/div/div/div/div/a"));

        public bool OpenPost(out Post? postObj, int postNumFromTopLeft = 0)
        {
            if (PostLinks.Count < 1)
            {
                postObj = null;
                return false;
            }
            
            PostLinks[postNumFromTopLeft].Click();
            postObj = new Post(_driver);
            return true;
        }

        public IList<Post> GetLastPosts(int limit = 4)
        {
            if (CheckClosedAccount())
                return new List<Post>();

            var opened = OpenPost(out Post? current);
            var result = new List<Post>();
            var count = 1;

            while (opened && current != null && count <= Math.Min(limit, PostLinks.Count))
            {
                result.Add(current);
                count++;
                opened = current.TrySwitchToNext(out current);
            }

            return result;
        }
    }
}
