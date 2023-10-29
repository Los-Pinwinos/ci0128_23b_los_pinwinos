using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoCoMPro.Migrations
{
    /// <inheritdoc />
    public partial class CambiosCalificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarionombreDeUsuario",
                table: "Reportes",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "calificacion",
                table: "Registros",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    usuarioCalificador = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    usuarioCreadorRegistro = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    creacionRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    calificacion = table.Column<int>(type: "int", nullable: false),
                    UsuarionombreDeUsuario = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => new { x.usuarioCalificador, x.creacionRegistro, x.usuarioCreadorRegistro });
                    table.ForeignKey(
                        name: "FK_Calificaciones_Registros_creacionRegistro_usuarioCreadorRegistro",
                        columns: x => new { x.creacionRegistro, x.usuarioCreadorRegistro },
                        principalTable: "Registros",
                        principalColumns: new[] { "creacion", "usuarioCreador" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Usuario_UsuarionombreDeUsuario",
                        column: x => x.UsuarionombreDeUsuario,
                        principalTable: "Usuario",
                        principalColumn: "nombreDeUsuario");
                    table.ForeignKey(
                        name: "FK_Calificaciones_Usuario_usuarioCalificador",
                        column: x => x.usuarioCalificador,
                        principalTable: "Usuario",
                        principalColumn: "nombreDeUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_UsuarionombreDeUsuario",
                table: "Reportes",
                column: "UsuarionombreDeUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_creacionRegistro_usuarioCreadorRegistro",
                table: "Calificaciones",
                columns: new[] { "creacionRegistro", "usuarioCreadorRegistro" });

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_UsuarionombreDeUsuario",
                table: "Calificaciones",
                column: "UsuarionombreDeUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Reportes_Usuario_UsuarionombreDeUsuario",
                table: "Reportes",
                column: "UsuarionombreDeUsuario",
                principalTable: "Usuario",
                principalColumn: "nombreDeUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reportes_Usuario_UsuarionombreDeUsuario",
                table: "Reportes");

            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Reportes_UsuarionombreDeUsuario",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "UsuarionombreDeUsuario",
                table: "Reportes");

            migrationBuilder.AlterColumn<double>(
                name: "calificacion",
                table: "Registros",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
