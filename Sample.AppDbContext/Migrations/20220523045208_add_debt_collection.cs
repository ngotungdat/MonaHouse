using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.AppDbContext.Migrations
{
    public partial class add_debt_collection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DebtCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DebtConllection = table.Column<float>(type: "real", nullable: true),
                    DebtRemaining = table.Column<float>(type: "real", nullable: true),
                    ConllecttionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_DebtCollections", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DebtCollections");
        }
    }
}
