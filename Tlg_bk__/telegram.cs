using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TLSharp.Core;
using TeleSharp.TL.Messages;
using TeleSharp.TL;

namespace Tlg_bk__
{
    public class Telegram
    {
        public TelegramClient client;
        string telNumber;
        string hash;
        TeleSharp.TL.TLUser user;
        public Telegram(string telNumber)
        {
            client = new TelegramClient(Convert.ToInt32(ConfigurationManager.AppSettings["api_id"]), ConfigurationManager.AppSettings["api_hash"]);
            this.telNumber = telNumber;
            GetConnect();
            Task.WaitAll();

        }
        private async void GetConnect()
        {
            await client.ConnectAsync();
            try
            {
                hash = await client.SendCodeRequestAsync(telNumber);
            }
            catch
            {
                hash = await client.SendCodeRequestAsync(telNumber);
            }
        }
        public async void GetAutorisation(string code)
        {
            try
            {
                user = await client.MakeAuthAsync(telNumber, hash, code);
            }
            catch
            {

            }
        }


    }
}
