using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(nombre))]
    public class Provincia
    {
        // Nombre
        [StringLength(10, MinimumLength = 5)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Nombre de la provincia")]
        public required string nombre { get; set; }

        // Colección
        public ICollection<Canton>? cantones { get; set; }
    }
}
