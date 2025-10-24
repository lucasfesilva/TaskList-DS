using TaskList_DS.Domain.Entities;

namespace TaskList_DS.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskEntity>> GetAllAsync();
        Task<TaskEntity> GetByIdAsync(int id);
        Task AddAsync(TaskEntity task);
        Task DeleteAsync(TaskEntity task);
        Task UpdateAsync(TaskEntity task);
    }
}
