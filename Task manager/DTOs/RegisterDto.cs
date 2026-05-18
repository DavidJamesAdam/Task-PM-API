using System.ComponentModel.DataAnnotations;

namespace Task_manager.DTOs;

public class RegisterDto
{
  [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Invalid email format")]
  [StringLength(256)]
  public required string Email { get; set; }
  [Required(ErrorMessage = "Password is required")]
  [StringLength(16, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 16 characters")]
  public required string Password { get; set; }
  [Required(ErrorMessage = "First name is required")]
  [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
  public required string Fname { get; set; }
  [Required(ErrorMessage = "Last name is required")]
  [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
  public required string Lname { get; set; }
}