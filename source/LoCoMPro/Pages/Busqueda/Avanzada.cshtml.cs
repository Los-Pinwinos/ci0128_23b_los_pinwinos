using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Busqueda;


namespace LoCoMPro.Pages.Busqueda
{
    public class BusquedaAvanzadaModel : BusquedaModel
    {
        // Constructor
        public BusquedaAvanzadaModel(LoCoMProContext contexto, IConfiguration configuracion) 
            : base(contexto, configuracion)
        {
            // Inicializar
            this.InicializarAvanzado();
        }

        // Avanzado
        [BindProperty(SupportsGet = true)]
        public string? marca { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? provincia { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? canton { get; set; }

        // Inicializar avanzado
        private void InicializarAvanzado()
        {
            // Inicializar
            this.producto = "";
            this.marca = "";
            this.provincia = "";
            this.canton = "";
        }

        // On GET avanzado
        public async Task<IActionResult> OnGetBuscarAvanzadoAsync(int? indicePagina
            , string? nombreProducto, string? filtroProducto
            , string? nombreMarca, string? filtroMarca
            , string? nombreProvincia, string? filtroProvincia
            , string? nombreCanton, string? filtroCanton
            , string? nombresProvincias, string? filtrosProvincias
            , string? nombresCantones, string? filtrosCantones
            , string? nombresTiendas, string? filtrosTiendas
            , string? nombresMarcas, string? filtrosMarcas
            , string? columnaOrdenado, string? sentidoOrdenado)
        {
            return Page();
        }

        // Verificar parámetros
        private int? verificarParametrosAvanzados(int? indicePagina
            , string? nombreMarca, string? filtroMarca
            , string? nombreProvincia, string? filtroProvincia
            , string? nombreCanton, string? filtroCanton)
        {
            // Revisar si hay que regresar numero de página
            if (!string.IsNullOrEmpty(nombreMarca))
            {
                indicePagina = 1;
            }
            else
            {
                nombreMarca = filtroMarca;
            }
            this.marca = nombreMarca;

            if (!string.IsNullOrEmpty(nombreProvincia))
            {
                indicePagina = 1;
            }
            else
            {
                nombreProvincia = filtroProvincia;
            }
            this.provincia = nombreProvincia;

            if (!string.IsNullOrEmpty(nombreCanton))
            {
                indicePagina = 1;
            }
            else
            {
                nombreCanton = filtroCanton;
            }
            this.canton = nombreCanton;

            return indicePagina;
        }

        // Sobrecarga de buscar productos
        new protected IQueryable<BusquedaVM> buscarProductos()
        {
            IQueryable<BusquedaVM> productosIQ = contexto.Registros
                .Include(r => r.producto)
                .GroupBy(r => new
                {
                    r.productoAsociado,
                    r.nombreTienda,
                    r.nombreProvincia,
                    r.nombreCanton,
                    r.nombreDistrito
                })
                .Select(group => new BusquedaVM
                {
                    nombre = group.Key.productoAsociado,
                    precio = group.OrderByDescending(item => item.creacion).First().precio,
                    unidad = group.First().producto.nombreUnidad,
                    fecha = group.OrderByDescending(item => item.creacion).First().creacion,
                    tienda = group.Key.nombreTienda,
                    provincia = group.Key.nombreProvincia,
                    canton = group.Key.nombreCanton,
                    marca = !string.IsNullOrEmpty(group.First().producto.marca) ?
                        group.First().producto.marca : "Sin marca"
                });
      
            // Retornar busqueda
            return productosIQ;
        }

        // Buscar por marca
        private IQueryable<BusquedaVM> buscarMarca(IQueryable<BusquedaVM> productosIQ)
        {
            // Ver si se usa el nombre de busqueda
            if (!string.IsNullOrEmpty(marca))
            {
                return productosIQ.Where(r => r.marca.Contains(marca));
            }
            else
            {
                return productosIQ;
            }
        }

        // Buscar por provincia
        private IQueryable<BusquedaVM> buscarProvincia(IQueryable<BusquedaVM> productosIQ)
        {
            // Ver si se usa el nombre de busqueda
            if (!string.IsNullOrEmpty(provincia))
            {
                return productosIQ.Where(r => r.provincia.Contains(provincia));
            }
            else
            {
                return productosIQ;
            }
        }

        // Buscar por canton
        private IQueryable<BusquedaVM> buscarCanton(IQueryable<BusquedaVM> productosIQ)
        {
            // Ver si se usa el nombre de busqueda
            if (!string.IsNullOrEmpty(canton))
            {
                return productosIQ.Where(r => r.canton.Contains(canton));
            }
            else
            {
                return productosIQ;
            }
        }
    }
}
