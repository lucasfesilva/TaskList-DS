using TaskList_DS.Application.Interfaces;
using TaskList_DS.Domain.Entities;
using TaskList_DS.Domain.Interfaces;

namespace TaskList_DS.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(TaskEntity task)
        {
            await _repository.AddAsync(task);
        }

        public async Task DeleteAsync(TaskEntity task)
        {
            await _repository.DeleteAsync(task);
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TaskEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(TaskEntity task)
        {
            await _repository.UpdateAsync(task);
        }
    }
}
