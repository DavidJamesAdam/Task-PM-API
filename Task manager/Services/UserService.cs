using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Task_manager.Models;
using Task_manager.DTOs;
using Task_manager.Exceptions;

public class UserService : IUserService
{
  private readonly TaskContext _context;
  private readonly UserManager<Users> _userManager;

  public UserService(TaskContext context, UserManager<Users> userManager)
  {
    _context = context;
    _userManager = userManager;
  }

  public async Task<IEnumerable<UserResponseDto>> GetUsersAsync()
  {
    var users = await _context.Users
    .Select(u => new UserResponseDto
    {
      Id = u.Id,
      Email = u.Email!,
      Fname = u.Fname!,
      Lname = u.Lname!
    }).ToListAsync();

    return users;
  }

  public async Task<UserResponseDto?> GetUserByIdAsync(Guid id)
  {
    var user = await _context.Users
    .Where(u => u.Id == id)
    .Select(u => new UserResponseDto
    {
      Id = u.Id,
      Email = u.Email!,
      Fname = u.Fname!,
      Lname = u.Lname!
    }).FirstOrDefaultAsync() ?? throw new NotFoundException("User not found");;

    return user;
  }

  public async Task<bool> UpdateUserAsync(Guid id, UpdateUserDto dto)
  {
    var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("User not found");

    user.Fname = dto.Fname;
    user.Lname = dto.Lname;
    user.Updated_at = DateTime.UtcNow;

    await _context.SaveChangesAsync();

    return true;
  }

  public async Task<CreateUserResultDto> CreateUserAsync(RegisterDto dto)
  {
    var user = new Users
    {
      UserName = dto.Email,
      Email = dto.Email,
      Fname = dto.Fname,
      Lname = dto.Lname
    };

    var result = await _userManager.CreateAsync(user, dto.Password);

    if (!result.Succeeded)
    {
      var errors = result.Errors
        .GroupBy(e => MapIdentityErrorToField(e.Code))
        .ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToArray());

      return new CreateUserResultDto
      {
        Succeeded = false,
        Errors = errors
      };
    }

    return new CreateUserResultDto
    {
      Succeeded = true,
      User = new UserResponseDto
      {
        Id = user.Id,
        Email = user.Email!,
        Fname = user.Fname!,
        Lname = user.Lname!
      }
    };
  }

  public async Task<bool> DeleteUserAsync(Guid id) {
      var users = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync() ?? throw new NotFoundException("User not found");

      users.Deleted_at = DateTime.UtcNow;

      await _context.SaveChangesAsync();

      return true;
  }

  private static string MapIdentityErrorToField(string code)
  {
    if (code.StartsWith("Password", StringComparison.OrdinalIgnoreCase))
    {
      return "Password";
    }

    if (code.StartsWith("DuplicateEmail", StringComparison.OrdinalIgnoreCase))
    {
      return "Email";
    }

    return "General";
  }
}