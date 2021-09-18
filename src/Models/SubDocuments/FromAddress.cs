namespace Communications.Business.Models.SubDocuments
{
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements]
    public class FromAddress
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}