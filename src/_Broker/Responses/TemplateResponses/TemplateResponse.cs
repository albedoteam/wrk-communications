using System;

namespace Communications.Responses
{
    public interface TemplateResponse
    {
        string Id { get; set; }

        public string AccountId { get; set; }

        public string MessageType { get; set; }

        public string ContentType { get; set; }

        public string ContentPattern { get; set; }

        bool Enabled { get; set; }

        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}