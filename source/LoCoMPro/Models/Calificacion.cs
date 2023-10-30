using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(usuarioCalificador), nameof(creacionRegistro), nameof(usuarioCreadorRegistro))]
    public class Calificacion
    {
        // Fecha y hora de creación
        [Range(0, 5, ErrorMessage = "La calificación debe ser un valor entre 0 y 5")]
        [Display(Name = "Calificación")]
        public int calificacion { get; set; }

        // Usuario que califica el registro
        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "Usuario calificador")]
        public required string usuarioCalificador { get; set; }

        [ForeignKey("usuarioCalificador")]
        public Usuario? calificador { get; set; }

        // Usuario creador del registro
        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "Creador del registro")]
        public required string usuarioCreadorRegistro { get; set; }

        // Fecha y hora de creación del registro
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "1/2/2000", "1/1/2200")]
        [Display(Name = "Fecha y hora de creación del registro")]
        public required DateTime creacionRegistro { get; set; }

        // Propiedad de navegación registro
        [ForeignKey("creacionRegistro, usuarioCreadorRegistro")]
        public Registro? registro { get; set; }
    }
}
