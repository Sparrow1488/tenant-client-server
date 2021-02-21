using Chairman_Client.Chairman.Packages;
using Multi_Server_Test.Server;
using System;
using System.Threading.Tasks;
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
        public async Task<string> AddNews(News news) //return string изменить на соответствующий запросу
        {
            var addNewsPackage = new AddNewsPackage(news);
            return await JumboServer.ActiveServer.SendAndGetAsync(addNewsPackage);
        }
        public async Task<string> GetLetters()
        {
            var requestPackage = new GetLettersPackage();
            return await JumboServer.ActiveServer.SendAndGetAsync(requestPackage);
        }
    }
}
