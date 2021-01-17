using System.Collections.Generic;

namespace Communications.Requests
{
    public interface UpdateConfigurationRequest
    {
        string Id { get; set; }
        string Name { get; set; }
        string Provider { get; set; }
        List<ConfigurationContractRequest> Contracts { get; set; }
        bool Enabled { get; set; }
    }
}