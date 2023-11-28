using LoCoMPro.Data;
using LoCoMPro.Utils.Buscadores;
using LoCoMPro.Utils.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using LoCoMPro.ViewModels.Moderacion;

namespace LoCoMPro.Pages.Moderacion
{
    public class ModeloFechasObsoletas : PageModel
    {
        protected readonly LoCoMProContext contexto;
        protected readonly IConfiguration configuracion;

        // Constructor
        public ModeloFechasObsoletas(LoCoMProContext contexto, IConfiguration configuracion)
        {
            this.contexto = contexto;
            this.configuracion = configuracion;
            // Inicializar
            this.Inicializar();
        }

        public string? outliers { get; set; }
        public OutlierFechaVM outlierVM { get; set; } = default!;

        // Paginación
        public int paginaDefault { get; set; }
        public int resultadosPorPagina { get; set; }

        // Inicializar atributos
        public void Inicializar()
        {
            this.outlierVM = new OutlierFechaVM
            {
                producto = "",
                tienda = "",
                provincia = "",
                canton = "",
                distrito = "",
                cantidadRegistros = 0,
                fechaCorte = null
            };
            this.paginaDefault = 1;
            this.resultadosPorPagina = this.configuracion.GetValue("TamPaginaCuenta", 10);
        }

        // Método On get para cargar los resultados o redireccionar
        public IActionResult OnGet()
        {
            // Si el usuario no está loggeado
            if (User.Identity == null || !User.Identity.IsAuthenticated ||
                !User.IsInRole("moderador"))
            {
                // Establece mensaje para redireccionar
                ViewData["redireccion"] = "redireccionar";
            }
            else
            {
                // Configurar buscador
                IBuscador<OutlierFechaVM> buscador = new BuscadorDeOutliersFecha(this.contexto);
                // Consultar la base de datos
                IQueryable<OutlierFechaVM> busqueda = buscador.buscar();

                // Si la busqueda tuvo resultados
                List<OutlierFechaVM> resultados = busqueda.ToList();
                if (resultados.Count != 0)
                {
                    // Asignar data de JSON
                    this.outliers = JsonConvert.SerializeObject(resultados);
                }
                else
                {
                    this.outliers = "Sin resultados";
                }
            }
            return Page();
        }
    }
}
