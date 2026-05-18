using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_manager.Migrations
{
    /// <inheritdoc />
    public partial class ChangedStatusColumnsToSingleStatusColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "completed",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "in_progress",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "todo",
                table: "tasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "assigned_to",
                table: "tasks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "tasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "assigned_to",
                table: "tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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
    }
}
