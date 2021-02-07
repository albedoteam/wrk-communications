using MongoDB.Bson.Serialization.Attributes;

namespace Communications.Business.Models.SubDocuments
{
    [BsonIgnoreExtraElements]
    public class ContentParameter
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}