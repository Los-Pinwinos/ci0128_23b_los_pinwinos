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
        public DbSet<LoCoMPro.Models.Registro> Registros { get; set; }
        public DbSet<LoCoMPro.Models.Producto> Productos { get; set; }
        public DbSet<LoCoMPro.Models.Unidad> Unidades { get; set; }

        public DbSet<LoCoMPro.Models.Usuario> Usuarios { get; set; }
        public DbSet<LoCoMPro.Models.Moderador> Moderadores { get; set; }
        public DbSet<LoCoMPro.Models.Administrador> Administradores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasOne(c => c.vivienda)
                .WithMany() // Assuming a one-to-many relationship
                .HasForeignKey(c => new { c.distritoVivienda, c.cantonVivienda, c.provinciaVivienda })
                .OnDelete(DeleteBehavior.SetNull); // Specify the "on delete set null" behavior for the composite foreign key
        }
    }
}