using OpenQA.Selenium;

namespace MyChudoFrame.Forms
{
    public class BaseForm : BaseEntity
    {
        private IWebElement Locator;

        public BaseForm(By TitleLocator)
        {
            Locator = Browser.GetDriver().FindElement(TitleLocator);
        }

    }
}
