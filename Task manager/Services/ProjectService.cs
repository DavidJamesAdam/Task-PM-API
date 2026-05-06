using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Task_manager.Models;
using Task_manager.DTOs;

public class ProjectService : IProjectService
{
  private readonly TaskContext _context;

  public ProjectService(TaskContext context)
  {
    _context = context;
  }

  public async Task<CreateProjectDto> CreateProjectAsync(CreateProjectDto dto)
  {
    Guid userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var project = new Projects
    {
      ProjectName = dto.Project_name
    };
    Console.WriteLine(userId);
    projects.Users.Id = userId;
    _context.Projects.Add(projects);
    var result = await _context.SaveChangesAsync();

    return new ProjectResponseDto
    {
      Project_name =
    };
  }

}