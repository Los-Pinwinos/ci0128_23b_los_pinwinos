using System.ComponentModel.DataAnnotations;
using LoCoMPro.Utils.Validadores;

namespace LoCoMPro.ViewModels.Cuenta
{
    // Modelo viata para agregar al sistema nuevos usuarios
    public class CrearUsuarioVM
    {
        // Nombre de usuario
        [Required(ErrorMessage = "Debe incluir un nombre de usuario")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "El nombre de usuario debe tener entre 10 y 12 carácteres")]
        public required string nombreDeUsuario { get; set; }

        // Correo eléctronico (único)
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debe incluir un correo electrónico")]
        [EmailAddress(ErrorMessage = "Formato de correo electronico inválido")]
        public required string correo { get; set; }

        // Contraseña
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe incluir una contraseña")]
        [StringLength(8, ErrorMessage = "La contraseña debe tener exactamente 8 carácteres")]
        [DebeCoincidir("confirmarContrasena", ErrorMessage = "Las contraseñas ingresadas deben ser iguales")]
        public required string contrasena { get; set; }

        // Repetición de la contraseña para confirmación
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe incluir una confirmación de la contraseña")]
        [StringLength(8, ErrorMessage = "La contraseña debe tener exactamente 8 carácteres")]
        public required string confirmarContrasena { get; set; }

        // Nombre de la provincia donde vive el usuario
        [StringLength(10, MinimumLength = 1)]
        public string? provinciaVivienda { get; set; }

        // Nombre del cantón donde vive el usuario
        [StringLength(20, MinimumLength = 1)]
        public string? cantonVivienda { get; set; }

        // Nombre del distrito donde vive el usuario
        [StringLength(25, MinimumLength = 1)]
        public string? distritoVivienda { get; set; }
    }
}
