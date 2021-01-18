namespace Communications.Abstractions
{
    public interface IMessageParameter
    {
        string Key { get; set; }
        string Value { get; set; }
    }
}