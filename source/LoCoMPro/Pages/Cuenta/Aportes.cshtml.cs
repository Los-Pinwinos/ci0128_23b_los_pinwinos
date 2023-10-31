using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Busqueda;
using LoCoMPro.Utils.Buscadores;
using LoCoMPro.Utils.Interfaces;
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
        // Visual
        [BindProperty]
        public IList<string> provinciasV { get; set; } = default!;
        public IList<string> cantonesV { get; set; } = default!;
        public IList<string> tiendasV { get; set; } = default!;
        public IList<string?> marcasV { get; set; } = default!;
        public BusquedaVM productoVM { get; set; } = default!;

        // Paginación
        public int paginaDefault { get; set; }
        public int resultadosPorPagina { get; set; }

        // Inicializar atributos
        public void Inicializar()
        {

            // Inicializar datos de vista
            this.provinciasV = new List<string>();
            this.cantonesV = new List<string>();
            this.tiendasV = new List<string>();
            this.marcasV = new List<string?>();

            this.productoVM = new BusquedaVM
            {
                nombre = ""
                                                ,
                canton = ""
                                                ,
                fecha = new DateTime()
                                                ,
                precio = 0
                                                ,
                marca = ""
                                                ,
                provincia = ""
                                                ,
                tienda = ""
                                                ,
                unidad = ""
                                                ,
                categoria = ""
            };
            this.paginaDefault = 1;
            this.resultadosPorPagina = this.configuracion.GetValue("TamPagina", 4);
        }

        // ON GET buscar
        public IActionResult OnGet()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                // Establece mensaje para redireccionar si el usuario no está ingresado
                // en el sistema
                ViewData["MensajeRedireccion"] = "Por favor ingrese al sistema.";
            } else
            {
                // Configurar buscador
                IBuscador<BusquedaVM> buscador = new BuscadorDeAportes(this.contexto, User.Identity.Name);
                // Consultar la base de datos
                IQueryable<BusquedaVM> busqueda = buscador.buscar();
                // Asignar data de JSON
                this.aportes = JsonConvert.SerializeObject(busqueda.ToList());
            }
            return Page();
        }
    }
}