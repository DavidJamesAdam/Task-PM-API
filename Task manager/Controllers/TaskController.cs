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

    // PUT: api/Task/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTasks(Guid id, UpdateTaskDto dto)
    {
      await _taskInterface.UpdateTaskAsync(id, dto);

      return NoContent();
    }

    // POST: api/Task
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Tasks>> PostTasks(CreateTaskDto dto)
    {
      var response = await _taskInterface.CreateTaskAsync(dto);

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
