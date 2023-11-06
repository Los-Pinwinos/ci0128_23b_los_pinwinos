using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoCoMPro.Migrations
{
    /// <inheritdoc />
    public partial class EntidadesCompletasSprint2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    nombre = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.nombre);
                });

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    nombre = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.nombre);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.nombre);
                });

            migrationBuilder.CreateTable(
                name: "Cantones",
                columns: table => new
                {
                    nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    nombreProvincia = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cantones", x => new { x.nombre, x.nombreProvincia });
                    table.ForeignKey(
                        name: "FK_Cantones_Provincias_nombreProvincia",
                        column: x => x.nombreProvincia,
                        principalTable: "Provincias",
                        principalColumn: "nombre",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    nombre = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    marca = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    nombreUnidad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    nombreCategoria = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.nombre);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_nombreCategoria",
                        column: x => x.nombreCategoria,
                        principalTable: "Categorias",
                        principalColumn: "nombre",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Productos_Unidades_nombreUnidad",
                        column: x => x.nombreUnidad,
                        principalTable: "Unidades",
                        principalColumn: "nombre",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Distritos",
                columns: table => new
                {
                    nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    nombreCanton = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    nombreProvincia = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distritos", x => new { x.nombre, x.nombreCanton, x.nombreProvincia });
                    table.ForeignKey(
                        name: "FK_Distritos_Cantones_nombreCanton_nombreProvincia",
                        columns: x => new { x.nombreCanton, x.nombreProvincia },
                        principalTable: "Cantones",
                        principalColumns: new[] { "nombre", "nombreProvincia" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tiendas",
                columns: table => new
                {
                    nombre = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    nombreDistrito = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    nombreCanton = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    nombreProvincia = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiendas", x => new { x.nombre, x.nombreDistrito, x.nombreCanton, x.nombreProvincia });
                    table.ForeignKey(
                        name: "FK_Tiendas_Distritos_nombreDistrito_nombreCanton_nombreProvincia",
                        columns: x => new { x.nombreDistrito, x.nombreCanton, x.nombreProvincia },
                        principalTable: "Distritos",
                        principalColumns: new[] { "nombre", "nombreCanton", "nombreProvincia" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    nombreDeUsuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hashContrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    calificacion = table.Column<double>(type: "float", nullable: false),
                    distritoVivienda = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    cantonVivienda = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    provinciaVivienda = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    esAdministrador = table.Column<bool>(type: "bit", nullable: false),
                    esModerador = table.Column<bool>(type: "bit", nullable: false),
                    Distritonombre = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    DistritonombreCanton = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    DistritonombreProvincia = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.nombreDeUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Distritos_Distritonombre_DistritonombreCanton_DistritonombreProvincia",
                        columns: x => new { x.Distritonombre, x.DistritonombreCanton, x.DistritonombreProvincia },
                        principalTable: "Distritos",
                        principalColumns: new[] { "nombre", "nombreCanton", "nombreProvincia" });
                    table.ForeignKey(
                        name: "FK_Usuario_Distritos_distritoVivienda_cantonVivienda_provinciaVivienda",
                        columns: x => new { x.distritoVivienda, x.cantonVivienda, x.provinciaVivienda },
                        principalTable: "Distritos",
                        principalColumns: new[] { "nombre", "nombreCanton", "nombreProvincia" },
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Registros",
                columns: table => new
                {
                    creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreador = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    calificacion = table.Column<double>(type: "float", nullable: false),
                    productoAsociado = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    nombreTienda = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    nombreDistrito = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    nombreCanton = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    nombreProvincia = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registros", x => new { x.creacion, x.usuarioCreador });
                    table.ForeignKey(
                        name: "FK_Registros_Productos_productoAsociado",
                        column: x => x.productoAsociado,
                        principalTable: "Productos",
                        principalColumn: "nombre",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registros_Tiendas_nombreTienda_nombreDistrito_nombreCanton_nombreProvincia",
                        columns: x => new { x.nombreTienda, x.nombreDistrito, x.nombreCanton, x.nombreProvincia },
                        principalTable: "Tiendas",
                        principalColumns: new[] { "nombre", "nombreDistrito", "nombreCanton", "nombreProvincia" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registros_Usuario_usuarioCreador",
                        column: x => x.usuarioCreador,
                        principalTable: "Usuario",
                        principalColumn: "nombreDeUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Etiquetas",
                columns: table => new
                {
                    etiqueta = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreador = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiquetas", x => new { x.etiqueta, x.creacion, x.usuarioCreador });
                    table.ForeignKey(
                        name: "FK_Etiquetas_Registros_creacion_usuarioCreador",
                        columns: x => new { x.creacion, x.usuarioCreador },
                        principalTable: "Registros",
                        principalColumns: new[] { "creacion", "usuarioCreador" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fotografias",
                columns: table => new
                {
                    nombreFotografia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreador = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    fotografia = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotografias", x => new { x.nombreFotografia, x.creacion, x.usuarioCreador });
                    table.ForeignKey(
                        name: "FK_Fotografias_Registros_creacion_usuarioCreador",
                        columns: x => new { x.creacion, x.usuarioCreador },
                        principalTable: "Registros",
                        principalColumns: new[] { "creacion", "usuarioCreador" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reportes",
                columns: table => new
                {
                    usuarioCreadorReporte = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    usuarioCreadorRegistro = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    creacionRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    comentario = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    verificado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarionombreDeUsuario = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportes", x => new { x.usuarioCreadorReporte, x.creacionRegistro, x.usuarioCreadorRegistro });
                    table.ForeignKey(
                        name: "FK_Reportes_Registros_creacionRegistro_usuarioCreadorRegistro",
                        columns: x => new { x.creacionRegistro, x.usuarioCreadorRegistro },
                        principalTable: "Registros",
                        principalColumns: new[] { "creacion", "usuarioCreador" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reportes_Usuario_UsuarionombreDeUsuario",
                        column: x => x.UsuarionombreDeUsuario,
                        principalTable: "Usuario",
                        principalColumn: "nombreDeUsuario");
                    table.ForeignKey(
                        name: "FK_Reportes_Usuario_usuarioCreadorReporte",
                        column: x => x.usuarioCreadorReporte,
                        principalTable: "Usuario",
                        principalColumn: "nombreDeUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_creacionRegistro_usuarioCreadorRegistro",
                table: "Calificaciones",
                columns: new[] { "creacionRegistro", "usuarioCreadorRegistro" });

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_UsuarionombreDeUsuario",
                table: "Calificaciones",
                column: "UsuarionombreDeUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Cantones_nombreProvincia",
                table: "Cantones",
                column: "nombreProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_Distritos_nombreCanton_nombreProvincia",
                table: "Distritos",
                columns: new[] { "nombreCanton", "nombreProvincia" });

            migrationBuilder.CreateIndex(
                name: "IX_Etiquetas_creacion_usuarioCreador",
                table: "Etiquetas",
                columns: new[] { "creacion", "usuarioCreador" });

            migrationBuilder.CreateIndex(
                name: "IX_Fotografias_creacion_usuarioCreador",
                table: "Fotografias",
                columns: new[] { "creacion", "usuarioCreador" });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_nombreCategoria",
                table: "Productos",
                column: "nombreCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_nombreUnidad",
                table: "Productos",
                column: "nombreUnidad");

            migrationBuilder.CreateIndex(
                name: "IX_Registros_nombreTienda_nombreDistrito_nombreCanton_nombreProvincia",
                table: "Registros",
                columns: new[] { "nombreTienda", "nombreDistrito", "nombreCanton", "nombreProvincia" });

            migrationBuilder.CreateIndex(
                name: "IX_Registros_productoAsociado",
                table: "Registros",
                column: "productoAsociado");

            migrationBuilder.CreateIndex(
                name: "IX_Registros_usuarioCreador",
                table: "Registros",
                column: "usuarioCreador");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_creacionRegistro_usuarioCreadorRegistro",
                table: "Reportes",
                columns: new[] { "creacionRegistro", "usuarioCreadorRegistro" });

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_UsuarionombreDeUsuario",
                table: "Reportes",
                column: "UsuarionombreDeUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Tiendas_nombreDistrito_nombreCanton_nombreProvincia",
                table: "Tiendas",
                columns: new[] { "nombreDistrito", "nombreCanton", "nombreProvincia" });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Distritonombre_DistritonombreCanton_DistritonombreProvincia",
                table: "Usuario",
                columns: new[] { "Distritonombre", "DistritonombreCanton", "DistritonombreProvincia" });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_distritoVivienda_cantonVivienda_provinciaVivienda",
                table: "Usuario",
                columns: new[] { "distritoVivienda", "cantonVivienda", "provinciaVivienda" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "Etiquetas");

            migrationBuilder.DropTable(
                name: "Fotografias");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "Registros");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Tiendas");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Unidades");

            migrationBuilder.DropTable(
                name: "Distritos");

            migrationBuilder.DropTable(
                name: "Cantones");

            migrationBuilder.DropTable(
                name: "Provincias");
        }
    }
}
