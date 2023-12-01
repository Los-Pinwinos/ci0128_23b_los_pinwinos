using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.ViewModels.Busqueda;
using LoCoMPro.ViewModels.Cuenta;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LoCoMPro.Utils.Buscadores
{
    // Buscador especializado para la pagina de busqueda de favoritos
    public class BuscadorDeProductosFavoritos : IBuscador<BusquedaFavoritosVM>
    {
        // Contexto
        protected readonly LoCoMProContext contexto;

        // Usuario con los favoritos
        protected string? usuario { get; set; }
        // Constructor
        public BuscadorDeProductosFavoritos(LoCoMProContext contexto, string? usuario = null)
        {
            this.contexto = contexto;
            this.usuario = usuario;
        }

        // Setters
        public void setUsuario(string? usuario)
        {
            this.usuario = usuario;
        }

        // Buscar favoritos del usuario
        public IQueryable<BusquedaFavoritosVM> buscar()
        {
            if (this.usuario != null)
            {
                Usuario? usuario = this.contexto.Usuarios
                    .Include(u => u.favoritos)
                    .FirstOrDefault(u => u.nombreDeUsuario == this.usuario);

                // Obtener el nombre de los favoritos
                IList<string> favoritos = usuario?.favoritos.Select(f => f.nombre).ToList() ?? new List<string>();

                if (usuario != null)
                {
                    IQueryable<BusquedaFavoritosVM> resultadosIQ = this.contexto.Registros
                        .Include(r => r.tienda)
                        .Where(r => r.visible && favoritos.Contains(r.productoAsociado))
                        .GroupBy(r => new
                        {
                            r.nombreTienda,
                            r.nombreProvincia,
                            r.nombreCanton,
                            r.nombreDistrito
                        })
                        .Select(grupo => new BusquedaFavoritosVM
                        {
                            nombreTienda = grupo.Key.nombreTienda,
                            nombreProvincia = grupo.Key.nombreProvincia,
                            nombreCanton = grupo.Key.nombreCanton,
                            nombreDistrito = grupo.Key.nombreDistrito,
                            // Contar la cantidad de productos distintos que estén en la lista de favoritos
                            cantidadEncontrada = grupo.Select(r => r.productoAsociado).Distinct().Count(),
                            porcentajeEncontrado = ((float)grupo.Select(r => r.productoAsociado).Distinct().Count() / favoritos.Count) * 100,
                            // Obtener la suma agregada de los precios de los registros cuyo producto es distinto
                            precioTotal = grupo
                                            .GroupBy(r => r.productoAsociado)
                                            .Select(grupo2 => grupo2.OrderByDescending(r => r.creacion).First().precio)
                                            .Sum(),
                            // Obtener la distancia entre puntos de la vivienda del usuario y la tienda. Se utiliza una fórmula que considera la curvatura de la tierra.
                            distanciaTotal = Localizador.DistanciaKm(grupo.First().tienda!.latitud, grupo.First().tienda!.longitud
                                                        , usuario.latitudVivienda, usuario.longitudVivienda)
                        })
                        .Where(f => f.cantidadEncontrada > 0)
                        .OrderByDescending(f => f.cantidadEncontrada);
                    return resultadosIQ;
                }
                else
                {
                    return Enumerable.Empty<BusquedaFavoritosVM>().AsQueryable();
                }
            }
            else
            {
                return Enumerable.Empty<BusquedaFavoritosVM>().AsQueryable();
            }
        }
    }
}