public class TaskResponseDto
{
  public required Guid Id{ get; set;}
  public required string Task_name{ get; set; }
  public required Guid ProjectId{ get; set; }
  public required Guid UserId{ get; set; }
}

public class CreateTaskDto
{
  public required string Task_name{ get; set;}
}

public class UpdateTaskDto
{
  public required string Task_name{ get; set;}
}

public class CreateTaskResultDto
{
  public bool Succeeded { get; set; }
  public TaskResponseDto? Task { get; set; }
  public Dictionary<string, string> Errors { get; set; } = new();
}