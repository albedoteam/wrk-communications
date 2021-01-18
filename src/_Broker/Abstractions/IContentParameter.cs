namespace Communications.Absctractions
{
    public interface IContentParameter
    {
        string Key { get; set; }
        string Value { get; set; }
        bool Required { get; set; }
    }
}