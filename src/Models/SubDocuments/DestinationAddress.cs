using Communications.Business.Models.Enums;

namespace Communications.Business.Models.SubDocuments
{
    public class DestinationAddress
    {
        public DestinationType DestinationType { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}