using ExchangeSystem.v2.Entities;
using ExchangeSystem.v2.Packages.Default;
using ExchangeSystem.v2.Sendlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TenantClient.Commands;
using TenantClient.Local;

namespace TenantClient.ViewModels
{
    internal class CreatePostsVm : BaseVM
    {
        public Publication EditPost
        {
            get => _editPost;
            set
            {
                _editPost = value;
                OnPropertyChanged("EditPost");
            }
        }
        private Publication _editPost = new Publication();
        private string _userToken = "";
        public ComboBoxItem SelectedPostType { get; set; }
        public MyCommand SendPublication
        {
            get => new MyCommand(async (obj) =>
            {
                if(SelectedPostType == null)
                {
                    NoticeMessage = "Выберите тип публикации";
                    return;
                }
                if (!UserTokenWasExist())
                {
                    NoticeMessage = "Не удалось получить ваш токен авторизации. Войдите в систему";
                    return;
                }
                if (PublicationIsValid())
                {
                    GetTokenFromStorage();
                    EditPost.Type = SelectPostTypeByInputItem();
                    var response = await SendPublicationOnServer();
                    ProcessResponse(response);
                }
            });
        }
        private bool UserTokenWasExist()
        {
            bool tokenExist = ClientTokenStorage.TokenWasExist();
            return tokenExist;
        }
        private void GetTokenFromStorage()
        {
            ClientTokenStorage.TryGet(out _userToken);
        }
        private bool PublicationIsValid()
        {
            bool isValid = false;
            if (!string.IsNullOrWhiteSpace(EditPost.Title) ||
                !string.IsNullOrWhiteSpace(EditPost.Text))
            {
                isValid = true;
            }
            return isValid;
        }
        private async Task<ResponsePackage> SendPublicationOnServer()
        {
            ResponsePackage response;
            var pack = new AddPublication(EditPost);
            pack.SetToken(_userToken);
            var sendler = new RequestSendler(new ConnectionSettings());
            response = await sendler.SendRequest(pack);
            return response;
        }
        private void ProcessResponse(ResponsePackage response)
        {
            if (response.Status == ResponseStatus.Ok)
            {
                NoticeMessage = "Публикация успешно размещена";
                ResetEditPost();
            }
            else
                NoticeMessage = response.ErrorMessage;
        }
        private void ResetEditPost()
        {
            EditPost = new Publication();
        }
        private PostType SelectPostTypeByInputItem()
        {
            var content = SelectedPostType.Content as string;
            PostType.TryParse(content, out PostType type);
            return type;
        }
    }
}
