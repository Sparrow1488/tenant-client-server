using ExchangeSystem.v2.Entities;
using ExchangeSystem.v2.Packages;
using ExchangeSystem.v2.Packages.Default;
using ExchangeSystem.v2.Sendlers;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TenantClient.Commands;

namespace TenantClient.ViewModels
{
    internal class PostsVm : BaseVM
    {
        /// <summary>
        /// СРАЗУ ПРИ СОЗДАНИИ ПОЛУЧАЕТ СПИСОК ПУБЛИКАЦИЙ
        /// </summary>
        public PostsVm()
        {
            GetAllPosts?.Execute(null);
        }
        public ObservableCollection<Publication> Publications
        {
            get => _posts;
            set
            {
                _posts = value;
                OnPropertyChanged("Publications");
            }
        }
        private ObservableCollection<Publication> _posts = new ObservableCollection<Publication>();
        public MyCommand GetAllPosts
        {
            get => new MyCommand(async(obj) =>
            {
                NoticeMessage = "Загружаем публикации...";
                await Task.Run(() =>
                {
                    var pack = new GetAllPublications();
                    var response =  SendRequest(pack).Result;
                    if (response.Status == ResponseStatus.Ok)
                    {
                        var jArrayPostsId = response.ResponseData as JArray;
                        var publicationsId = jArrayPostsId.ToObject<int[]>();
                        GetPublicationsFromServer(publicationsId);
                    }
                });
                NoticeMessage = "";
            });
        }
        private async Task<ResponsePackage> SendRequest(BaseRequestPackage package)
        {
            var sender = new RequestSendler(new ConnectionSettings("127.0.0.1", 80));
            var response = await sender.SendRequest(package);
            return response;
        }
        private async Task GetPublicationsFromServer(int[] postsId)
        {
            var sender = new RequestSendler(new ConnectionSettings("127.0.0.1", 80));
            var getPostRequest = new GetPublicationsRange(postsId);
            var serverResponse = await sender.SendRequest(getPostRequest);
            if(serverResponse.Status == ResponseStatus.Ok)
            {
                var jGetPosts = serverResponse.ResponseData as JArray;
                var getPublications = jGetPosts.ToObject<ObservableCollection<Publication>>();
                Publications = getPublications;
                sender.Reset();
            }
        }
    }
}