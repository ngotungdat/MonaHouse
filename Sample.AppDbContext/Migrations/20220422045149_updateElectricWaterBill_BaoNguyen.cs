using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.AppDbContext.Migrations
{
    public partial class updateElectricWaterBill_BaoNguyen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectricityWaterBills");

            migrationBuilder.CreateTable(
                name: "ElectricWaterBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    ElectricPrice = table.Column<double>(type: "float", nullable: true),
                    WaterPrice = table.Column<double>(type: "float", nullable: true),
                    OldNumberElectric = table.Column<double>(type: "float", nullable: true),
                    NewNumberElectric = table.Column<double>(type: "float", nullable: true),
                    OldNumberWater = table.Column<double>(type: "float", nullable: true),
                    NewNumberWater = table.Column<double>(type: "float", nullable: true),
                    WriteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WaterImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElectricImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElectricBill = table.Column<double>(type: "float", nullable: true),
                    WaterBill = table.Column<double>(type: "float", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricWaterBills", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectricWaterBills");

            migrationBuilder.CreateTable(
                name: "ElectricityWaterBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    ElectricBill = table.Column<double>(type: "float", nullable: true),
                    ElectricImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElectricPrice = table.Column<double>(type: "float", nullable: true),
                    NewNumberElectric = table.Column<double>(type: "float", nullable: true),
                    NewNumberWater = table.Column<double>(type: "float", nullable: true),
                    OldNumberElectric = table.Column<double>(type: "float", nullable: true),
                    OldNumberWater = table.Column<double>(type: "float", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WaterBill = table.Column<double>(type: "float", nullable: true),
                    WaterImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaterPrice = table.Column<double>(type: "float", nullable: true),
                    WriteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricityWaterBills", x => x.Id);
                });
        }
    }
}
