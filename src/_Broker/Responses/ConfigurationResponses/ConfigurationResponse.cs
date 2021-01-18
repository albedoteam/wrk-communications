using System;
using System.Collections.Generic;
using Communications.Abstractions;

namespace Communications.Responses
{
    public interface ConfigurationResponse
    {
        string Id { get; set; }
        string AccountId { get; set; }
        string Name { get; set; }
        public string Provider { get; set; }
        public List<IConfigurationContract> Contracts { get; set; }
        public bool Enabled { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}