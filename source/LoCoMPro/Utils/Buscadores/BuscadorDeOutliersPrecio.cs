using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.ViewModels.Moderacion;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq;
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
            List<ProductoPrecioOutlierVM> productos = registros.Select(p => new ProductoPrecioOutlierVM { nombre = p.producto })
                                                               .Distinct()
                                                               .ToList();

            List<RegistroOutlierPrecioVM> registrosOutliers = new List<RegistroOutlierPrecioVM> ();

            // Paso 3: para cada producto, realizar los cálculos

            // TODO(Angie): verificar si se tienen al menos 4 para que todo esto tenga sentido

            for (int i = 0; i < productos.Count; ++i)
            {
                // Se obtienen los registros de ese producto
                List<RegistroOutlierPrecioVM> registrosProducto = registros
                    .Where(r => r.producto == productos[i].nombre)
                    .Select(r => new RegistroOutlierPrecioVM
                    {
                        fecha = r.fecha,
                        usuario = r.usuario,
                        producto = r.producto,
                        precio = r.precio,
                        tienda = r.tienda,
                        provincia = r.provincia,
                        canton = r.canton
                    })
                    .OrderBy(r => r.precio)
                    .ToList();

                // Guardar los datos
                productos[i].minimo = registrosProducto.Min(r => r.precio);
                productos[i].maximo = registrosProducto.Max(r => r.precio);
                productos[i].promedio = registrosProducto.Average(r => r.precio);
                productos[i].desviacionEstandar = calcularDesviacionEstandar(registrosProducto, productos[i].promedio);
                productos[i].q1 = calcularCuartil(registrosProducto, 1);
                productos[i].q3 = calcularCuartil(registrosProducto, 3);
                productos[i].iqr = productos[i].q3 - productos[i].q1;
            }


            // Paso 4: encontrar los registros outliers
            // TODO(Angie): seguir

            // TODO(Angie): cambiar
            return Enumerable.Empty<RegistroOutlierPrecioVM>().AsQueryable();
        }

        private double calcularDesviacionEstandar(List<RegistroOutlierPrecioVM> registrosProducto, decimal promedio)
        {
            double desviacionEstandar = -1;

            // El cálculo de la desviación estándar requiere al menos 2 registros
            if (registrosProducto.Count >= 2)
            {
                double sumaDeCuadrados = registrosProducto.Sum(v => Math.Pow((double) v.precio - (double) promedio, 2));
                desviacionEstandar = Math.Sqrt(sumaDeCuadrados / (registrosProducto.Count - 1));
            }

            return desviacionEstandar;
        }

        private decimal calcularCuartil(List<RegistroOutlierPrecioVM> registrosProducto, int cuartil)
        {
            // El primer cuartil es tal que el 25% de los datos son menores a él
            // El tercer cuatil es tal qeu el 75% de los datos son menores a él
            double percentil = (cuartil == 1) ? 0.25 : 0.75;

            int termino = (int) (registrosProducto.Count * percentil);
            decimal resultadoCuartil = (registrosProducto[termino].precio + registrosProducto[termino+1].precio) / 2;

            return resultadoCuartil;
        }
    }
}