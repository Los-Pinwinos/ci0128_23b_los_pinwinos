using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Cuenta;
using LoCoMPro.Utils.Buscadores;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.Utils;
using Newtonsoft.Json;

namespace LoCoMPro.Pages.Cuenta
{
    public class ModeloAportes : PageModel
    {
        protected readonly LoCoMProContext contexto;
        protected readonly IConfiguration configuracion;

        // Constructor
        public ModeloAportes(LoCoMProContext contexto, IConfiguration configuracion)
        {
            this.contexto = contexto;
            this.configuracion = configuracion;
            // Inicializar
            this.Inicializar();
        }

        public string? aportes { get; set; }
        public AporteVM aporteVM { get; set; } = default!;

        // Paginación
        public int paginaDefault { get; set; }
        public int resultadosPorPagina { get; set; }

        // Inicializar atributos
        public void Inicializar()
        {

            this.aporteVM = new AporteVM
            { 
                fecha = DateTime.Now,
                producto = "",
                precio = 0,
                unidad = "",
                categoria = "",
                tienda = "",
                canton = "",
                provincia = "",
                calificacion = 0
            };
            this.paginaDefault = 1;
            this.resultadosPorPagina = this.configuracion.GetValue("TamPaginaCuenta", 10);
        }

        // Método On get para cargar los resultados o redireccionar
        public IActionResult OnGet()
        {
            // Si el usuario no está loggeado
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                // Establece mensaje para redireccionar
                ViewData["MensajeRedireccion"] = "Por favor ingrese al sistema.";
            } else
            {
                // Configurar buscador
                IBuscador<AporteVM> buscador = new BuscadorDeAportes(this.contexto, User.Identity.Name);
                // Consultar la base de datos
                IQueryable<AporteVM> busqueda = buscador.buscar();

                // Si la busqueda tuvo resultados
                List<AporteVM> resultados = busqueda.ToList();
                if (resultados.Count != 0)
                {
                    // Asignar data de JSON
                    this.aportes = ControladorJson.ConvertirAJson(resultados);
                }
                else
                {
                    this.aportes = "Sin resultados";
                }
            }
            return Page();
        }
    }
}