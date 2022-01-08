using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CorePlusMongoDBApi.Models
{
    public class Products
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required(ErrorMessage = "product name is required")]
        public string product_name { get; set; }

        [Required(ErrorMessage = "description is required")]
        public string description { get; set; }

        [Required(ErrorMessage = "price is required")]
        public string price { get; set; }
        [Required(ErrorMessage = "category is required")]
        public string category { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Categories { get; set; }

        [BsonIgnore]
        public List <Category> CategoryList { get; set; }

    }
}
