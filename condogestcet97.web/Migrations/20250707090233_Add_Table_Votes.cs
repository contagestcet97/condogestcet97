using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace condogestcet97.web.Migrations
{
    /// <inheritdoc />
    public partial class Add_Table_Votes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Condos_CondoId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Interventions_InterventionId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Meetings_MeetingId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Apartments_ApartmentId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Condos_CondoId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Interventions_Incidents_IncidentId",
                table: "Interventions");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Condos_CondoId",
                table: "Meetings");

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    MeetingId = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    VotesInFavour = table.Column<int>(type: "int", nullable: false),
                    VotesAgainst = table.Column<int>(type: "int", nullable: false),
                    VotesAbstained = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_MeetingId",
                table: "Votes",
                column: "MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Condos_CondoId",
                table: "Apartments",
                column: "CondoId",
                principalTable: "Condos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Interventions_InterventionId",
                table: "Documents",
                column: "InterventionId",
                principalTable: "Interventions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Meetings_MeetingId",
                table: "Documents",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Apartments_ApartmentId",
                table: "Incidents",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Condos_CondoId",
                table: "Incidents",
                column: "CondoId",
                principalTable: "Condos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interventions_Incidents_IncidentId",
                table: "Interventions",
                column: "IncidentId",
                principalTable: "Incidents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Condos_CondoId",
                table: "Meetings",
                column: "CondoId",
                principalTable: "Condos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Condos_CondoId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Interventions_InterventionId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Meetings_MeetingId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Apartments_ApartmentId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Condos_CondoId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Interventions_Incidents_IncidentId",
                table: "Interventions");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Condos_CondoId",
                table: "Meetings");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Condos_CondoId",
                table: "Apartments",
                column: "CondoId",
                principalTable: "Condos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Interventions_InterventionId",
                table: "Documents",
                column: "InterventionId",
                principalTable: "Interventions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Meetings_MeetingId",
                table: "Documents",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Apartments_ApartmentId",
                table: "Incidents",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Condos_CondoId",
                table: "Incidents",
                column: "CondoId",
                principalTable: "Condos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interventions_Incidents_IncidentId",
                table: "Interventions",
                column: "IncidentId",
                principalTable: "Incidents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Condos_CondoId",
                table: "Meetings",
                column: "CondoId",
                principalTable: "Condos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
