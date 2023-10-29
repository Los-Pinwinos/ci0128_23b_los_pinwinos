using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoCoMPro.Models;

namespace LoCoMPro.Data
{
    public class LoCoMProContext : DbContext
    {
        public LoCoMProContext(DbContextOptions<LoCoMProContext> options)
            : base(options)
        {

        }

        public DbSet<LoCoMPro.Models.Canton> Cantones { get; set; }
        public DbSet<LoCoMPro.Models.Distrito> Distritos { get; set; }
        public DbSet<LoCoMPro.Models.Provincia> Provincias { get; set; }
        public DbSet<LoCoMPro.Models.Tienda> Tiendas { get; set; }

        public DbSet<LoCoMPro.Models.Categoria> Categorias { get; set; }
        public DbSet<LoCoMPro.Models.Etiqueta> Etiquetas { get; set; }
        public DbSet<LoCoMPro.Models.Fotografia> Fotografias { get; set; }
        public DbSet<LoCoMPro.Models.Reporte> Reportes { get; set; }
        public DbSet<LoCoMPro.Models.Calificacion> Calificaciones { get; set; }
        public DbSet<LoCoMPro.Models.Registro> Registros { get; set; }
        public DbSet<LoCoMPro.Models.Producto> Productos { get; set; }
        public DbSet<LoCoMPro.Models.Unidad> Unidades { get; set; }

        public DbSet<LoCoMPro.Models.Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasOne(c => c.vivienda)
                .WithMany()
                .HasForeignKey(c => new { c.distritoVivienda, c.cantonVivienda, c.provinciaVivienda })
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Reporte>()
                .HasOne(c => c.creadorReporte)
                .WithMany()
                .HasForeignKey(c => new { c.usuarioCreadorReporte })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.calificador)
                .WithMany()
                .HasForeignKey(c => new { c.usuarioCalificador })
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}