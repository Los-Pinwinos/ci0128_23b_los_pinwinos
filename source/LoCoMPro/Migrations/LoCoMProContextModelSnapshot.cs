﻿// <auto-generated />
using System;
using LoCoMPro.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LoCoMPro.Migrations
{
    [DbContext(typeof(LoCoMProContext))]
    partial class LoCoMProContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LoCoMPro.Models.Calificacion", b =>
                {
                    b.Property<string>("usuarioCalificador")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("creacionRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("usuarioCreadorRegistro")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UsuarionombreDeUsuario")
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("calificacion")
                        .HasColumnType("int");

                    b.HasKey("usuarioCalificador", "creacionRegistro", "usuarioCreadorRegistro");

                    b.HasIndex("UsuarionombreDeUsuario");

                    b.HasIndex("creacionRegistro", "usuarioCreadorRegistro");

                    b.ToTable("Calificaciones");
                });

            modelBuilder.Entity("LoCoMPro.Models.Canton", b =>
                {
                    b.Property<string>("nombre")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("nombreProvincia")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("nombre", "nombreProvincia");

                    b.HasIndex("nombreProvincia");

                    b.ToTable("Cantones");
                });

            modelBuilder.Entity("LoCoMPro.Models.Categoria", b =>
                {
                    b.Property<string>("nombre")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("nombre");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("LoCoMPro.Models.Distrito", b =>
                {
                    b.Property<string>("nombre")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("nombreCanton")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("nombreProvincia")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("nombre", "nombreCanton", "nombreProvincia");

                    b.HasIndex("nombreCanton", "nombreProvincia");

                    b.ToTable("Distritos");
                });

            modelBuilder.Entity("LoCoMPro.Models.Etiqueta", b =>
                {
                    b.Property<string>("etiqueta")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("creacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("usuarioCreador")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("etiqueta", "creacion", "usuarioCreador");

                    b.HasIndex("creacion", "usuarioCreador");

                    b.ToTable("Etiquetas");
                });

            modelBuilder.Entity("LoCoMPro.Models.Fotografia", b =>
                {
                    b.Property<string>("nombreFotografia")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("creacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("usuarioCreador")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<byte[]>("fotografia")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("nombreFotografia", "creacion", "usuarioCreador");

                    b.HasIndex("creacion", "usuarioCreador");

                    b.ToTable("Fotografias");
                });

            modelBuilder.Entity("LoCoMPro.Models.Producto", b =>
                {
                    b.Property<string>("nombre")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("marca")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("nombreCategoria")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("nombreUnidad")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("nombre");

                    b.HasIndex("nombreCategoria");

                    b.HasIndex("nombreUnidad");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("LoCoMPro.Models.Provincia", b =>
                {
                    b.Property<string>("nombre")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("nombre");

                    b.ToTable("Provincias");
                });

            modelBuilder.Entity("LoCoMPro.Models.Registro", b =>
                {
                    b.Property<DateTime>("creacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("usuarioCreador")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("calificacion")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("descripcion")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("nombreCanton")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("nombreDistrito")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("nombreProvincia")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("nombreTienda")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<decimal>("precio")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("productoAsociado")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("creacion", "usuarioCreador");

                    b.HasIndex("productoAsociado");

                    b.HasIndex("usuarioCreador");

                    b.HasIndex("nombreTienda", "nombreDistrito", "nombreCanton", "nombreProvincia");

                    b.ToTable("Registros");
                });

            modelBuilder.Entity("LoCoMPro.Models.Reporte", b =>
                {
                    b.Property<string>("usuarioCreadorReporte")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("creacionRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("usuarioCreadorRegistro")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UsuarionombreDeUsuario")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("comentario")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("creacion")
                        .HasColumnType("datetime2");

                    b.HasKey("usuarioCreadorReporte", "creacionRegistro", "usuarioCreadorRegistro");

                    b.HasIndex("UsuarionombreDeUsuario");

                    b.HasIndex("creacionRegistro", "usuarioCreadorRegistro");

                    b.ToTable("Reportes");
                });

            modelBuilder.Entity("LoCoMPro.Models.Tienda", b =>
                {
                    b.Property<string>("nombre")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("nombreDistrito")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("nombreCanton")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("nombreProvincia")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("nombre", "nombreDistrito", "nombreCanton", "nombreProvincia");

                    b.HasIndex("nombreDistrito", "nombreCanton", "nombreProvincia");

                    b.ToTable("Tiendas");
                });

            modelBuilder.Entity("LoCoMPro.Models.Unidad", b =>
                {
                    b.Property<string>("nombre")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("nombre");

                    b.ToTable("Unidades");
                });

            modelBuilder.Entity("LoCoMPro.Models.Usuario", b =>
                {
                    b.Property<string>("nombreDeUsuario")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Distritonombre")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("DistritonombreCanton")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("DistritonombreProvincia")
                        .HasColumnType("nvarchar(10)");

                    b.Property<double>("calificacion")
                        .HasColumnType("float");

                    b.Property<string>("cantonVivienda")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("distritoVivienda")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("esAdministrador")
                        .HasColumnType("bit");

                    b.Property<bool>("esModerador")
                        .HasColumnType("bit");

                    b.Property<string>("estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("hashContrasena")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("provinciaVivienda")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("nombreDeUsuario");

                    b.HasIndex("Distritonombre", "DistritonombreCanton", "DistritonombreProvincia");

                    b.HasIndex("distritoVivienda", "cantonVivienda", "provinciaVivienda");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("LoCoMPro.Models.Calificacion", b =>
                {
                    b.HasOne("LoCoMPro.Models.Usuario", null)
                        .WithMany("calificaciones")
                        .HasForeignKey("UsuarionombreDeUsuario");

                    b.HasOne("LoCoMPro.Models.Usuario", "calificador")
                        .WithMany()
                        .HasForeignKey("usuarioCalificador")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LoCoMPro.Models.Registro", "registro")
                        .WithMany("calificaciones")
                        .HasForeignKey("creacionRegistro", "usuarioCreadorRegistro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("calificador");

                    b.Navigation("registro");
                });

            modelBuilder.Entity("LoCoMPro.Models.Canton", b =>
                {
                    b.HasOne("LoCoMPro.Models.Provincia", "provincia")
                        .WithMany("cantones")
                        .HasForeignKey("nombreProvincia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("provincia");
                });

            modelBuilder.Entity("LoCoMPro.Models.Distrito", b =>
                {
                    b.HasOne("LoCoMPro.Models.Canton", "canton")
                        .WithMany("distritos")
                        .HasForeignKey("nombreCanton", "nombreProvincia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("canton");
                });

            modelBuilder.Entity("LoCoMPro.Models.Etiqueta", b =>
                {
                    b.HasOne("LoCoMPro.Models.Registro", "registro")
                        .WithMany("etiquetas")
                        .HasForeignKey("creacion", "usuarioCreador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("registro");
                });

            modelBuilder.Entity("LoCoMPro.Models.Fotografia", b =>
                {
                    b.HasOne("LoCoMPro.Models.Registro", "registro")
                        .WithMany("fotografias")
                        .HasForeignKey("creacion", "usuarioCreador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("registro");
                });

            modelBuilder.Entity("LoCoMPro.Models.Producto", b =>
                {
                    b.HasOne("LoCoMPro.Models.Categoria", "categoria")
                        .WithMany("productos")
                        .HasForeignKey("nombreCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoCoMPro.Models.Unidad", "unidad")
                        .WithMany("productos")
                        .HasForeignKey("nombreUnidad")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("categoria");

                    b.Navigation("unidad");
                });

            modelBuilder.Entity("LoCoMPro.Models.Registro", b =>
                {
                    b.HasOne("LoCoMPro.Models.Producto", "producto")
                        .WithMany("registros")
                        .HasForeignKey("productoAsociado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoCoMPro.Models.Usuario", "creador")
                        .WithMany("registros")
                        .HasForeignKey("usuarioCreador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoCoMPro.Models.Tienda", "tienda")
                        .WithMany("registros")
                        .HasForeignKey("nombreTienda", "nombreDistrito", "nombreCanton", "nombreProvincia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("creador");

                    b.Navigation("producto");

                    b.Navigation("tienda");
                });

            modelBuilder.Entity("LoCoMPro.Models.Reporte", b =>
                {
                    b.HasOne("LoCoMPro.Models.Usuario", null)
                        .WithMany("reportes")
                        .HasForeignKey("UsuarionombreDeUsuario");

                    b.HasOne("LoCoMPro.Models.Usuario", "creadorReporte")
                        .WithMany()
                        .HasForeignKey("usuarioCreadorReporte")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LoCoMPro.Models.Registro", "registro")
                        .WithMany("reportes")
                        .HasForeignKey("creacionRegistro", "usuarioCreadorRegistro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("creadorReporte");

                    b.Navigation("registro");
                });

            modelBuilder.Entity("LoCoMPro.Models.Tienda", b =>
                {
                    b.HasOne("LoCoMPro.Models.Distrito", "distrito")
                        .WithMany("tiendas")
                        .HasForeignKey("nombreDistrito", "nombreCanton", "nombreProvincia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("distrito");
                });

            modelBuilder.Entity("LoCoMPro.Models.Usuario", b =>
                {
                    b.HasOne("LoCoMPro.Models.Distrito", null)
                        .WithMany("habitantes")
                        .HasForeignKey("Distritonombre", "DistritonombreCanton", "DistritonombreProvincia");

                    b.HasOne("LoCoMPro.Models.Distrito", "vivienda")
                        .WithMany()
                        .HasForeignKey("distritoVivienda", "cantonVivienda", "provinciaVivienda")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("vivienda");
                });

            modelBuilder.Entity("LoCoMPro.Models.Canton", b =>
                {
                    b.Navigation("distritos");
                });

            modelBuilder.Entity("LoCoMPro.Models.Categoria", b =>
                {
                    b.Navigation("productos");
                });

            modelBuilder.Entity("LoCoMPro.Models.Distrito", b =>
                {
                    b.Navigation("habitantes");

                    b.Navigation("tiendas");
                });

            modelBuilder.Entity("LoCoMPro.Models.Producto", b =>
                {
                    b.Navigation("registros");
                });

            modelBuilder.Entity("LoCoMPro.Models.Provincia", b =>
                {
                    b.Navigation("cantones");
                });

            modelBuilder.Entity("LoCoMPro.Models.Registro", b =>
                {
                    b.Navigation("calificaciones");

                    b.Navigation("etiquetas");

                    b.Navigation("fotografias");

                    b.Navigation("reportes");
                });

            modelBuilder.Entity("LoCoMPro.Models.Tienda", b =>
                {
                    b.Navigation("registros");
                });

            modelBuilder.Entity("LoCoMPro.Models.Unidad", b =>
                {
                    b.Navigation("productos");
                });

            modelBuilder.Entity("LoCoMPro.Models.Usuario", b =>
                {
                    b.Navigation("calificaciones");

                    b.Navigation("registros");

                    b.Navigation("reportes");
                });
#pragma warning restore 612, 618
        }
    }
}
