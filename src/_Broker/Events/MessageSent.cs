using System;
using System.Collections.Generic;
using Communications.Absctractions;

namespace Communications.Events
{
    public interface MessageSent
    {
        string Id { get; set; }

        public string AccountId { get; set; }

        public string Provider { get; set; }

        public string MessageType { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string ContentType { get; set; }

        public string Content { get; set; }

        public List<IDestinationAddress> Destinations { get; set; }

        public DateTime? SentAt { get; set; }
        public string Status { get; set; }
        public string DetailMessage { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime? UpdatedAt { get; set; }

        bool IsDeleted { get; set; }

        DateTime? DeletedAt { get; set; }
    }
}