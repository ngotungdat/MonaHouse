using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.AppDbContext.Migrations
{
    public partial class update_roomReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MoneyDebtRoomReceipt",
                table: "RoomReceipts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MoneyRecive",
                table: "RoomReceipts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoteRecive",
                table: "RoomReceipts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymenMethod",
                table: "RoomReceipts",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoneyDebtRoomReceipt",
                table: "RoomReceipts");

            migrationBuilder.DropColumn(
                name: "MoneyRecive",
                table: "RoomReceipts");

            migrationBuilder.DropColumn(
                name: "NoteRecive",
                table: "RoomReceipts");

            migrationBuilder.DropColumn(
                name: "PaymenMethod",
                table: "RoomReceipts");
        }
    }
}
