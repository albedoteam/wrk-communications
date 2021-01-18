namespace Communications.Abstractions
{
    public interface IConfigurationContract
    {
        string MessageType { get; set; }
        int FreeQuota { get; set; }
        decimal TaxPerMessage { get; set; }
    }
}