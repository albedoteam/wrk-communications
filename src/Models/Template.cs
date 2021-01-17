using AlbedoTeam.Communications.Business.Models.Enums;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using AlbedoTeam.Sdk.DataLayerAccess.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AlbedoTeam.Communications.Business.Models
{
    [BsonCollection("Templates")]
    public class Template: Document
    {
        public string AccountId { get; set; }
        
        [BsonRepresentation(BsonType.String)]
        public MessageType MessageType { get; set; }
        
        [BsonRepresentation(BsonType.String)]
        public ContentType ContentType { get; set; }
        
        public string ContentPattern { get; set; }
    }
}