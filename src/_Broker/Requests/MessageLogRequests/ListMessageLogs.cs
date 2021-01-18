namespace Communications.Requests
{
    public interface ListMessageLogs
    {
        int Page { get; set; }
        int PageSize { get; set; }
        bool ShowDeleted { get; set; }
    }
}