﻿using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages.Default;
using MVVM_Pattern_Test.ClientEntities;
using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.MyApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVVM_Pattern_Test.ViewModels.Admin
{
    public class AdminNewsVM : BaseVM
    {
        #region Constructor
        public AdminNewsVM()
        {
            NewPost = new Publication();
            NewPost.SenderId = 0;
        }
        #endregion

        #region Props
        public override string Notice { get { return _infoMessage; } protected set { _infoMessage = value; OnPropertyChanged(); } }
        public Publication NewPost
        {
            get { return _post; }
            set { _post = value; OnPropertyChanged(); }
        }
        private Publication _post;
        public List<Source> Sources
        {
            get { return _sources; }
            set { _sources = value; OnPropertyChanged(); }
        }
        private List<Source> _sources = new List<Source>();
        #endregion

        #region Commands
        public MyCommand SendNews
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    string token = new FilesHelper().OpenTokenLocal();
                    if (!string.IsNullOrWhiteSpace(token) && CheckValidation())
                    {
                        var manager = new ExSysManager();
                        NewPost.Sources = Sources.ToArray();
                        var response = await manager.AddPublication(NewPost, token);
                        if (response.Status == ResponseStatus.Ok)
                        {
                            Notice = "Публикация успешно размещена";
                            ToBaseForm();
                        }
                        else
                            Notice = response.ErrorMessage;
                    }
                });
            }
        }
        public MyCommand AttachFile
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    string base64Data = string.Empty;
                    string extensionFile = string.Empty;
                    new FilesHelper().OpenFile(out base64Data, out extensionFile);
                    if (!string.IsNullOrWhiteSpace(base64Data) && !string.IsNullOrWhiteSpace(extensionFile))
                    {
                        var source = new Source()
                        {
                            Base64Data = base64Data,
                            Extension = extensionFile,
                            DateCreate = DateTime.Now
                        };
                        Sources.Add(source);
                        Notice = "Файл успешно добавлен";
                    }
                });
            }
        }
        #endregion

        #region OtherMetods
        public void ToBaseForm()
        {
            NewPost = new Publication();
        }
        public bool CheckValidation()
        {
            if (string.IsNullOrWhiteSpace(NewPost.Text) || string.IsNullOrWhiteSpace(NewPost.Title))
                return false;
            return true;
        }
        #endregion
    }
}
