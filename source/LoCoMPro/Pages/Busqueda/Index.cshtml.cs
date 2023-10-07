using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Busqueda;
using Microsoft.IdentityModel.Tokens;


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
        // Filtros
        public string[] provincias { get; set; } = default!;
        public string[] cantones { get; set; } = default!;
        // Ordenamiento
        public string? columnaOrdenActual { get; set; }
        public string? sentidoOrdenActual { get; set; }

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
            this.provincias = new string[] { };
            this.cantones = new string[] { };

            this.columnaOrdenActual = null;
            this.sentidoOrdenActual = "asc";

            this.provinciasV = new List<string>();
            this.cantonesV = new List<string>();
            this.tiendasV = new List<string>();
            this.marcasV = new List<string?>();

            this.productosVM = new ListaPaginada<BusquedaVM>();
        }

        // ON GET buscar
        public async Task<IActionResult> OnGetAsync(int? indicePagina
            , string? nombreProducto, string? filtroProducto
            , string? nombresProvincias, string? filtrosProvincias
            , string? nombresCantones, string? filtrosCantones
            , string? columnaOrdenado, string? sentidoOrdenado)
        {
            if ((!string.IsNullOrEmpty(nombreProducto) ||
                !string.IsNullOrEmpty(filtroProducto)) &&
                contexto.Productos != null)
            {
                // Verificar parámetros y asignar índice de página correcto
                indicePagina = this.verificarParametros(indicePagina
                    , nombreProducto, filtroProducto
                    , nombresProvincias, filtrosProvincias
                    , nombresCantones, filtrosCantones
                    , columnaOrdenado, sentidoOrdenado);

                // Hacer la consulta de productos con registros
                // Consultar la base de datos
                IQueryable<BusquedaVM> productosIQ = this.buscarProductos();

                // Cargar filtros
                this.cargarFiltros(productosIQ);

                // Filtrar
                productosIQ = this.filtrarProductos(productosIQ);

                // Ordenar
                productosIQ = this.ordenarProductos(productosIQ);

                // Paginar
                await this.paginarProductos(productosIQ, indicePagina);
            }
            return Page();
        }

        // Verificar parámetros de ON GET Buscar
        protected int? verificarParametros(int? indicePagina
            , string? nombreProducto, string? filtroProducto
            , string? nombresProvincias, string? filtrosProvincias
            , string? nombresCantones, string? filtrosCantones
            , string? columnaOrdenado, string? sentidoOrdenado)
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
            this.provincias = !string.IsNullOrEmpty(nombresProvincias) ?
                nombresProvincias.Split(',') : new string[0];

            if (!string.IsNullOrEmpty(nombresCantones))
            {
                indicePagina = 1;
            }
            else
            {
                nombresCantones = filtrosCantones;
            }
            this.cantones = !string.IsNullOrEmpty(nombresCantones) ?
                nombresCantones.Split(',') : new string[0];

            // En caso de haber recibido una columna para ordernar
            // y un sentido
            if (!string.IsNullOrEmpty(columnaOrdenado) &&
                !string.IsNullOrEmpty(sentidoOrdenado))
            {
                // Guarda los valores recibidos en sus propiedades
                this.columnaOrdenActual = columnaOrdenado;
                this.sentidoOrdenActual = sentidoOrdenado;
            }
            // Si no se recibió ambos parametros de ordenado
            else
            {
                // Se reinician a sus valores originales
                this.columnaOrdenActual = null;
                this.sentidoOrdenActual = "asc";
            }
            
            return indicePagina;
        }

        // Buscar productos
        protected IQueryable<BusquedaVM> buscarProductos()
        {
            IQueryable<BusquedaVM> productosIQ = contexto.Registros
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
            productosIQ = this.buscarNombre(productosIQ);
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
            this.cargarFiltrosProvincia(productosIQ);
            // Cargar filtros de canton
            this.cargarFiltrosCanton(productosIQ);
            // Cargar filtros de tienda
            this.cargarFiltrosTienda(productosIQ);
            // Cargar filtros de marca
            this.cargarFiltrosMarca(productosIQ);
        }

        // Cargar los filtros de provincias
        protected void cargarFiltrosProvincia(IQueryable<BusquedaVM> productosIQ)
        {
            // Si los productos no están vacíos
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
        protected void cargarFiltrosCanton(IQueryable<BusquedaVM> productosIQ)
        {
            // Si los productos no están vacíos
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
        protected void cargarFiltrosTienda(IQueryable<BusquedaVM> productosIQ)
        {
            // Si los productos no están vacíos
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
        protected void cargarFiltrosMarca(IQueryable<BusquedaVM> productosIQ)
        {
            // Si los productos no están vacíos
            if (!productosIQ.IsNullOrEmpty())
            {
                // Obtener todas las marcas distintas
                this.marcasV = productosIQ
                    .Select(p => p.marca)
                    .Distinct()
                    .ToList();
            }
        }

        // Filtrar productos
        protected IQueryable<BusquedaVM> filtrarProductos(IQueryable<BusquedaVM> productosIQ)
        {
            // Filtrar por provincia
            productosIQ = this.filtrarProvincia(productosIQ);
            // Filtrar por canton
            productosIQ = this.filtrarCanton(productosIQ);
            // Retornar productos filtrados
            return productosIQ;
        }

        // Filtrar por provincia
        protected IQueryable<BusquedaVM> filtrarProvincia(IQueryable<BusquedaVM> productosIQ)
        {
            if (this.provincias.Length > 0)
            {
                // Asignar filtro
                string[] filtro = this.provincias;
                // Filtrar por provincia
                productosIQ = productosIQ.Where(r => filtro.Contains(r.provincia));

            }
            // Rertornar resultados
            return productosIQ;
        }

        // Filtrar por canton
        protected IQueryable<BusquedaVM> filtrarCanton(IQueryable<BusquedaVM> productosIQ)
        {
            if (this.cantones.Length > 0)
            {
                // Convertir provincias a lista
                string[] filtro = this.cantones;
                // Filtrar
                productosIQ = productosIQ.Where(r => filtro.Contains(r.canton));

            }
            // Rertornar resultados
            return productosIQ;
        }

        // Ordenar producto
        public IQueryable<BusquedaVM> ordenarProductos(IQueryable<BusquedaVM> productosIQ)
        {
            // En caso de tener parametros de ordenamiento
            if (!string.IsNullOrEmpty(this.columnaOrdenActual) &&
                !string.IsNullOrEmpty(this.sentidoOrdenActual))
            {
                // Ordena los productos
                productosIQ = this.ordenar(productosIQ);
            }

            // Retorna los productos ordenados
            return productosIQ;
        }

        protected IQueryable<BusquedaVM> ordenar(IQueryable<BusquedaVM> productosIQ)
        {
            // Ordenamientos ascendentes
            if (this.sentidoOrdenActual == "asc")
            {
                // Columna de ordenado
                switch (this.columnaOrdenActual)
                {
                    case "precio":
                        // Ordenar por precio ascendente
                        productosIQ = productosIQ.OrderBy(p => p.precio);
                        break;

                    default:
                        break;
                }
            }
            // Ordenamientos descendentes
            else
            {
                // Columna de ordenado
                switch (this.columnaOrdenActual)
                {
                    case "precio":
                        // Ordenar por precio descendente
                        productosIQ = productosIQ.OrderByDescending(p => p.precio);
                        break;

                    case "provincia":
                        // Ordenar por provincia descendente
                        productosIQ = productosIQ.OrderByDescending(p => p.provincia);
                        break;

                    default:
                        break;
                }
            }
            return productosIQ;
        }

        // Paginar productos
        protected async Task paginarProductos(IQueryable<BusquedaVM> productosFinales, int? indicePagina)
        {
            // Obtener tamaño de página
            var tamPagina = configuracion.GetValue("TamPagina", 4);
            // Crear productos paginados
            this.productosVM = await ListaPaginada<BusquedaVM>.CrearAsync(
                productosFinales.AsNoTracking(), indicePagina ?? 1, tamPagina);
        }
    }
}
