using System.Collections.Generic;
using System.Threading.Tasks;
using AlbedoTeam.Communications.Contracts.Common;

namespace Communications.Business.Services.Abstractions
{
    public interface ICommunicationService
    {
        Task<SendResult> Send(Message message);
    }

    public class SendResult
    {
        public bool Success { get; set; }
        public string DetailMessage { get; set; }
    }

    public class Message
    {
        public Provider Provider { get; set; }
        public MessageType MessageType { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public Dictionary<string, string> Destinations { get; set; }
        public string Content { get; set; }
    }
}