using ExchangeSystem.Requests.Objects.Entities;
using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.MyApplication;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MVVM_Pattern_Test.ViewModels.NewsViewModels
{
    public class RecieveNewsVM : BaseVM
    {
        public RecieveNewsVM()
        {
            RecieveNews.Execute(null);
        }
        public override string Notice { get { return _infoMessage; } protected set { _infoMessage = value; OnPropertyChanged(); } }
        public List<Publication> RecievedNews
        { 
            get { return _recievedNews; } 
            set { _recievedNews = value; OnPropertyChanged(); } 
        }
        private List<Publication> _recievedNews = new List<Publication>();
        public ObservableCollection<PostStruct> RecievedNewsStruct
        {
            get { return _recievedNewsStruct; }
            set { _recievedNewsStruct = value; OnPropertyChanged(); }
        }
        private ObservableCollection<PostStruct> _recievedNewsStruct = new ObservableCollection<PostStruct>();
        public List<Border> NewsPanels
        {
            get { return _newsPanels; }
            set { _newsPanels = value; OnPropertyChanged(); }
        }
        private List<Border> _newsPanels = new List<Border>();

        #region Commands
        public MyCommand RecieveNews
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    //RecievedNewsStruct = new ObservableCollection<PostStruct>();
                    //var getNews = await JumboServer.ActiveServer.ReceiveNewsCollectionAsync();
                    //Notice = "Загрузка новостей...";
                    //if (getNews != null)
                    //{
                    //    foreach (var item in getNews)
                    //    {
                    //        RecievedNewsStruct.Add(new PostStruct(item));
                    //    }
                    //    RecievedNews = getNews;
                    //    Notice = "Новости загружены";
                    //}
                    //else
                    //    Notice = "Пока новостей нет :(";
                });
            }
        }
        #endregion
    }
}
