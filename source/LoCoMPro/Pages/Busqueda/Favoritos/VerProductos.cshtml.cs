using LoCoMPro.Data;
using LoCoMPro.Utils.Buscadores;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.ViewModels.Busqueda;
using LoCoMPro.ViewModels.Busqueda.Favoritos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace LoCoMPro.Pages.Busqueda.Favoritos
{
    public class VerProductosModel : PageModel
    {
		protected readonly LoCoMProContext contexto;
		protected readonly IConfiguration configuracion;

		// Constructor
		public VerProductosModel(LoCoMProContext contexto, IConfiguration configuracion)
		{
			this.contexto = contexto;
			this.configuracion = configuracion;
			// Inicializar
			this.Inicializar();
		}

		public string? tienda { get; set; }
		public string? provincia { get; set; }
        public string? canton { get; set; }
        public string? precioTotal { get; set; }
        public string? distanciaTotal { get; set; }

        public string? favoritos { get; set; }
		public ProductoFavoritoVM productoFavoritoVM { get; set; } = default!;

		// Paginación
		public int paginaDefault { get; set; }
		public int resultadosPorPagina { get; set; }

		// Inicializar atributos
		public void Inicializar()
		{

			this.productoFavoritoVM = new ProductoFavoritoVM
			{
				producto = "",
				precio = 0,
				marca = "",
				categoria = "",
				unidad = ""
			};
			this.paginaDefault = 1;
			this.resultadosPorPagina = this.configuracion.GetValue("TamPaginaCuenta", 10);
		}

		public IActionResult OnGet(string? nombreTienda, string? nombreProvincia, string? nombreCanton, string? nombreDistrito, string? precioTotal, string? distanciaTotal)
		{
			// Si el usuario no está loggeado
			if (User.Identity == null || !User.Identity.IsAuthenticated)
			{
				// Establece mensaje para redireccionar
				ViewData["MensajeRedireccion"] = "Por favor ingrese al sistema.";
			}
			else
			{
				// Asignar propiedades necesarias
				this.tienda = nombreTienda;
				this.provincia = nombreProvincia;
				this.canton = nombreCanton;
				this.distanciaTotal = distanciaTotal + "km";
				this.precioTotal = "₡" + precioTotal;

				// Configurar buscador
				IBuscador<ProductoFavoritoVM> buscador = new BuscadorDeProductosFavoritosEnTienda(this.contexto,User.Identity.Name
															, nombreTienda , nombreProvincia , nombreCanton , nombreDistrito);
				// Consultar la base de datos
				IQueryable<ProductoFavoritoVM> busqueda = buscador.buscar();

				// Si la busqueda tuvo resultados
				List<ProductoFavoritoVM> resultados = busqueda.ToList();
				if (resultados.Count != 0)
				{
					// Asignar data de JSON
					this.favoritos = JsonConvert.SerializeObject(resultados);
				}
				else
				{
					this.favoritos = "Sin resultados";
				}
			}
			return Page();
		}
	}
}
