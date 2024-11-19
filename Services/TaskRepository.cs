using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public class TaskRepository
    {
        private static readonly List<TaskItem> _tasks = [];
        private static int _nextId = 1;

        public IEnumerable<TaskItem> GetTasks(string? status = null, string? priority = null)
        {
            var query = _tasks.AsQueryable();
            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(priority))
                query = query.Where(t => t.Priority.Equals(priority, StringComparison.OrdinalIgnoreCase));
            return [.. query];
        }

        public TaskItem? GetTask(int id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

        public TaskItem AddTask(TaskItem task)
        {
        
            task.Id = _nextId++;
            task.Status = string.IsNullOrWhiteSpace(task.Status) ? "Incomplete" : task.Status;
            task.Priority = string.IsNullOrWhiteSpace(task.Priority) ? "Medium" : task.Priority;
            _tasks.Add(task);
            return task;
        }

        public bool UpdateTask(int id, TaskItem updatedTask)
        {
            var task = GetTask(id);
            if (task == null) return false;

            task.Name = updatedTask.Name;
            task.Status = updatedTask.Status;
            task.Priority = updatedTask.Priority;
            task.UserId = updatedTask.UserId;
            return true;
        }

        public bool DeleteTask(int id) => _tasks.RemoveAll(t => t.Id == id) > 0;
    }
}