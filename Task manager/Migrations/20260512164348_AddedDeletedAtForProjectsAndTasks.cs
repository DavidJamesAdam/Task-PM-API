using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_manager.Migrations
{
    /// <inheritdoc />
    public partial class AddedDeletedAtForProjectsAndTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "tasks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "projects",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "projects");
        }
    }
}
