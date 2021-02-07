using AlbedoTeam.Communications.Contracts.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Communications.Business.Models.SubDocuments
{
    [BsonIgnoreExtraElements]
    public class ConfigurationContract
    {
        [BsonRepresentation(BsonType.String)]
        public MessageType MessageType { get; set; }

        public FromAddress From { get; set; }
        public int FreeQuota { get; set; }
        public decimal TaxPerMessage { get; set; }
    }
}