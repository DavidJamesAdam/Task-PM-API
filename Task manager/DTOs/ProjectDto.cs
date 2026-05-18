using System.ComponentModel.DataAnnotations;

public class ProjectResponseDto
{
  public required Guid Id { get; set; }
  public required string Project_name { get; set; }
  public required Guid UserId { get; set; }
}

public class CreateProjectDto
{
  [Required(ErrorMessage = "Project name is required")]
  [StringLength(100, MinimumLength = 1, ErrorMessage = "Project name must be between 1 and 100 characters")]
  public required string Project_name { get; set; }
}

public class UpdateProjectDto
{
  [Required(ErrorMessage = "Project name is required")]
  [StringLength(100, MinimumLength = 1, ErrorMessage = "Project name must be between 1 and 100 characters")]
  public required string Project_name { get; set; }
}

public class CreateProjectResultDto
{
  public bool Succeeded { get; set; }
  public ProjectResponseDto? Project { get; set; }
  public Dictionary<string, string> Errors { get; set; } = new();
}