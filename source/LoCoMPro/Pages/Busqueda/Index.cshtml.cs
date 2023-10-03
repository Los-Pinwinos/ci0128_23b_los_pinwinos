using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.ViewModels.Busqueda;
using Microsoft.IdentityModel.Tokens;


namespace LoCoMPro.Pages.Busqueda
{
    public class ModeloBusqueda : PageModel
    {
        private readonly LoCoMProContext contexto;
        private readonly IConfiguration configuracion;

        // Constructor
        public ModeloBusqueda(LoCoMProContext context, IConfiguration configuration)
        {
            contexto = context;
            configuracion = configuration;
            // Inicializar
            this.Inicializar();
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

        // Ordenamiento
        public string? ordenProvincia { get; set; }

        // Inicializar atributos
        public void Inicializar()
        {
            // Inicializar
            this.productosVM = new List<BusquedaVM>();
            this.provinciasV = new List<string>();
            this.cantonesV = new List<string>();
            this.tiendasV = new List<string>();
            this.marcasV = new List<string?>();
            this.productosPaginados = new ListaPaginada<Registro>();
            this.ordenProvincia = null;
        }

        // ON GET buscar
        public async Task<IActionResult> OnGetAsyncBuscar(int? indicePagina
            , string? nombreProducto, string? filtroProducto
            , string? nombresProvincias, string? filtrosProvincias
            , string? nombresCantones, string? filtrosCantones)
        {
            if ((!string.IsNullOrEmpty(nombreProducto) || !string.IsNullOrEmpty(filtroProducto)) && contexto.Productos != null)
            {
                // Verificar parámetros y asignar índice de página correcto
                indicePagina = verificarParametros(indicePagina
                    , nombreProducto, filtroProducto
                    , nombresProvincias, filtrosProvincias
                    , nombresCantones, filtrosCantones);

                // Hacer la consulta de productos con registros
                IQueryable<Registro> productosIQ = this.buscarProductos();

                // Cargar filtros
                this.cargarFiltros(productosIQ);

                // Filtrar
                productosIQ = this.filtrarProductos(productosIQ);

                // Paginar
                await this.paginarProductos(productosIQ, indicePagina);
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
            this.producto = nombreProducto;

            if (!string.IsNullOrEmpty(nombresProvincias))
            {
                indicePagina = 1;
            }
            else
            {
                nombresProvincias = filtrosProvincias;
            }
            this.provincias = !string.IsNullOrEmpty(nombresProvincias) ? nombresProvincias.Split(',') : new string[0];

            if (!string.IsNullOrEmpty(nombresCantones))
            {
                indicePagina = 1;
            }
            else
            {
                nombresCantones = filtrosCantones;
            }
            this.cantones = !string.IsNullOrEmpty(nombresCantones) ? nombresCantones.Split(',') : new string[0];

            return indicePagina;
        }

        // Buscar productos
        private IQueryable<Registro> buscarProductos()
        {
            // Consultar la base de datos
            IQueryable<Registro> productosIQ = contexto.Registros
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
            productosIQ = this.buscarNombre(productosIQ);
            // Retornar busqueda
            return productosIQ;
        }

        // Buscar por nombre
        private IQueryable<Registro> buscarNombre(IQueryable<Registro> productosIQ)
        {
            // Ver si se usa el nombre de busqueda
            if (!string.IsNullOrEmpty(this.producto))
            {
                return productosIQ.Where(p => p.producto.nombre.Contains(this.producto));
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
            this.cargarFiltrosProvincia(productosIQ);
            // Cargar filtros de canton
            this.cargarFiltrosCanton(productosIQ);
            // Cargar filtros de tienda
            this.cargarFiltrosTienda(productosIQ);
            // Cargar filtros de marca
            this.cargarFiltrosMarca(productosIQ);
        }

        // Cargar los filtros de provincias
        private void cargarFiltrosProvincia(IQueryable<Registro> productosIQ)
        {
            // Si los productos no están vacíos
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todas las provincias distintas
                this.provinciasV = productosIQ
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
                this.cantonesV = productosIQ
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
                this.tiendasV = productosIQ
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
                this.marcasV = productosIQ.Where(p => p.producto.marca != null)
                    .Select(p => p.producto.marca).Distinct().ToList() ?? new List<string?>();
            }
        }
        
        // Filtrar productos
        private IQueryable<Registro> filtrarProductos(IQueryable<Registro> productosIQ)
        {
            // Filtrar por provincia
            productosIQ = this.filtrarProvincia(productosIQ);
            // Filtrar por canton
            productosIQ = this.filtrarCanton(productosIQ);
            // Retornar productos filtrados
            return productosIQ;
        }

        // Filtrar por provincia
        private IQueryable<Registro> filtrarProvincia(IQueryable<Registro> productosIQ)
        {
            if (this.provincias.Length > 0)
            {
                // Asignar filtro
                string[] filtro = this.provincias;
                // Filtrar por provincia
                productosIQ = productosIQ.Where(r => filtro.Contains(r.nombreProvincia));

            }
            // Rertornar resultados
            return productosIQ;
        }

        // Filtrar por canton
        private IQueryable<Registro> filtrarCanton(IQueryable<Registro> productosIQ)
        {
            if (this.cantones.Length > 0)
            {
                // Convertir provincias a lista
                string[] filtro = this.cantones;
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
            var tamPagina = this. configuracion.GetValue("TamPagina", 4);
            // Crear productos paginados
            this.productosPaginados = await ListaPaginada<Registro>.CrearAsync(
                productosFinales.AsNoTracking(), indicePagina ?? 1, tamPagina);

            // Por cada resultado
            // Por cada resultado
            foreach (var productoRegistro in this.productosPaginados)
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
                this.productosVM.Add(nuevoProductoVM);
            }
        }
    }
}
