using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MyChudoFrame.PageObject
{
    public class BaseElement : BaseEntity
    {
        protected IWebDriver Driver = Browser.GetDriver();
        protected By Locator;
        protected IWebElement Element;

        protected BaseElement(By locator)
        {
            Locator = locator;
        }

        protected IWebElement FindElement(By locator)
        {
            return Driver.FindElement(locator);
        }

        protected ReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            return Driver.FindElements(locator);
        }

        public void Click()
        {
            Element = FindElement(Locator);
            Element.Click();
        }

        public void ClickAndWaitToPageLoad()
        {
            Element = FindElement(Locator);
            Element.Click();
            new WebDriverWait(Driver, TimeSpan.FromSeconds(imlpicityWait)).Until(waiting =>
            {
                var result =
                    ((IJavaScriptExecutor)Driver).ExecuteScript(
                        "return document['readyState'] ? 'complete' == document.readyState : true");
                return result is Boolean && (Boolean)result;
            }); ;
        }

        public void DoubleClick()
        {
            Element = FindElement(Locator);
            Element.Click();
            Element.Click();
        }

        public string GetText()
        {
            Element = FindElement(Locator);
            return Element.Text;
        }

        public bool PesentElement()
        {
            Element = FindElement(Locator);
            return Element.Displayed;
        }
    }
}
