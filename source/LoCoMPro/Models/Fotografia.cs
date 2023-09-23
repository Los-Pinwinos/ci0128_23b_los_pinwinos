using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(fotografia), nameof(creacion), nameof(usuarioCreador))]
    public class Fotografia
	{
		// Fotografia
		[Display(Name = "Fotografía")]
		public required byte[] fotografia { get; set; }

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
        public Registro? registro { get; set; }
        
    }
}