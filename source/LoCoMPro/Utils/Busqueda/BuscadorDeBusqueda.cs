using LoCoMPro.Data;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.ViewModels.Busqueda;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Utils.Busqueda
{
    // Buscador especializado para la pagina de busqueda
    public class BuscadorDeBusqueda : IBuscador<BusquedaVM>
    {
        // Contexto
        protected readonly LoCoMProContext contexto;

        // Busquedas
        protected string? producto { get; set; }

        // Constructor
        public BuscadorDeBusqueda(LoCoMProContext contexto, string? producto = null)
        {
            this.contexto = contexto;
            this.producto = producto;
        }

        // Setters
        public void setProducto(string? producto)
        {
            this.producto = producto;
        }

        // Buscar
        public virtual IQueryable<BusquedaVM> buscar()
        {
            IQueryable<BusquedaVM> resultadosIQ = buscarTodo();
            // Buscar por producto
            resultadosIQ = this.buscarProducto(resultadosIQ);
            return resultadosIQ;
        }

        // Buscar todo
        protected IQueryable<BusquedaVM> buscarTodo()
        {
            IQueryable<BusquedaVM> resultadosIQ = this.contexto.Registros
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
            return resultadosIQ;
        }
        // Buscar por nombre
        protected IQueryable<BusquedaVM> buscarProducto(IQueryable<BusquedaVM> entradaIQ)
        {
            // Ver si se usa el nombre de busqueda
            if (!string.IsNullOrEmpty(producto))
            {
                return entradaIQ.Where(r => r.nombre.Contains(producto));
            }
            else
            {
                return entradaIQ;
            }
        }
    }
}
