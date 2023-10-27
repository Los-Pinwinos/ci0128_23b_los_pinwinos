using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(nombreFotografia), nameof(creacion), nameof(usuarioCreador))]
    public class Fotografia
    {
        // Fotografía
        [Display(Name = "Fotografía")]
        [MaxLength]
        public required byte[] fotografia { get; set; }

        // Nombre de la fotografía
        [StringLength(200), MinLength(5)]
        public required string nombreFotografia { get; set; }

        // Fecha y hora de creación
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "1/2/2000", "1/1/2200")]
        [Display(Name = "Fecha y hora de creación")]
        public required DateTime creacion { get; set; }

        // Usuario creador
        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "Creador")]
        public required string usuarioCreador { get; set; }

        // Propiedad de navegación registro
        [ForeignKey("creacion, usuarioCreador")]
        public Registro? registro { get; set; }
    }
}