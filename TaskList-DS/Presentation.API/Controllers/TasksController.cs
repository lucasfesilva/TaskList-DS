using Microsoft.AspNetCore.Mvc;
using TaskList_DS.Application.Interfaces;
using TaskList_DS.Domain.Entities;

namespace TaskList_DS.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _service.GetByIdAsync(id);
            return task == null ? NotFound("Tarefa não localizada") : Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(TaskEntity task)
        {
            if (task.DoneAt < task.CreatedAt)
            {
                return BadRequest("A data de conclusão não pode ser menor que a data de criação!");
            }
            await _service.AddAsync(task);
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null)
                return NotFound("Tarefa não localizada");
            await _service.DeleteAsync(task);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] TaskEntity updatedEntity)
        {
            if (updatedEntity == null || id != updatedEntity.Id)
                return BadRequest("Erro ao atualizar Tarefa");

            var existingTask = await _service.GetByIdAsync(id);
            if (existingTask == null)
                return NotFound("Tarefa não localizada");

            existingTask.TaskTitle = updatedEntity.TaskTitle;
            existingTask.TaskDescription = updatedEntity.TaskDescription;
            existingTask.Status = updatedEntity.Status;
            existingTask.DoneAt = updatedEntity.DoneAt;

            await _service.UpdateAsync(existingTask);
            return Ok(existingTask);
        }

    }
}
