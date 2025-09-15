using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace condogestcet97.web.Migrations
{
    /// <inheritdoc />
    public partial class FinancialReport_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPdfSent = table.Column<bool>(type: "bit", nullable: false),
                    ExpensesTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RevenueTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialReports", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialReports");
        }
    }
}
