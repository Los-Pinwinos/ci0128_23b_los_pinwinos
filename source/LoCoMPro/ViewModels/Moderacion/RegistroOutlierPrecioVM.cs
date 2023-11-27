using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Moderacion
{
    public class RegistroOutlierPrecioVM
    {
        [Display(Name = "Fecha")]
        public required DateTime fecha { get; set; }

        [Display(Name = "Usuario")]
        public required string usuario { get; set; }

        [Display(Name = "Producto")]
        public required string producto { get; set; }

        [Display(Name = "Precio")]
        public required decimal precio { get; set; }

        [Display(Name = "Tienda")]
        public required string tienda { get; set; }

        [Display(Name = "Provincia")]
        public required string provincia { get; set; }

        [Display(Name = "Cantón")]
        public required string canton { get; set; }

        [Display(Name = "Promedio")]
        public decimal promedio { get; set; } = 0;

        [Display(Name = "Mínimo")]
        public decimal minimo { get; set; } = 0;

        [Display(Name = "Máximo")]
        public decimal maximo { get; set; } = 0;

        [Display(Name = "Desviación Estándar")]
        public double desviacionEstandar { get; set; } = 0.0;
    }
}
