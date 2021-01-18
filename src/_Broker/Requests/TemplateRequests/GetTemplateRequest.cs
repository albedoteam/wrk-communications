namespace Communications.Requests
{
    public interface GetTemplateRequest
    {
        string Id { get; set; }
        bool ShowDeleted { get; set; }
    }
}