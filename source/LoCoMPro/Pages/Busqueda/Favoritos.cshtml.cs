using LoCoMPro.Data;
using LoCoMPro.Utils.Buscadores;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.ViewModels.Busqueda;
using LoCoMPro.ViewModels.Cuenta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace LoCoMPro.Pages.Busqueda
{
    public class FavoritosModel : PageModel
    {
        protected readonly LoCoMProContext contexto;
        protected readonly IConfiguration configuracion;

        // Constructor
        public FavoritosModel(LoCoMProContext contexto, IConfiguration configuracion)
        {
            this.contexto = contexto;
            this.configuracion = configuracion;
            // Inicializar
            this.Inicializar();
        }

        public string? favoritos { get; set; }
        public BusquedaFavoritosVM busquedaFavoritoVM { get; set; } = default!;

        // Paginación
        public int paginaDefault { get; set; }
        public int resultadosPorPagina { get; set; }

        // Inicializar atributos
        public void Inicializar()
        {

            this.busquedaFavoritoVM = new BusquedaFavoritosVM
            {
                nombreTienda = "",
                nombreProvincia = "",
                nombreCanton = "",
                nombreDistrito = "",
                cantidadEncontrada = 0,
                porcentajeEncontrado = 0,
                precioTotal = 0,
                distanciaTotal = 0,
            };
            this.paginaDefault = 1;
            this.resultadosPorPagina = this.configuracion.GetValue("TamPaginaCuenta", 10);
        }

        public IActionResult OnGet()
        {
            // Si el usuario no está loggeado
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                // Establece mensaje para redireccionar
                ViewData["MensajeRedireccion"] = "Por favor ingrese al sistema.";
            }
            else
            {
                // Configurar buscador
                IBuscador<BusquedaFavoritosVM> buscador = new BuscadorDeProductosFavoritos(this.contexto, User.Identity.Name);
                // Consultar la base de datos
                IQueryable<BusquedaFavoritosVM> busqueda = buscador.buscar();

                // Si la busqueda tuvo resultados
                List<BusquedaFavoritosVM> resultados = busqueda.ToList();
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
