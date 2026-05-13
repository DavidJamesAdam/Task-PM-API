using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_manager.Migrations
{
    /// <inheritdoc />
    public partial class AddedForeignKeyConstraintsToProjectsAndTasksModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_AspNetUsers_UsersId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_projects_ProjectsId",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_ProjectsId",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_UsersId",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "ProjectsId",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "tasks");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_project_id",
                table: "tasks",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_user_id",
                table: "tasks",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_AspNetUsers_user_id",
                table: "tasks",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_projects_project_id",
                table: "tasks",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_AspNetUsers_user_id",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_projects_project_id",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_project_id",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_user_id",
                table: "tasks");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectsId",
                table: "tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tasks_ProjectsId",
                table: "tasks",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_UsersId",
                table: "tasks",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_AspNetUsers_UsersId",
                table: "tasks",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_projects_ProjectsId",
                table: "tasks",
                column: "ProjectsId",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
