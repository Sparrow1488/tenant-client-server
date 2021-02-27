using Chairman_Client.Chairman.Packages;
using Multi_Server_Test.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.ServerMeta;

namespace Chairman_Client.Server.Chairman.Functions
{
    public class Functions
    {
        public static Functions Active;
        public Functions(string secretCode, JumboServer server)
        {
            if (secretCode == "secret" && server != null)
            {
                Active = this;
            }
            else
                throw new NullReferenceException("Вам отказано в доступе");
        }
        public async Task<List<Letter>> GetLetters()
        {
            try
            {
                var requestPackage = new GetLettersPackage();
                var jsonListLetters = await JumboServer.ActiveServer.SendAndGetAsync(requestPackage);
                var listLetters = JsonConvert.DeserializeObject<List<Letter>>(jsonListLetters);
                return listLetters;
            }
            catch (JsonReaderException)
            {
                return null;
            }
        }
        public async Task<string> AddNews(News news)
        {
            var pack = new AddNewsPackage(news);
            var response = await JumboServer.ActiveServer.SendAndGetAsync(pack);
            return response;
        }
    }
}
