using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Busqueda
{
    public class BusquedaVM
    {
        // Nombre
        [Display(Name = "Nombre")]
        public required string nombre { get; set; }
        // Precio
        [Display(Name = "Precio")]
        public required decimal precio { get; set; }
        // Unidad
        [Display(Name = "Unidad")]
        public required string unidad { get; set; }
        // Fecha
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public required DateTime fecha { get; set; }
        // Establecimiento
        [Display(Name = "Tienda")]
        public required string tienda { get; set; }
        // Provincia
        [Display(Name = "Provincia")]
        public required string provincia { get; set; }
        // Canton
        [Display(Name = "Cantón")]
        public required string canton { get; set; }
        // Marca
        [Display(Name = "Marca")]
        public string? marca { get; set; }
        // Categoría
        [Display(Name = "Categoría")]
        public required string categoria { get; set; }
    }
}