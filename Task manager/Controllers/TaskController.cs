using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_manager.Models;

namespace Task_manager.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class TaskController : ControllerBase
  {
    private readonly ITaskInterface _taskInterface;

    public TaskController(ITaskInterface taskInterface)
    {
      _taskInterface = taskInterface;
    }

    // GET: api/Task
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetTasks()
    {
      var tasks = await _taskInterface.GetTasksAsync();
      return Ok(tasks);
    }

    // GET: api/Task/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Tasks>> GetTasks(Guid id)
    {
      var tasks = await _taskInterface.GetTaskByIdAsync(id);

      return Ok(tasks);
    }

    // GET: api/Task/me
    [HttpGet("me")]
    public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetCreatedTasksForCurrentUser()
    {
      var createdTasks = await _taskInterface.GetCreatedTasksForCurrentUserAsync();

      return Ok(createdTasks);
    }

    // PATCH: api/Task/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPatch("{id}")]
    public async Task<IActionResult> PutTasks(Guid id, UpdateTaskDto dto)
    {
      await _taskInterface.UpdateTaskAsync(id, dto);

      return NoContent();
    }

    // POST: api/Task
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("{project_id}")]
    public async Task<ActionResult<Tasks>> PostTasks(Guid project_id, CreateTaskDto dto)
    {
      var response = await _taskInterface.CreateTaskAsync(project_id, dto);

      return CreatedAtAction("GetTasks", new { id = response.Task?.Id }, response);
    }

    // DELETE: api/Task/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTasks(Guid id)
    {
      await _taskInterface.DeleteTaskAsync(id);

      return NoContent();
    }
  }
}
