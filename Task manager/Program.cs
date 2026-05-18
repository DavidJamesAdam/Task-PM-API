using Microsoft.EntityFrameworkCore;
using Task_manager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<Users>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<TaskContext>();
// builder.Services.AddIdentity<Users, IdentityRole<Guid>>()
//     .AddEntityFrameworkStores<TaskContext>()
//     .AddDefaultTokenProviders();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskInterface, TaskService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers()
  .AddJsonOptions(opts =>
    opts.JsonSerializerOptions.Converters.Add(
      new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false)
    )
  );

var app = builder.Build();
// Map Identity Endpoints
app.MapGroup("/api/auth").MapIdentityApi<Users>().WithTags("Auth");

var command = args.FirstOrDefault();

if (command == "seed")
{
    using var scope = app.Services.CreateScope();
    await DatabaseSeeder.SeedAsync(scope.ServiceProvider);

    Console.WriteLine("Seeding complete");
    return;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.MapScalarApiReference(); // http://localhost:5035/scalar/v1 to view documentation
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();