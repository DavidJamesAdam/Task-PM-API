using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Task_manager.Models;
using Task_manager.DTOs;
using System.Security.Claims;
using Task_manager.Exceptions;
using Microsoft.IdentityModel.Tokens;

public class ProjectService : IProjectService
{
  private readonly TaskContext _context;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public ProjectService(TaskContext context, IHttpContextAccessor httpContextAccessor)
  {
    _context = context;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task<IEnumerable<ProjectResponseDto>> GetProjectsAsync()
  {
    var projects = await _context.Projects.Select(p => new ProjectResponseDto
    {
      Id = p.Id,
      Project_name = p.ProjectName,
      UserId = p.UserId,
    }).ToListAsync();

    return projects;
  }

  public async Task<ProjectResponseDto?> GetProjectByIdAsync(Guid id)
  {
    var project = await _context.Projects
    .Where(p => p.Id == id)
    .Select(p => new ProjectResponseDto
    {
      Id = p.Id,
      Project_name = p.ProjectName,
      UserId = p.UserId,
    }).FirstOrDefaultAsync() ?? throw new NotFoundException("Project not found");

    return project;
  }

  public async Task<IEnumerable<TaskResponseDto>> GetTasksByProjectIdAsync(Guid project_id)
  {
    var projectExists = await _context.Projects
    .AnyAsync(p => p.Id == project_id && p.Deleted_at == null);

    if (!projectExists)
    {
      throw new NotFoundException("Project not found");
    }

    var tasks = await _context.Tasks.Where(p => p.ProjectId == project_id).Select(t => new TaskResponseDto
    {
      Id = t.Id,
      Task_name = t.TaskName,
      AssignedTo = t.AssignedTo,
      Status = t.Status,
      UserId = t.UserId,
      ProjectId = t.ProjectId
    }).ToListAsync();

    return tasks;
  }

  public async Task<IEnumerable<ProjectResponseDto?>> GetProjectsForCurrentUserAsync()
  {
    string? userString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

    Guid userGuid = Guid.Parse(userString!);

    var userProjects = await _context.Projects.Where(p => p.UserId == userGuid).Select(p => new ProjectResponseDto
    {
      Id = p.Id,
      Project_name = p.ProjectName,
      UserId = p.UserId,
    }).ToListAsync();

    return userProjects;
  }

  public async Task<CreateProjectResultDto> CreateProjectAsync(CreateProjectDto dto)
  {
    string? userString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (userString == null)
    {
      return new CreateProjectResultDto
      {
        Succeeded = false,
        Errors = { { "message", "oopies" } }
      };
    }

    bool projectExists = await _context.Projects
        .AnyAsync(p => p.ProjectName == dto.Project_name && p.Deleted_at == null);

    if (projectExists)
    {
      throw new ConflictException("A project with this name already exists");
    }

    Guid userGuid = Guid.Parse(userString);

    var project = new Projects
    {
      ProjectName = dto.Project_name,
      UserId = userGuid
    };


    _context.Projects.Add(project);
    var result = await _context.SaveChangesAsync();

    return new CreateProjectResultDto
    {
      Succeeded = true,
      Project = new ProjectResponseDto
      {
        Id = project.Id,
        Project_name = project.ProjectName,
        UserId = project.UserId
      }
    };
  }

  public async Task<bool> UpdateProjectAsync(Guid id, UpdateProjectDto dto)
  {
    var project = await _context.Projects.Where(p => p.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("Project not found");

    bool projectExists = await _context.Projects
        .AnyAsync(p => p.ProjectName == dto.Project_name && p.Deleted_at == null);

    if (projectExists)
    {
      throw new ConflictException("A project with this name already exists");
    }

    project.ProjectName = dto.Project_name;
    project.Updated_at = DateTime.UtcNow;

    await _context.SaveChangesAsync();

    return true;
  }

  public async Task<bool> DeleteProjectAsync(Guid id)
  {
    var project = await _context.Projects.FindAsync(id) ?? throw new NotFoundException("Project not found");

    project.Deleted_at = DateTime.UtcNow;

    await _context.SaveChangesAsync();

    return true;
  }
}