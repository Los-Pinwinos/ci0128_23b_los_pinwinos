using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LoCoMPro.Models
{
    // [PrimaryKey(nameof(usuarioAdminstrador), nameof(usuarioOrdinario), nameof(accion))]
    public class AdministraSobre
    {
        // TODO(Los Pinwinos): Implementar esto
        /*
        // Usuario adminstrador
        [StringLength(12, MinimumLength = 10)]
        [Display(Name = "Usuario adminstrador")]
        public required string usuarioAdminstrador { get; set; }

        // Propiedad de navegaci�n adminstrador
        [ForeignKey("usuarioAdminstrador")]
        public required Administrador administrador { get; set; }

        // Usuario ordinario
        [StringLength(12, MinimumLength = 10)]
        [Display(Name = "Usuario adminstrado")]
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