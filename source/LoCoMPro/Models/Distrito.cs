using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(nombre), nameof(nombreCanton), nameof(nombreProvincia))]
    public class Distrito
    {
        // Nombre
        [StringLength(30, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Distrito")]
        public required string nombre { get; set; }

        // Cantón
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Nombre cantón")]
        public required string nombreCanton { get; set; }

        // Nombre provincia
        [StringLength(10, MinimumLength = 5)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Nombre de la provincia")]
        public required string nombreProvincia { get; set; }

        // Propiedad de navegación cantón
        [ForeignKey("nombreCanton, nombreProvincia")]
        public Canton? canton { get; set; }

        // Colecciones
        public ICollection<Usuario>? habitantes { get; set; }
        public ICollection<Tienda>? tiendas { get; set; }
    }
}
