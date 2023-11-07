using LoCoMPro.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.DetallesRegistro
{
    public class DetallesRegistroVM
    {
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "1/2/2000", "1/1/2200")]
        [Display(Name = "Fecha y hora de creación")]
        public required DateTime creacion { get; set; }

        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "Creador")]
        public required string usuarioCreador { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(150, MinimumLength = 1)]
        [Display(Name = "Descripción")]
        public string? descripcion { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Precio")]
        public required decimal precio { get; set; }

        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Unidad de medida")]
        public required string nombreUnidad { get; set; }

        [Range(0, 5,
            ErrorMessage = "La calificación debe estar entre 0 y 5 puntos")]
        [Display(Name = "Calificación")]
        public double calificacion { get; set; }

        [StringLength(256, MinimumLength = 1)]
        [Display(Name = "Nombre del producto")]
        public required string productoAsociado { get; set; }

        public int cantidadCalificaciones { get; set; }

        public ICollection<Fotografia>? fotografias { get; set; }
    }
}
