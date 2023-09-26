﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(nombre), nameof(nombreProvincia))]
    public class Canton
    {
        // Nombre
        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Nombre del cantón")]
        public required string nombre { get; set; }

        // Nombre provincia
        [StringLength(10, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Nombre de la provincia")]
        public required string nombreProvincia { get; set; }

        // Propiedad de navegación provincia
        [ForeignKey("nombreProvincia")]
        public Provincia? provincia { get; set; }

        // Colección
        public ICollection<Distrito>? distritos { get; set; }
    }
}