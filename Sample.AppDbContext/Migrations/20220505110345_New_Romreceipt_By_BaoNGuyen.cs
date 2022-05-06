using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.AppDbContext.Migrations
{
    public partial class New_Romreceipt_By_BaoNGuyen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ElectricBill = table.Column<double>(type: "float", nullable: true),
                    WaterBill = table.Column<double>(type: "float", nullable: true),
                    RoomUtilitieBill = table.Column<double>(type: "float", nullable: true),
                    RoomBill = table.Column<double>(type: "float", nullable: true),
                    TotalBill = table.Column<double>(type: "float", nullable: true),
                    PlusBill = table.Column<double>(type: "float", nullable: true),
                    SubBill = table.Column<double>(type: "float", nullable: true),
                    FinalBill = table.Column<double>(type: "float", nullable: true),
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
                    table.PrimaryKey("PK_RoomReceipts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomReceipts");
        }
    }
}
