using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.ViewModels.Busqueda;
using Microsoft.IdentityModel.Tokens;


namespace LoCoMPro.Pages.Busqueda
{
    public class BusquedaModel : PageModel
    {
        protected readonly LoCoMProContext _context;
        protected readonly IConfiguration _configuration;

        // Constructor
        public BusquedaModel(LoCoMProContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            // Inicializar
            Inicializar();
        }

        // Busquedas
        [BindProperty(SupportsGet = true)]
        public string? producto { get; set; }
        // Filtros
        public string[] provincias { get; set; } = default!;
        public string[] cantones { get; set; } = default!;
        // Ordenamiento
        public string? ordenPrecio { get; set; } = default!;
        public string? ordenPrecioActual = "desc";


        // Visual
        [BindProperty]
        public IList<string> provinciasV { get; set; } = default!;
        public IList<string> cantonesV { get; set; } = default!;
        public IList<string> tiendasV { get; set; } = default!;
        public IList<string?> marcasV { get; set; } = default!;

        // Paginacion
        public ListaPaginada<BusquedaVM> productosVM { get; set; } = default!;

        // Inicializar atributos
        public void Inicializar()
        {
            // Inicializar
            provinciasV = new List<string>();
            cantonesV = new List<string>();
            tiendasV = new List<string>();
            marcasV = new List<string?>();
            productosVM = new ListaPaginada<BusquedaVM>();
            provincias = new string[] { };
            cantones = new string[] { };
        }

        // ON GET buscar
        public async Task<IActionResult> OnGetAsync(int? indicePagina
            , string? nombreProducto, string? filtroProducto
            , string? nombresProvincias, string? filtrosProvincias
            , string? nombresCantones, string? filtrosCantones
            , string? ordenadoPrecio, string? sentidoPrecio)
        {
            if ((!string.IsNullOrEmpty(nombreProducto) || !string.IsNullOrEmpty(filtroProducto)) && _context.Productos != null)
            {
                // Verificar parámetros y asignar índice de página correcto
                indicePagina = verificarParametros(indicePagina
                    , nombreProducto, filtroProducto
                    , nombresProvincias, filtrosProvincias
                    , nombresCantones, filtrosCantones
                    , ordenadoPrecio, sentidoPrecio);

                // Hacer la consulta de productos con registros
                // Consultar la base de datos
                IQueryable<BusquedaVM> productosIQ = buscarProductos();

                // Cargar filtros
                cargarFiltros(productosIQ);

                // Filtrar
                productosIQ = filtrarProductos(productosIQ);

                // Ordenar por precio
                if (ordenadoPrecio != null)
                {
                    productosIQ = ordenarProducto(productosIQ);
                }

                // Paginar
                await paginarProductos(productosIQ, indicePagina);
            }
            return Page();
        }

        // Verificar parámetros de ON GET Buscar
        protected int? verificarParametros(int? indicePagina
            , string? nombreProducto, string? filtroProducto
            , string? nombresProvincias, string? filtrosProvincias
            , string? nombresCantones, string? filtrosCantones
            , string? ordenadoPrecio, string? sentidoPrecio)
        {
            // Revisar si hay que regresar numero de página
            if (!string.IsNullOrEmpty(nombreProducto))
            {
                indicePagina = 1;
            }
            else
            {
                nombreProducto = filtroProducto;
            }
            producto = nombreProducto;

            if (!string.IsNullOrEmpty(nombresProvincias))
            {
                indicePagina = 1;
            }
            else
            {
                nombresProvincias = filtrosProvincias;
            }
            provincias = !string.IsNullOrEmpty(nombresProvincias) ? nombresProvincias.Split(',') : new string[0];

            if (!string.IsNullOrEmpty(nombresCantones))
            {
                indicePagina = 1;
            }
            else
            {
                nombresCantones = filtrosCantones;
            }
            cantones = !string.IsNullOrEmpty(nombresCantones) ? nombresCantones.Split(',') : new string[0];


            if (!string.IsNullOrEmpty(ordenadoPrecio))
            {
                ordenPrecio = ordenadoPrecio;
            }
            if (!string.IsNullOrEmpty(sentidoPrecio))
            {
                ordenPrecioActual = sentidoPrecio;
            }
            return indicePagina;
        }

        // Buscar productos
        protected IQueryable<BusquedaVM> buscarProductos()
        {
            IQueryable<BusquedaVM> productosIQ = _context.Registros
                        .Include(r => r.producto)
                        .OrderByDescending(r => r.creacion)
                        .GroupBy(r => new { r.productoAsociado, r.nombreTienda, r.nombreProvincia, r.nombreCanton, r.nombreDistrito })
                        .Select(group => new BusquedaVM
                        {
                            nombre = group.First().productoAsociado,
                            precio = group.First().precio,
                            unidad = group.First().producto.nombreUnidad,
                            fecha = group.First().creacion,
                            tienda = group.First().nombreTienda,
                            provincia = group.First().nombreProvincia,
                            canton = group.First().nombreCanton,
                            marca = !string.IsNullOrEmpty(group.First().producto.marca) ?
                                    group.First().producto.marca : "Sin marca"
                        });
            // Buscar por nombre
            productosIQ = buscarNombre(productosIQ);
            // Retornar busqueda
            return productosIQ;
        }

