namespace Communications.Business.Models.SubDocuments
{
    using AlbedoTeam.Communications.Contracts.Common;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements]
    public class DestinationAddress
    {
        [BsonRepresentation(BsonType.String)]
        public DestinationType DestinationType { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
    }
}