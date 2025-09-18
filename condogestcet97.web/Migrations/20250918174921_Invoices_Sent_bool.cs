using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace condogestcet97.web.Migrations.DataContextFinancialMigrations
{
    /// <inheritdoc />
    public partial class Invoices_Sent_bool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InvoicesSent",
                table: "Quotas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoicesSent",
                table: "Quotas");
        }
    }
}
