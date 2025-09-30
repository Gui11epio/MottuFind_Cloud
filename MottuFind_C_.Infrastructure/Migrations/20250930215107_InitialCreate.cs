using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuFind_C_.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_FILIAIS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cidade = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pais = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FILIAIS", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_USUARIO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Setor = table.Column<int>(type: "int", nullable: false),
                    NomeUsuario = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIO", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_PATIOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FilialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PATIOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_PATIOS_TB_FILIAIS_FilialId",
                        column: x => x.FilialId,
                        principalTable: "TB_FILIAIS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_LEITORES_RFID",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Localizacao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IpDispositivo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_LEITORES_RFID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_LEITORES_RFID_TB_PATIOS_PatioId",
                        column: x => x.PatioId,
                        principalTable: "TB_PATIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_MOTOS",
                columns: table => new
                {
                    Placa = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modelo = table.Column<int>(type: "int", nullable: false),
                    Marca = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PatioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_MOTOS", x => x.Placa);
                    table.ForeignKey(
                        name: "FK_TB_MOTOS_TB_PATIOS_PatioId",
                        column: x => x.PatioId,
                        principalTable: "TB_PATIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_TAGS_RFID",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodigoIdentificacao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MotoPlaca = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TAGS_RFID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_TAGS_RFID_TB_MOTOS_MotoPlaca",
                        column: x => x.MotoPlaca,
                        principalTable: "TB_MOTOS",
                        principalColumn: "Placa",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_LEITURAS_RFID",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataHora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LeitorId = table.Column<int>(type: "int", nullable: false),
                    TagRfidId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_LEITURAS_RFID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_LEITURAS_RFID_TB_LEITORES_RFID_LeitorId",
                        column: x => x.LeitorId,
                        principalTable: "TB_LEITORES_RFID",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_LEITURAS_RFID_TB_TAGS_RFID_TagRfidId",
                        column: x => x.TagRfidId,
                        principalTable: "TB_TAGS_RFID",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "TB_USUARIO");

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
