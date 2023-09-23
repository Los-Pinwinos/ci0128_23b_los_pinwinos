using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(nombre))]
    public class Producto
    {
        // Nombre
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Nombre del producto")]
        public required string nombre { get; set; }

        // Marca
        [StringLength(256, MinimumLength = 0)]
        [RegularExpression(@"")]
        [Display(Name = "Marca del producto")]
        public string? marca { get; set; }

        // Nombre de la unidad
        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Unidad de medida")]
        public required string nombreUnidad { get; set; }

        // Propiedad de navegación unidad
        [ForeignKey("nombreUnidad")]
        public Unidad? unidad { get; set; }

        // Nombre de la categoría
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Categoría")]
        public required string nombreCategoria { get; set; }

        // Propiedad de navegación categoría
        [ForeignKey("nombreCategoria")]
        public Categoria? categoria { get; set; }

        // Colección
        public ICollection<Registro>? registros { get; set; }
    }
}
