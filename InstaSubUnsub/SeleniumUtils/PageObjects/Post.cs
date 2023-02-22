using OpenQA.Selenium;

namespace SeleniumUtils.PageObjects
{
    public class Post
    {
        private readonly IWebDriver _driver;
        private readonly Lazy<IWebElement> _element;
        
        public Post(IWebDriver webDriver)
        {
            _driver = webDriver;
            _element = new Lazy<IWebElement>(() => new Wait(_driver).WaitForElement(By.XPath(".//div[@role='dialog']//article")) );
        }

        public IWebElement RootElement
        { 
            get { return _element.Value; } 
        }

        private string? _description;
        public string Description {
            get
            {
                _description ??= new Wait(_driver, _element.Value)
                    .TryWaitForElement(By.XPath(".//div[@role=\"dialog\"]//ul/div[@role=\"button\"]/li//h1"))
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
                    var element = new Wait(_driver, _element.Value).TryWaitForElement(By.XPath(".//a/div/time"));
                    if (element != null)
                    {
                        var dateTxt = element.GetAttribute("datetime");
                        _date = DateTime.Parse(dateTxt);
                    }   
                }

                return _date ?? default;
            }
        }
        
        public bool TrySwitchToNext(out Post nextPost)
        {
            //save current to compare
            _description ??= Description;
            _date ??= PublishDate;

            _element.Value.SendKeys(Keys.ArrowRight);

            nextPost = new Post(_driver);
            var nextPostDesc = nextPost.Description;
            var nextPostDate = nextPost.PublishDate;

            return !(_description == nextPostDesc && _date == nextPostDate);
        }
    }
}
