using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core;

namespace Tlg_bk__
{
    class TlgManipulation
    {
        TelegramClient client;
        List<WebSelenium> Browsers;
        Timer tickMix;
        public TlgManipulation(TelegramClient client, List<WebSelenium> browsers)
        {
            this.Browsers = browsers;
            this.client = client;
            GetChats();
        }
        public TlgManipulation(TelegramClient client)
        {
            this.client = client;
            UpdateCoff();
        }
        private async void UpdateCoff()
        {
            Thread.Sleep(1500);
            var dialogs = (TLDialogsSlice)await client.GetUserDialogsAsync();
            var chats = dialogs.Chats.ToList().OfType<TLChannel>().Select(a => a).Where(c => c.Title.Contains("ВИРУС ГЭЛЭКСИ") || c.Title.Contains("ВИРУС МИКС") || c.Title.Contains("ВИРУС СЕТ")|| c.Title.Contains("ДЖОКЕР 4 МЕСЯЦА")|| c.Title.Contains("Крейзи"));
            TLChannel mixChat = chats.ToList().Find(a => a.Title.Contains("ВИРУС МИКС"));
            TLChannel galaxyChat = chats.ToList().Find(a => a.Title.Contains("ВИРУС ГЭЛЭКСИ"));
            TLChannel setChat = chats.ToList().Find(a => a.Title.Contains("ВИРУС СЕТ"));
            TLChannel jokerChat = chats.ToList().Find(a => a.Title.Contains("ДЖОКЕР 4 МЕСЯЦА"));
            TLChannel crazyChat = chats.ToList().Find(a => a.Title.Contains("Крейзи"));
            TLChannelMessages messMix = (TLChannelMessages)await client.GetHistoryAsync(new TLInputPeerChannel { ChannelId = mixChat.Id, AccessHash = mixChat.AccessHash.Value });
            TLChannelMessages messGalaxy = (TLChannelMessages)await client.GetHistoryAsync(new TLInputPeerChannel { ChannelId = galaxyChat.Id, AccessHash = galaxyChat.AccessHash.Value });
            TLChannelMessages messSet = (TLChannelMessages)await client.GetHistoryAsync(new TLInputPeerChannel { ChannelId = setChat.Id, AccessHash = setChat.AccessHash.Value });
            TLChannelMessages messJoker = (TLChannelMessages)await client.GetHistoryAsync(new TLInputPeerChannel { ChannelId = jokerChat.Id, AccessHash = jokerChat.AccessHash.Value });
            TLChannelMessages messCrazy = (TLChannelMessages)await client.GetHistoryAsync(new TLInputPeerChannel { ChannelId = crazyChat.Id, AccessHash = crazyChat.AccessHash.Value });
            if (messMix.Messages.ToList()[0] is TLMessage)
            {
                UpdateIdLastMess("ВИРУС МИКС", ((TLMessage)messMix.Messages.ToList()[0]).Id);
            }
            if (messGalaxy.Messages.ToList()[0] is TLMessage)
            {
                UpdateIdLastMess("ВИРУС ГЭЛЭКСИ", ((TLMessage)messGalaxy.Messages.ToList()[0]).Id);
            }
            if (messSet.Messages.ToList()[0] is TLMessage)
            {
                UpdateIdLastMess("ВИРУС СЕТ", ((TLMessage)messSet.Messages.ToList()[0]).Id);
            }
            if (messJoker.Messages.ToList()[0] is TLMessage)
            {
                UpdateIdLastMess("ВИРУС ДЖОКЕР", ((TLMessage)messJoker.Messages.ToList()[0]).Id);
            }
            if (messCrazy.Messages.ToList()[0] is TLMessage)
            {
                UpdateIdLastMess("ВИРУС КРЕЙЗИ", ((TLMessage)messCrazy.Messages.ToList()[0]).Id);
            }
        }

        private async void GetChats()
        {
            Thread.Sleep(1500);
            var dialogs = (TLDialogsSlice)await client.GetUserDialogsAsync();
            var chats = dialogs.Chats.ToList().OfType<TLChannel>().Select(a => a).Where(c => c.Title.Contains("ДЖОКЕР 4 МЕСЯЦА") 
            //||c.Title.Contains("ВИРУС ГЭЛЭКСИ") 
            || c.Title.Contains("ВИРУС МИКС")
            || c.Title.Contains("Крейзи")
            //|| c.Title.Contains("ВИРУС СЕТ")
            );
            tickMix = new Timer(new TimerCallback(bk), InformationChats(chats), 0, 30000);
           // tickGalaxy = new Timer(new TimerCallback(bk), galaxy, 0, 1500);
           // tickSet = new Timer(new TimerCallback(bk), set, 0, 1500);
        }
        private List<ArgsStruct> InformationChats(IEnumerable<TLChannel> chats)
        {
            List<TLChannel> chatsVirus = new List<TLChannel> {
            chats.ToList().Find(a => a.Title.Contains("ВИРУС МИКС")),
            chats.ToList().Find(a=> a.Title.Contains("Крейзи")),
            //chats.ToList().Find(a => a.Title.Contains("ВИРУС ГЭЛЭКСИ")),
            //chats.ToList().Find(a => a.Title.Contains("ВИРУС СЕТ")),
            chats.ToList().Find(a=> a.Title.Contains("ДЖОКЕР 4 МЕСЯЦА"))
            };
            List<ArgsStruct> chatsVirusInfo = new List<ArgsStruct>();
            ArgsStruct chatInfo;
            foreach (var cht in chatsVirus)
            {
                chatInfo = new ArgsStruct();
                if (cht.Title.Contains("ВИРУС МИКС"))
                {
                    chatInfo.nameChat = "ВИРУС МИКС";
                }
                //else if (cht.Title.Contains("ВИРУС ГЭЛЭКСИ"))
                //{
                //    chatInfo.nameChat = "ВИРУС ГЭЛЭКСИ";
                //}
                //else if (cht.Title.Contains("ВИРУС СЕТ"))
                //{
                //    chatInfo.nameChat = "ВИРУС СЕТ";
                //}
                else if (cht.Title.Contains("Крейзи"))
                {
                    chatInfo.nameChat = "ВИРУС КРЕЙЗИ";
                }
                else if(cht.Title.Contains("ДЖОКЕР 4 МЕСЯЦА"))
                {
                    chatInfo.nameChat = "ВИРУС ДЖОКЕР";
                }    
                chatInfo.InputPeerChannel = new TLInputPeerChannel { ChannelId = cht.Id, AccessHash = cht.AccessHash.Value };
                chatsVirusInfo.Add(chatInfo);
            }
            return chatsVirusInfo;
        }

