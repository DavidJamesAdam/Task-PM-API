using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Task_manager.Models;
using Task_manager.DTOs;

namespace Task_manager.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
    {
      var users = await _userService.GetUsersAsync();

      return Ok(users);
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> GetUserById(Guid id)
    {
      var user = await _userService.GetUserByIdAsync(id);

      if (user == null)
      {
        return NotFound();
      }

      return Ok(user);
    }

    // PUT: api/Users/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDto dto)
    {
      var updated = await _userService.UpdateUserAsync(id, dto);

      if (!updated)
      {
        return NotFound();
      }

      return NoContent();
    }

    // POST: api/Users
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserResponseDto>> CreateUser(RegisterDto dto)
    {
      var result = await _userService.CreateUserAsync(dto);

      if (!result.Succeeded)
      {
        foreach (var pair in result.Errors)
        {
          foreach (var message in pair.Value)
          {
            ModelState.AddModelError(pair.Key, message);
          }
        }

        return ValidationProblem(ModelState);
      }

      return CreatedAtAction("GetUsers", new { id = result.User!.Id }, result.User);
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {

      var user = await _userService.DeleteUserAsync(id);
      if (!user)
      {
        return NotFound();
      }

      return NoContent();
    }

    // private bool UsersExists(Guid id)
    // {
    //   return _context.Users.Any(e => e.Id == id);
    // }
  }
}
