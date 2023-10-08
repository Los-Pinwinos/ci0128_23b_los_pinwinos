using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Cuenta
{
    // Modelo viata para ingresar al sistema con usuarios ya existentes
    public class IngresarUsuarioVM
    {
        // Nombre de usuario
        [Required(ErrorMessage = "Debe incluir un nombre de usuario")]
        [StringLength(20, MinimumLength = 5,
            ErrorMessage = "Nombre de usuario inválido")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-+_=*./\\%$#@!¡¿?()~])[-a-zA-Z\d+_=*./\\%$#@!¡¿?()~]+$",
            ErrorMessage = "Nombre de usuario inválido")]
        public required string nombreDeUsuario { get; set; }

        // Contraseña
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe incluir una contraseña")]
        [StringLength(20, MinimumLength = 8,
            ErrorMessage = "Contraseña inválida")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-+_=*./\\%$#@!¡¿?()~])[-a-zA-Z\d+_=*./\\%$#@!¡¿?()~]+$",
            ErrorMessage = "Contraseña inválida")]
        public required string contrasena { get; set; }
    }
}