namespace Communications.Requests
{
    public interface ListMessageLogsRequest
    {
        int Page { get; set; }
        int PageSize { get; set; }
        bool ShowDeleted { get; set; }
    }
}