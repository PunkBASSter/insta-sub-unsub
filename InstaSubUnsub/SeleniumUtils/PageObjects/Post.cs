using InstaCommon;
using OpenQA.Selenium;
using SeleniumUtils.Helpers;

namespace SeleniumUtils.PageObjects
{
    public class Post
    {
        private readonly IWebDriver _driver;
        private readonly Lazy<IWebElement?> _element;
        
        public Post(IWebDriver webDriver)
        {
            _driver = webDriver;
            _element = new Lazy<IWebElement?>(() => new Wait(_driver).TryWaitForElement(By.XPath(".//div[@role='dialog']//article")));
        }

        public Post Load()
        {
            _ = _element.Value;
            return this;
        }

        private string? _description;
        public string Description {
            get
            {
                _description ??= new Wait(_driver, _element.Value)
                    .TryWaitForElement(By.XPath("//div[@role=\"dialog\"]//ul/div[@role=\"button\"]/li//h1"))
                    ?.Text ?? string.Empty;
                return _description;
            } 
        }

        private DateTime? _date;
        public DateTime PublishDate
        {
            get
            {
                if (_date is null)
                {
                    var dateElement = _element.Value?.FindElements(By.XPath(".//time")).LastOrDefault();
                    if (dateElement != null)
                    {
                        var dateTxt = dateElement.GetAttribute("datetime");
                        _date = DateTime.Parse(dateTxt);
                    }   
                }

                return _date ?? default;
            }
        }

        public bool LikeWithRetries(int attempts = 2)
        {
            bool likeResult;
            do
            {
                attempts--;
                likeResult = Like();
            }
            while (!likeResult && attempts > 0);
            return likeResult;
        }

        private bool Like()
        {
            if (_element.Value == null)
                return false;

            var wait = new Wait(_driver, _element.Value);
            var likeButton = wait.WaitForElement(By.XPath("//article//section//button//*[@aria-label='Нравится']/ancestor::button"));
            if (likeButton != null && likeButton.Displayed)
                likeButton.Click();

            new Delay().Random();

            wait.TryFindElement(
                By.XPath("//article//section//button//*[@aria-label='Не нравится']/ancestor::button"),
                out IWebElement? dislikeButton);
            return dislikeButton!= null && dislikeButton.Displayed;
        }

        public void Close()
        {
            _element.Value?.SendKeys(Keys.Escape);
        }

        public bool TrySwitchToNext(out Post nextPost)
        {
            try
            {
                _description ??= Description;
                _date ??= PublishDate;

                if (string.IsNullOrEmpty(_description) && (_date == default || _date == default(DateTime)))
                {
                    nextPost = this;
                    return false;
                }

                _element.Value?.SendKeys(Keys.ArrowRight);

                nextPost = new Post(_driver).Load();
                var nextPostDesc = nextPost.Description;
                var nextPostDate = nextPost.PublishDate;

                return !(_description == nextPostDesc && _date == nextPostDate);
            }
            catch(Exception)
            {
                nextPost = this;
                return false; 
            }
        }
    }
}
