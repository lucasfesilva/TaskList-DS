using TaskList_DS.Domain.Entities;

namespace TaskList_DS.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskEntity>> GetAllAsync();
        Task<TaskEntity> GetByIdAsync(int id);
        Task AddAsync(TaskEntity task);
        Task DeleteAsync(TaskEntity task);
        Task UpdateAsync(TaskEntity task);
    }
}
