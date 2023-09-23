using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(nombre), nameof(nombreDistrito), nameof(nombreCanton), nameof(nombreProvincia))]
    public class Tienda
    {
        // Nombre
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Nombre de la tienda")]
        public required string nombre { get; set; }

        // Nombre distrito
        [StringLength(25, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Nombre del distrito")]
        public required string nombreDistrito { get; set; }

        // Nombre cantón
        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Nombre del distrito")]
        public required string nombreCanton { get; set; }

        // Nombre provincia
        [StringLength(10, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Nombre de la provincia")]
        public required string nombreProvincia { get; set; }

        // Propiedad de navegación distrito
        [ForeignKey("nombreDistrito, nombreCanton, nombreProvincia")]
        [Display(Name = "Distrito")]
        public Distrito? distrito { get; set; }

        // Colección
        // TODO (Emilia): Integrar con Registro
        /*
        public ICollection<Registro>? registros { get; set; }
        */

        // Coordenadas
        // TODO(Los Pinwinos): coordenadas
    }
}
