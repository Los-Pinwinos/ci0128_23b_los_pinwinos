﻿using LoCoMPro.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.AgregarProducto
{
    public class AgregarProdVM
    {
        // Nombre del producto
        [StringLength(256, MinimumLength = 1)]
        public required string nombreProducto { get; set; }

        // Marca del producto
        [StringLength(256, MinimumLength = 0)]
        public string? marcaProducto { get; set; }

        // Nombre de la unidad
        [StringLength(20, MinimumLength = 1)]
        public required string nombreUnidad { get; set; }

        // Nombre de la categoría
        [StringLength(256, MinimumLength = 1)]
        public required string nombreCategoria { get; set; }

        // Descripción
        [DataType(DataType.MultilineText)]
        [StringLength(150, MinimumLength = 0)]
        public string? descripcion { get; set; }

        // Etiquetas
        [StringLength(150, MinimumLength = 0)]
        public string? etiqueta { get; set; }

        // Precio
        [StringLength(18, MinimumLength = 1)]
        public required string precio { get; set; }
    }
}