using System;
using System.Collections.Generic;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using AlbedoTeam.Sdk.DataLayerAccess.Attributes;
using Communications.Business.Models.Enums;
using Communications.Business.Models.SubDocuments;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Communications.Business.Models
{
    [BsonCollection("MessageLogs")]
    public class MessageLog: Document
    {
        public string AccountId { get; set; }
        
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
        public DateTime SentAt { get; set; }
    }
}