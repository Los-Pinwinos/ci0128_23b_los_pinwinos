using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Cuenta
{
    // Modelo viata para ingresar al sistema con usuarios ya existentes
    public class IngresarUsuarioVM
    {
        // Nombre de usuario
        [Required(ErrorMessage = "Debe incluir un nombre de usuario")]
        [StringLength(20, MinimumLength = 5,
        ErrorMessage = "El nombre de usuario debe tener entre 5 y 20 caracteres")]
        public required string nombreDeUsuario { get; set; }

        // Contraseña
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe incluir una contraseña")]
        [StringLength(20, MinimumLength = 8,
            ErrorMessage = "La contraseña debe tener entre 8 y 20 carácteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-+_=*./\\%$#@!¡¿?()~])[-a-zA-Z\d+_=*./\\%$#@!¡¿?()~]+$",
            ErrorMessage =
            "La contraseña debe contener al menos: una minúscula, una mayúscula, un dígito y un carácter especial")]
        public required string contrasena { get; set; }
    }
}