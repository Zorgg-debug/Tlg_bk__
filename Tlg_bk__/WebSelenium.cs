using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tlg_bk__
{
    public class WebSelenium
    {
        static public IWebDriver driver;
        static public int status = 0;
        public WebSelenium()
        {
            string prof = @"C:\Firefox\Profile1";
            FirefoxOptions options = new FirefoxOptions();
            FirefoxProfile profile = new FirefoxProfile(prof);
            options.BrowserExecutableLocation = (@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe");
            options.Profile = profile;
            driver = new FirefoxDriver(options);
            driver.Url = @"https://1xstavka.ru/";
        }

        public void GetRate(string flag, string cof, string url)
        {
            status = 1;
            openNewTab(url);
            GetBk(flag, cof);
            //CloseNewTab();
            status = 0;
        }
        public int Status()
        {
            return status;
        }
        private void GetBk(string flag, string cof)
        {
            bool flaq = true;
            try
            {
                var loaded = (new WebDriverWait(driver, System.TimeSpan.FromSeconds(150))).Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".multiselect.s-scoreboard-nav-multiselect")));

            }
            catch
            {
                flaq = false;
            }
            if (flaq)
            {
                switch (flag)
                {
                    case "ИТМ":
                        switch (cof)
                        {
                            case "1":
                                try
                                {
                                 var elemItm1 = driver.FindElement(By.XPath("//*[contains(text(),'Индивидуальный тотал 1-го')]")).FindElement(By.XPath("//*[@data-type='12']/ancestor::div[1]"));
                                 elemItm1.Click(); }
                                catch (Exception ex) { GetInfIntoFile(ex.Message); }
                                break;
                            case "2":
                                var elemItm2 = driver.FindElement(By.XPath("//*[contains(text(),'Индивидуальный тотал 2-го')]")).FindElement(By.XPath("//*[@data-type='14']/ancestor::div[1]"));
                                try { elemItm2.Click(); }
                                catch (Exception ex) { GetInfIntoFile(ex.Message); }
                                break;
                        }
                        break;
                    case "ТГ":
                        var elss = driver.FindElement(By.XPath("//*[contains(text(),'Тотал')]")).FindElements(By.XPath("//*[@data-type='10']/ancestor::div[1]"));
                        elss[elss.Count - 1].Click();
                        break;
                    case "ТГВ":
                        var rates = driver.FindElement(By.XPath("//*[contains(text(),'Тотал')]")).FindElements(By.XPath("//*[@data-type='10']/ancestor::div[1]"));
                        try
                        {
                            ChangeRate(rates, cof).Click();
                        }
                        catch(Exception ex) 
                        { 
                            GetInfIntoFile(ex.Message); 
                        }
                        //driver.FindElement(By.XPath("//*[contains(text(),'Тотал')]")).FindElement(By.XPath("//*[@data-type='10']")).FindElement(By.XPath("//*[contains(text(),'" + cof + "')]")).Click();
                        break;
                    case "ТМПТ":
                        try
                        {
                            driver.FindElement(By.CssSelector(".multiselect__single")).Click();
                        driver.FindElement(By.CssSelector(".multiselect__element")).FindElement(By.XPath("//*[contains(text(),'1-й Тайм')]/ancestor::span[1]")).Click();
                            driver.FindElement(By.XPath("//*[contains(text(),'Тотал. 1-й Тайм')]")).FindElement(By.XPath("//*[contains(text(),'" + cof.Replace(".0", "") + " М" + "')]/ancestor::div[1]")).Click();
                        }
                        catch (Exception ex) { GetInfIntoFile(ex.Message); }
                        break;
                    case "ОЗ":
                        try
                        {
                            driver.FindElement(By.XPath("//*[contains(text(),'Обе забьют')]")).FindElement(By.XPath("//*[@data-type='181']/ancestor::div[1]")).Click();

                        }
                        catch (Exception ex) { GetInfIntoFile(ex.Message); }
                        break;

                }
                try
                {
                    (new WebDriverWait(driver, System.TimeSpan.FromSeconds(10))).Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".c-coupon-modal__header")));
                    ResultRate(driver.FindElements(By.CssSelector(".c-coupon-modal__header"))[0].Text, flag, cof);
                }
                catch
                {
                    try
                    {
                        (new WebDriverWait(driver, System.TimeSpan.FromSeconds(10))).Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".swal2-popup.swal2-modal.swal2-show")));
                        ResultRate(driver.FindElements(By.CssSelector(".swal2-popup.swal2-modal.swal2-show"))[0].Text, flag, cof);
                    }
                    catch
                    {
                        GetBk(flag, cof);
                    }
                }
            }
        }
        private IWebElement ChangeRate(ReadOnlyCollection<IWebElement> rates, string coff)
        {
            double cof = Convert.ToDouble(coff.Replace(".",","));
            double TmpCof = 30;
            IWebElement resultrate = null;
            foreach (var rt in rates)
            {
                string TmpRate = rt.FindElement(By.CssSelector(".bet_type")).Text;
                if (Math.Abs(cof - Convert.ToDouble(TmpRate.Replace(".",",").Replace(" М","")))<= TmpCof)
                {
                    TmpCof = Math.Abs(cof - Convert.ToDouble(TmpRate.Replace(".", ",").Replace(" М", "")));
                    resultrate = rt;
                }
            }
            return resultrate;
        }

        private void ResultRate(string text, string flag, string cof)
        {
            if(text==string.Empty)
            {
                GetBk(flag, cof);
            }
            else if(text.ToLower().Contains("не удалось"))
            {
                driver.FindElement(By.CssSelector(".swal2-confirm.swal2-styled")).Click();
                GetBk(flag, cof);
            }
            else if(text.ToLower().Contains("ставка принята"))
            {
                driver.FindElement(By.CssSelector(".o-btn-group__item")).Click();
            }    
        }
        private void openNewTab(string url)
        {
            try
            {
                driver.Navigate().GoToUrl(url);
            }
            catch
            {

            }
        }
        private void CloseNewTab()
        {
            driver.Navigate().GoToUrl(@"https://1xstavka.ru/");
        }
        private void GetInfIntoFile(string textException)
        {
            File.AppendAllText(Environment.CurrentDirectory + @"\exc.txt", textException +DateTime.Now.ToString() +"\n");
        }


    }
}
