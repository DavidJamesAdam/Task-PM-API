using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_manager.Migrations
{
    /// <inheritdoc />
    public partial class StoreTaskStatusAsString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "tasks",
                type: "text",
                nullable: false,
                defaultValue: "Todo",
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "tasks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "Todo");
        }
    }
}
