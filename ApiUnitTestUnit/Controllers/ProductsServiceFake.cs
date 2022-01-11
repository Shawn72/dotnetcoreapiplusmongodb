using CorePlusMongoDBApi.Interfaces;
using CorePlusMongoDBApi.Models;
using CorePlusMongoDBApi.Services;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiUnitTestUnit.Controllers
{

    public class ProductsServiceFake
    {
        private readonly IMongoCollection<Products> _products;
        public ProductsServiceFake()
        {
            IEcommerceDBSettings settings= new EcommerceDBSettings();
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _products = database.GetCollection<Products>(settings.ProductsCollectionName);
        }

        public async Task<List<Products>> GetAllAsync()
        {
            return await _products.Find(s => true).ToListAsync();
        }
        public async Task<Products> GetByIdAsync(string id)
        {
            return await _products.Find<Products>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Products> CreateAsync(Products product)
        {
            await _products.InsertOneAsync(product);
            return product;
        }
        public async Task UpdateAsync(string id, Products product)
        {
            await _products.ReplaceOneAsync(s => s.Id == id, product);
        }
        public async Task DeleteAsync(string id)
        {
            await _products.DeleteOneAsync(s => s.Id == id);
        }
    }
}
