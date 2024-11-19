using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskRepository _repository = new();

        [HttpGet]
        public IActionResult GetTasks([FromQuery] string status, [FromQuery] string priority)
        {
            var tasks = _repository.GetTasks(status, priority);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var task = _repository.GetTask(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public IActionResult AddTask([FromBody] TaskItem newTask)
        {
            if (newTask == null)
            {
                return BadRequest("Task cannot be null");
            }

            if (string.IsNullOrWhiteSpace(newTask.Name))
            {
                Console.WriteLine("Task name is empty");
                return BadRequest("Task name is required");
            }

            var task = _repository.AddTask(newTask);

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskItem updatedTask)
        {
            if (!_repository.UpdateTask(id, updatedTask)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            if (!_repository.DeleteTask(id)) return NotFound();
            return NoContent();
        }
    }
}