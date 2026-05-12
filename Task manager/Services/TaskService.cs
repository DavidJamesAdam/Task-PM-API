using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Task_manager.Models;
using Task_manager.DTOs;
using System.Security.Claims;
using Task_manager.Exceptions;

public class TaskService : ITaskInterface
{
  private readonly TaskContext _context;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public TaskService(TaskContext context, IHttpContextAccessor httpContextAccessor)
  {
    _context = context;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task<IEnumerable<TaskResponseDto>> GetTasksAsync()
  {
    var tasks = await _context.Tasks.Select(t => new TaskResponseDto
    {
      Id = t.Id,
      Task_name = t.TaskName,
      UserId = t.UserId,
      ProjectId = t.ProjectId
    }).ToListAsync();

    return tasks;
  }

  public async Task<TaskResponseDto?> GetTaskByIdAsync(Guid id)
  {
    var Task = await _context.Tasks
    .Where(t => t.Id == id)
    .Select(t => new TaskResponseDto
    {
      Id = t.Id,
      Task_name = t.TaskName,
      UserId = t.UserId,
      ProjectId = t.ProjectId
    }).FirstOrDefaultAsync() ?? throw new NotFoundException("Task not found");

    return Task;
  }

  public async Task<CreateTaskResultDto> CreateTaskAsync(CreateTaskDto dto)
  {
    string? userString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (userString == null)
    {
      return new CreateTaskResultDto
      {
        Succeeded = false,
        Errors = { { "message", "oopies" } }
      };
    }

    bool TaskExists = await _context.Tasks
        .AnyAsync(t => t.TaskName == dto.Task_name && t.Deleted_at == null);

    if (TaskExists)
    {
      throw new ConflictException("A Task with this name already exists");
    }

    Guid userGuid = Guid.Parse(userString);

    var Task = new Tasks
    {
      TaskName = dto.Task_name,
      UserId = userGuid
    };


    _context.Tasks.Add(Task);
    var result = await _context.SaveChangesAsync();

    return new CreateTaskResultDto
    {
      Succeeded = true,
      Task = new TaskResponseDto
      {
        Id = Task.Id,
        Task_name = Task.TaskName,
        UserId = Task.UserId
      }
    };
  }

  public async Task<bool> UpdateTaskAsync(Guid id, UpdateTaskDto dto)
  {
    var Task = await _context.Tasks.Where(t => t.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Task not found");

    bool TaskExists = await _context.Tasks
        .AnyAsync(t => t.TaskName == dto.Task_name && t.Deleted_at == null);

    if (TaskExists)
    {
      throw new ConflictException("A Task with this name already exists");
    }

    Task.TaskName = dto.Task_name;

    await _context.SaveChangesAsync();

    return true;
  }

  public async Task<bool> DeleteTaskAsync(Guid id)
  {
    var Task = await _context.Tasks.FindAsync(id) ?? throw new NotFoundException("Task not found");

    Task.Deleted_at = DateTime.UtcNow;

    await _context.SaveChangesAsync();

    return true;
  }
}