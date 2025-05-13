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
                name: "Filiais",
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
                    table.PrimaryKey("PK_Filiais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patios",
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
                    table.PrimaryKey("PK_Patios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patios_Filiais_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filiais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeitoresRfid",
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
                    table.PrimaryKey("PK_LeitoresRfid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeitoresRfid_Patios_PatioId",
                        column: x => x.PatioId,
                        principalTable: "Patios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Motos",
                columns: table => new
                {
                    Placa = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Modelo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Marca = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PatioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TagId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motos", x => x.Placa);
                    table.ForeignKey(
                        name: "FK_Motos_Patios_PatioId",
                        column: x => x.PatioId,
                        principalTable: "Patios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagsRfid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CodigoIdentificacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Ativa = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    MotoPlaca = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsRfid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagsRfid_Motos_MotoPlaca",
                        column: x => x.MotoPlaca,
                        principalTable: "Motos",
                        principalColumn: "Placa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeiturasRfid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DataHora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    LeitorId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TagId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeiturasRfid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeiturasRfid_LeitoresRfid_LeitorId",
                        column: x => x.LeitorId,
                        principalTable: "LeitoresRfid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeiturasRfid_TagsRfid_TagId",
                        column: x => x.TagId,
                        principalTable: "TagsRfid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeitoresRfid_PatioId",
                table: "LeitoresRfid",
                column: "PatioId");

            migrationBuilder.CreateIndex(
                name: "IX_LeiturasRfid_LeitorId",
                table: "LeiturasRfid",
                column: "LeitorId");

            migrationBuilder.CreateIndex(
                name: "IX_LeiturasRfid_TagId",
                table: "LeiturasRfid",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Motos_PatioId",
                table: "Motos",
                column: "PatioId");

            migrationBuilder.CreateIndex(
                name: "IX_Patios_FilialId",
                table: "Patios",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_TagsRfid_MotoPlaca",
                table: "TagsRfid",
                column: "MotoPlaca",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeiturasRfid");

            migrationBuilder.DropTable(
                name: "LeitoresRfid");

            migrationBuilder.DropTable(
                name: "TagsRfid");

            migrationBuilder.DropTable(
                name: "Motos");

            migrationBuilder.DropTable(
                name: "Patios");

            migrationBuilder.DropTable(
                name: "Filiais");
        }
    }
}
