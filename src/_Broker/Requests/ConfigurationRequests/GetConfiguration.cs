namespace Communications.Requests
{
    public interface GetConfiguration
    {
        string Id { get; set; }
        bool ShowDeleted { get; set; }
    }
}