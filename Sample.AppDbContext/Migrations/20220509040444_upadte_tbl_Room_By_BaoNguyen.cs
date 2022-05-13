using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.AppDbContext.Migrations
{
    public partial class upadte_tbl_Room_By_BaoNguyen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateGetOutRoom",
                table: "Room",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateInToRoom",
                table: "Room",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ElectricWaterPackage",
                table: "Room",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateGetOutRoom",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "DateInToRoom",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "ElectricWaterPackage",
                table: "Room");
        }
    }
}
