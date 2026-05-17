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
  private readonly ILogger<TaskService> _logger;

  public TaskService(TaskContext context, IHttpContextAccessor httpContextAccessor, ILogger<TaskService> logger)
  {
    _context = context;
    _httpContextAccessor = httpContextAccessor;
    _logger = logger;
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

  public async Task<CreateTaskResultDto> CreateTaskAsync(Guid project_id, CreateTaskDto dto)
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

    var projectExists = await _context.Projects.AnyAsync(p => p.Id == project_id && p.Deleted_at == null);
    if (!projectExists)
    {
      throw new NotFoundException("Project not found");
    }

    Guid userGuid = Guid.Parse(userString);

    var Task = new Tasks
    {
      TaskName = dto.Task_name,
      UserId = userGuid,
      ProjectId = project_id
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
        UserId = Task.UserId,
        ProjectId = Task.ProjectId
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
    Task.Updated_at = DateTime.UtcNow;

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