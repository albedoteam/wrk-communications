namespace Communications.Business.Models
{
    using System.Collections.Generic;
    using AlbedoTeam.Communications.Contracts.Common;
    using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
    using AlbedoTeam.Sdk.DataLayerAccess.Attributes;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using SubDocuments;

    [Collection("Configurations")]
    public class Configuration : DocumentWithAccount
    {
        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Provider Provider { get; set; }

        public List<ConfigurationContract> Contracts { get; set; }

        public bool Enabled { get; set; }
    }
}