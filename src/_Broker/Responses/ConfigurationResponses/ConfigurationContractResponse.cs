namespace Communications.Responses
{
    public interface ConfigurationContractResponse
    {
        string MessageType { get; set; }
        int FreeQuota { get; set; }
        decimal TaxPerMessage { get; set; }
    }
}