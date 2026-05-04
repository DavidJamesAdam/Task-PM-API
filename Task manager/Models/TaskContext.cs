using Microsoft.EntityFrameworkCore;
namespace Task_manager.Models;

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options)
        : base(options)
    {
    }

    public DbSet<Users> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fname).HasColumnName("fname");
            entity.Property(e => e.Lname).HasColumnName("lname");
            entity.Property(e => e.Created_at).HasColumnName("created_at");
        });
    }
}