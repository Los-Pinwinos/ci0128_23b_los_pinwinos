using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Cuenta
{
    // Modelo viata para ingresar al sistema con usuarios ya existentes
    public class IngresarUsuarioVM
    {
        // Nombre de usuario
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe incluir un nombre de usuario")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Formato de usuario inválido")]
        public string nombreDeUsuario { get; set; }

        // Contraseña
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe incluir una contraseña")]
        [StringLength(20, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-+_=*./\\%$#@!¡¿?()~])[-a-zA-Z\d+_=*./\\%$#@!¡¿?()~]+$",
            ErrorMessage =
            "Formato de contraseña inválido")]
        public string contrasena { get; set; }
    }
}