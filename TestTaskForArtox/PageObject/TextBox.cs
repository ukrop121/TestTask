using OpenQA.Selenium;

namespace MyChudoFrame.PageObject
{
    public class TextBox : BaseElement
    {
        public TextBox(By locator) : base(locator) { }

        public void SendKey(string str)
        {
            Element = FindElement(Locator);
            Element.Clear();
            Element.SendKeys(str);
        }
    }
}
