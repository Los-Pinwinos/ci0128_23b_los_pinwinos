﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.Models
{
    [PrimaryKey(nameof(nombre), nameof(nombreCanton), nameof(nombreProvincia))]
    public class Distrito
    {
        // Nombre
        [StringLength(25, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Distrito")]
        public string nombre { get; set; }

        // Cantón
        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Nombre cantón")]
        public string nombreCanton { get; set; }

        // Nombre provincia
        [StringLength(10, MinimumLength = 1)]
        [RegularExpression(@"")]
        [Display(Name = "Nombre de la provincia")]
        public string nombreProvincia { get; set; }


        [ForeignKey("nombreCanton, nombreProvincia")]
        public Canton canton { get; set; }


        // TODO(nosotros): deescomentar las colecciones de distrito
        // cuando ya estén los modelos de Usuario y Tiendas

        // Colecciones
        // public ICollection<Usuario> habitantes { get; set; }

        // public ICollection<Tienda> tiendas { get; set; }
    }
}
