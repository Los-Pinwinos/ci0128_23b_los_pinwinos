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
    }
}
