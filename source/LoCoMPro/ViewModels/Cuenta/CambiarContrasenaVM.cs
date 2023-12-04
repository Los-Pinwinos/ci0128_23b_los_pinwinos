using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LoCoMPro.Utils.Validadores;

namespace LoCoMPro.ViewModels.Cuenta
{

    public class CambiarContrasenaVM
    {
        public string? nombreDeUsuario { get; set; }

        // Contraseña actual
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe incluir una contraseña")]
        [StringLength(20, MinimumLength = 8,
        ErrorMessage = "La contraseña debe tener entre 8 y 20 carácteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-+_=*./\\%$#@!¡¿?()~])[-a-zA-Z\d+_=*./\\%$#@!¡¿?()~]+$",
        ErrorMessage =
        "La contraseña debe contener al menos: una minúscula, una mayúscula, un dígito y un carácter especial")]
        public required string contrasenaActual { get; set; }


        // Contraseña nueva
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe incluir una contraseña")]
        [StringLength(20, MinimumLength = 8,
        ErrorMessage = "La contraseña debe tener entre 8 y 20 carácteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-+_=*./\\%$#@!¡¿?()~])[-a-zA-Z\d+_=*./\\%$#@!¡¿?()~]+$",
        ErrorMessage =
        "La contraseña debe contener al menos: una minúscula, una mayúscula, un dígito y un carácter especial")]
        public required string contrasenaNueva { get; set; }


        // Repetición de la contraseña para confirmación
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe incluir una contraseña")]
        [StringLength(20, MinimumLength = 8,
        ErrorMessage = "La contraseña debe tener entre 8 y 20 carácteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-+_=*./\\%$#@!¡¿?()~])[-a-zA-Z\d+_=*./\\%$#@!¡¿?()~]+$",
        ErrorMessage =
        "La contraseña debe contener al menos: una minúscula, una mayúscula, un dígito y un carácter especial")]
        public required string confirmarContrasena { get; set; }
    }
}

