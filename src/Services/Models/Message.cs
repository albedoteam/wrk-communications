using System.Collections.Generic;
using AlbedoTeam.Communications.Contracts.Common;
using Communications.Business.Models.SubDocuments;

namespace Communications.Business.Services.Models
{
    public class Message
    {
        public Provider Provider { get; set; }
        public MessageType MessageType { get; set; }
        public FromAddress From { get; set; }
        public string Subject { get; set; }
        public Dictionary<string, string> Destinations { get; set; }
        public string Content { get; set; }
    }
}