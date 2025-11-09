using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChallangeMottu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_MOTOS_MOTTU",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PLACA = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Posicao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 50, nullable: false),
                    ULTIMA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MOTOS_MOTTU", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_LOCALIZACAO_ATUAL_MOTTU",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    MotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoordenadaX = table.Column<double>(type: "float", nullable: false),
                    CoordenadaY = table.Column<double>(type: "float", nullable: false),
                    DataHoraAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LOCALIZACAO_ATUAL_MOTTU", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_LOCALIZACAO_ATUAL_MOTTU_T_MOTOS_MOTTU_MotoId",
                        column: x => x.MotoId,
                        principalTable: "T_MOTOS_MOTTU",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_USUARIOS_MOTTU",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SENHA_HASH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SENHA_SALT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_USUARIOS_MOTTU", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_USUARIOS_MOTTU_T_MOTOS_MOTTU_MotoId",
                        column: x => x.MotoId,
                        principalTable: "T_MOTOS_MOTTU",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_LOCALIZACAO_ATUAL_MOTTU_MotoId",
                table: "T_LOCALIZACAO_ATUAL_MOTTU",
                column: "MotoId");

            migrationBuilder.CreateIndex(
                name: "IX_T_USUARIOS_MOTTU_MotoId",
                table: "T_USUARIOS_MOTTU",
                column: "MotoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_LOCALIZACAO_ATUAL_MOTTU");

            migrationBuilder.DropTable(
                name: "T_USUARIOS_MOTTU");

            migrationBuilder.DropTable(
                name: "T_MOTOS_MOTTU");
        }
    }
}
