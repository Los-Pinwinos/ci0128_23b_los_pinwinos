using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Cuenta
{
    // Modelo viata para ingresar al sistema con usuarios ya existentes
    public class IngresarUsuarioVM
    {
        // Nombre de usuario
        public string nombreDeUsuario { get; set; }

        // Contraseña
        [DataType(DataType.Password)]
        public string contrasena { get; set; }
    }
}