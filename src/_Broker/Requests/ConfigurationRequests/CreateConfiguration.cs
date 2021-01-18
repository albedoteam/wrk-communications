using System.Collections.Generic;
using Communications.Absctractions;

namespace Communications.Requests
{
    public interface CreateConfiguration
    {
        string AccountId { get; set; }
        string Name { get; set; }
        string Provider { get; set; }
        List<IConfigurationContract> Contracts { get; set; }
        bool Enabled { get; set; }
    }
}