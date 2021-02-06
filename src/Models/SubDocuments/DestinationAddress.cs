using AlbedoTeam.Communications.Contracts.Common;

namespace Communications.Business.Models.SubDocuments
{
    public class DestinationAddress
    {
        public DestinationType DestinationType { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}