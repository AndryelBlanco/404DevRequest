using _404DevRequest.Models;

namespace _404DevRequest.Repositories.Interfaces
{
    public interface IMongoRepository
    {
        List<TodoItem> Get();
        TodoItem Get(string id);
        TodoItem Create(TodoItem item);
        void Update(string id, TodoItem item);
        void Delete(string id);
    }
}
