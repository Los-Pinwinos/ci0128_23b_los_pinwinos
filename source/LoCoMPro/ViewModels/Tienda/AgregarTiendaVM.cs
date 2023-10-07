using LoCoMPro.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.ViewModels.Tienda
{
    public class AgregarTiendaVM
    {
        // Nombre
        [Required(ErrorMessage = "Debe incluir un nombre de la tienda")]
        [StringLength(256, MinimumLength = 1)]
        public required string nombre { get; set; }

        // Nombre distrito
        [Required(ErrorMessage = "Debe incluir un distrito")]
        [StringLength(25, MinimumLength = 1)]
        public required string nombreDistrito { get; set; }

        // Nombre cantón
        [Required(ErrorMessage = "Debe incluir un cantón")]
        [StringLength(20, MinimumLength = 1)]
        public required string nombreCanton { get; set; }

        // Nombre provincia
        [Required(ErrorMessage = "Debe incluir una provincia")]
        [StringLength(10, MinimumLength = 1)]
        public required string nombreProvincia { get; set; }
    }
}
