using System.ComponentModel.DataAnnotations.Schema;

namespace Task_manager.Models;

[Table("projects")]
public class Projects
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Column("project_name")]
    public required string ProjectName { get; set;}
    [Column("user_id")]
    public required Guid UserId { get; set; }
    public Users User { get; set; } = null!;
    public ICollection<Tasks> Tasks { get; } = new List<Tasks>();
    // Relation
}