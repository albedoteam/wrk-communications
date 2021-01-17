namespace Communications.Requests
{
    public interface CreateTemplateRequest
    {
        string AccountId { get; set; }

        string Name { get; set; }

        string MessageType { get; set; }
        
        string ContentType { get; set; }
        
        string ContentPattern { get; set; }
    }
}