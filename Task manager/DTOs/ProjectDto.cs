public class CreateProjectDto
{
  public required string Project_name{ get; set;}
}

public class ProjectResponseDto
{
  public required string Project_name{ get; set;}
  public required Guid UserId{ get; set; }
}