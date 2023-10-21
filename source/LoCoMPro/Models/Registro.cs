using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(creacion), nameof(usuarioCreador))]
    public class Registro
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

        [ForeignKey("usuarioCreador")]
        public Usuario? creador { get; set; }

        // Descripción
        [DataType(DataType.MultilineText)]
        [StringLength(150, MinimumLength = 1)]
        [Display(Name = "Descripción")]
        public string? descripcion { get; set; }

        // Precio
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Precio")]
        public required decimal precio { get; set; }

        // Producto asociado
        [StringLength(256, MinimumLength = 1)]
        [Display(Name = "Nombre del producto")]
        public required string productoAsociado { get; set; }

        // Propiedad de navegación producto
        [ForeignKey("productoAsociado")]
        public Producto? producto { get; set; }

        // Nombre tienda
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ0-9#])*")]
        [Display(Name = "Nombre de la tienda")]
        public required string nombreTienda { get; set; }

        // Nombre distrito
        [StringLength(30, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Nombre del distrito")]
        public required string nombreDistrito { get; set; }

        // Nombre cantón
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Nombre del cantón")]
        public required string nombreCanton { get; set; }

        // Nombre provincia
        [StringLength(10, MinimumLength = 5)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Nombre de la provincia")]
        public required string nombreProvincia { get; set; }

        // Propiedad de navegación tienda
        [ForeignKey("nombreTienda, nombreDistrito, nombreCanton, nombreProvincia")]
        public Tienda? tienda { get; set; }

        [Column(TypeName = "decimal(3, 2)")]
        [Required]
        [Range(0, 5, ErrorMessage = "The 'calificación' must be a decimal number between 0 and 5.")]
        public decimal calificacion { get; set; } = 0;


        // Colecciones
        public ICollection<Etiqueta>? etiquetas { get; set; }

        public ICollection<Fotografia>? fotografias { get; set; }
    }
}