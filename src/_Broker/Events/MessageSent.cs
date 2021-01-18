using System;
using System.Collections.Generic;
using Communications.Abstractions;

namespace Communications.Events
{
    public interface MessageSent
    {
        string Id { get; set; }
        string AccountId { get; set; }
        string Provider { get; set; }
        string MessageType { get; set; }
        string From { get; set; }
        string Subject { get; set; }
        string ContentType { get; set; }
        string Content { get; set; }
        List<IDestinationAddress> Destinations { get; set; }
        DateTime? SentAt { get; set; }
        string Status { get; set; }
        string DetailMessage { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}