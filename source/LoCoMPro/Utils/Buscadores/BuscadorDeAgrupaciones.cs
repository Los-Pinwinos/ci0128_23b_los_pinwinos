using LoCoMPro.Data;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.ViewModels.Busqueda;
using LoCoMPro.ViewModels.Cuenta;
using LoCoMPro.ViewModels.Moderacion;
using LoCoMProTests.Utils.Clustering;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Utils.Buscadores
{
    public class BuscadorDeAgrupaciones : IBuscador<ProductosSimilaresVM>
    {
        protected readonly LoCoMProContext contexto;
        private Agrupador agrupador;

        public BuscadorDeAgrupaciones(LoCoMProContext contexto)
        {
            this.agrupador = new Agrupador();
            this.contexto = contexto;
        }

        public IQueryable<ProductosSimilaresVM> buscar()
        {
            var nombreProductos = this.buscarTodos();
            var resultadosCluster = agrupador.agrupar(nombreProductos);
            if (resultadosCluster == null || resultadosCluster.Count == 0)
            {
                return Enumerable.Empty<ProductosSimilaresVM>().AsQueryable(); ;
                
            }
            return this.obtenerPrimeros(resultadosCluster);
        }

        private List<string> buscarTodos()
        {
            List<string> nombresProductos = this.contexto.Productos
                                .Select(producto => producto.nombre)
                                .ToList();
            return nombresProductos;
        }

        private IQueryable<ProductosSimilaresVM> obtenerPrimeros(Dictionary<string, List<string>> resultadosCluster)
        {
            List<string> listaLlaves = resultadosCluster.Keys.ToList();

            // Obtener la información de todas las llaves
            IQueryable<ProductosSimilaresVM> resultadosIQ = this.contexto.Productos
                .Where(producto => listaLlaves.Contains(producto.nombre))
                .Select(producto => new ProductosSimilaresVM
                {
                    nombreProducto = producto.nombre,
                    nombreCategoria = producto.categoria.nombre,
                    nombreMarca = producto.marca,
                    unidad = producto.unidad.nombre
                }).AsQueryable();

            return resultadosIQ;
        }
    }
}
