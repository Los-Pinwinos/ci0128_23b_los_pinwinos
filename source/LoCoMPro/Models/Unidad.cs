using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(nombre))]
    public class Unidad
    {
        // Nombre
        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Unidad de medida")]
        public required string nombre { get; set; }

        // Collecciones
        public ICollection<Producto>? productos { get; set; }
    }
}