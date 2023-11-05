using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Moderacion
{
    public class ReporteVM
    {
        // Comentario
        [StringLength(256), MinLength(1)]
        [Display(Name = "Comentario")]
        public required string comentario { get; set; }

        // Fecha y hora de creación
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "1/2/2000", "1/1/2200")]
        [Display(Name = "Fecha")]
        public required DateTime fecha { get; set; }

        // Usuario creador del reporte
        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "Por")]
        public required string creador { get; set; }

        [Range(0, 5)]
        [Display(Name = "Calificación del creador")]
        public double calificacionCreador { get; set; }
    }
}
