using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.AppDbContext.Migrations
{
    public partial class update_roomcontractByBaoNguyen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DepositMoney",
                table: "RoomContractRepresentatives",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PrepayMoney",
                table: "RoomContractRepresentatives",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepositMoney",
                table: "RoomContractRepresentatives");

            migrationBuilder.DropColumn(
                name: "PrepayMoney",
                table: "RoomContractRepresentatives");
        }
    }
}
