using Microsoft.EntityFrameworkCore.Migrations;

namespace Todos.Migrations
{
    public partial class UpdateToDoWithIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ToDoModel",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoModel_UserId",
                table: "ToDoModel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoModel_AspNetUsers_UserId",
                table: "ToDoModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoModel_AspNetUsers_UserId",
                table: "ToDoModel");

            migrationBuilder.DropIndex(
                name: "IX_ToDoModel_UserId",
                table: "ToDoModel");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ToDoModel");
        }
    }
}
