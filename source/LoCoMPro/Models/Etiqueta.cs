using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(etiqueta), nameof(creacion), nameof(usuarioCreador))]
    public class Etiqueta
    {
        // Etiqueta
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Etiquetas")]
        public required string etiqueta { get; set; }

        // TODO(Los Pinwinos): Investigar DisplayFormatAttribute annotation
        // Fecha y hora de creación
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "9/9/2023", "9/9/2040")]
        [Display(Name = "Fecha y hora de creación")]
        public required DateTime creacion { get; set; }

        // Usuario creador
        [StringLength(12, MinimumLength = 10)]
        [Display(Name = "Creador")]
        public required string usuarioCreador { get; set; }

        // Propiedad de navegación registro
        [ForeignKey("creacion, usuarioCreador")]
        public Registro registro { get; set; } = null!;
        
    }
}