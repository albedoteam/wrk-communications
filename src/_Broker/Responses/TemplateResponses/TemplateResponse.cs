using System;

namespace Communications.Responses
{
    public interface TemplateResponse
    {
        string Id { get; set; }
        string AccountId { get; set; }
        string MessageType { get; set; }
        string ContentType { get; set; }
        string ContentPattern { get; set; }
        bool Enabled { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}