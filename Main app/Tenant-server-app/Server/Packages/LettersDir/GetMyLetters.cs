using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Server.Packages.LettersDir
{
    public class GetMyLettersPackage : Package
    {
        /// <summary>
        /// Передать объект пользователя, у которого обязательно должен присутствовать ID
        /// </summary>
        /// <param name="sendObj"></param>
        public GetMyLettersPackage(RequestObject sendObj) : base(sendObj)
        {
            SendingMeta = new PackageMeta(new ServerConfig().HOST, "Letter/get-my");
        }
    }
}
