using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.AppDbContext.Migrations
{
    public partial class update_RoomReceipt_By_BaoNguyen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "RoomReceipts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "RoomReceipts",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "RoomReceipts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RoomReceipts");
        }
    }
}
