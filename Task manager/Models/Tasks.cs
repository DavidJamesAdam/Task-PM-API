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
  [ForeignKey("Projects")]
  public required Guid ProjectId { get; set; }
  [Column("deleted_at")]
  public DateTime? Deleted_at { get; set; }
  public Projects Projects { get; set; } = null!;
  [Column("user_id")]
  [ForeignKey("Users")]
  public required Guid UserId { get; set; }
  public Users Users { get; set; } = null!;
}