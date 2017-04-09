using MyChudoFrame.PageObject;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyChudoFrame.Forms
{
    public class RelaxNurseryBarbershopForms : BaseForm
    {
        private readonly static By _TitleLocator = By.XPath("//h1[contains(text(), 'Детские парикмахерские в Киеве')]");
        private Button _btFilter = new Button(By.XPath("//span[contains(@data-reactid, 'unwrapButton.0.1')]"));
        private Lable _lbCountAllItems = new Lable(By.XPath("//span[@class='CatalogNav__count u-grayLightest']"));
        private MultiBox _mbPaginationPage = new MultiBox(By.XPath("//a[@class='Pagination__page']"));
        private MultiBox _mbItemNurseryBarbershop = new MultiBox(By.XPath("//a[contains(@class, 'Place__headerLink Place__title')]"));
        private Button _btTimeWorkFilter = new Button(By.XPath("//div[contains(text(), 'Время работы')]/following-sibling::div/div"));
        private Checkbox _cbTimeWork2100 = new Checkbox(By.XPath("//span[@class='Radio__text'][contains(text(), '21:00')]"));
        private Checkbox _cbLeftCoust = new Checkbox(By.XPath("//div[contains(@class, 'List FilterSidebar')]//span[contains(text(), 'Левый берег')]"));
        private Link _lkFilterForManShowMore = new Link(By.XPath("//div[contains(text(), 'Для мужчин')]/following-sibling::div/div[7]"));
        private Checkbox _cbFilterForManShaveHead = new Checkbox(By.XPath("//span[contains(text(), 'Бритье головы')]"));
        private Button _btShowResult = new Button(By.XPath("//button[contains(@class, 'Button--primary')]"));
        private const int _DefaultElementsOnPage = 25;

        public RelaxNurseryBarbershopForms() : base(_TitleLocator) { }

        /// <summary>
        /// The Method check the list of barbershop on first page by pattern list
        /// </summary>
        public void CheckItem()
        {
            var barbarshopItem = _mbItemNurseryBarbershop.ElementsList();
            var barbershopNameList = new List<string>();

            foreach (var item in barbarshopItem)
            {
                barbershopNameList.Add(item.Text);
            }
            Assert.IsTrue(barbershopNameList.SequenceEqual(GetDataFromTXTFirstPage()));
        }

        /// <summary>
        /// The method check the pagination, check amount all pages, 
        /// click the random page and check amount item on the page 
        /// </summary>
        public void CheckPagination()
        {
            int amountPaginationPage = Convert.ToInt32(_lbCountAllItems.GetText()) / _DefaultElementsOnPage;
            if (Convert.ToInt32(_lbCountAllItems.GetText()) % _DefaultElementsOnPage != 0)
            {
                amountPaginationPage += 1;
            }

            Assert.IsTrue(amountPaginationPage == _mbPaginationPage.GetAmountItems() + 1);

            var paginationPage = _mbPaginationPage.SelectRandomItem();
            int pageNumber = Convert.ToInt32(paginationPage.Text);
            paginationPage.Click();

            if (pageNumber == amountPaginationPage)
            {
                Assert.IsTrue(_mbItemNurseryBarbershop.GetAmountItems() == Convert.ToInt32(_lbCountAllItems.GetText()) % _DefaultElementsOnPage);
            }
            else
            {
                Assert.IsTrue(_mbItemNurseryBarbershop.GetAmountItems() == _DefaultElementsOnPage);
            }
        }

        /// <summary>
        /// The method chek filter. Select filter time of work, left coust and shave head for man. 
        /// After then check the filter result by pattern list.
        /// </summary>
        public void CheckFilter()
        {
            _btFilter.ClickAndWaitToPageLoad();
            _cbLeftCoust.ClickAndWaitToPageLoad();
            _btTimeWorkFilter.ClickAndWaitToPageLoad();
            _cbTimeWork2100.ClickAndWaitToPageLoad();
            _lkFilterForManShowMore.ClickAndWaitToPageLoad();
            _cbFilterForManShaveHead.ClickAndWaitToPageLoad();
            _btShowResult.ClickAndWaitToPageLoad();

            var barbarshopItem = _mbItemNurseryBarbershop.ElementsList();
            var barbershopNameList = new List<string>();

            foreach (var item in barbarshopItem)
            {
                barbershopNameList.Add(item.Text);
            }
            Assert.IsTrue(barbershopNameList.SequenceEqual(GetDataFromTXTFilter()));
        }
    }
}
