
public class ProjectResponseDto
{
  public required Guid Id{ get; set;}
  public required string Project_name{ get; set;}
  public required Guid UserId{ get; set; }
}

public class CreateProjectDto
{
  public required string Project_name{ get; set;}
}

public class UpdateProjectDto
{
  public required string Project_name{ get; set;}
}

public class CreateProjectResultDto
{
  public bool Succeeded { get; set; }
  public ProjectResponseDto? Project { get; set; }
  public Dictionary<string, string> Errors { get; set; } = new();
}