        // Buscar por nombre
        protected IQueryable<BusquedaVM> buscarNombre(IQueryable<BusquedaVM> productosIQ)
        {
            // Ver si se usa el nombre de busqueda
            if (!string.IsNullOrEmpty(producto))
            {
                return productosIQ.Where(r => r.nombre.Contains(producto));
            }
            else
            {
                return productosIQ;
            }
        }

        // Cargar los filtros
        protected void cargarFiltros(IQueryable<BusquedaVM> productosIQ)
        {
            // Cargar filtros de provincia
            cargarFiltrosProvincia(productosIQ);
            // Cargar filtros de canton
            cargarFiltrosCanton(productosIQ);
            // Cargar filtros de tienda
            cargarFiltrosTienda(productosIQ);
            // Cargar filtros de marca
            cargarFiltrosMarca(productosIQ);
        }

        // Cargar los filtros de provincias
        protected void cargarFiltrosProvincia(IQueryable<BusquedaVM> productosIQ)
        {
            // Si los productos no están vacíos
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todas las provincias distintas
                provinciasV = productosIQ
                    .Select(r => r.provincia)
                    .Distinct()
                    .ToList();
            }
        }

        // Cargar los filtros de cantones
        protected void cargarFiltrosCanton(IQueryable<BusquedaVM> productosIQ)
        {
            // Si los productos no están vacíos
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todos los cantones distintos
                cantonesV = productosIQ
                    .Select(r => r.canton)
                    .Distinct()
                    .ToList();
            }
        }

        // Cargar los filtros de tiendas
        protected void cargarFiltrosTienda(IQueryable<BusquedaVM> productosIQ)
        {
            // Si los productos no están vacíos
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todas las tiendas distintas
                tiendasV = productosIQ
                    .Select(r => r.tienda)
                    .Distinct()
                    .ToList();
            }
        }

        // Cargar los filtros de marcas
        protected void cargarFiltrosMarca(IQueryable<BusquedaVM> productosIQ)
        {
            // Si los productos no están vacíos
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todas las marcas distintas
                marcasV = productosIQ
                    .Select(p => p.marca)
                    .Distinct()
                    .ToList();
            }
        }

        // Filtrar productos
        protected IQueryable<BusquedaVM> filtrarProductos(IQueryable<BusquedaVM> productosIQ)
        {
            // Filtrar por provincia
            productosIQ = filtrarProvincia(productosIQ);
            // Filtrar por canton
            productosIQ = filtrarCanton(productosIQ);
            // Retornar productos filtrados
            return productosIQ;
        }

        // Filtrar por provincia
        protected IQueryable<BusquedaVM> filtrarProvincia(IQueryable<BusquedaVM> productosIQ)
        {
            if (provincias.Length > 0)
            {
                // Asignar filtro
                string[] filtro = provincias;
                // Filtrar por provincia
                productosIQ = productosIQ.Where(r => filtro.Contains(r.provincia));

            }
            // Rertornar resultados
            return productosIQ;
        }

        // Filtrar por canton
        protected IQueryable<BusquedaVM> filtrarCanton(IQueryable<BusquedaVM> productosIQ)
        {
            if (cantones.Length > 0)
            {
                // Convertir provincias a lista
                string[] filtro = cantones;
                // Filtrar
                productosIQ = productosIQ.Where(r => filtro.Contains(r.canton));

            }
            // Rertornar resultados
            return productosIQ;
        }

        // Ordenar producto
        public IQueryable<BusquedaVM> ordenarProducto(IQueryable<BusquedaVM> productosIQ)
        {
            // Ordenar por precio
            productosIQ = ordenarPorPrecio(productosIQ);
            return productosIQ;
        }

        public IQueryable<BusquedaVM> ordenarPorPrecio(IQueryable<BusquedaVM> productosIQ)
        {
            if (!string.IsNullOrEmpty(ordenPrecio))
            {
                if (ordenPrecio == "precio")
                {
                    if (ordenPrecioActual == "asc")
                    {
                        // Ordenar por precio ascendente
                        productosIQ = productosIQ.OrderBy(p => p.precio);
                    } else
                    {
                        // Ordenar por precio descendente
                        productosIQ = productosIQ.OrderByDescending(p => p.precio);

                    }
                }
            }
            return productosIQ;
        }

        // Paginar productos
        protected async Task paginarProductos(IQueryable<BusquedaVM> productosFinales, int? indicePagina)
        {
            // Obtener tamaño de página
            var tamPagina = _configuration.GetValue("TamPagina", 4);
            // Crear productos paginados
            productosVM = await ListaPaginada<BusquedaVM>.CrearAsync(
                productosFinales.AsNoTracking(), indicePagina ?? 1, tamPagina);
        }
    }
}
