using LoCoMPro.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.ViewModels.Moderacion
{
    public class RegistroVM
    {
        // Producto del registro
        [Display(Name = "Producto")]
        public required string producto { get; set; }

        // Precio del registro
        [Display(Name = "Precio")]
        public required decimal precio { get; set; }

        // Tienda donde estaba el producto
        [Display(Name = "Tienda")]
        public required string tienda { get; set; }

        // Fecha de creación del registro
        [Display(Name = "Fecha")]
        public required DateTime fecha { get; set; }

        // Unidad del producto
        [Display(Name = "Unidad")]
        public required string unidad { get; set; }

        // Nombre de la provincia donde estaba la tienda
        [Display(Name = "Provincia")]
        public required string provincia { get; set; }

        // Usuario creador del registro
        [Display(Name = "Usuario")]
        public required string usuario { get; set; }

        // Marca del producto
        [Display(Name = "Marca")]
        public string? marca { get; set; }

        // Nombre del cantón donde estaba la tienda
        [Display(Name = "Cantón")]
        public required string canton { get; set; }

        // Calificación del usuario creador del registro
        [Display(Name = "Calificación")]
        public double calificacionCreador { get; set; }

        // Cantidad de calificaciones del creador del registro
        [Display(Name = "Cantidad de calificaciones")]
        public int cantidadCalificacionesCreador { get; set; }

        // Nombre de la categoría del producto
        [Display(Name = "Categoría")]
        public required string categoria { get; set; }

        // Descripción del registro
        [Display(Name = "Descripción")]
        public string? descripcion { get; set; }

        // Calificación del registro
        [Display(Name = "Calificación")]
        public double calificacionRegistro { get; set; }

        // Cantidad de calificaciones del registro del registro
        [Display(Name = "Cantidad de calificaciones")]
        public int cantidadCalificacionesRegistro { get; set; }
    }
}