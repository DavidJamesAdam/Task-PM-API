using System.ComponentModel.DataAnnotations.Schema;

namespace Task_manager.Models;

[Table("tasks")]
public class Tasks
{
  [Column("id")]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }
  [Column("task_name")]
  public required string TaskName { get; set; }
  [Column("project_id")]
  public required Guid ProjectId { get; set; }
  public required Projects Projects { get; set; }
  [Column("user_id")]
  public required Guid UserId { get; set; }
  public required Users Users { get; set; }
}