using System.Collections.Generic;
using Communications.Abstractions;

namespace Communications.Commands
{
    public interface SendMessage
    {
        string AccountId { get; set; }
        string TemplateId { get; set; }
        string From { get; set; }
        string Subject { get; set; }
        List<IDestinationAddress> Destinations { get; set; }
        List<IMessageParameter> Parameters { get; set; }
    }
}