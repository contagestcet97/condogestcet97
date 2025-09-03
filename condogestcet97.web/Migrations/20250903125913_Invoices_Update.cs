using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace condogestcet97.web.Migrations
{
    /// <inheritdoc />
    public partial class Invoices_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Invoices");
        }
    }
}
