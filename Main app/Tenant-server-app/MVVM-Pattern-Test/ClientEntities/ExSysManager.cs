using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Objects.Packages.Default;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Sendlers;
using ExchangeSystem.Requests.Sendlers.Close;
using ExchangeSystem.Requests.Sendlers.Open;
using Microsoft.Win32;
using System;
using System.IO;
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
            ResponsePackage response = new ResponsePackage("", ResponseStatus.Exception, "");
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                    throw new ArgumentException("Переданная вами строка не является токеном");

                var pack = new TokenAuthorization(new UserPassport("", "", token));
                var sendler = new RequestSendler(_connectionSettings);
                response = await sendler.SendRequest(pack);
            }
            catch { return response; }
            return response;
        }
        public async Task<ResponsePackage> GetPublications()
        {
            ResponsePackage response = new ResponsePackage("", ResponseStatus.Exception, "Возникла ошибка при получении публикаций (client)");
            try
            {
                var pack = new ReceivePublications();
                var sendler = new RequestSendler(_connectionSettings);
                response = await sendler.SendRequest(pack);
            }
            catch { return response; }
            return response;
        }
        public async Task<ResponsePackage> GetLetters(string token)
        {
            var pack = new ReceiveLetters();
            pack.UserToken = token;
            ResponsePackage response;
            var sendler = new RequestSendler(_connectionSettings);
            response = await sendler.SendRequest(pack);
            return response;
        }
        public async Task<ResponsePackage> AddPublication(Publication post, string token)
        {
            ResponsePackage response = new ResponsePackage("", ResponseStatus.Exception, "Возникла ошибка при отправке публикации");
            try
            {
                var pack = new NewPublication(post);
                pack.UserToken = token;
                var sendler = new RequestSendler(_connectionSettings);
                response = await sendler.SendRequest(pack);
            }
            catch { return response; }
            return response;
        }
        public async Task<ResponsePackage> GetSource(int[] sourceIds)
        {
            var response = new ResponsePackage("", ResponseStatus.Exception, "Возникла ошибка при отправке запроса на сервер");
            try
            {
                var pack = new ReceiveSource(new Publication() { sourcesId = sourceIds });
                var sendler = new RequestSendler(_connectionSettings);
                response = await sendler.SendRequest(pack);
                return response;
            }
            catch { return response; }
        }
        
    }
}
