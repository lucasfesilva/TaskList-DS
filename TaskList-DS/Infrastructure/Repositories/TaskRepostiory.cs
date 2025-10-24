using Microsoft.EntityFrameworkCore;
using TaskList_DS.Domain.Entities;
using TaskList_DS.Domain.Interfaces;
using TaskList_DS.Infrastructure.Data;

namespace TaskList_DS.Infrastructure.Repositories
{
    public class TaskRepostiory : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepostiory(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TaskEntity task)
        {
            await _context.taskEntities.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskEntity task)
        {
            _context.taskEntities.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            return await _context.taskEntities.ToListAsync();
        }

        public async Task<TaskEntity> GetByIdAsync(int id)
        {
            return await _context.taskEntities.FindAsync(id);
        }

        public async Task UpdateAsync(TaskEntity task)
        {
            _context.taskEntities.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
