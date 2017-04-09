using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyChudoFrame
{
    [TestClass]
    public abstract class BaseTest : BaseEntity
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Logger.InitLogger();
            Browser.GetDriver();
            Browser.GetDriver().Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(imlpicityWait));
            Browser.GoToURL();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Browser.Closed();
            Browser.Qiut();
        }
    }
}
