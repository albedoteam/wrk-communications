using Communications.Business.Models.Enums;

namespace Communications.Business.Models.SubDocuments
{
    public class ConfigurationContract
    {
        public MessageType MessageType { get; set; }
        public int FreeQuota { get; set; }
        public decimal TaxPerMessage { get; set; }
    }
}