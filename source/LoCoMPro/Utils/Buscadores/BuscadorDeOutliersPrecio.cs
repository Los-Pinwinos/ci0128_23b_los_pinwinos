using LoCoMPro.Data;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.ViewModels.Moderacion;
using Microsoft.EntityFrameworkCore;

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
            IQueryable<RegistroOutlierPrecioVM> registros = obtenerRegistros();

            // Paso 2: se encuentran los productos
            List<string> productos = registros.Select(p => p.producto).Distinct().ToList();

            // Paso 3: analizar productos
            List<RegistroOutlierPrecioVM> registrosOutliers = analizarProductos(productos, registros);

            return registrosOutliers.AsQueryable();
        }

        private IQueryable<RegistroOutlierPrecioVM> obtenerRegistros()
        {
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
                    canton = r.nombreCanton,
                    distrito = r.nombreDistrito
                });
            return registros;
        }

        private List<RegistroOutlierPrecioVM> analizarProductos(List<string> productos, 
            IQueryable<RegistroOutlierPrecioVM> registros)
        {
            List<RegistroOutlierPrecioVM> registrosOutliers = new();

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
                        canton = r.canton,
                        distrito = r.distrito
                    })
                    .OrderBy(r => r.tienda)
                    .ThenBy(r => r.provincia)
                    .ThenBy(r => r.canton)
                    .ThenBy(r => r.distrito)
                    .ThenBy(r => r.precio)
                    .ToList();

                // Se tiene la lista completa de los registros que se deben analizar
                if (registrosProducto.Count > 4)
                {  // Se obtienen las tiendas de esos registros y se analiza si hay outliers
                    analizarTiendas(registrosOutliers, registrosProducto);
                }
            }

            return registrosOutliers;
        }

        private void analizarTiendas(List<RegistroOutlierPrecioVM> registrosOutliers,
            List<RegistroOutlierPrecioVM> registrosProducto)
        {
            List<RegistroOutlierPrecioVM> registrosProdTienda = new()
            {
                registrosProducto[0]
            };
            int posUltimo = registrosProdTienda.Count - 1;

            // Se comienza en 1 porque ya se agregó el primero
            for (int i = 1; i < registrosProducto.Count; ++i)
            {
                // La lista de registros productos está ordenada
                if (registrosProducto[i].tienda != registrosProdTienda[posUltimo].tienda
                    || registrosProducto[i].provincia != registrosProdTienda[posUltimo].provincia
                    || registrosProducto[i].canton != registrosProdTienda[posUltimo].canton
                    || registrosProducto[i].distrito != registrosProdTienda[posUltimo].distrito
                    || i == (registrosProducto.Count - 1))
                { // Se tiene la lista completa de los registros que se deben analizar
                    registrosProdTienda.Add(registrosProducto[i]);
                    if (registrosProdTienda.Count > 4)
                    {  // Si es menor a 4, no se puede realizar el cálculo para saber si es outlier
                        encontrarOutliers(registrosProdTienda, registrosOutliers);
                    }

                    // Se limpia la lista para analizar el siguiente bloque
                    registrosProdTienda.Clear();
                    posUltimo = -1;
                }

                // Se agrega a la lista para analizar
                registrosProdTienda.Add(registrosProducto[i]);
                ++posUltimo;
            }
        }

        private void encontrarOutliers(List<RegistroOutlierPrecioVM> registrosProducto, List<RegistroOutlierPrecioVM> registrosOutliers)
        {
            // Variables
            decimal minimo = registrosProducto.Min(r => r.precio);
            decimal maximo = registrosProducto.Max(r => r.precio);
            decimal promedio = registrosProducto.Average(r => r.precio);
            double desviacionEstandar = calcularDesviacionEstandar(registrosProducto, promedio);
            decimal q1 = calcularCuartil(registrosProducto, 1);
            decimal q3 = calcularCuartil(registrosProducto, 3);
            decimal iqr = q3 - q1;  // Rango intercuartílico
            decimal distanciaOutlier = 1.5m * iqr;  // Distancia necesaria para ser considerado un outlier

            // Recorrer los registros verificando si son outliers
            for (int j = 0; j < registrosProducto.Count; ++j)
            {
                // Si el precio es menor que el cuatil 1 menos el rango intercuatílico se considera outlier
                // Si el precio es mayor que el cuatil 3 más el rango intercuatílico se considera outlier
                if (registrosProducto[j].precio < q1 - distanciaOutlier || registrosProducto[j].precio > q3 + distanciaOutlier)
                {
                    registrosProducto[j].minimo = minimo;
                    registrosProducto[j].maximo = maximo;
                    registrosProducto[j].promedio = promedio;
                    registrosProducto[j].desviacionEstandar = desviacionEstandar;

                    registrosOutliers.Add(registrosProducto[j]);
                }
            }
        }

        private static double calcularDesviacionEstandar(List<RegistroOutlierPrecioVM> registrosProducto, decimal promedio)
        {
            double desviacionEstandar = -1;
            double sumaDeCuadrados = registrosProducto.Sum(v => Math.Pow((double) v.precio - (double) promedio, 2));
            desviacionEstandar = Math.Sqrt(sumaDeCuadrados / (registrosProducto.Count - 1));

            return desviacionEstandar;
        }

        private static decimal calcularCuartil(List<RegistroOutlierPrecioVM> registrosProducto, int cuartil)
        {
            // El primer cuartil es tal que el 25% de los datos son menores a él
            // El segundo cuartil es la mediana
            // El tercer cuatil es tal que el 75% de los datos son menores a él

            int posCorte = registrosProducto.Count / 2;
            decimal resultado = 0;

            switch(cuartil)
            {
                case 1:  // Se debe tomar la primera mitad de la lista
                    resultado = resultadoCuartil(registrosProducto.Take(posCorte).ToList());
                    break;
                case 2:  // Se debe tomar la lista completa
                    resultado = resultadoCuartil(registrosProducto);
                    break;
                case 3:  // Se debe tomar la segunda mitad de la lista
                    resultado = resultadoCuartil(registrosProducto.Skip(posCorte).ToList());
                    break;
            }

            return resultado;
        }

        private static decimal resultadoCuartil(List<RegistroOutlierPrecioVM> registros)
        {
            int posMitad = registros.Count / 2;
            decimal resultado = registros[posMitad].precio;
            if (registros.Count % 2 == 0)
            {
                resultado = (resultado + registros[posMitad - 1].precio) / 2;
            }

            return resultado;
        }
    }
}