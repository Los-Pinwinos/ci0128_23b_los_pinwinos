using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(usuarioCreadorReporte), nameof(creacionRegistro), nameof(usuarioCreadorRegistro))]

    public class Reporte
    {

        // Comentario
        [StringLength(256), MinLength(1)]
        [Display(Name = "Comentario")]
        public required string comentario { get; set; }

        // Fecha y hora de creación
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "1/2/2000", "1/1/2200")]
        [Display(Name = "Fecha y hora de creación")]
        public required DateTime creacion { get; set; }

        // Usuario creador del reporte
        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "Creador del reporte")]
        public required string usuarioCreadorReporte { get; set; }

        [ForeignKey("usuarioCreadorReporte")]
        public Usuario? creadorReporte { get; set; }

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

        // Propiedad para indicar si un reporte ha sido verificado
        public required bool verificado { get; set; } = false;
    }
}
