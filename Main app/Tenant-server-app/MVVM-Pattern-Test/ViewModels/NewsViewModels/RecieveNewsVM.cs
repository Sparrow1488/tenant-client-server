using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages.Default;
using MVVM_Pattern_Test.ClientEntities;
using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.MyApplication;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public List<Publication> ReceivedPublications
        { 
            get { return _recievedNews; } 
            private set { _recievedNews = value; OnPropertyChanged(); } 
        }
        private List<Publication> _recievedNews = new List<Publication>();
        public ObservableCollection<PostStruct> RecievedNewsStruct
        {
            get { return _recievedNewsStruct; }
            set { _recievedNewsStruct = value; OnPropertyChanged(); }
        }
        private ObservableCollection<PostStruct> _recievedNewsStruct = new ObservableCollection<PostStruct>();
        //public List<Border> NewsPanels
        //{
        //    get { return _newsPanels; }
        //    set { _newsPanels = value; OnPropertyChanged(); }
        //}
        //private List<Border> _newsPanels = new List<Border>();

        #region Commands
        public MyCommand RecieveNews
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    var manager = new ExSysManager();
                    var response = await manager.GetPublications();
                    if(response.Status == ResponseStatus.Ok)
                    {
                        var publications = response.ResponseData as ICollection<Publication>;
                        ReceivedPublications = publications.ToList();
                        CreatePublicationStructs();
                    }
                });
            }
        }
        public void CreatePublicationStructs()
        {
            if(ReceivedPublications != null && ReceivedPublications.Count > 0)
            {
                foreach (var post in ReceivedPublications)
                {
                    RecievedNewsStruct.Add(new PostStruct(post));
                }
            }
        }
        #endregion
    }
}
