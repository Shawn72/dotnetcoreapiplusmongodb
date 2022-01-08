namespace CorePlusMongoDBApi.Interfaces
{
    public interface IEcommerceDBSettings
    {

        string ProductsCollectionName { get; set; }
        string CategoryCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}
