using System.ComponentModel.DataAnnotations.Schema;

namespace Task_manager.Models;

[Table("users")]
public class Users
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id{ get; set;}
    [Column("fname")]
    public required string Fname{ get; set; }
    [Column("lname")]
    public required string Lname{ get; set; }
    [Column("created_at")]
    public DateTime Created_at { get; set; } = DateTime.UtcNow;
    [Column("deleted_at")]
    public DateTime? Deleted_at { get; set; }
    public ICollection<Projects> Projects { get; } = new List<Projects>();
}