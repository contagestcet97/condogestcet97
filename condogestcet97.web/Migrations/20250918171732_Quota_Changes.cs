using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace condogestcet97.web.Migrations.DataContextFinancialMigrations
{
    /// <inheritdoc />
    public partial class Quota_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentValue",
                table: "Quotas");

            migrationBuilder.AddColumn<int>(
                name: "ApartmentsCount",
                table: "Quotas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApartmentsCount",
                table: "Quotas");

            migrationBuilder.AddColumn<decimal>(
                name: "PaymentValue",
                table: "Quotas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
