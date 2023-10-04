using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Cuenta
{
    // Modelo viata para ingresar al sistema con usuarios ya existentes
    public class IngresarUsuarioVM
    {
        // Nombre de usuario
        [Required(ErrorMessage = "Debe incluir un nombre de usuario")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "El nombre de usuario debe tener entre 10 y 12 carácteres")]
        public required string nombreDeUsuario { get; set; }

        // Contraseña
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe incluir una contraseña")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "La contraseña debe tener exactamente 8 carácteres")]
        public required string contrasena { get; set; }
    }
}
