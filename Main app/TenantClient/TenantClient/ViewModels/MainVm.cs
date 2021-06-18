using System;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using TenantClient.Commands;
using TenantClient.Exceptions;
using TenantClient.Pages;

namespace TenantClient.ViewModels
{
    public class MainVm : BaseVM
    {
        public Page SelectedPage
        {
            get => _selectedPage;
            set
            {
                _selectedPage = value;
                OnPropertyChanged("SelectedPage");
            }
        }
        private Page _selectedPage = new Profile();
        public MyCommand ShowPageByTag
        {
            get => new MyCommand((obj) =>
            {
                if (obj == null)
                    throw  new ArgumentNullException($"В качестве аргумента был получен null. Ожидалось {typeof(Button).ToString()}");
                var senderButton = obj as Button;
                var pageName = senderButton.Tag.ToString();
                SelectedPage = FindPageByTag(pageName);
            });
        }
        /// <summary>
        /// Рефлексией осуществляется поиск странички по тегу (имени самого класса странички)
        /// </summary>
        /// <param name="tag">Название класса странички</param>
        /// <exception cref="FindPageException">Не удалось найти и создать по указаному тегу объект страницы</exception>
        private Page FindPageByTag(string tag)
        {
            Type parent = typeof(Page);
            Type findType = Assembly.GetExecutingAssembly()
                                                    .GetTypes()
                                                    .Where(type => parent.IsAssignableFrom(type) &&
                                                        !type.IsInterface &&
                                                        !type.IsAbstract).ToArray()
                                                            .Where(type => type.Name.ToLower() == tag.ToLower())
                                                                .FirstOrDefault();
            if (findType == null)
                throw new FindPageException($"Не удалось найти страницу, исходя из заданного тега: {tag}");
            var page = findType.Assembly.CreateInstance(findType.FullName) as Page;
            return page;
        }
    }
}
