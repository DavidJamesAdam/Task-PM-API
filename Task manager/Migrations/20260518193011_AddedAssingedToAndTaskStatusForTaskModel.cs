using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_manager.Migrations
{
    /// <inheritdoc />
    public partial class AddedAssingedToAndTaskStatusForTaskModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "assigned_to",
                table: "tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "completed",
                table: "tasks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "in_progress",
                table: "tasks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "todo",
                table: "tasks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assigned_to",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "completed",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "in_progress",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "todo",
                table: "tasks");
        }
    }
}
