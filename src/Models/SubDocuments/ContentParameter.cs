namespace Communications.Business.Models.SubDocuments
{
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements]
    public class ContentParameter
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}