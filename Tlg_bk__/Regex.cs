using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Tlg_bk__
{
    class Regx
    {
        WebSelenium Browser;
        public Regx(string text, string url, List<WebSelenium> Browsers)
        {
            while(true)
            {
                if(GetFreeBrowser(Browsers)!=null)
                {
                    Browser = GetFreeBrowser(Browsers);
                    break;
                }
            }
            Regex rgxTotal = new Regex(@"((?<=ТМ\s)\d+\.?\d?)");
            Regex itmPlayer = new Regex(@"((?<=ИГРОКА\s)\d)");
            if (text.ToLower().Contains("ставка на тм")) //для вируса сета(Волейбол)
            {
                var total = rgxTotal.Match(text).Value;
                Browser.GetRate("ТГВ", rgxTotal.Match(text).Value, url);
            }
            else if (text.ToLower().Contains("коэф 1")) //для вируса микса на тотал меньше по геймам (неактуально)
            {
                Browser.GetRate("ТГ", "", url);
            }
            else if (text.ToLower().Contains("ставка на итм")) //для вируса микса на индивидуальный тотал игрока
            {
                Browser.GetRate("ИТМ", itmPlayer.Match(text).Value, url);
            }
            else if (text.ToLower().Contains("первого тайма")) //для вируса гелэкси
            {
                Browser.GetRate("ТМПТ", rgxTotal.Match(text).Value, url);
            }
            else if (text.ToLower().Contains("оз - нет"))//для вируса джокер
            {
                Browser.GetRate("ОЗ", "", url);
            }
        }
        private WebSelenium GetFreeBrowser(List<WebSelenium> Browsers)
        {
            bool status = true;
            WebSelenium _brs=null;

            while (status)
            {
                foreach (var brs in Browsers)
                {
                    if (brs.Status() == 0)
                    {
                        _brs = brs;
                        status = false;
                        break;
                    }
                }
            }
            return _brs;
        }

    }
}
