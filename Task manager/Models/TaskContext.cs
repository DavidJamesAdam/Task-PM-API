using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace Task_manager.Models;

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options)
        : base(options)
    {
    }

    public DbSet<Users> Users { get; set; } = null!;
    public DbSet<Projects> Projects { get; set; } = null!;
    public DbSet<Tasks> Tasks { get; set; } = null!;
}