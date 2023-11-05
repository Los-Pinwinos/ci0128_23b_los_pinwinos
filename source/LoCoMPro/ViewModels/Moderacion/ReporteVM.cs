using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Moderacion
{
    public class ReporteVM
    {
        // Comentario
        [Display(Name = "Comentario")]
        public required string comentario { get; set; }

        // Fecha y hora de creación
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha")]
        public required DateTime fecha { get; set; }

        // Usuario creador del reporte
        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "Por")]
        public required string creador { get; set; }

        [Display(Name = "Calificación del creador")]
        public required double calificacionCreador { get; set; }
    }
}
