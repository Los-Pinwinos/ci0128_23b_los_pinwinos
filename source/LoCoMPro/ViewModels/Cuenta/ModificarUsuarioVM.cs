using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LoCoMPro.Utils.Validadores;

namespace LoCoMPro.ViewModels.Cuenta
{
    // Modelo viata para modificar usuarios en el sistema
    public class ModificarUsuarioVM
    {
        // Nombre de usuario
        public string? nombreDeUsuario { get; set; }

        // Correo eléctronico (único)
        public string? correo { get; set; }

        // Nombre de la provincia donde vive el usuario
        [StringLength(10, MinimumLength = 1)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        public string? provinciaVivienda { get; set; }

        // Nombre del cantón donde vive el usuario
        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        public string? cantonVivienda { get; set; }

        // Nombre del distrito donde vive el usuario
        [StringLength(25, MinimumLength = 1)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        public string? distritoVivienda { get; set; }
    }
}