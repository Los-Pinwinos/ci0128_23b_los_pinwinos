using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Busqueda;
using Microsoft.IdentityModel.Tokens;
using LoCoMPro.Utils.Buscadores;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.Utils;
using Newtonsoft.Json;
using System.Web;

namespace LoCoMPro.Pages.Busqueda
{
    public class BusquedaModel : PageModel
    {
        protected readonly LoCoMProContext contexto;
        protected readonly IConfiguration configuracion;

        // Constructor
        public BusquedaModel(LoCoMProContext contexto, IConfiguration configuracion)
        {
            this.contexto = contexto;
            this.configuracion = configuracion;
            // Inicializar
            this.Inicializar();
        }

        // Busquedas
        [BindProperty(SupportsGet = true)]
        public string? producto { get; set; }
        public string? resultadosBusqueda { get; set; }
        // Visual
        [BindProperty]
        public IList<string> provinciasV { get; set; } = default!;
        public IList<string> cantonesV { get; set; } = default!;
        public IList<string> tiendasV { get; set; } = default!;
        public IList<string?> marcasV { get; set; } = default!;
        public IList<string> categoriasV { get; set; } = default!;
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
            this.categoriasV = new List<string>();

            this.productoVM = new BusquedaVM
            {
                nombre = "",
                canton = "",
                fecha = new DateTime(),
                precio = 0,
                marca = "",
                provincia = "",
                tienda = "",
                unidad = "",
                categoria = ""
            };

            this.paginaDefault = 1;
            this.resultadosPorPagina = this.configuracion.GetValue("TamPagina", 4);
        }

        // ON GET buscar
        public IActionResult OnGet(string? nombreProducto)
        {
            if (!string.IsNullOrEmpty(nombreProducto) ||
                contexto.Productos != null)
            {
                // Asignar valores
                producto = nombreProducto;
                // Configurar buscador
                IBuscador<BusquedaVM> buscador = new BuscadorDeProductos(this.contexto, nombreProducto);
                // Consultar la base de datos
                IQueryable<BusquedaVM> busqueda = buscador.buscar();
                // Cargar filtros
                this.cargarFiltros(busqueda);
                // Si la busqueda tuvo resultados
                List<BusquedaVM> resultados = busqueda.ToList();
                if (resultados.Count != 0)
                {
                    // Asignar data de JSON
                    this.resultadosBusqueda = JsonConvert.SerializeObject(resultados);
                }
                else
                {
                    this.resultadosBusqueda = "Sin resultados";
                }
            }
            return Page();
        }

        // Cargar los filtros
        public void cargarFiltros(IQueryable<BusquedaVM> productosIQ)
        {
            // Cargar filtros de provincia
            this.cargarFiltrosProvincia(productosIQ);
            // Cargar filtros de canton
            this.cargarFiltrosCanton(productosIQ);
            // Cargar filtros de tienda
            this.cargarFiltrosTienda(productosIQ);
            // Cargar filtros de marca
            this.cargarFiltrosMarca(productosIQ);
            // Cargar filtros de categoria
            this.cargarFiltrosCategoria(productosIQ);

        }

        // Cargar los filtros de provincias
        public void cargarFiltrosProvincia(IQueryable<BusquedaVM> productosIQ)
        {
            // Si los productos no estan vacios
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todas las provincias distintas
                this.provinciasV = productosIQ
                    .Select(r => r.provincia)
                    .Distinct()
                    .ToList();
            }
        }

        // Cargar los filtros de cantones
        public void cargarFiltrosCanton(IQueryable<BusquedaVM> productosIQ)
        {
            // Si los productos no estan vacios
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todos los cantones distintos
                this.cantonesV = productosIQ
                    .Select(r => r.canton)
                    .Distinct()
                    .ToList();
            }
        }

        // Cargar los filtros de tiendas
        public void cargarFiltrosTienda(IQueryable<BusquedaVM> productosIQ)
        {
            // Si los productos no estan vacios
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todas las tiendas distintas
                this.tiendasV = productosIQ
                    .Select(r => r.tienda)
                    .Distinct()
                    .ToList();
            }
        }

        // Cargar los filtros de marcas
        public void cargarFiltrosMarca(IQueryable<BusquedaVM> productosIQ)
        {
            // Si los productos no estan vacios
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todas las marcas distintas
                this.marcasV = productosIQ
                    .Select(p => p.marca)
                    .Distinct()
                    .ToList();
            }
        }

        public void cargarFiltrosCategoria(IQueryable<BusquedaVM> productosIQ)
        {
            // Si los productos no estan vacios
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todas las categorías distintas
                this.categoriasV = productosIQ
                    .Select(p => p.categoria)
                    .Distinct()
                    .ToList();
            }
        }
    }
}