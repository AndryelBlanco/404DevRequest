using _404DevRequest.Models;
using _404DevRequest.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace _404DevRequest.Repositories
{
    public class MongoRepository        
    {
        private readonly IMongoCollection<TodoItem> _mongoRepository;

        public MongoRepository(IOptions<DatabaseSettings> dabaseSettings)
        {
            var mongoClient = new MongoClient(dabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dabaseSettings.Value.DatabaseName);

            _mongoRepository = mongoDatabase.GetCollection<TodoItem>("TodoList");
        }

        public async Task<List<TodoItem>> GetAsync() =>
            await _mongoRepository.Find(_ => true).ToListAsync();

        public async Task<TodoItem?> GetAsync(string id) =>
            await _mongoRepository.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TodoItem newTodoItem)
        {
            await _mongoRepository.InsertOneAsync(newTodoItem);
        }

        public async Task UpdateAsync(string id, TodoItem updatedTodoItem) =>
            await _mongoRepository.ReplaceOneAsync(x => x.Id == id, updatedTodoItem);

        public async Task RemoveAsync(string id) => await _mongoRepository.DeleteOneAsync(x => x.Id == id);
    }
}
