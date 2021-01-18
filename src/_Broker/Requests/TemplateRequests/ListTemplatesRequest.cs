namespace Communications.Requests
{
    public interface ListTemplatesRequest
    {
        int Page { get; set; }
        int PageSize { get; set; }
        bool ShowDeleted { get; set; }
    }
}