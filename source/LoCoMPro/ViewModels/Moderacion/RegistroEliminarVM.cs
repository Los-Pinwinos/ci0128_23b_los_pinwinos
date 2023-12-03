using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Moderacion
{
    public class RegistroEliminarVM
    {
        [Display(Name = "Fecha")]
        public required DateTime fecha { get; set; }

        [Display(Name = "Usuario")]
        public required string usuario { get; set; }
    }
}
