# Task-PM-API
Basic CRUD interface using .NET (ASP.NET Code), RESTful APIs, and PostgreSQL to manage and track tasks

# TECH STACK
## ASP.NET Core Web API
- https://dotnet.microsoft.com/en-us/learn/aspnet/blazor-tutorial/install
## C#
## Entity Framework Core
- https://learn.microsoft.com/en-us/ef/core/
- https://learn.microsoft.com/en-us/ef/core/get-started/overview/install
## PostgreSQL

# Endpoints
- POST /users
- GET /users
- POST /projects
- GET /projects
- POST /tasks
- PUT /tasks/{id}
- DELETE /tasks/{id}

# DB
- Models:
- - User
- - Project
- - Task
- Relationships:
- - User -> Projects
- - Project -> Tasks

# Resources
## File structure guide
- https://medium.com/@speedcodelabs/structuring-your-net-core-api-best-practices-and-common-folder-structure-3c05ee298e5a

## Databse Migrations
https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/managing?tabs=dotnet-core-cli