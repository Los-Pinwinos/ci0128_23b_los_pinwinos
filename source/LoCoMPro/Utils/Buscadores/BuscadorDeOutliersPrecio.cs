using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.ViewModels.Moderacion;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
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
            List<RegistroOutlierPrecioVM> registrosOutliers = new List<RegistroOutlierPrecioVM>();

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
            List<string> productos = registros.Select(p => p.producto).Distinct().ToList();

            // Paso 3: para cada producto, realizar los cálculos
            for (int i = 0; i < productos.Count; ++i)
            {
                // Se obtienen los registros de ese producto
                List<RegistroOutlierPrecioVM> registrosProducto = registros
                    .Where(r => r.producto == productos[i])
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

                // Si es menor a 4, no se puede realizar el cálculo para saber si es outlier
                if (registrosProducto.Count > 4)
                {
                    // Paso 4: encontrar si hay registros outliers
                    registrosOutliers = encontrarOutliers(registrosProducto);
                }
            }

            return registrosOutliers.AsQueryable();
        }

        private List<RegistroOutlierPrecioVM> encontrarOutliers(List<RegistroOutlierPrecioVM> registrosProducto)
        {
            // Variables
            List<RegistroOutlierPrecioVM> registrosOutliers = new List<RegistroOutlierPrecioVM>();
            decimal promedio = registrosProducto.Min(r => r.precio);
            decimal minimo = registrosProducto.Max(r => r.precio);
            decimal maximo = registrosProducto.Average(r => r.precio);
            double desviacionEstandar = calcularDesviacionEstandar(registrosProducto, promedio);
            decimal q1 = calcularCuartil(registrosProducto, 1);
            decimal q3 = calcularCuartil(registrosProducto, 3);
            decimal iqr = q3 - q1;

            // Recorrer los registros verificando si son outliers
            for (int j = 0; j < registrosProducto.Count; ++j)
            {
                // Si el precio es menor que el cuatil 1 menos el rango intercuatílico se considera outlier
                // Si el precio es mayor que el cuatil 3 más el rango intercuatílico se considera outlier
                if (registrosProducto[j].precio < q1 - iqr || registrosProducto[j].precio > q3 + iqr)
                {
                    registrosProducto[j].minimo = minimo;
                    registrosProducto[j].maximo = maximo;
                    registrosProducto[j].promedio = promedio;
                    registrosProducto[j].desviacionEstandar = desviacionEstandar;

                    registrosOutliers.Add(registrosProducto[j]);
                }
            }

            return registrosOutliers;
        }

        private double calcularDesviacionEstandar(List<RegistroOutlierPrecioVM> registrosProducto, decimal promedio)
        {
            double desviacionEstandar = -1;
            double sumaDeCuadrados = registrosProducto.Sum(v => Math.Pow((double) v.precio - (double) promedio, 2));
            desviacionEstandar = Math.Sqrt(sumaDeCuadrados / (registrosProducto.Count - 1));

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