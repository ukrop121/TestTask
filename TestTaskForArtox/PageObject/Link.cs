using OpenQA.Selenium;

namespace MyChudoFrame.PageObject
{
    public class Link : BaseElement
    {
        public Link(By locator) : base(locator) { }

        public string GetHref()
        {
            return Element.GetAttribute("href");
        }
    }
}
