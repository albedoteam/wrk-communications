namespace Communications.Requests
{
    public interface GetTemplate
    {
        string Id { get; set; }
        bool ShowDeleted { get; set; }
    }
}