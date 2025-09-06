using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sprint1_C_.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_FILIAIS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Pais = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FILIAIS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_PATIOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Largura = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Comprimento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    FilialId = table.Column<int>(type: "NUMBER(10)", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "TB_LEITORES_RFID",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IpDispositivo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PatioId = table.Column<int>(type: "NUMBER(10)", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "TB_MOTOS",
                columns: table => new
                {
                    Placa = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Modelo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Marca = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PatioId = table.Column<int>(type: "NUMBER(10)", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "TB_TAGS_RFID",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CodigoIdentificacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    MotoPlaca = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "TB_LEITURAS_RFID",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DataHora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    LeitorId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TagRfidId = table.Column<int>(type: "NUMBER(10)", nullable: false)
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
