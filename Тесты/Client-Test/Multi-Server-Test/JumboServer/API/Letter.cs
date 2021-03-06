﻿using JumboServer.Packages;
using Newtonsoft.Json;
using System;

namespace JumboServer.API
{
    public class Letter : RequestObject
    {
        public string LetterType { get; }
        public string Title { get; }
        public int Id { get; }
        public string[] SourcesTokens { get; set; }
        public string Description { get; }
        public string SenderLogin { get; }
        public int SenderId { get; }
        public DateTime DateCreate { get; }

        [JsonConstructor]
        public Letter(string title, string description, string senderLogin, string letterType, DateTime dateCreate, int id, int senderId, string[] sourcesTokens)
        {
            Title = title;
            Description = description;
            SenderLogin = senderLogin;
            LetterType = letterType;
            if (dateCreate == null)
                DateCreate = DateTime.Now;
            else DateCreate = dateCreate;
            Id = id;
            SenderId = senderId;
            SourcesTokens = sourcesTokens;
        }
        public override string ToString()
        {
            return $"TYPE: {LetterType}\n" +
                   $"TITLE: {Title}\n" +
                   $"FROM: " + SenderLogin;
        }
    }
}
