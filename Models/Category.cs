using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CorePlusMongoDBApi.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string category_name { get; set; }
    }
}
