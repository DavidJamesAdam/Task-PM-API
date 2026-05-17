using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Task_manager.Models;

namespace Task_manager.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class ProjectController : ControllerBase
  {
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
      _projectService = projectService;
    }

    // GET: api/Project
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Projects>>> GetProjects()
    {
      var projects = await _projectService.GetProjectsAsync();

      return Ok(projects);
    }

    // GET: api/Project/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Projects>> GetProjectById(Guid id)
    {
      var projects = await _projectService.GetProjectByIdAsync(id);

      return Ok(projects);
    }

    // GET: api/Project/5/tasks
    // Get all tasks for a specific project
    [HttpGet("{project_id}/tasks")]
        public async Task<ActionResult<Tasks>> GetTasksByProjectId(Guid project_id)
    {
      var projects = await _projectService.GetTasksByProjectIdAsync(project_id);

      return Ok(projects);
    }

    // PUT: api/Project/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPatch("{id}")]
    public async Task<IActionResult> PutProjects(Guid id, UpdateProjectDto dto)
    {
      await _projectService.UpdateProjectAsync(id, dto);

      return NoContent();
    }

    // POST: api/Project
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<CreateProjectDto>> CreateProject(CreateProjectDto dto)
    {
      var result = await _projectService.CreateProjectAsync(dto);

      return CreatedAtAction("GetProjects", new { id = result.Project?.Id }, result);
    }

    // DELETE: api/Project/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProjects(Guid id)
    {
      await _projectService.DeleteProjectAsync(id);

      return NoContent();
    }
  }
}
