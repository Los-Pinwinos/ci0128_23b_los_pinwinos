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


        [Column(TypeName = "decimal(3, 2)")]
        [Required]
        [Range(0, 5, ErrorMessage = "The 'calificación' must be a decimal number between 0 and 5.")]
        public decimal calificacion { get; set; } = 0;


        // Descripción
        [DataType(DataType.MultilineText)]
        [StringLength(150, MinimumLength = 1)]
        [Display(Name = "Descripción")]
        public string? descripcion { get; set; }


        // Colecciones
        public ICollection<Etiqueta>? etiquetas { get; set; }

        public ICollection<Fotografia>? fotografias { get; set; }
    }
}
