using System.Collections.Generic;
using Communications.Abstractions;

namespace Communications.Requests
{
    public interface UpdateConfiguration
    {
        string Id { get; set; }
        string Name { get; set; }
        string Provider { get; set; }
        List<IConfigurationContract> Contracts { get; set; }
        bool Enabled { get; set; }
    }
}