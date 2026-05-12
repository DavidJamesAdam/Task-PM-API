using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_manager.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserIdColumnsInProjectAndTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_AspNetUsers_UsersId",
                table: "projects");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "projects",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_projects_UsersId",
                table: "projects",
                newName: "IX_projects_user_id");

            migrationBuilder.AddColumn<Guid>(
                name: "project_id",
                table: "tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_projects_AspNetUsers_user_id",
                table: "projects",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_AspNetUsers_user_id",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "project_id",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "tasks");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "projects",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_projects_user_id",
                table: "projects",
                newName: "IX_projects_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_projects_AspNetUsers_UsersId",
                table: "projects",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
