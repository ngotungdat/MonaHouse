using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.AppDbContext.Migrations
{
    public partial class udpate_PackageInroom_1752022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PackageDescription",
                table: "PackageOfUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackageMoreInfo",
                table: "PackageOfUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackageName",
                table: "PackageOfUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PackagePrice",
                table: "PackageOfUsers",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "PackageOfUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomLimited",
                table: "PackageOfUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "note",
                table: "PackageOfUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageDescription",
                table: "PackageOfUsers");

            migrationBuilder.DropColumn(
                name: "PackageMoreInfo",
                table: "PackageOfUsers");

            migrationBuilder.DropColumn(
                name: "PackageName",
                table: "PackageOfUsers");

            migrationBuilder.DropColumn(
                name: "PackagePrice",
                table: "PackageOfUsers");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "PackageOfUsers");

            migrationBuilder.DropColumn(
                name: "RoomLimited",
                table: "PackageOfUsers");

            migrationBuilder.DropColumn(
                name: "note",
                table: "PackageOfUsers");
        }
    }
}
