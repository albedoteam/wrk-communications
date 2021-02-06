using AlbedoTeam.Communications.Contracts.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Communications.Business.Models.SubDocuments
{
    public class DestinationAddress
    {
        [BsonRepresentation(BsonType.String)]
        public DestinationType DestinationType { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
    }
}