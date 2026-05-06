using Task_manager.DTOs;
using Task_manager.Models;

public interface IUserService
{
  Task<IEnumerable<UserResponseDto>> GetUsersAsync();
  Task<UserResponseDto?> GetUserByIdAsync(Guid id);
  Task<bool> UpdateUserAsync(Guid id, UpdateUserDto dto);
  Task<CreateUserResultDto> CreateUserAsync(RegisterDto dto);
  Task<bool> DeleteUserAsync(Guid id);
}