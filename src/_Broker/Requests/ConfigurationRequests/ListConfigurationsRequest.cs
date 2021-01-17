namespace Communications.Requests
{
    public interface ListConfigurationsRequest
    {
        int Page { get; set; }
        int PageSize { get; set; }
        bool ShowDeleted { get; set; }
    }
}