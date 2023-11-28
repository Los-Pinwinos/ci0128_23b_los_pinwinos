using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.Utils.SQL;
using LoCoMPro.ViewModels.Moderacion;

namespace LoCoMPro.Utils.Buscadores
{
    // Buscador especializado para la pagina de busqueda
    public class BuscadorDeOutliersFecha : IBuscador<OutlierFechaVM>
    {
        // Contexto
        protected readonly LoCoMProContext contexto;

        // Constructor
        public BuscadorDeOutliersFecha(LoCoMProContext contexto)
        {
            this.contexto = contexto;
        }

        // Buscar
        public virtual IQueryable<OutlierFechaVM> buscar()
        {
            IQueryable<OutlierFechaVM> resultados = buscarTodo();

            // Agregar los datos faltantes
            resultados = this.agregarFechaCorte(resultados);

            // Sacar los resultados que no presentan fecha de corte
            // (No son outliers)
            resultados = this.limpiarNoOutliers(resultados);

            return resultados;
        }

        // Buscar todo
        protected IQueryable<OutlierFechaVM> buscarTodo()
        {
            IQueryable<OutlierFechaVM> resultados = this.contexto.Registros
                .Where(r => r.visible)
                .GroupBy(r => new
                {
                    r.productoAsociado,
                    r.nombreTienda,
                    r.nombreDistrito,
                    r.nombreCanton,
                    r.nombreProvincia
                })
                .Select(group => new OutlierFechaVM
                {
                    producto = group.Key.productoAsociado,
                    tienda = group.Key.nombreTienda,
                    distrito = group.Key.nombreDistrito,
                    canton = group.Key.nombreCanton,
                    provincia = group.Key.nombreProvincia,
                    cantidadRegistros = 0,
                    fechaCorte = null
                });
            return resultados;
        }

        protected IQueryable<OutlierFechaVM> agregarFechaCorte(IQueryable<OutlierFechaVM> resultados)
        {
            var totalRegistros = this.contexto.Registros
                .Where(r => r.visible)
                .ToList();

            List<OutlierFechaVM> resultadosModificados = new List<OutlierFechaVM>();

            foreach (OutlierFechaVM grupo in resultados)
            {
                ControladorComandosSql calculadorFechasCorte = new ControladorComandosSql();
                calculadorFechasCorte.ConfigurarNombreComando("encontrarFechaCorte");
                calculadorFechasCorte.ConfigurarParametroComando("producto", grupo.producto);
                calculadorFechasCorte.ConfigurarParametroComando("tienda", grupo.tienda);
                calculadorFechasCorte.ConfigurarParametroComando("distrito", grupo.distrito);
                calculadorFechasCorte.ConfigurarParametroComando("canton", grupo.canton);
                calculadorFechasCorte.ConfigurarParametroComando("provincia", grupo.provincia);

                grupo.fechaCorte = (DateTime)(calculadorFechasCorte.EjecutarFuncion()[0][0]);
                
                // Cerrar el calculador para liberar la conexión
                calculadorFechasCorte.cerrar();

                if (grupo.fechaCorte != null)
                {
                    grupo.cantidadRegistros = totalRegistros
                    .Count(r => r.productoAsociado == grupo.producto &&
                           r.nombreTienda == grupo.tienda &&
                           r.nombreDistrito == grupo.distrito &&
                           r.nombreCanton == grupo.canton &&
                           r.nombreProvincia == grupo.provincia &&
                           r.creacion <= grupo.fechaCorte);
                }

                resultadosModificados.Add(grupo);
            }

            return resultadosModificados.AsQueryable();
        }

        protected IQueryable<OutlierFechaVM> limpiarNoOutliers(IQueryable<OutlierFechaVM> resultados)
        {
            return resultados.Where(r => r.cantidadRegistros > 0);
        }
    }
}