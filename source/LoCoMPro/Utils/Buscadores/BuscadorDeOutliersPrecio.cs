using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.ViewModels.Busqueda;
using LoCoMPro.ViewModels.Cuenta;
using LoCoMPro.ViewModels.Moderacion;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LoCoMPro.Utils.Buscadores
{
    public class BuscadorDeOutliersPrecio : IBuscador<RegistroOutlierPrecioVM>
    {
        protected readonly LoCoMProContext contexto;

        // Constructor
        public BuscadorDeOutliersPrecio(LoCoMProContext contexto)
        {
            this.contexto = contexto;
        }

        // Buscar favoritos del usuario
        public IQueryable<RegistroOutlierPrecioVM> buscar()
        {
            // Paso 1: se obtienen los registros
            IQueryable<RegistroOutlierPrecioVM> registros = this.contexto.Registros
                .Include(r => r.producto)
                .Where(r => r.visible)
                .Select(r => new RegistroOutlierPrecioVM
                {
                    fecha = r.creacion,
                    usuario = r.usuarioCreador,
                    producto = r.productoAsociado,
                    precio = r.precio,
                    tienda = r.nombreTienda,
                    provincia = r.nombreProvincia,
                    canton = r.nombreCanton
                });

            // Paso 2: se obtienen los productos
            List<string> productos = registros.Select(r => r.producto).Distinct().ToList();

            // Paso 3: para cada producto, realizan los cálculos
            // TODO(Angie): seguir

            // TODO(Angie): cambiar
            return Enumerable.Empty<RegistroOutlierPrecioVM>().AsQueryable();
        }
    }
}