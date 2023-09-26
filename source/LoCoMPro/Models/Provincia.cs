﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(nombre))]
    public class Provincia
    {
        // Nombre
        [StringLength(10, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Nombre de la provincia")]
        public required string nombre { get; set; }

        // Colección
        public ICollection<Canton>? cantones { get; set; }
    }
}