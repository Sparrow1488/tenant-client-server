using System;
using System.Collections.Generic;
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
        private Dictionary<string, Page> _loadedPages = new Dictionary<string, Page>();
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
                if (string.IsNullOrWhiteSpace(obj as string))
                    throw  new ArgumentNullException($"В качестве аргумента был получен null. Ожидался 'Button.Tag'");
                var pageName = obj as string;
                string validPageName = ValidatePageName(pageName);
                var pageRetreived = ShowLoadedPageIfExist(validPageName);
                if (!pageRetreived)
                {
                    SelectedPage = FindPageByTag(validPageName);
                    InsertPageInLoadedPages(SelectedPage, validPageName);
                }
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
                                                            .Where(type => ValidatePageName(type.Name) == ValidatePageName(tag))
                                                                .FirstOrDefault();
            if (findType == null)
                throw new FindPageException($"Не удалось найти страницу, исходя из заданного тега: {tag}");
            var page = findType.Assembly.CreateInstance(findType.FullName) as Page;
            return page;
        }

        private void InsertPageInLoadedPages(Page page, string pageName)
        {
            if (IsNull(page) || IsNull(pageName))
                throw new ArgumentNullException($"Переданные параметры: {nameof(page)} и {nameof(pageName)} имели значение null");
            var formatedPageName = pageName.ToLower();
            var wasExist = _loadedPages.TryGetValue(formatedPageName, out Page existPage);
            if (!wasExist)
                _loadedPages.Add(formatedPageName, page);
        }

        private bool IsNull(object checkingObj)
        {
            if (checkingObj == null)
                return true;
            return false;
        }

        private string ValidatePageName(string pageName)
        {
            return pageName.ToLower();
        }

        /// <summary>
        /// Отображает страницу, если таковая уже существует
        /// </summary>
        /// <returns>true - если страница была найдена и уже отображена</returns>
        private bool ShowLoadedPageIfExist(string pageName)
        {
            var wasExist = _loadedPages.TryGetValue(pageName, out Page existPage);
            if (wasExist)
                SelectedPage = existPage;
            else
                return false;
            return true;
        }
    }
}
