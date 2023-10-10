using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

// TODO(Los Pinwinos): Implementar

namespace LoCoMPro.Models
{
    //[PrimaryKey(nameof(categoriaPadre), nameof(categoriaHijo))]
    public class CategoriaPertenece
    {
        // TODO(Los pinwinos): Arreglar
        /*
        // Categoria padre
        [StringLength(1, MinimumLength = 1)]
        [RegularExpression(@"")]
         [Display(Name = "Categor�a padre")]
        public required string categoriaPadre { get; set; }

        [ForeignKey("categoriaPadre")]
        public required Categoria catPadre { get; set; }


        // Categoria hijo
        [StringLength(1, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Categor�a hijo")]
        public required string categoriaHijo { get; set; }

        [ForeignKey("categoriaHijo")]
        public required Categoria catHijo { get; set; }
        */
    }
}