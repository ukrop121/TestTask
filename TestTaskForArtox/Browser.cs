using System.IO;
using System;
using Microsoft.Office.Interop.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace MyChudoFrame
{
    public static class Browser
    {
        private static BaseEntity _baseEntity = new BaseEntity();
        private static IWebDriver _Driver;
        private static string _dataTime = DateTime.Now.ToString().Replace(".", string.Empty).Replace(":", string.Empty);
        private static string _ScreenPath = Directory.GetCurrentDirectory() + @"\ScreenShot\" + _dataTime;

        public static IWebDriver GetDriver()
        {
            var options = new ChromeOptions();
            options.AddArguments("--test-type, --disable-extensions", "--start-maximized");
            options.AddUserProfilePreference("download.default_directory", "C:\\VisualDownloads");
            options.AddUserProfilePreference("profile.default_content_settings.popups", 0);
            options.AddUserProfilePreference("profile.default_content_settings.multiple-automatic-downloads", 1);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.extensions_to_open", "");
            options.AddUserProfilePreference("safebrowsing.enabled", true);

            if (_Driver == null)
            {
                _Driver = new ChromeDriver(options);
            }
            return _Driver;
        }

        public static void WindowsMaximize()
        {
            _Driver.Manage().Window.Maximize();
        }

        public static void Qiut()
        {
            _Driver.Quit();
            _Driver = null;
        }

        public static void Closed()
        {
            _Driver.Close();
        }

        public static void GoToURL()
        {
            _Driver.Navigate().GoToUrl(_baseEntity.GetUrlRelax());
        }

        public static void GoToBack()
        {
            _Driver.Navigate().Back();
        }

        public static void Shoot(string сlassName)
        {
            var screenshot = ((ITakesScreenshot)_Driver).GetScreenshot();
            screenshot.SaveAsFile($"{_ScreenPath}{сlassName}.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        public static void SwitchTab()
        {
            _Driver.SwitchTo().Window(_Driver.WindowHandles[1]).Close();
            _Driver.SwitchTo().Window(_Driver.WindowHandles[0]);
        }

        public static void NewTab()
        {
            var but =
                _Driver.FindElement(
                    By.XPath("//button[@class='btn btn-transp-light size-sm text-size-xs js-signup-button']"));
            but.Click();
            var body = _Driver.FindElement(By.CssSelector("body"));
            body.SendKeys(Keys.Control + "t");
        }
    }
}
