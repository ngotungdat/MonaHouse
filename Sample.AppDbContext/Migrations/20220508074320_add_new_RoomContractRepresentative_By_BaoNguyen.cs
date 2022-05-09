using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.AppDbContext.Migrations
{
    public partial class add_new_RoomContractRepresentative_By_BaoNguyen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomContractRepresentatives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateIntoRoom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deposit = table.Column<int>(type: "int", nullable: false),
                    Prepay = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    TotalMoney = table.Column<double>(type: "float", nullable: false),
                    TakeMoney = table.Column<double>(type: "float", nullable: false),
                    DebtMoney = table.Column<double>(type: "float", nullable: false),
                    HomeTown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumCusInRoom = table.Column<int>(type: "int", nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_RoomContractRepresentatives", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomContractRepresentatives");
        }
    }
}
