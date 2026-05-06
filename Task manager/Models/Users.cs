using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Task_manager.Models;

[Table("users")]
public class Users : IdentityUser<Guid>
{
    [Column("fname")]
    public string? Fname{ get; set; }
    [Column("lname")]
    public string? Lname{ get; set; }
    [Column("created_at")]
    public DateTime Created_at { get; set; } = DateTime.UtcNow;
    [Column("updated_at")]
    public DateTime? Updated_at { get; set; }
    [Column("deleted_at")]
    public DateTime? Deleted_at { get; set; }
    public ICollection<Projects> Projects { get; } = new List<Projects>();
}