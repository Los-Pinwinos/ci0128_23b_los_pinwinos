using LoCoMPro.Models;
using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Moderacion
{
    public class ProductosSimilaresVM
    {
        // Nombre
        [StringLength(256, MinimumLength = 1)]
        [Display(Name = "Producto con similares")]
        public required string nombreProducto { get; set; }

        // Nombre de la categoría
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Categoría")]
        public required string nombreCategoria { get; set; }

        // Marca
        [StringLength(256, MinimumLength = 1)]
        [Display(Name = "Marca")]
        public string? nombreMarca { get; set; }

        // Unidad
        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Unidad")]
        public required string unidad { get; set; }
    }
}
