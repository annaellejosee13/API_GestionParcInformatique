using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_projet_parc_informatique.Migrations
{
    /// <inheritdoc />
    public partial class DataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Processeur",
                table: "Postes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Remontees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Id_poste = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    DateRemontee = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EtatSysteme = table.Column<string>(type: "TEXT", nullable: false),
                    CPU = table.Column<double>(type: "REAL", nullable: false),
                    RAM = table.Column<double>(type: "REAL", nullable: false),
                    DisqueDur = table.Column<double>(type: "REAL", nullable: false),
                    ConnectiviteReseau = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remontees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remontees_Postes_Id_poste",
                        column: x => x.Id_poste,
                        principalTable: "Postes",
                        principalColumn: "Id_poste",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Remontees_Id_poste",
                table: "Remontees",
                column: "Id_poste");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Remontees");

            migrationBuilder.DropColumn(
                name: "Processeur",
                table: "Postes");
        }
    }
}