        private async void bk(object args)
        {
            List<ArgsStruct> chatVirus = (List<ArgsStruct>)args;
            TLChannelMessages mess;
            string nameChat=string.Empty;
            List<(string namechat, int id, string text, string url)> resultLastMessage = new List<(string,int, string, string)>();
            foreach(var cht in chatVirus)
            {
                
                Thread.Sleep(600);
                mess = (TLChannelMessages)await client.GetHistoryAsync(cht.InputPeerChannel,0,0,0,1);
                if (mess.Messages.ToList()[0] is TLMessage message)
                {
                    if (message.ReplyMarkup!=null)
                    {
                        var but = ((TLReplyInlineMarkup)message.ReplyMarkup).Rows.ToList();
                        (string namechat, int id, string text, string url) argsMess = (cht.nameChat.ToString(), message.Id, GetTextFromTelegramMessage(but), GetUrlFromTelegramMessege(but));
                        resultLastMessage.Add(argsMess);
                    }
                }
            }
            HandlerMessageTelegram(resultLastMessage);
        }
        private void HandlerMessageTelegram(List<(string namechat, int id, string text, string url)> resultLastMessage)
        {
            if (resultLastMessage.Count() > 0)
            {
                foreach (var lstMsg in resultLastMessage)
                {
                    if (lstMsg.id != GetInformationFromBd(lstMsg.namechat))
                    {
                        UpdateIdLastMess(lstMsg.namechat, lstMsg.id);
                        new Regx(lstMsg.text, lstMsg.url, Browsers);
                    }
                }
            }
        }
        private string GetTextFromTelegramMessage(List<TLKeyboardButtonRow> buttons)
        {
            string text = "";
            foreach (var btn in buttons.ToList())
            {
                foreach (var _btn in btn.Buttons.ToList())
                {
                    if (_btn is TLKeyboardButtonCallback)
                    {
                        if (((TLKeyboardButtonCallback)_btn).Text.ToLower().Contains("ставка на тм") || ((TLKeyboardButtonCallback)_btn).Text.Contains("коэф 1") ||
                            ((TLKeyboardButtonCallback)_btn).Text.Contains("первого тайма") || ((TLKeyboardButtonCallback)_btn).Text.Contains("СТАВКА НА ИТМ") 
                            || ((TLKeyboardButtonCallback)_btn).Text.Contains("ОЗ - нет"))
                        {
                            text = ((TLKeyboardButtonCallback)_btn).Text;
                            break;
                        }
                    }
                }
            }
            if (text == "")
                return string.Empty;
            else
                return text;
        }
        private string GetUrlFromTelegramMessege(List<TLKeyboardButtonRow> buttons)
        {
            string url = string.Empty;
            foreach (var btn in buttons.ToList())
            {
                foreach (var _btn in btn.Buttons.ToList())
                {
                    if (_btn is TLKeyboardButtonUrl)
                    {
                        if (((TLKeyboardButtonUrl)_btn).Text == "1ХСТАВКА")
                        {
                            url = ((TLKeyboardButtonUrl)_btn).Url;
                            break;
                        }
                    }
                }
            }
            if (url == "")
                return string.Empty;
            else
                return url;
        }
        private void UpdateIdLastMess(string nameChat, int lastMessId)
        {
            using (SQLiteConnection conn = new SQLiteConnection($@"Data source={Environment.CurrentDirectory}\Db.db; version=3;"))
            {
                string commandText = "update messId set mId = @lastMessId where Type = @nameChat";
                SQLiteCommand comm = new SQLiteCommand(commandText, conn);
                comm.Parameters.AddWithValue("@lastMessId", lastMessId);
                comm.Parameters.AddWithValue("@nameChat", nameChat);
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void StopTimer()
        {
            tickMix.Dispose();
        }
        private int GetInformationFromBd(string type)
        {
            int Id = 0;
            using (SQLiteConnection conn = new SQLiteConnection($@"Data source={Environment.CurrentDirectory}\Db.db; version=3;"))
            {
                string commandText = "select mId from messId where Type = @type";
                SQLiteCommand comm = new SQLiteCommand(commandText, conn);
                comm.Parameters.AddWithValue("@type", type);
                conn.Open();
                Id = Convert.ToInt32(comm.ExecuteScalar());
                conn.Close();
            }
            return Id;
        }


    }
}
