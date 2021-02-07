using MongoDB.Bson.Serialization.Attributes;

namespace Communications.Business.Models.SubDocuments
{
    [BsonIgnoreExtraElements]
    public class FromAddress
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}