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
        private readonly LoCoMProContext _context;
        private readonly IConfiguration _configuration;

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
        public ListaPaginada<Registro> productosPaginados { get; set; } = default!;
        
        // Inicializar atributos
        public void Inicializar()
        {
            // Inicializar
            productosVM = new List<BusquedaVM>();
            provinciasV = new List<string>();
            cantonesV = new List<string>();
            tiendasV = new List<string>();
            marcasV = new List<string?>();
            productosPaginados = new ListaPaginada<Registro>();
        }

        // ON GET buscar
        public async Task<IActionResult> OnGetAsyncBuscar(int? indicePagina
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
                IQueryable<Registro> productosIQ = buscarProductos();

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
        private IQueryable<Registro> buscarProductos()
        {
            // Consultar la base de datos
            IQueryable<Registro> productosIQ = _context.Registros
                    .OrderByDescending(r => r.creacion)
                    .GroupBy(r => new { r.productoAsociado, r.nombreTienda, r.nombreProvincia, r.nombreCanton, r.nombreDistrito })
                    .Select(group => new Registro
                    {
                        usuarioCreador = group.First().usuarioCreador,
                        creador = group.First().creador,
                        creacion = group.First().creacion,
                        descripcion = group.First().descripcion,
                        precio = group.First().precio,
                        productoAsociado = group.First().productoAsociado,
                        producto = group.First().producto,
                        nombreTienda = group.First().nombreTienda,
                        nombreDistrito = group.First().nombreDistrito,
                        nombreCanton = group.First().nombreCanton,
                        nombreProvincia = group.First().nombreProvincia,
                        tienda = group.First().tienda,
                        etiquetas = group.First().etiquetas,
                        fotografias = group.First().fotografias
                    });
            // Buscar por nombre
            productosIQ = buscarNombre(productosIQ);
            // Retornar busqueda
            return productosIQ;
        }

        // Buscar por nombre
        private IQueryable<Registro> buscarNombre(IQueryable<Registro> productosIQ)
        {
            // Ver si se usa el nombre de busqueda
            if (!string.IsNullOrEmpty(producto))
            {
                return productosIQ.Where(p => p.producto.nombre.Contains(producto));
            }
            else
            {
                return productosIQ;
            }
        }

        // Cargar los filtros
        private void cargarFiltros(IQueryable<Registro> productosIQ)
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
        private void cargarFiltrosProvincia(IQueryable<Registro> productosIQ)
        {
            // Si los productos no están vacíos
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todas las provincias distintas
                provinciasV = productosIQ
                    .Select(r => r.nombreProvincia)
                    .Distinct()
                    .ToList();
            }
        }

        // Cargar los filtros de cantones
        private void cargarFiltrosCanton(IQueryable<Registro> productosIQ)
        {
            // Si los productos no están vacíos
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todos los cantones distintos
                cantonesV = productosIQ
                    .Select(r => r.nombreCanton)
                    .Distinct()
                    .ToList();
            }
        }

        // Cargar los filtros de tiendas
        private void cargarFiltrosTienda(IQueryable<Registro> productosIQ)
        {
            // Si los productos no están vacíos
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todas las tiendas distintas
                tiendasV = productosIQ
                    .Select(r => r.nombreTienda)
                    .Distinct()
                    .ToList();
            }
        }

        // Cargar los filtros de marcas
        private void cargarFiltrosMarca(IQueryable<Registro> productosIQ)
        {
            // Si los productos no están vacíos
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todas las marcas distintas
                marcasV = productosIQ.Where(p => p.producto.marca != null)
                    .Select(p => p.producto.marca).Distinct().ToList() ?? new List<string?>();
            }
        }
        
        // Filtrar productos
        private IQueryable<Registro> filtrarProductos(IQueryable<Registro> productosIQ)
        {
            // Filtrar por provincia
            productosIQ = filtrarProvincia(productosIQ);
            // Filtrar por canton
            productosIQ = filtrarCanton(productosIQ);
            // Retornar productos filtrados
            return productosIQ;
        }

        // Filtrar por provincia
        private IQueryable<Registro> filtrarProvincia(IQueryable<Registro> productosIQ)
        {
            if (provincias.Length > 0)
            {
                // Asignar filtro
                string[] filtro = provincias;
                // Filtrar por provincia
                productosIQ = productosIQ.Where(r => filtro.Contains(r.nombreProvincia));

            }
            // Rertornar resultados
            return productosIQ;
        }

        // Filtrar por canton
        private IQueryable<Registro> filtrarCanton(IQueryable<Registro> productosIQ)
        {
            if (cantones.Length > 0)
            {
                // Convertir provincias a lista
                string[] filtro = cantones;
                // Filtrar
                productosIQ = productosIQ.Where(r => filtro.Contains(r.nombreCanton));

            }
            // Rertornar resultados
            return productosIQ;
        }

        // Paginar productos
        private async Task paginarProductos(IQueryable<Registro> productosFinales, int? indicePagina)
        {
            // Obtener tamaño de página
            var tamPagina = _configuration.GetValue("TamPagina", 4);
            // Crear productos paginados
            productosPaginados = await ListaPaginada<Registro>.CrearAsync(
                productosFinales.AsNoTracking(), indicePagina ?? 1, tamPagina);

            // Por cada resultado
            // Por cada resultado
            foreach (var productoRegistro in productosPaginados)
            {
                // Crear producto VM
                var nuevoProductoVM = new BusquedaVM
                {
                    nombre = productoRegistro.productoAsociado,
                    precio = productoRegistro.precio,
                    unidad = productoRegistro.producto.nombreUnidad,
                    fecha = productoRegistro.creacion,
                    tienda = productoRegistro.nombreTienda,
                    provincia = productoRegistro.nombreProvincia,
                    canton = productoRegistro.nombreCanton,
                    marca = productoRegistro.producto.marca != null ? productoRegistro.producto.marca : "Sin marca"
                };
                // Agregar a productos
                productosVM.Add(nuevoProductoVM);
            }
        }
    }
}
