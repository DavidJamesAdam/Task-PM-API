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
    [ForeignKey("Users")]
    public required Guid UserId { get; set; }
    [Column("created_at")]
    public DateTime? Created_at { get; set; }
    [Column("updated_at")]
    public DateTime? Updated_at { get; set; }
    [Column("deleted_at")]
    public DateTime? Deleted_at { get; set; }
    public Users User { get; set; } = null!;
    public ICollection<Tasks> Tasks { get; } = new List<Tasks>();
    // Relation
}