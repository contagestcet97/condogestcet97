using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace condogestcet97.web.Migrations.DataContextCondominiumMigrations
{
    /// <inheritdoc />
    public partial class UserIds_changed_to_Int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NotifierId",
                table: "Incidents",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NotifierId",
                table: "Incidents",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
