using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(nombreDeUsuario))]
    public class Usuario
    {
        // Nombre de usuario
        [StringLength(12, MinimumLength = 10)]
        [Display(Name = "Nombre de usuario")]
        public required string nombreDeUsuario { get; set; }

        // Correo (único)
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Correo electronico inválido")]
        [Display(Name = "Correo electrónico")]
        public required string correo { get; set; }

        // Contraseña
        [Required]
        [DataType(DataType.Password)]
        [StringLength(8)]
        [Display(Name = "Contraseña")]
        public required string contrasena { get; set; }

        // Estado
        [Required]
        [RegularExpression(@"[ABI]",
            ErrorMessage =
            "El estado debe ser A (activo), I (inactivo), B(bloqueado)")]
        [Display(Name = "Estado")]
        public char estado { get; set; }

        // Calificación
        [Range(0, 5,
            ErrorMessage = "La calificación debe estar entre 0 y 5 puntos")]
        [Display(Name = "Calificación")]
        public int? calificacion { get; set; }

        // Distrito vivienda
        [StringLength(25, MinimumLength = 1)]
        [Display(Name = "Distrito de vivienda")]
        public string? distritoVivienda { get; set; }

        // Cantón vivienda
        [StringLength(20, MinimumLength = 1)]
        [Display(Name = "Cantón de vivienda")]
        public string? cantonVivienda { get; set; }

        // Provincia vivienda
        [StringLength(10, MinimumLength = 1)]
        [Display(Name = "Provincia de vivienda")]
        public string? provinciaVivienda { get; set; }

        [ForeignKey("distritoVivienda, cantonVivienda, provinciaVivienda")]
        public Distrito? vivienda { get; set; }

        // TODO(Emilia): Integrar con Registro
        /*
        // Colección
        public ICollection<Registro>? registros { get; set; }
        */
    }
}