using LoCoMPro.Data;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.ViewModels.Busqueda;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Utils.Buscadores
{
    // Buscador especializado para la pagina de busqueda
    public class BuscadorDeAportes : IBuscador<BusquedaVM>
    {
        // Contexto
        protected readonly LoCoMProContext contexto;

        // Usuario que realizó los aportes
        protected string? usuario { get; set; }

        // Constructor
        public BuscadorDeAportes(LoCoMProContext contexto, string? usuario = null)
        {
            this.contexto = contexto;
            this.usuario = usuario;
        }

        // Setters
        public void setUsuario(string? usuario)
        {
            this.usuario = usuario;
        }
  
        // Buscar aportes del usuario
        public IQueryable<BusquedaVM> buscar()
        {
            IQueryable<BusquedaVM> resultadosIQ = this.contexto.Registros
                .Where(r => r.usuarioCreador == this.usuario)
                .OrderByDescending(r => r.creacion)
                .Select(r => new BusquedaVM
                {
                    nombre = r.productoAsociado,
                    precio = r.precio,
                    unidad = r.producto.nombreUnidad,
                    fecha = r.creacion,
                    tienda = r.nombreTienda,
                    provincia = r.nombreProvincia,
                    canton = r.nombreCanton,
                    marca = !string.IsNullOrEmpty(r.producto.marca) ? r.producto.marca : "Sin marca",
                    categoria = r.producto.nombreCategoria
                });

            return resultadosIQ;
        }
    }
}