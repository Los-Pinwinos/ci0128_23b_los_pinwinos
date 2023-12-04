using LoCoMPro.Data;
using LoCoMPro.Utils.Buscadores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using LoCoMPro.ViewModels.Moderacion;
using LoCoMPro.Utils.SQL;
using System.Text.RegularExpressions;

namespace LoCoMPro.Pages.Moderacion
{
    public class ModeloFechasObsoletas : PageModel
    {
        protected readonly LoCoMProContext contexto;
        protected readonly IConfiguration configuracion;

        // Constructor
        public ModeloFechasObsoletas(LoCoMProContext contexto, IConfiguration configuracion)
        {
            this.contexto = contexto;
            this.configuracion = configuracion;
            // Inicializar
            this.Inicializar();
        }

        public string? outliers { get; set; }
        public OutlierFechaVM outlierVM { get; set; } = default!;

        // Paginación
        public int paginaDefault { get; set; }
        public int resultadosPorPagina { get; set; }

        // Inicializar atributos
        public void Inicializar()
        {
            this.outlierVM = new OutlierFechaVM
            {
                producto = "",
                tienda = "",
                provincia = "",
                canton = "",
                distrito = "",
                cantidadRegistros = 0,
                fechaCorte = null
            };
            this.paginaDefault = 1;
            this.resultadosPorPagina = this.configuracion.GetValue("TamPaginaCuenta", 10);
        }

        // Método On get para cargar los resultados o redireccionar
        public IActionResult OnGet()
        {
            // Si el usuario no está loggeado
            if (User.Identity == null || !User.Identity.IsAuthenticated ||
                !User.IsInRole("moderador"))
            {
                // Establece mensaje para redireccionar
                ViewData["redireccion"] = "redireccionar";
            }
            else
            {
                // Configurar buscador
                BuscadorDeOutliersFecha buscador = new BuscadorDeOutliersFecha(this.contexto);
                // Consultar la base de datos
                IQueryable<OutlierFechaVM> busqueda = buscador.buscar();

                // Si la busqueda tuvo resultados
                List<OutlierFechaVM> resultados = busqueda.ToList();
                if (resultados.Count != 0)
                {
                    // Asignar data de JSON
                    this.outliers = JsonConvert.SerializeObject(resultados);
                }
                else
                {
                    this.outliers = "Sin resultados";
                }
            }
            return Page();
        }

        public void OnGetEliminarRegistros(string registrosStr)
        {
            // Si se recibieron registros por eliminar
            if (registrosStr != null)
            {
                List<OutlierFechaVM> registros = JsonConvert.DeserializeObject<List<OutlierFechaVM>>(registrosStr)!;
                if (registros != null && registros.Count > 0)
                {
                    // Por cada registro obsoleto
                    foreach (OutlierFechaVM outlier in registros)
                    {
                        // Crea un controlador de comandos para llamar al procedimiento de ocultar registros obsoletos
                        ControladorComandosSql ocultador = new ControladorComandosSql();
                        ocultador.ConfigurarNombreComando("ocultarRegistrosObsoletos");
                        // Establece sus parametros
                        ocultador.ConfigurarParametroComando("producto", outlier.producto);
                        ocultador.ConfigurarParametroComando("tienda", outlier.tienda);
                        ocultador.ConfigurarParametroComando("distrito", outlier.distrito);
                        ocultador.ConfigurarParametroComando("canton", outlier.canton);
                        ocultador.ConfigurarParametroComando("provincia", outlier.provincia);
                        // Se le agrega un segundo para no tomar en cuenta los microsegundos perdidos
                        ocultador.ConfigurarParametroComando("fechaCorte", outlier.fechaCorte!.Value.AddSeconds(1));

                        // Ejecuta el procedimiento
                        ocultador.EjecutarProcedimiento();
                    }
                }
            }
        }
    }
}
