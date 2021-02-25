using System.Collections.Generic;
using AlbedoTeam.Communications.Contracts.Common;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using AlbedoTeam.Sdk.DataLayerAccess.Attributes;
using Communications.Business.Models.SubDocuments;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Communications.Business.Models
{
    [BsonCollection("Templates")]
    public class Template : DocumentWithAccount
    {
        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public MessageType MessageType { get; set; }

        [BsonRepresentation(BsonType.String)]
        public ContentType ContentType { get; set; }

        public string ContentPattern { get; set; }

        public List<ContentParameter> ContentParameters { get; set; }

        public bool Enabled { get; set; }
    }
}