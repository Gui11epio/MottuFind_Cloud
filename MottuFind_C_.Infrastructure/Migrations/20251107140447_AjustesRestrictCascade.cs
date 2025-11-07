using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuFind_C_.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjustesRestrictCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_FILIAIS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FILIAIS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_USUARIOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Setor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NomeUsuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIOS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_PATIOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PATIOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PATIOS_FILIAIS",
                        column: x => x.FilialId,
                        principalTable: "TB_FILIAIS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_LEITORES_RFID",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Localizacao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IpDispositivo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_LEITORES_RFID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LEITORES_PATIOS",
                        column: x => x.PatioId,
                        principalTable: "TB_PATIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_MOTOS",
                columns: table => new
                {
                    Placa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PatioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_MOTOS", x => x.Placa);
                    table.ForeignKey(
                        name: "FK_MOTOS_PATIOS",
                        column: x => x.PatioId,
                        principalTable: "TB_PATIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_TAGS_RFID",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoIdentificacao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MotoPlaca = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TAGS_RFID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TAGS_MOTOS",
                        column: x => x.MotoPlaca,
                        principalTable: "TB_MOTOS",
                        principalColumn: "Placa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_LEITURAS_RFID",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeitorId = table.Column<int>(type: "int", nullable: false),
                    TagRfidId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_LEITURAS_RFID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LEITURAS_LEITORES",
                        column: x => x.LeitorId,
                        principalTable: "TB_LEITORES_RFID",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LEITURAS_TAGS",
                        column: x => x.TagRfidId,
                        principalTable: "TB_TAGS_RFID",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_LEITORES_RFID_PatioId",
                table: "TB_LEITORES_RFID",
                column: "PatioId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_LEITURAS_RFID_LeitorId",
                table: "TB_LEITURAS_RFID",
                column: "LeitorId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_LEITURAS_RFID_TagRfidId",
                table: "TB_LEITURAS_RFID",
                column: "TagRfidId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_MOTOS_PatioId",
                table: "TB_MOTOS",
                column: "PatioId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PATIOS_FilialId",
                table: "TB_PATIOS",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TAGS_RFID_MotoPlaca",
                table: "TB_TAGS_RFID",
                column: "MotoPlaca",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_LEITURAS_RFID");

            migrationBuilder.DropTable(
                name: "TB_USUARIOS");

            migrationBuilder.DropTable(
                name: "TB_LEITORES_RFID");

            migrationBuilder.DropTable(
                name: "TB_TAGS_RFID");

            migrationBuilder.DropTable(
                name: "TB_MOTOS");

            migrationBuilder.DropTable(
                name: "TB_PATIOS");

            migrationBuilder.DropTable(
                name: "TB_FILIAIS");
        }
    }
}
