using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Packages.Default;
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
        /// <summary>
        /// Авторизоваться в системе
        /// </summary>
        /// <returns>Null - возникла неизвестная ошибка</returns>
        public async Task<ResponsePackage> LogIn(string login, string password, bool useAesRsa)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(string.Format("Логин: {0} или пароль: {1} являются не валидными!", nameof(login), nameof(password)));
            var pack = new Authorization(new UserPassport(login, password));
            ResponsePackage response;
            try
            {
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
            }
            catch { return null; }
            return response;
        }
        public async Task<ResponsePackage> TokenLogIn(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Переданная вами строка не является токеном");

            ResponsePackage response;
            var pack = new TokenAuthorization(new UserPassport("", "", token));
            var sendler = new RequestSendler(_connectionSettings);
            response = await sendler.SendRequest(pack);
            return response;
        }
        public async Task<ResponsePackage> GetPublications()
        {
            var pack = new ReceivePublications();
            ResponsePackage response;
            var sendler = new RequestSendler(_connectionSettings);
            response = await sendler.SendRequest(pack);
            return response;
        }
    }
}
