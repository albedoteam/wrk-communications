namespace Communications.Requests
{
    public interface ListConfigurations
    {
        int Page { get; set; }
        int PageSize { get; set; }
        bool ShowDeleted { get; set; }
    }
}