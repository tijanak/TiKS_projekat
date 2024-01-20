using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slucaj_Lokacija_LokacijaId",
                table: "Slucaj");

            migrationBuilder.DropIndex(
                name: "IX_Slucaj_LokacijaId",
                table: "Slucaj");

            migrationBuilder.AlterColumn<int>(
                name: "LokacijaId",
                table: "Slucaj",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Slucaj_LokacijaId",
                table: "Slucaj",
                column: "LokacijaId",
                unique: true,
                filter: "[LokacijaId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Slucaj_Lokacija_LokacijaId",
                table: "Slucaj",
                column: "LokacijaId",
                principalTable: "Lokacija",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slucaj_Lokacija_LokacijaId",
                table: "Slucaj");

            migrationBuilder.DropIndex(
                name: "IX_Slucaj_LokacijaId",
                table: "Slucaj");

            migrationBuilder.AlterColumn<int>(
                name: "LokacijaId",
                table: "Slucaj",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Slucaj_LokacijaId",
                table: "Slucaj",
                column: "LokacijaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Slucaj_Lokacija_LokacijaId",
                table: "Slucaj",
                column: "LokacijaId",
                principalTable: "Lokacija",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
