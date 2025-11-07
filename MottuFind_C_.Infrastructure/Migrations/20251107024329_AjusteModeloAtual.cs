using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuFind_C_.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteModeloAtual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TAGS_MOTOS",
                table: "TB_TAGS_RFID");

            migrationBuilder.AddForeignKey(
                name: "FK_TAGS_MOTOS",
                table: "TB_TAGS_RFID",
                column: "MotoPlaca",
                principalTable: "TB_MOTOS",
                principalColumn: "Placa",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TAGS_MOTOS",
                table: "TB_TAGS_RFID");

            migrationBuilder.AddForeignKey(
                name: "FK_TAGS_MOTOS",
                table: "TB_TAGS_RFID",
                column: "MotoPlaca",
                principalTable: "TB_MOTOS",
                principalColumn: "Placa",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
