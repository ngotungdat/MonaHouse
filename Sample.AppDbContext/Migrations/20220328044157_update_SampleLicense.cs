using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.AppDbContext.Migrations
{
    public partial class update_SampleLicense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "LicenseSamples");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "LicenseSamples");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "LicenseSamples");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "LicenseSamples",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "LicenseSamples");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "LicenseSamples",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "LicenseSamples",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LicenseSamples",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
