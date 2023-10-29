using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(nombre))]
    public class Categoria
    {
        // Nombre
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Categoría")]
        public required string nombre { get; set; }

        // Colecciones
        public ICollection<Producto>? productos { get; set; }
    }
}