using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LoCoMPro.Utils.Validadores;

namespace LoCoMPro.ViewModels.Cuenta
{

    public class CambiarContraseñaVM
    {
        // Nombre de usuario
        [Required(ErrorMessage = "Debe incluir un nombre de usuario")]
        [StringLength(20, MinimumLength = 5,
        ErrorMessage = "El nombre de usuario debe tener entre 5 y 20 caracteres")]
        public required string nombreDeUsuario { get; set; }

        // Contraseña actual
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe incluir una contraseña")]
        [StringLength(20, MinimumLength = 8,
            ErrorMessage = "La contraseña debe tener entre 8 y 20 carácteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-+_=*./\\%$#@!¡¿?()~])[-a-zA-Z\d+_=*./\\%$#@!¡¿?()~]+$",
            ErrorMessage =
            "La contraseña debe contener al menos: una minúscula, una mayúscula, un dígito y un carácter especial")]
        [DebeCoincidir("confirmarContrasena", ErrorMessage = "Las contraseñas ingresadas deben ser iguales")]
        public required string contrasenaActual { get; set; }

        // Contraseña nueva
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe incluir una contraseña")]
        [StringLength(20, MinimumLength = 8,
            ErrorMessage = "La contraseña debe tener entre 8 y 20 carácteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-+_=*./\\%$#@!¡¿?()~])[-a-zA-Z\d+_=*./\\%$#@!¡¿?()~]+$",
            ErrorMessage =
            "La contraseña debe contener al menos: una minúscula, una mayúscula, un dígito y un carácter especial")]
        [DebeCoincidir("confirmarContrasena", ErrorMessage = "Las contraseñas ingresadas deben ser iguales")]
        public required string contrasenaNueva { get; set; }

        // Repetición de la contraseña para confirmación
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe incluir una confirmación de la contraseña")]
        [StringLength(20, MinimumLength = 8,
            ErrorMessage = "La contraseña debe tener entre 8 y 20 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-+_=*./\\%$#@!¡¿?()~])[-a-zA-Z\d+_=*./\\%$#@!¡¿?()~]+$",
            ErrorMessage =
            "La contraseña debe contener al menos: una minúscula, una mayúscula, un dígito y un carácter especial")]
        public required string confirmarContrasena { get; set; }
    }
}

