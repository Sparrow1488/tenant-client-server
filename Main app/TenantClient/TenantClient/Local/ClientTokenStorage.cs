using System.IO;
using System.Threading.Tasks;
using TenantClient.Exceptions;

namespace TenantClient.Local
{
    public static class ClientTokenStorage
    {
        public static string Token { get; private set; }
        private static string _saveTokenName = "authorization";
        private static string _saveTokenExtension = ".token";
        private static string _saveTokenFullName = _saveTokenName + _saveTokenExtension;

        /// <summary>
        /// Сохраняет переданный токен в коренную папку с программой
        /// </summary>
        /// <param name="saveForSession">true - если необходимо сохранить на период текущей сессии в переменную</param>
        /// <exception cref="TokenException"/>
        public static async Task SaveOnMachineAsync(string token, bool saveForSession)
        {
            if (!TokenIsValid(token))
                throw new TokenException("Переданный токен не может быть сохранен, поскольку не является валидным");
            if (saveForSession)
                SaveForSession(token);
            try
            {
                using (var sw = new StreamWriter(_saveTokenFullName, false))
                    await sw.WriteAsync(token);
            }
            catch { throw new ClientException("Возникла неизвестная ошибка при сохранении токена"); }
        }
        /// <summary>
        /// Пытается получить токен, сохраненный на локальную машину
        /// </summary>
        /// <returns>false - неудача</returns>
        public static bool TryGet(out string token)
        {
            token = string.Empty;
            if (!TokenWasExist())
                return false;
            using (var sr = new StreamReader(_saveTokenFullName))
                token = sr.ReadToEnd();
            if (TokenIsValid(token))
                return true;
            return false;
        }
        /// <summary>
        /// Сохраняет токен в локальную переменную данного класса для упрощения дальнейшей работы с ним
        /// </summary>
        /// <exception cref="TokenException"/>
        public static void SaveForSession(string token)
        {
            if (!TokenIsValid(token))
                throw new TokenException("Вы не можете сохранить токен в локальную переменную, поскольку он не является валидным");
            Token = token;
        }
        /// <summary>
        /// Проверяет токен на валидность
        /// </summary>
        public static bool TokenIsValid(string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
                return true;
            return false;
        }
        /// <summary>
        /// Удаляет токен как из локальной переменной (очищает сессию), так и на локальной машине (в виде файла)
        /// </summary>
        public static void Remove()
        {
            Token = string.Empty;
            if (TokenWasExist())
                File.Delete(_saveTokenFullName);
        }
        public static bool TokenWasExist()
        {
            if (File.Exists(_saveTokenFullName))
                return true;
            return false;
        }
    }
}
