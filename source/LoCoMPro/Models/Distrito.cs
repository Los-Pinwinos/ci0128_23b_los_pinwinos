using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(nombre), nameof(nombreCanton), nameof(nombreProvincia))]
    public class Distrito
    {
        // Nombre
        [StringLength(25, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Distrito")]
        public required string nombre { get; set; }

        // Cantón
        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Nombre cantón")]
        public required string nombreCanton { get; set; }

        // Nombre provincia
        [StringLength(10, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Nombre de la provincia")]
        public required string nombreProvincia { get; set; }

        [ForeignKey("nombreCanton, nombreProvincia")]
        public required Canton canton { get; set; }

        // Colecciones
        public ICollection<Usuario>? habitantes { get; set; }

        // TODO(Luis): Integrar con Tienda
        /*
        public ICollection<Tienda>? tiendas { get; set; }
        */
    }
}
