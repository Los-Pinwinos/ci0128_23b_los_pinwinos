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
        // Controlador de comandos para obtener fechas de corte
        protected ControladorComandosSql calculadorFechasCorte;

        // Constructor
        public BuscadorDeOutliersFecha(LoCoMProContext contexto)
        {
            this.contexto = contexto;
            this.calculadorFechasCorte = new ControladorComandosSql();
            this.calculadorFechasCorte.ConfigurarNombreComando("encontrarFechaCorte");
        }

        // Buscar
        public virtual IQueryable<OutlierFechaVM> buscar()
        {
            IQueryable<OutlierFechaVM> resultados = buscarTodo();

            // Buscar por producto
            resultados = this.agregarFechaCorte(resultados);

            // Buscar por producto
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
            foreach (OutlierFechaVM grupo in resultados)
            {
                this.calculadorFechasCorte.limpiarParametros();
                this.calculadorFechasCorte.ConfigurarParametroComando("producto", grupo.producto);
                this.calculadorFechasCorte.ConfigurarParametroComando("tienda", grupo.tienda);
                this.calculadorFechasCorte.ConfigurarParametroComando("distrito", grupo.distrito);
                this.calculadorFechasCorte.ConfigurarParametroComando("canton", grupo.canton);
                this.calculadorFechasCorte.ConfigurarParametroComando("provincia", grupo.provincia);

                grupo.fechaCorte = (DateTime)(this.calculadorFechasCorte.EjecutarFuncion()[0][0]);

                if (grupo.fechaCorte != null)
                {
                    grupo.cantidadRegistros = this.contexto.Registros
                        .Where(r => r.visible &&
                               r.productoAsociado == grupo.producto &&
                               r.nombreTienda == grupo.tienda &&
                               r.nombreDistrito == grupo.distrito &&
                               r.nombreCanton == grupo.canton &&
                               r.nombreProvincia == grupo.provincia &&
                               r.creacion <= grupo.fechaCorte)
                        .Count();
                }
            }
            return resultados;
        }

        protected IQueryable<OutlierFechaVM> limpiarNoOutliers(IQueryable<OutlierFechaVM> resultados)
        {
            return resultados.Where(r => r.cantidadRegistros != 0);
        }
    }
}