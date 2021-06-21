﻿using ExchangeSystem.v2.Entities;
using ExchangeSystem.v2.Packages;
using ExchangeSystem.v2.Packages.Default;
using ExchangeSystem.v2.Sendlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantClient.Commands;

namespace TenantClient.ViewModels
{
    public class PostsVm : BaseVM
    {
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
                var pack = new GetAllPublications();
                var response = await SendRequest(pack);
                if (response.Status == ResponseStatus.Ok)
                {
                    var jArrayPostsId = response.ResponseData as JArray;
                    var publicationsId = jArrayPostsId.ToObject<int[]>();
                    await GetPublicationsFromServer(publicationsId);
                }
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
            //for (int i = 0; i < postsId.Length; i++)
            //{
                var sender = new RequestSendler(new ConnectionSettings("127.0.0.1", 80));
                var getPostRequest = new GetPublicationsRange(postsId);
                var serverResponse = await sender.SendRequest(getPostRequest);
                var jGetPosts = serverResponse.ResponseData as JArray;
                var getPublications = jGetPosts.ToObject<ObservableCollection<Publication>>();
                Publications = getPublications;
                sender.Reset();
                await Task.Delay(50);
            //}
        }
    }
}
