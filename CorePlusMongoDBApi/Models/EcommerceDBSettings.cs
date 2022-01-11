using CorePlusMongoDBApi.Interfaces;

namespace CorePlusMongoDBApi.Models
{
    public class EcommerceDBSettings : IEcommerceDBSettings
    {
        public string ProductsCollectionName { get ; set ; }
        public string CategoryCollectionName { get; set ; }
        public string ConnectionString { get ; set; }
        public string DatabaseName { get; set ; }
    }
}
