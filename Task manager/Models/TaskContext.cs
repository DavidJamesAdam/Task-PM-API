using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Task_manager.Models;

public class TaskContext : IdentityDbContext<Users, IdentityRole<Guid>, Guid>
{
  public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Users>()
        .HasQueryFilter(u => u.Deleted_at == null);

    modelBuilder.Entity<Projects>()
        .HasQueryFilter(p => p.Deleted_at == null);

    modelBuilder.Entity<Tasks>()
        .HasQueryFilter(t => t.Deleted_at == null);

    modelBuilder.Entity<Tasks>()
      .Property(t => t.Status)
      .HasConversion<string>()
      .HasDefaultValue(TaskStatus.Todo);
  }

  public DbSet<Projects> Projects { get; set; } = null!;
  public DbSet<Tasks> Tasks { get; set; } = null!;
}

