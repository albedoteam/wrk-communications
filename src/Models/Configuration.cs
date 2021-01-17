using System.Collections.Generic;
using AlbedoTeam.Communications.Business.Models.Enums;
using AlbedoTeam.Communications.Business.Models.SubDocuments;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using AlbedoTeam.Sdk.DataLayerAccess.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AlbedoTeam.Communications.Business.Models
{
    [BsonCollection("Configurations")]
    public class Configuration: Document
    {
        public string AccountId { get; set; }
        
        [BsonRepresentation(BsonType.String)]
        public Provider Provider { get; set; }
        
        public List<ConfigurationContract> Contracts { get; set; }
    }
}