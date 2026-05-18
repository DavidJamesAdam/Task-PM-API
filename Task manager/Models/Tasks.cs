using System.ComponentModel.DataAnnotations.Schema;

namespace Task_manager.Models;

public enum TaskStatus { Todo, InProgress, Done }

[Table("tasks")]
public class Tasks
{
  [Column("id")]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }
  [Column("task_name")]
  public required string TaskName { get; set; }
  [Column("assigned_to")]
  public Guid? AssignedTo { get; set; }
  [Column("status")]
  public TaskStatus Status { get; set; } = TaskStatus.Todo;
  [Column("project_id")]
  [ForeignKey("Projects")]
  public required Guid ProjectId { get; set; }
  [Column("created_at")]
  public DateTime? Created_at { get; set; } = DateTime.UtcNow;
  [Column("updated_at")]
  public DateTime? Updated_at { get; set; }
  [Column("deleted_at")]
  public DateTime? Deleted_at { get; set; }
  public Projects Projects { get; set; } = null!;
  [Column("user_id")]
  [ForeignKey("Users")]
  public required Guid UserId { get; set; }
  public Users Users { get; set; } = null!;
}