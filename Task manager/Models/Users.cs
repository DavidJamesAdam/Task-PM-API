namespace Task_manager.Models;

public class Users
{
    public Guid Id{ get; set;}
    public required string Fname{ get; set; }
    public required string Lname{ get; set; }
    public required DateTime Created_at { get; set; }
}