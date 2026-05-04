using System.ComponentModel.DataAnnotations.Schema;

namespace Task_manager.Models;

[Table("users")]
public class Users
{
    [Column("id")]
    public Guid Id{ get; set;}
    [Column("fname")]
    public required string Fname{ get; set; }
    [Column("lname")]
    public required string Lname{ get; set; }
    [Column("created_at")]
    public required DateTime Created_at { get; set; }
    [Column("deleted_at")]
    public required DateTime Deleted_at { get; set; }
    public ICollection<Projects> Projects { get; } = new List<Projects>();
}