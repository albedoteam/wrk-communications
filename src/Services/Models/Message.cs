namespace Communications.Business.Services.Models
{
    using System.Collections.Generic;
    using AlbedoTeam.Communications.Contracts.Common;
    using Business.Models.SubDocuments;

    public class Message
    {
        public string AccountId { get; set; }
        public Provider Provider { get; set; }
        public MessageType MessageType { get; set; }
        public FromAddress From { get; set; }
        public string Subject { get; set; }
        public Dictionary<string, string> Destinations { get; set; }
        public string Content { get; set; }
    }
}