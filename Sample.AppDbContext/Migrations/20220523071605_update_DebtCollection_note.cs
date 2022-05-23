using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.AppDbContext.Migrations
{
    public partial class update_DebtCollection_note : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DebtRemaining",
                table: "DebtCollections",
                type: "float",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DebtConllection",
                table: "DebtCollections",
                type: "float",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "note",
                table: "DebtCollections",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "note",
                table: "DebtCollections");

            migrationBuilder.AlterColumn<float>(
                name: "DebtRemaining",
                table: "DebtCollections",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "DebtConllection",
                table: "DebtCollections",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
