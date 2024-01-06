using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donacija_Korisnik_KorisnikID",
                table: "Donacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Donacija_Slucaj_SlucajID",
                table: "Donacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Novost_Slucaj_SlucajID",
                table: "Novost");

            migrationBuilder.DropForeignKey(
                name: "FK_Trosak_Slucaj_SlucajID",
                table: "Trosak");

            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Slucaj");

            migrationBuilder.AlterColumn<int>(
                name: "SlucajID",
                table: "Trosak",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Opis",
                table: "Slucaj",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slike",
                table: "Slucaj",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "SlucajID",
                table: "Novost",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Prioritet",
                table: "Kategorija",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "SlucajID",
                table: "Donacija",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KorisnikID",
                table: "Donacija",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kategorija_Prioritet",
                table: "Kategorija",
                column: "Prioritet",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donacija_Korisnik_KorisnikID",
                table: "Donacija",
                column: "KorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donacija_Slucaj_SlucajID",
                table: "Donacija",
                column: "SlucajID",
                principalTable: "Slucaj",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Novost_Slucaj_SlucajID",
                table: "Novost",
                column: "SlucajID",
                principalTable: "Slucaj",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trosak_Slucaj_SlucajID",
                table: "Trosak",
                column: "SlucajID",
                principalTable: "Slucaj",
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
                name: "FK_Donacija_Slucaj_SlucajID",
                table: "Donacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Novost_Slucaj_SlucajID",
                table: "Novost");

            migrationBuilder.DropForeignKey(
                name: "FK_Trosak_Slucaj_SlucajID",
                table: "Trosak");

            migrationBuilder.DropIndex(
                name: "IX_Kategorija_Prioritet",
                table: "Kategorija");

            migrationBuilder.DropColumn(
                name: "Slike",
                table: "Slucaj");

            migrationBuilder.DropColumn(
                name: "Prioritet",
                table: "Kategorija");

            migrationBuilder.AlterColumn<int>(
                name: "SlucajID",
                table: "Trosak",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Opis",
                table: "Slucaj",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slika",
                table: "Slucaj",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SlucajID",
                table: "Novost",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SlucajID",
                table: "Donacija",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "KorisnikID",
                table: "Donacija",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Donacija_Korisnik_KorisnikID",
                table: "Donacija",
                column: "KorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Donacija_Slucaj_SlucajID",
                table: "Donacija",
                column: "SlucajID",
                principalTable: "Slucaj",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Novost_Slucaj_SlucajID",
                table: "Novost",
                column: "SlucajID",
                principalTable: "Slucaj",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Trosak_Slucaj_SlucajID",
                table: "Trosak",
                column: "SlucajID",
                principalTable: "Slucaj",
                principalColumn: "ID");
        }
    }
}
