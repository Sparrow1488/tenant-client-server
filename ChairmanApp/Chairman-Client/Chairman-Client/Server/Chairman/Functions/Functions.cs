﻿using ChairmanClient.Server;
using ChairmanClient.Server.Packages;
using ChairmanClient.Server.ServerMeta;
using Multi_Server_Test.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chairman_Client.Server.Chairman.Functions
{
    public class Functions
    {
        public static Functions Active;
        private string SERVERHOST = "127.0.0.1";
        private Person Chairman { get; }
        public Functions(string secretCode, JumboServer server)
        {
            if (secretCode == "secret" && server != null)
            {
                Chairman = server.ActiveUser;
                Active = this;
            }
            else
                throw new NullReferenceException("Вам отказано в доступе");
        }
        public string ShowChairmanLogin()
        {
            return Chairman.Login;
        }
        public async Task<string> AddNews(News news)
        {
            var meta = new PackageMeta(SERVERHOST, "addNews");
            return await JumboServer.ActiveServer.SendAndGetAsync(news, meta);
        }
        public async Task<string> GetLetters()
        {
            var meta = new PackageMeta(SERVERHOST, "getLetters");
            return await JumboServer.ActiveServer.SendAndGetAsync(null, meta);
        }
    }
}
