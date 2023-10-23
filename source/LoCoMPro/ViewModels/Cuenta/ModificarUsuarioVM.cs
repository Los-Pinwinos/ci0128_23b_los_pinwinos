using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LoCoMPro.Utils.Validadores;

namespace LoCoMPro.ViewModels.Cuenta
{
    // Modelo viata para modificar usuarios en el sistema
    public class ModificarUsuarioVM
    {
        // Nombre de usuario
        [Required(ErrorMessage = "Debe incluir un nombre de usuario")]
        [StringLength(20, MinimumLength = 5,
        ErrorMessage = "El nombre de usuario debe tener entre 5 y 20 caracteres")]
        public required string nombreDeUsuario { get; set; }

        // Correo eléctronico (único)
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debe incluir un correo electrónico")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        public required string correo { get; set; }

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