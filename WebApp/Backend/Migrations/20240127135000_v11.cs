using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donacija_Korisnik_KorisnikID",
                table: "Donacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Slucaj_Korisnik_KorisnikID",
                table: "Slucaj");

            migrationBuilder.AlterColumn<int>(
                name: "KorisnikID",
                table: "Slucaj",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donacija_Korisnik_KorisnikID",
                table: "Donacija",
                column: "KorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Slucaj_Korisnik_KorisnikID",
                table: "Slucaj",
                column: "KorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donacija_Korisnik_KorisnikID",
                table: "Donacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Slucaj_Korisnik_KorisnikID",
                table: "Slucaj");

            migrationBuilder.AlterColumn<int>(
                name: "KorisnikID",
                table: "Slucaj",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Donacija_Korisnik_KorisnikID",
                table: "Donacija",
                column: "KorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Slucaj_Korisnik_KorisnikID",
                table: "Slucaj",
                column: "KorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID");
        }
    }
}
