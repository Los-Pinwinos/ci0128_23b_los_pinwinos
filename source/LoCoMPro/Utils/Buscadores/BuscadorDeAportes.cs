using LoCoMPro.Data;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.ViewModels.Cuenta;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Utils.Buscadores
{
    // Buscador especializado para la pagina de busqueda
    public class BuscadorDeAportes : IBuscador<AporteVM>
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
        public IQueryable<AporteVM> buscar()
        {
            IQueryable<AporteVM> resultadosIQ = this.contexto.Registros
                .Where(r => r.usuarioCreador == this.usuario &&
                       r.visible)
                .OrderByDescending(r => r.creacion)
                .Select(r => new AporteVM
                {
                    fecha = r.creacion,
                    producto = r.productoAsociado,
                    precio = r.precio,
                    unidad = r.producto.nombreUnidad,
                    categoria = r.producto.nombreCategoria,
                    tienda = r.nombreTienda,
                    provincia = r.nombreProvincia,
                    canton = r.nombreCanton,
                    calificacion = r.calificacion
                });

            return resultadosIQ;
        }
    }
}