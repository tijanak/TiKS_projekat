using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorija", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lokacija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacija", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Zivotinja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Vrsta = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zivotinja", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Slucaj",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    Slika = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LokacijaId = table.Column<int>(type: "int", nullable: false),
                    KorisnikID = table.Column<int>(type: "int", nullable: true),
                    ZivotinjaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slucaj", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Slucaj_Korisnik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Slucaj_Lokacija_LokacijaId",
                        column: x => x.LokacijaId,
                        principalTable: "Lokacija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Slucaj_Zivotinja_ZivotinjaId",
                        column: x => x.ZivotinjaId,
                        principalTable: "Zivotinja",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donacija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    SlucajID = table.Column<int>(type: "int", nullable: true),
                    KorisnikID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donacija", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Donacija_Korisnik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Donacija_Slucaj_SlucajID",
                        column: x => x.SlucajID,
                        principalTable: "Slucaj",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "KategorijaSlucaj",
                columns: table => new
                {
                    KategorijaID = table.Column<int>(type: "int", nullable: false),
                    SlucajeviID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategorijaSlucaj", x => new { x.KategorijaID, x.SlucajeviID });
                    table.ForeignKey(
                        name: "FK_KategorijaSlucaj_Kategorija_KategorijaID",
                        column: x => x.KategorijaID,
                        principalTable: "Kategorija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KategorijaSlucaj_Slucaj_SlucajeviID",
                        column: x => x.SlucajeviID,
                        principalTable: "Slucaj",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Novost",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tekst = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlucajID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Novost", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Novost_Slucaj_SlucajID",
                        column: x => x.SlucajID,
                        principalTable: "Slucaj",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Trosak",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namena = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    SlucajID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trosak", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Trosak_Slucaj_SlucajID",
                        column: x => x.SlucajID,
                        principalTable: "Slucaj",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donacija_KorisnikID",
                table: "Donacija",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Donacija_SlucajID",
                table: "Donacija",
                column: "SlucajID");

            migrationBuilder.CreateIndex(
                name: "IX_KategorijaSlucaj_SlucajeviID",
                table: "KategorijaSlucaj",
                column: "SlucajeviID");

            migrationBuilder.CreateIndex(
                name: "IX_Novost_SlucajID",
                table: "Novost",
                column: "SlucajID");

            migrationBuilder.CreateIndex(
                name: "IX_Slucaj_KorisnikID",
                table: "Slucaj",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Slucaj_LokacijaId",
                table: "Slucaj",
                column: "LokacijaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Slucaj_ZivotinjaId",
                table: "Slucaj",
                column: "ZivotinjaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trosak_SlucajID",
                table: "Trosak",
                column: "SlucajID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donacija");

            migrationBuilder.DropTable(
                name: "KategorijaSlucaj");

            migrationBuilder.DropTable(
                name: "Novost");

            migrationBuilder.DropTable(
                name: "Trosak");

            migrationBuilder.DropTable(
                name: "Kategorija");

            migrationBuilder.DropTable(
                name: "Slucaj");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Lokacija");

            migrationBuilder.DropTable(
                name: "Zivotinja");
        }
    }
}
