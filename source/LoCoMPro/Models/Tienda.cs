﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(nombre), nameof(nombreDistrito), nameof(nombreCanton), nameof(nombreProvincia))]
    public class Tienda
    {
        // Nombre
        [StringLength(256, MinimumLength = 1)]
        // Revisa que comience con una letra y luego puede tener más letras, números o numeral
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ0-9#])*")]
        [Display(Name = "Nombre de la tienda")]
        public required string nombre { get; set; }

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

        // Propiedad de navegación distrito
        [ForeignKey("nombreDistrito, nombreCanton, nombreProvincia")]
        [Display(Name = "Distrito")]
        public Distrito? distrito { get; set; }

        // Colección
        public ICollection<Registro>? registros { get; set; }

        // Coordenadas
        public double latitud { get; set; } = 0;
        public double longitud { get; set; } = 0;
    }
}