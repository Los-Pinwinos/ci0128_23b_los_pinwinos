using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(nombre))]
    public class Categoria
    {
        // Nombre
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Categoría")]
        public required string nombre { get; set; }

        // Colecciones
        public ICollection<Producto>? productos { get; set; }

        // TODO(Los pinwino): Arreglar
        /*
        public ICollection<CategoriaPertenece>? categoriasPadre { get; set; }

        public ICollection<CategoriaPertenece>? categoriasHijo { get; set; }
        */
    }
}
