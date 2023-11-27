using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Moderacion
{
    public class ProductoPrecioOutlierVM
    {
        [Display(Name = "Nombre")]
        public required string nombre { get; set; }

        [Display(Name = "Promedio")]
        public decimal? promedio { get; set; } = 0;

        [Display(Name = "Mínimo")]
        public decimal? minimo { get; set; } = 0;

        [Display(Name = "Máximo")]
        public decimal? maximo { get; set; } = 0;

        [Display(Name = "Desviación Estándar")]
        public double? desviacionEstandar { get; set; } = 0.0;

        [Display(Name = "Cuartil 1")]
        public decimal? q1 { get; set; } = 0;

        [Display(Name = "Cuartil 3")]
        public decimal? q3 { get; set; } = 0;

        [Display(Name = "Rango intercuatílico")]
        public decimal? iqr { get; set; } = 0;
    }
}
