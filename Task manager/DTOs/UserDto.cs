public class UserResponseDto
{
  public Guid Id { get; set; }
  public string Email { get; set; } = null!;
  public string Fname { get; set; } = null!;
  public string Lname { get; set; } = null!;
}

public class UpdateUserDto
{
  public string Fname {get; set;} = null!;
  public string Lname {get; set;} = null!;
}

public class CreateUserResultDto
{
  public bool Succeeded { get; set; }
  public UserResponseDto? User { get; set; }
  public Dictionary<string, string[]> Errors { get; set; } = new();
}
