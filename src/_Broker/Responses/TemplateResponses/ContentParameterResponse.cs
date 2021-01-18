namespace Communications.Responses
{
    public interface ContentParameterResponse
    {
        string Key { get; set; }
        string Value { get; set; }
        bool Required { get; set; }
    }
}