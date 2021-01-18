namespace Communications.Requests
{
    public interface UpdateTemplateRequest
    {
        string Id { get; set; }
        
        string AccountId { get; set; }

        string Name { get; set; }

        string MessageType { get; set; }
        
        string ContentType { get; set; }
        
        string ContentPattern { get; set; }
        
        bool Enabled { get; set; }
    }
}