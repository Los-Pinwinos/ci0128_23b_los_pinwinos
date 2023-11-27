using LoCoMPro.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.ViewModels.Moderacion
{
    public class OutlierFechaVM
    {
        // Nombre del producto
        [StringLength(256, MinimumLength = 1)]
        [Display(Name = "Producto")]
        public required string producto { get; set; }

        // Nombre de la tienda
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ0-9#])*")]
        [Display(Name = "Tienda")]
        public required string tienda { get; set; }

        // Nombre de la provincia
        [StringLength(10, MinimumLength = 5)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Provincia")]
        public required string provincia { get; set; }

        // Nombre del cantón
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Cantón")]
        public required string canton { get; set; }

        // Nombre del distrito
        [StringLength(30, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
        [Display(Name = "Distrito")]
        public required string distrito { get; set; }

        // Cantidad de registros outliers
        [Display(Name = "Cantidad de registros viejos")]
        public required int cantidadRegistros { get; set; }

        // Fecha de corte
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "1/2/2000", "1/1/2200")]
        [Display(Name = "Fecha de corte")]
        public DateTime? fechaCorte { get; set; }
    }
}
