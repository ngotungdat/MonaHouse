using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.AppDbContext.Migrations
{
    public partial class updateRoom_Electric_Water_price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ElectricPrice",
                table: "Room",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WaterPrice",
                table: "Room",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElectricPrice",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "WaterPrice",
                table: "Room");
        }
    }
}
