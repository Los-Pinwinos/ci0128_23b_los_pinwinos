using LoCoMPro.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.ViewModels.VerRegistros
{
    [PrimaryKey(nameof(creacion), nameof(usuarioCreador))]
    public class VerRegistrosVM
    {
        // Fecha y hora de creación
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "1/2/2000", "1/1/2200")]
        [Display(Name = "Fecha y hora de creación")]
        public required DateTime creacion { get; set; }

        // Usuario creador
        [StringLength(20, MinimumLength = 5)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-+_=*./\\%$#@!¡¿?()~])[-a-zA-Z\d+_=*./\\%$#@!¡¿?()~]+$")]
        [Display(Name = "Creador")]
        public required string usuarioCreador { get; set; }

        // Precio
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Precio")]
        public required decimal precio { get; set; }


        // Calificación
        [Range(0, 5,
            ErrorMessage = "La calificación debe estar entre 0 y 5 puntos")]
        [Display(Name = "Calificación")]
        public double calificacion { get; set; }


        // Descripción
        [DataType(DataType.MultilineText)]
        [StringLength(150, MinimumLength = 1)]
        [Display(Name = "Descripción")]
        public string? descripcion { get; set; }

        // Colecciones

        public ICollection<Fotografia>? fotografias { get; set; }
    }
}
