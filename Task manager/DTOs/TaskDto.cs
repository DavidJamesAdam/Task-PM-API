using System.ComponentModel.DataAnnotations;

public class TaskResponseDto
{
  public required Guid Id { get; set; }
  public required string Task_name { get; set; }
  public required Guid ProjectId { get; set; }
  public required Guid UserId { get; set; }
}

public class CreateTaskDto
{
  [Required(ErrorMessage = "Task name is required")]
  [StringLength(200, MinimumLength = 1, ErrorMessage = "Task name must be between 1 and 200 characters")]
  public required string Task_name { get; set; }
}

public class UpdateTaskDto
{
  [Required(ErrorMessage = "Task name is required")]
  [StringLength(200, MinimumLength = 1, ErrorMessage = "Task name must be between 1 and 200 characters")]
  public required string Task_name { get; set; }
}

public class CreateTaskResultDto
{
  public bool Succeeded { get; set; }
  public TaskResponseDto? Task { get; set; }
  public Dictionary<string, string> Errors { get; set; } = new();
}