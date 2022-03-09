using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.AppDbContext.Migrations
{
    public partial class minh_v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Contract",
                newName: "UserInRoomName");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Contract",
                newName: "UserInRoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserInRoomName",
                table: "Contract",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "UserInRoomId",
                table: "Contract",
                newName: "CustomerId");
        }
    }
}
