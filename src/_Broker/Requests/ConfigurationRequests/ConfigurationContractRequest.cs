namespace Communications.Requests
{
    public interface ConfigurationContractRequest
    {
        string MessageType { get; set; }
        int FreeQuota { get; set; }
        decimal TaxPerMessage { get; set; }
    }
}