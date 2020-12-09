using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tache.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departements",
                columns: table => new
                {
                    IdDepartement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartementName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departements", x => x.IdDepartement);
                });

            migrationBuilder.CreateTable(
                name: "Taches",
                columns: table => new
                {
                    IdTache = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TacheName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStart = table.Column<DateTime>(type: "Date", nullable: false),
                    Deadline = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taches", x => x.IdTache);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personnels",
                columns: table => new
                {
                    IdPersonnel = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fonction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateWork = table.Column<DateTime>(type: "Date", nullable: false),
                    ImagePersonnel = table.Column<byte[]>(type: "Image", nullable: true),
                    Departement = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnels", x => x.IdPersonnel);
                    table.ForeignKey(
                        name: "FK_Personnels_Departements_Departement",
                        column: x => x.Departement,
                        principalTable: "Departements",
                        principalColumn: "IdDepartement",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelsTaches",
                columns: table => new
                {
                    personnelsIdPersonnel = table.Column<int>(type: "int", nullable: false),
                    tachesIdTache = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelsTaches", x => new { x.personnelsIdPersonnel, x.tachesIdTache });
                    table.ForeignKey(
                        name: "FK_PersonnelsTaches_Personnels_personnelsIdPersonnel",
                        column: x => x.personnelsIdPersonnel,
                        principalTable: "Personnels",
                        principalColumn: "IdPersonnel",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonnelsTaches_Taches_tachesIdTache",
                        column: x => x.tachesIdTache,
                        principalTable: "Taches",
                        principalColumn: "IdTache",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_Departement",
                table: "Personnels",
                column: "Departement");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelsTaches_tachesIdTache",
                table: "PersonnelsTaches",
                column: "tachesIdTache");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonnelsTaches");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Personnels");

            migrationBuilder.DropTable(
                name: "Taches");

            migrationBuilder.DropTable(
                name: "Departements");
        }
    }
}
