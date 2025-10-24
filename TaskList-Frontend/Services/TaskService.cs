using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TaskList_Frontend.Models;

namespace TaskList_Frontend.Services
{
    public class TaskService
    {
        private readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5258/"),
        };


        public async Task<List<TaskEntity>> GetTasksAsync()
        {
            var tasks = await _httpClient.GetFromJsonAsync<List<TaskEntity>>("api/tasks");
            return tasks ?? new List<TaskEntity>();
        }

        public async Task AddTask(TaskEntity task)
        {
            await _httpClient.PostAsJsonAsync("api/tasks", task);
        }

        public async Task UpdateTask(TaskEntity task)
        {
            await _httpClient.PutAsJsonAsync($"api/tasks/{task.Id}", task);
        }

        public async Task DeleteTask(int id)
        {
            await _httpClient.DeleteAsync($"api/tasks/{id}");
        }
    }
}
