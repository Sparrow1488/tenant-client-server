using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Sendlers;
using ExchangeSystem.Requests.Sendlers.Close;
using ExchangeSystem.Requests.Sendlers.Open;
using System;
using System.Threading.Tasks;

namespace MVVM_Pattern_Test.ClientEntities
{
    public class ExSysManager
    {
        private ConnectionSettings _connectionSettings = new ConnectionSettings("127.0.0.1", 80);
        public async Task<ResponsePackage> Authorization(string login, string password, bool useAesRsa)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(string.Format("Логин: {0} или пароль: {1} являются не валидными!", nameof(login), nameof(password)));
            var pack = new Authorization(new UserPassport(login, password));
            ResponsePackage response;
            if (useAesRsa)
            {
                var sendler = new AesRsaSendler(_connectionSettings);
                response = await sendler.SendRequest(pack);
            }
            else
            {
                var sendler = new RequestSendler(_connectionSettings);
                response = await sendler.SendRequest(pack);
            }
            return response;
        }
    }
}
