using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.Models
{
    // [PrimaryKey(nameof(usuarioModerador), nameof(usuarioOrdinario), nameof(accion))]
    public class ModeraSobre
    {
        // TODO(Kenneth): Implementar esto
        /*
        // Usuario moderador
        [StringLength(12, MinimumLength = 10)]
        [Display(Name = "Usuario moderador")]
        public required string usuarioModerador { get; set; }

        // Propiedad de navegaci�n moderador
        [ForeignKey("usuarioModerador")]
        public required Moderador moderador { get; set; }

        // Usuario ordinario
        [StringLength(12, MinimumLength = 10)]
        [Display(Name = "Usuario moderado")]
        public required string usuarioOrdinario { get; set; }

        // Propiedad de navegaci�n ordinario
        [ForeignKey("usuarioOrdinario")]
        public required Usuario ordinario { get; set; }

        // Acci�n
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Acci�n")]
        public required string accion { get; set; }
        */
    }
}