using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Pattern_Test.ClientEntities
{
    public class FilesHelper
    {
        private string _tokenFileName = "_authorization_Token";
        /// <summary>
        /// Сохраняет токен авторизации в корневую папку с программой
        /// </summary>
        /// <returns>Токен авторизации</returns>
        public string OpenTokenLocal()
        {
            string token = string.Empty;
            if (File.Exists(_tokenFileName + ".txt"))
            {
                using (var sr = File.OpenText(_tokenFileName + ".txt"))
                    token = sr.ReadToEnd();
                token = (string)JsonConvert.DeserializeObject(token);
            }
            return token;
        }
        /// <summary>
        /// Сохраняет токен в локальное хранилище для дальнейшей работы с ним
        /// </summary>
        public void SaveTokenLocal(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Переданная строка не является токеном");
            var jsonToken = JsonConvert.SerializeObject(token);
            var fileStream = File.CreateText($"./{_tokenFileName}.txt");
            fileStream.WriteLine(jsonToken);
            fileStream.Close();
        }
    }
}
