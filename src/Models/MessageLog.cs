namespace Communications.Business.Models
{
    using System;
    using System.Collections.Generic;
    using AlbedoTeam.Communications.Contracts.Common;
    using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
    using AlbedoTeam.Sdk.DataLayerAccess.Attributes;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using SubDocuments;

    [Collection("MessageLogs")]
    public class MessageLog : DocumentWithAccount
    {
        [BsonRepresentation(BsonType.String)]
        public Provider Provider { get; set; }

        [BsonRepresentation(BsonType.String)]
        public MessageType MessageType { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        [BsonRepresentation(BsonType.String)]
        public ContentType ContentType { get; set; }

        public string Content { get; set; }

        public List<DestinationAddress> Destinations { get; set; }
        public DateTime? SentAt { get; set; }
        public string Status { get; set; }
        public string DetailMessage { get; set; }
    }
}