using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyChudoFrame.PageObject
{
    class MultiBox : BaseElement
    {
        private Random random = new Random();
        private ReadOnlyCollection<IWebElement> WebElementList;
        public MultiBox(By locator) : base(locator) { }

        public IWebElement SelectRandomItem()
        {
            WebElementList = FindElements(Locator);
            return WebElementList[random.Next(WebElementList.Count - 1)];
        }

        public int GetAmountItems()
        {
            WebElementList = FindElements(Locator);
            return WebElementList.Count;
        }

        public ReadOnlyCollection<IWebElement> ElementsList()
        {
            return FindElements(Locator);
        }
    }
}
