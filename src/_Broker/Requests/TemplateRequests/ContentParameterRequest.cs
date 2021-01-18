namespace Communications.Requests
{
    public interface ContentParameterRequest
    {
        string Key { get; set; }
        string Value { get; set; }
        bool Required { get; set; }
    }
}