using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Cuenta
{
    public class FavoritoVM
    {
        // Nombre
        [StringLength(256, MinimumLength = 1)]
        [Display(Name = "Nombre del producto")]
        public required string nombreProducto { get; set; }

        // Nombre de la categoría
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Categoría")]
        public required string nombreCategoria { get; set; }

        // Marca
        [StringLength(256, MinimumLength = 1)]
        [Display(Name = "Marca del producto")]
        public string? nombreMarca { get; set; }
    }
}
