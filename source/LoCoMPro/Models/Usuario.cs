﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LoCoMPro.Models
{
    [Table("Usuario")]
    [PrimaryKey(nameof(nombreDeUsuario))]
    public class Usuario
    {
        // Nombre de usuario
        [Required]
        [StringLength(20, MinimumLength = 5,
            ErrorMessage = "El nombre de usuario debe tener entre 5 y 20 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-+_=*./\\%$#@!¡¿?()~])[-a-zA-Z\d+_=*./\\%$#@!¡¿?()~]+$",
           ErrorMessage =
           "El nombre de usuario debe contener al menos: una minúscula, una mayúscula, un dígito y un carácter especial")]
        [Display(Name = "Nombre de usuario")]
        public required string nombreDeUsuario { get; set; }

        // Correo (único)
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        [Display(Name = "Correo electrónico")]
        public required string correo { get; set; }

        // Contraseña
        [DataType(DataType.Password)]
        [Display(Name = "Hash contraseña")]
        public required string hashContrasena { get; set; }

        // Estado
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
        [StringLength(30, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Distrito de vivienda")]
        public string? distritoVivienda { get; set; }

        // Cantón vivienda
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Cantón de vivienda")]
        public string? cantonVivienda { get; set; }

        // Provincia vivienda
        [StringLength(10, MinimumLength = 5)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Provincia de vivienda")]
        public string? provinciaVivienda { get; set; }

        // Propiedad de navegación vivienda
        [ForeignKey("distritoVivienda, cantonVivienda, provinciaVivienda")]
        public Distrito? vivienda { get; set; }

        // Colección
        public ICollection<Registro>? registros { get; set; }
    }
}