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
            HandleScrapingWarning();

            //Instagram error is displayed on the page (anti-bot or something like that).
            foreach(var errorElement in LoadErrorElementLocators)
            {
                if (ElementIsVisible(errorElement)(_driver))
                {
                    if (_titleObserver.GetTitleChanges().FirstOrDefault(title => title.Contains(_userName)) != null)
                        throw new InstaAntiBotException($"Detected: К сожалению, эта страница недоступна." +
                            $" However page title contained {_userName} which indicates that the page exists but Instagram hides it for the current user agent.");
                    else
                        throw new UserPageUnavailable("Detected: К сожалению, эта страница недоступна. But there's no sign of anti-scrapping measures.");
                }
            }

            return base.HandleLoadErrors();
        }

        private void HandleScrapingWarning()
        {
            if (_titleObserver.GetTitleChanges().FirstOrDefault(title => title.Contains("Scraping Warning")) != null)
            {
                new Wait(_driver).TryFindElement(By.XPath("//div[@role='button']/div/div/span[text()='OK']"),
                    out IWebElement? okButton);

                okButton?.Click();

                throw new InstaAntiBotException($"Scraping Warning Challenge Page appeared while crawling user {_userName}.");
            }
        }

        private readonly By _followButtonLocator = By.XPath("//header/section//button//*[contains(text(),'Подписаться')]");
        private readonly By _followingsButtonLocator = By.XPath("//header/section//button//*[contains(text(),'Подписки')]");
        
        public bool Follow()
        {
            var blueButton = new Wait(_driver).TryWaitForElement(_followButtonLocator);
            if (blueButton != null)
                blueButton?.Click();

            var greyButton = new Wait(_driver).WaitForElement(_followingsButtonLocator);
            return greyButton != null && greyButton.Displayed && CheckAntiBotProtectionOnSubscription();
        }

        private bool CheckAntiBotProtectionOnSubscription()
        {
            new Wait(_driver).TryFindElement(
                By.XPath("//div[@role='dialog']//span[contains(text(),'Повторите попытку позже')]"), out IWebElement? antiBotDialog);
            if (antiBotDialog != null && antiBotDialog.Displayed)
                throw new InstaAntiBotException("Anti-bot dialog was shown after an attempt to follow a user.");

            return true;
        }

        public bool Unfollow() 
        {
            var greyButton = new Wait(_driver).TryWaitForElement(_followingsButtonLocator);
            if (greyButton != null)
            {
                greyButton?.Click();
                var unfollowMenuItem = new Wait(_driver)
                .TryWaitForElement(By.XPath(".//div[@role='dialog']//span[contains(text(),'Отменить подписку')]"));
                if (unfollowMenuItem != null)
                    unfollowMenuItem?.Click();
            }

            var blueButton = new Wait(_driver).WaitForElement(_followButtonLocator);
            return blueButton != null && blueButton.Displayed;
        }

        public bool CheckHasStory()
        {
            new Wait(_driver).TryFindElement(By.XPath(".//section//header//div[@style='cursor: pointer;']"), out IWebElement? storyElem);
            return storyElem != null && storyElem.Displayed;
        }

        public bool CheckClosedAccount()
        {
            new Wait(_driver).TryFindElement(By.XPath(".//article/div//*[contains(text(),'закрытый аккаунт')]"), out IWebElement? closedElement);
            return closedElement != null;
        }

        private ReadOnlyCollection<IWebElement> DescriptionParts =>
            _driver.FindElements(By.XPath("//header/section/div[3]/*[text()]"));

        public string GetDescriptionText()
        {
            var textParts = DescriptionParts.Select(p => p.Text).ToArray();
            if (textParts.Length < 1) textParts = new string[] { "placeholder" };
            return string.Join("\r\n", textParts);
        }

        public IWebElement FollowersNumElement =>
            _driver.FindElement(By.XPath(".//section/ul/li[2]//a/span/span"));

        public IWebElement FollowingsNumElement =>
            _driver.FindElement(By.XPath(".//section/ul/li[3]//a/span/span"));

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
            postObj = new Post(_driver).Load();
            
            return (postObj.PublishDate > default(DateTime));
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
