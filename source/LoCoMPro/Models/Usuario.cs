using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.Models
{
    [Table("Usuario")]
    [PrimaryKey(nameof(nombreDeUsuario))]
    public class Usuario
    {
        // Nombre de usuario
        [StringLength(12, MinimumLength = 10)]
        [Display(Name = "Nombre de usuario")]
        public required string nombreDeUsuario { get; set; }

        // Correo eléctronico (único)
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Correo electronico inválido")]
        [Display(Name = "Correo electrónico")]
        public required string correo { get; set; }

        // Contraseña
        [Display(Name = "Contraseña")]
        public required string hashContrasena { get; set; }

        // Estado de la cuenta
        [RegularExpression(@"[ABI]",
            ErrorMessage =
            "El estado debe ser A (activo), I (inactivo), B (bloqueado)")]
        [Display(Name = "Estado")]
        public char estado { get; set; }

        // Calificación de los aportes del usuario
        [Range(0, 5,
            ErrorMessage = "La calificación debe estar entre 0 y 5 puntos")]
        [Display(Name = "Calificación")]
        public int? calificacion { get; set; }

        // Nombre de la provincia donde vive el usuario
        [StringLength(10, MinimumLength = 1)]
        public string? provinciaVivienda { get; set; }

        // Nombre del cantón donde vive el usuario
        [StringLength(20, MinimumLength = 1)]
        public string? cantonVivienda { get; set; }

        // Nombre del distrito donde vive el usuario
        [StringLength(25, MinimumLength = 1)]
        public string? distritoVivienda { get; set; }

        // Propiedad de navegación vivienda
        [ForeignKey("provinciaVivienda, cantonVivienda, distritoVivienda")]
        public Distrito? vivienda { get; set; }

        // Colección de registros hechos por el usuario
        public ICollection<Registro>? registros { get; set; }
        
    }
}