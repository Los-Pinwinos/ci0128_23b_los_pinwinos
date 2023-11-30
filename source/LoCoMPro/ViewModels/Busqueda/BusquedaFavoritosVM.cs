using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Busqueda
{
    public class BusquedaFavoritosVM
    {
        // Nombre
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ0-9#])*")]
        [Display(Name = "Tienda")]
        public required string nombreTienda { get; set; }

        // Nombre distrito
        [StringLength(30, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Distrito")]
        public required string nombreDistrito { get; set; }

        // Nombre cantón
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Cantón")]
        public required string nombreCanton { get; set; }

        // Nombre provincia
        [StringLength(10, MinimumLength = 5)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Provincia")]
        public required string nombreProvincia { get; set; }

        [Display(Name = "Cantidad encontrada")]
        public required int cantidadEncontrada { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public required float porcentajeEncontrado { get; set; }

        [Display(Name = "Precio total")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public required decimal precioTotal { get; set; }

        [Display(Name = "Distancia total")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public required double distanciaTotal { get; set; }
    }
}
