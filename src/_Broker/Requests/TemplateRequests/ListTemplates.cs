namespace Communications.Requests
{
    public interface ListTemplates
    {
        int Page { get; set; }
        int PageSize { get; set; }
        bool ShowDeleted { get; set; }
    }
}