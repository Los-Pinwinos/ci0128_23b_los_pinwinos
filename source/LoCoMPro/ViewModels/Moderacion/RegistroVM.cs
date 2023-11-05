using LoCoMPro.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.ViewModels.Moderacion
{
    public class RegistroVM
    {
        // Producto del registro
        [StringLength(256, MinimumLength = 1)]
        [Display(Name = "Producto")]
        public required string producto { get; set; }

        // Precio del registro
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Precio")]
        public required decimal precio { get; set; }

        // Tienda donde estaba el producto
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ0-9#])*")]
        [Display(Name = "Tienda")]
        public required string tienda { get; set; }

        // Fecha de creación del registro
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "1/2/2000", "1/1/2200")]
        [Display(Name = "Fecha")]
        public required DateTime fecha { get; set; }

        // Unidad del producto
        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Unidad")]
        public required string unidad { get; set; }

        // Nombre de la provincia donde estaba la tienda
        [StringLength(10, MinimumLength = 5)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Provincia")]
        public required string provincia { get; set; }

        // Usuario creador del registro
        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "Usuario")]
        public required string usuario { get; set; }

        // Marca del producto
        [StringLength(256, MinimumLength = 1)]
        [Display(Name = "Marca")]
        public string? marca { get; set; }

        // Nombre del cantón donde estaba la tienda
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Cantón")]
        public required string canton { get; set; }

        // Calificación del usuario creador del registro
        [Range(0, 5)]
        [Display(Name = "Calificación")]
        public decimal calificacionCreador { get; set; }

        // Nombre de la categoría del producto
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Categoría")]
        public required string categoria { get; set; }

        // Descripción del registro
        [DataType(DataType.MultilineText)]
        [StringLength(150, MinimumLength = 1)]
        [Display(Name = "Descripción")]
        public string? descripcion { get; set; }

        // Calificación del registro
        [Range(0, 5)]
        [Display(Name = "Calificación")]
        public decimal calificacionRegistro { get; set; }
    }
}