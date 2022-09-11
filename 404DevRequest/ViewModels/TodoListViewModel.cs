using _404DevRequest.Models;

namespace _404DevRequest.ViewModels
{
    public class TodoListViewModel
    {
        public IEnumerable<TodoItem> TodoListItems { get; set; }
        public int CurrentPriority { get; set; }

    }
}
