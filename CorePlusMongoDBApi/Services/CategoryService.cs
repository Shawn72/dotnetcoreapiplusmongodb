using MongoDB.Driver;
using CorePlusMongoDBApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CorePlusMongoDBApi.Interfaces;
namespace CorePlusMongoDBApi.Services
{
    public class CategoryService
    {
        private readonly IMongoCollection<Category> _categories;
        public CategoryService(IEcommerceDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _categories = database.GetCollection<Category>(settings.CategoryCollectionName);
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await _categories.Find(s => true).ToListAsync();
        }
        public async Task<Category> GetByIdAsync(string id)
        {
            return await _categories.Find<Category>(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _categories.InsertOneAsync(category);
            return category;
        }
        public async Task UpdateAsync(string id, Category category)
        {
            await _categories.ReplaceOneAsync(c => c.Id == id, category);
        }
        public async Task DeleteAsync(string id)
        {
            await _categories.DeleteOneAsync(c => c.Id == id);
        }

    }
}
