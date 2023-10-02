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
    public class BusquedaVMModel : PageModel
    {
        private readonly LoCoMProContext _context;
        private readonly IConfiguration _configuration;

        // Constructor
        public BusquedaVMModel(LoCoMProContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            // Inicializar
            Inicializar();
        }

        // Busquedas
        [BindProperty(SupportsGet = true)]
        public string? producto { get; set; }
        public string[] provincias { get; set; } = default!;
        public string[] cantones { get; set; } = default!;

        // Visual
        [BindProperty]
        public IList<BusquedaVM> productosVM { get; set; } = default!;
        public IList<string> provinciasV { get; set; } = default!;
        public IList<string> cantonesV { get; set; } = default!;
        public IList<string> tiendasV { get; set; } = default!;
        public IList<string?> marcasV { get; set; } = default!;

        // Paginacion
        public ListaPaginada<Producto> productosPaginados { get; set; } = default!;
        
        // Inicializar atributos
        public void Inicializar()
        {
            // Inicializar
            productosVM = new List<BusquedaVM>();
            provinciasV = new List<string>();
            cantonesV = new List<string>();
            tiendasV = new List<string>();
            marcasV = new List<string?>();
            productosPaginados = new ListaPaginada<Producto>();
        }

        // ON GET buscar
        public async Task<IActionResult> OnGetAsync(int? indicePagina
            , string? nombreProducto, string? filtroProducto
            , string? nombresProvincias, string? filtrosProvincias
            , string? nombresCantones, string? filtrosCantones)
        {
            if ((!string.IsNullOrEmpty(nombreProducto) || !string.IsNullOrEmpty(filtroProducto)) && _context.Productos != null)
            {
                // Verificar parámetros y asignar índice de página correcto
                indicePagina = verificarParametros(indicePagina
                    , nombreProducto, filtroProducto
                    , nombresProvincias, filtrosProvincias
                    , nombresCantones, filtrosCantones);

                // Hacer la consulta de productos con registros
                IQueryable<Producto> productosIQ = buscarProductos();

                // Cargar filtros
                cargarFiltros(productosIQ);

                // Filtrar
                productosIQ = filtrarProductos(productosIQ);

                // Paginar
                await paginarProductos(productosIQ, indicePagina);
            }
            return Page();
        }

        // Verificar parámetros de ON GET Buscar
        private int? verificarParametros(int? indicePagina
            , string? nombreProducto, string? filtroProducto
            , string? nombresProvincias, string? filtrosProvincias
            , string? nombresCantones, string? filtrosCantones)
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

            return indicePagina;
        }

        // Buscar productos
        private IQueryable<Producto> buscarProductos()
        {
            // Consultar la base de datos
            IQueryable<Producto> productosIQ = _context.Productos
                    .Where(p => p.registros != null)
                    .Include(p => p.registros.OrderByDescending(r => r.creacion));
            // Buscar por nombre
            productosIQ = buscarNombre(productosIQ);
            // Retornar busqueda
            return productosIQ;
        }

        // Buscar por nombre
        private IQueryable<Producto> buscarNombre(IQueryable<Producto> productosIQ)
        {
            // Ver si se usa el nombre de busqueda
            if (!string.IsNullOrEmpty(producto))
            {
                return productosIQ.Where(s => s.nombre.Contains(producto));
            }
            else
            {
                return productosIQ;
            }
        }

        // Cargar los filtros
        private void cargarFiltros(IQueryable<Producto> productosIQ)
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
        private void cargarFiltrosProvincia(IQueryable<Producto> productosIQ)
        {
            // Si los productos no están vacíos
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todas las provincias distintas
                provinciasV = productosIQ
                    .SelectMany(p => p.registros.Select(r => r.nombreProvincia))
                    .Distinct()
                    .ToList();
            }
        }

        // Cargar los filtros de cantones
        private void cargarFiltrosCanton(IQueryable<Producto> productosIQ)
        {
            // Si los productos no están vacíos
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todos los cantones distintos
                cantonesV = productosIQ
                    .SelectMany(p => p.registros.Select(r => r.nombreCanton))
                    .Distinct()
                    .ToList();
            }
        }

        // Cargar los filtros de tiendas
        private void cargarFiltrosTienda(IQueryable<Producto> productosIQ)
        {
            // Si los productos no están vacíos
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todas las tiendas distintas
                tiendasV = productosIQ
                    .SelectMany(p => p.registros.Select(r => r.nombreTienda))
                    .Distinct()
                    .ToList();
            }
        }

        // Cargar los filtros de marcas
        private void cargarFiltrosMarca(IQueryable<Producto> productosIQ)
        {
            // Si los productos no están vacíos
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todas las marcas distintas
                marcasV = productosIQ.Where(p => p.marca != null).Select(p => p.marca).Distinct().ToList() ?? new List<string?>();
            }
        }
        
        // Filtrar productos
        private IQueryable<Producto> filtrarProductos(IQueryable<Producto> productosIQ)
        {
            // Filtrar por provincia
            productosIQ = filtrarProvincia(productosIQ);
            // Filtrar por canton
            productosIQ = filtrarCanton(productosIQ);
            // Retornar productos filtrados
            return productosIQ;
        }

        // Filtrar por provincia
        private IQueryable<Producto> filtrarProvincia(IQueryable<Producto> productosIQ)
        {
            if (provincias.Length > 0)
            {
                // Asignar filtro
                string[] filtro = provincias;
                // Filtrar por provincia
                productosIQ = productosIQ
                    .Where(p => p.registros.Any(r => filtro.Contains(r.nombreProvincia)))
                    .Select(p => new Producto
                    {
                        nombre = p.nombre,
                        marca = p.marca,
                        nombreUnidad = p.nombreUnidad,
                        nombreCategoria = p.nombreCategoria,
                        registros = p.registros
                            .OrderByDescending(r => filtro.Contains(r.nombreProvincia))  // Ordenar por provincia
                            .ThenByDescending(r => r.creacion) // Ordenar por fecha
                            .ToList()
                    });
            }
            // Rertornar resultados
            return productosIQ;
        }

        // Filtrar por canton
        private IQueryable<Producto> filtrarCanton(IQueryable<Producto> productosIQ)
        {
            if (cantones.Length > 0)
            {
                // Convertir provincias a lista
                string[] filtro = cantones;
                // Filtrar
                productosIQ = productosIQ
                    .Where(p => p.registros.Any(r => filtro.Contains(r.nombreCanton)))
                    .Select(p => new Producto
                    {
                        nombre = p.nombre,
                        marca = p.marca,
                        nombreUnidad = p.nombreUnidad,
                        nombreCategoria = p.nombreCategoria,
                        registros = p.registros
                            .OrderByDescending(r => filtro.Contains(r.nombreCanton))  // Ordenar por canton
                            .ThenByDescending(r => r.creacion) // Ordenar por fecha
                            .ToList()
                    });
            }
            // Rertornar resultados
            return productosIQ;
        }

        // Paginar productos
        private async Task paginarProductos(IQueryable<Producto> productosFinales, int? indicePagina)
        {
            // Obtener tamaño de página
            var tamPagina = _configuration.GetValue("TamPagina", 4);
            // Crear productos paginados
            productosPaginados = await ListaPaginada<Producto>.CrearAsync(
                productosFinales.AsNoTracking(), indicePagina ?? 1, tamPagina);

            // Por cada resultado
            foreach (var producto in productosPaginados)
            {
                // Verificar que el producto tenga registros asociados
                if (producto.registros != null)
                {
                    // Convertir a lista
                    IList<Registro> listaRegistros = producto.registros.ToList();
                    // Ver si ya no hay resultados
                    if (listaRegistros.Count > 0)
                    {
                        // Obtener el registro mas reciente
                        Registro registroMasReciente = listaRegistros.First();
                        // Crear producto VM
                        var nuevoProductoVM = new BusquedaVM
                        {
                            nombre = producto.nombre,
                            precio = registroMasReciente.precio,
                            unidad = producto.nombreUnidad,
                            fecha = registroMasReciente.creacion,
                            tienda = registroMasReciente.nombreTienda,
                            provincia = registroMasReciente.nombreProvincia,
                            canton = registroMasReciente.nombreCanton,
                            marca = producto.marca
                        };
                        // Agregar a productos
                        productosVM.Add(nuevoProductoVM);
                    }
                }
            }
        }
    }
}
