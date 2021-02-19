using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Server.Packages.Letters
{
    public abstract class Letter : RequestObject
    {
        public string LetterType { get;  set; }
        public string Title { get; }
        public string Description { get; }
        public Person Sender { get; }
        public DateTime DateCreate = DateTime.Now;
        public Letter(string title, string description, Person sender)
        {
            if (string.IsNullOrWhiteSpace(description) || sender == null)
            {
                throw new ArgumentNullException("Вы не можете отправить письмо без описания или не авторизовавались");
            }
            else
            {
                Title = title;
                Description = description;
                Sender = sender;
            }
        }
    }
}
