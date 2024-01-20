using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slucaj_Zivotinja_ZivotinjaId",
                table: "Slucaj");

            migrationBuilder.DropIndex(
                name: "IX_Slucaj_ZivotinjaId",
                table: "Slucaj");

            migrationBuilder.AlterColumn<int>(
                name: "ZivotinjaId",
                table: "Slucaj",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Slucaj_ZivotinjaId",
                table: "Slucaj",
                column: "ZivotinjaId",
                unique: true,
                filter: "[ZivotinjaId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Slucaj_Zivotinja_ZivotinjaId",
                table: "Slucaj",
                column: "ZivotinjaId",
                principalTable: "Zivotinja",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slucaj_Zivotinja_ZivotinjaId",
                table: "Slucaj");

            migrationBuilder.DropIndex(
                name: "IX_Slucaj_ZivotinjaId",
                table: "Slucaj");

            migrationBuilder.AlterColumn<int>(
                name: "ZivotinjaId",
                table: "Slucaj",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Slucaj_ZivotinjaId",
                table: "Slucaj",
                column: "ZivotinjaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Slucaj_Zivotinja_ZivotinjaId",
                table: "Slucaj",
                column: "ZivotinjaId",
                principalTable: "Zivotinja",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
