using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Server.Packages.Letters
{
    public class Letter : RequestObject
    {
        public string LetterType { get; protected set; }
        public string Title { get; }
        public string Description { get; }
        public string SenderLogin { get; }
        public DateTime DateCreate = DateTime.Now;
        public Letter(string title, string description, string senderLogin)
        {
            if (string.IsNullOrWhiteSpace(description) || senderLogin == null)
                throw new ArgumentNullException("Вы не можете отправить письмо без описания или не авторизовавались");
            else
            {
                Title = title;
                Description = description;
                SenderLogin = senderLogin;
            }
        }
        [JsonConstructor]
        public Letter(string title, string description, string senderLogin, string letterType)
        {
            if (string.IsNullOrWhiteSpace(description) || senderLogin == null)
                throw new ArgumentNullException("Вы не можете отправить письмо без описания или не авторизовавались");
            else
            {
                Title = title;
                Description = description;
                SenderLogin = senderLogin;
                LetterType = letterType;
            }
        }
    }
}
