using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.IO;
using OpenPop.Mime;
using OpenPop.Pop3;
using System.Text.RegularExpressions;
using OpenPop.Mime.Header;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace MyChudoFrame
{
    public class BaseEntity
    {
        private readonly Random Rng = new Random();
        private const string Chars = "abcdefgijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_.1234567890";
        private string _RegExpPatternForActivationLink = "https:\\S*";
        private string _BarbarshopListFirstPage = Directory.GetCurrentDirectory() + @"\TestSoure\BarbarsopList.txt";
        private string _BarbarshopListFilter = Directory.GetCurrentDirectory() + @"\TestSoure\BarbarsopListFilter.txt";
        private int _Pop3Port = 995;
        private bool _Pop3SSL = true;
        private string _MailActiveLinkFromAdrees = "MyKaspersky@kaspersky.com";
        private string _CookieDisableCapchaName = "FACKey", _CookieDisableCapchaValue = "5804b9e8-020b-4648-88b6-82ee30b4bd73",
                        _CookieDisableCapchaDomain = ".my.kaspersky.com", _CookieDisableCapchaPath = "/";
        protected const int imlpicityWait = 60;

        public string GetUrlRelax()
        {
            return ConfigurationSettings.AppSettings.Get("URLRelax");
        }

        public string GetUrlMail()
        {
            return ConfigurationSettings.AppSettings.Get("URLMail");
        }

        public string GetLogin()
        {
            return ConfigurationSettings.AppSettings.Get("Login");
        }

        public string GetPassword()
        {
            return ConfigurationSettings.AppSettings.Get("Password");
        }

        public string GetPopServer()
        {
            return ConfigurationSettings.AppSettings.Get("PopServer");
        }

        public string GetAccountPassword()
        {
            return ConfigurationSettings.AppSettings.Get("AccountPassword");
        }

        public string GetAccountGmail()
        {
            return ConfigurationSettings.AppSettings.Get("AccountGmail");
        }

        public string GetClassName()
        {
            return this.GetType().Name;
        }

        public string GetDataFromExel(int Rows, int Column)
        {
            Excel.Application ObjWorkExcel = new Excel.Application();
            Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(_BarbarshopListFirstPage, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];
            string Value = ObjWorkSheet.Cells[Rows, Column].Text;
            ObjWorkBook.Close(false, Type.Missing, Type.Missing);
            ObjWorkExcel.Quit();
            GC.Collect();
            return Value;
        }

        public List<string> GetDataFromTXTFirstPage()
        {
            var data = File.ReadAllLines(_BarbarshopListFirstPage);
            return new List<string>(data);
        }

        public List<string> GetDataFromTXTFilter()
        {
            var data = File.ReadAllLines(_BarbarshopListFilter);
            return new List<string>(data);
        }

        public string GetMailLink()
        {
            Message message = null;
            int allMessageCount;
            MessagePart plainTextPart = null;
            bool flag = true;

            while (flag)
            {
                using (Pop3Client client = new Pop3Client())
                {

                    client.Connect(GetPopServer(), _Pop3Port, _Pop3SSL);
                    client.Authenticate(String.Format(GetAccountGmail(), String.Empty), GetAccountPassword(), AuthenticationMethod.UsernameAndPassword);


                    allMessageCount = client.GetMessageCount();

                    for (int i = 1; i <= allMessageCount; i++)
                    {
                        MessageHeader mes = client.GetMessageHeaders(i);
                        if (mes.From.Address.Contains(_MailActiveLinkFromAdrees) && mes.Subject.Contains("Account activation"))
                        {
                            message = client.GetMessage(i);
                            plainTextPart = message.FindFirstHtmlVersion();
                            flag = false;
                        }
                    }

                    client.Disconnect();
                }
            }

            return Regex.Match(plainTextPart.GetBodyAsText(), _RegExpPatternForActivationLink).ToString().Replace("\"", String.Empty);
        }

        public Cookie CookieKPCDisableCapcha()
        {
            return new Cookie(_CookieDisableCapchaName, _CookieDisableCapchaValue, _CookieDisableCapchaDomain, _CookieDisableCapchaPath, DateTime.MaxValue);
        }

        public string RandomString()
        {
            int size = Rng.Next(5, 30);
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = Chars[Rng.Next(Chars.Length)];
            }
            return new string(buffer);
        }
    }
}
