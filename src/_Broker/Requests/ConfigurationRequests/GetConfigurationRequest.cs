namespace Communications.Requests
{
    public interface GetConfigurationRequest
    {
        string Id { get; set; }
        bool ShowDeleted { get; set; }
    }
}