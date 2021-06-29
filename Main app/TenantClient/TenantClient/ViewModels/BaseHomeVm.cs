using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using TenantClient.Commands;
using TenantClient.Exceptions;

namespace TenantClient.ViewModels
{
    internal class BaseHomeVm : BaseVM
    {
        protected Dictionary<string, Page> _loadedPages = new Dictionary<string, Page>();
        public Page SelectedPage
        {
            get => _selectedPage;
            set
            {
                _selectedPage = value;
                OnPropertyChanged("SelectedPage");
            }
        }
        private Page _selectedPage;
        public MyCommand ShowPageByTag
        {
            get => new MyCommand((obj) =>
            {
                if (string.IsNullOrWhiteSpace(obj as string))
                    throw new ArgumentNullException($"В качестве аргумента был получен null. Ожидался 'Tag'");
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
        protected Page FindPageByTag(string tag)
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

        protected bool IsNull(object checkingObj)
        {
            var isNull = false;
            if (checkingObj == null)
                isNull = true;
            return isNull;
        }

        protected string ValidatePageName(string pageName)
        {
            return pageName.ToLower();
        }

        /// <summary>
        /// Отображает страницу, если таковая уже существует
        /// </summary>
        /// <returns>true - если страница была найдена и уже отображена</returns>
        protected bool ShowLoadedPageIfExist(string pageName)
        {
            var pageWasExist = false;
            var wasExist = _loadedPages.TryGetValue(pageName, out Page existPage);
            if (wasExist)
            {
                SelectedPage = existPage;
                pageWasExist = true;
            }
            else
                pageWasExist = false;
            return pageWasExist;
        }
        protected void InsertPageInLoadedPages(Page page, string pageName)
        {
            if (IsNull(page) || IsNull(pageName))
                throw new ArgumentNullException($"Переданные параметры: {nameof(page)} и {nameof(pageName)} имели значение null");
            var formatedPageName = pageName.ToLower();
            var wasExist = _loadedPages.TryGetValue(formatedPageName, out Page existPage);
            if (!wasExist)
                _loadedPages.Add(formatedPageName, page);
        }

    }
}
