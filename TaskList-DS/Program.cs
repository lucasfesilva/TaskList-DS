using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using TaskList_DS.Application.Interfaces;
using TaskList_DS.Application.Services;
using TaskList_DS.Domain.Interfaces;
using TaskList_DS.Infrastructure.Data;
using TaskList_DS.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITaskRepository, TaskRepostiory>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
