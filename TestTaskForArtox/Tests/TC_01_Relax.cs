using MyChudoFrame.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyChudoFrame.Tests
{
    [TestClass]
    public class TC_01_Relax : BaseTest
    {
        [TestMethod]
        public void TestCaseRelax()
        {
            Logger.Info("Go to relax.ua");
            var relaxNurseryBarbershopForms = new RelaxNurseryBarbershopForms();

            Logger.Info("Check the barbershop list on first page");
            relaxNurseryBarbershopForms.CheckItem();

            Logger.Info("Check pagination");
            relaxNurseryBarbershopForms.CheckPagination();

            Logger.Info("Check filter");
            relaxNurseryBarbershopForms.CheckFilter();
        }
    }
}
