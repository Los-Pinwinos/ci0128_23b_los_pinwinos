using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoCoMPro.ViewModels.Busqueda.Favoritos
{
	public class ProductoFavoritoVM
	{
		// Producto
		[StringLength(256, MinimumLength = 1)]
		[Display(Name = "Producto")]
		public required string producto { get; set; }

		// Precio
		[DataType(DataType.Currency)]
		[Display(Name = "Precio")]
		public required decimal precio { get; set; }

		// Nombre de la unidad
		[StringLength(20, MinimumLength = 1)]
		[RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
		[Display(Name = "Unidad")]
		public required string unidad { get; set; }

		// Nombre de la categoría
		[StringLength(256, MinimumLength = 1)]
		[RegularExpression(@"[a-zA-ZÀ-ÿ]+( ?[a-zA-ZÀ-ÿ])*")]
		[Display(Name = "Categoría")]
		public required string categoria { get; set; }

		// Marca
		[StringLength(256, MinimumLength = 1)]
		[Display(Name = "Marca")]
		public required string marca { get; set; }
	}
}
