namespace Task_manager.DTOs;

public class RegisterDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Fname { get; set; }
    public required string Lname { get; set; }
}