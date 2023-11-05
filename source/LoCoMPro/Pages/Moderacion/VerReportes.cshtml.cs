using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Moderacion;
using LoCoMPro.Models;
using LoCoMPro.ViewModels.Cuenta;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Pages.Moderacion
{
    public class ModelovVerReportes : PageModel
    {
        protected readonly LoCoMProContext contexto;

        // Constructor
        public ModelovVerReportes(LoCoMProContext contexto)
        {
            // Inicializar
            this.contexto = contexto;
            this.reporteActual = null;
            this.reporte = null;
            this.registro = null;
            this.fotografias = null;
        }

        //  Reporte actual
        private Reporte? reporteActual;

        // Bind properties para mostrar en la página
        [BindProperty]
        public ReporteVM? reporte { get; set; } = null;
        [BindProperty]
        public RegistroVM? registro { get; set; } = null;
        // Fotografías
        public ICollection<Fotografia> fotografias { get; set; } = null;

        // On GET
        public IActionResult OnGet(string? usuarioReportador = null,
            string? fechaRegistro = null, string? usuarioReportado = null)
        {
            // Si se proveyeron para saltar un reporte
            if (usuarioReportador != null &&
                fechaRegistro != null &&
                usuarioReportado != null)
            {
                // Obtener el reporte siguiente al proveído
                this.buscarReporteSiguiente(usuarioReportador,
                    fechaRegistro,
                    usuarioReportado);
            }
            else
            {
                // Obtener el primer reporte visible
                this.buscarPrimerReporte();
            }

            // Obtener la información de los modelos vista
            // a partir del reporte
            this.obtenerModelosVista();
            
            // Retorna la página
            return Page();
        }

        private void buscarReporteSiguiente(string usuarioReportador,
            string fechaRegistro, string usuarioReportado)
        {
            // Obtener un datetime de un string
            DateTime fecha = this.traducirFecha(fechaRegistro);

            // Obtener el reporte actual como el siguiente
            // no verificado al indicado
            this.reporteActual = this.contexto.Reportes
                .Include(r => r.creadorReporte)
                .Include(r => r.registro)
                .Where(r => !r.verificado)
                .OrderBy(r => r.creacion)
                .SkipWhile(r =>
                    r.usuarioCreadorReporte != usuarioReportador &&
                    r.creacionRegistro != fecha &&
                    r.usuarioCreadorRegistro != usuarioReportado)
                .Skip(1)
                .FirstOrDefault();
        }

        private void buscarPrimerReporte()
        {
            // Obtener el primer reporte no verificado
            this.reporteActual = this.contexto.Reportes
                .Include(r => r.creadorReporte)
                .Include(r => r.registro)
                .Where(r => !r.verificado)
                .FirstOrDefault();
        }

        private DateTime traducirFecha(string hileraFecha)
        {
            // Si la hilera termina en punto
            if (!hileraFecha.Contains("."))
            {
                // Agregar 0´s faltantes
                hileraFecha += ".0000000";
            }
            else
            {
                for (int i = hileraFecha.IndexOf('.'); i <= hileraFecha.IndexOf('.') + 7; i++)
                {
                    if (i == hileraFecha.Length)
                    {
                        hileraFecha += "0";
                    }
                }
            }

           // Convertir a un datetime exacto
           return DateTime.ParseExact(hileraFecha,
                "yyyy-MM-ddTHH:mm:ss.fffffff",
                System.Globalization.CultureInfo.InvariantCulture);
        }

        private void obtenerModelosVista()
        {
            // Si existe un reporte actual
            if (this.reporteActual != null)
            {
                // Crear un reporte vista a partir del reporte actual
                this.reporte = new ReporteVM
                {
                    comentario = this.reporteActual.comentario,
                    fecha = this.reporteActual.creacion,
                    creador = this.reporteActual.usuarioCreadorReporte,
                    calificacionCreador = this.reporteActual.creadorReporte.calificacion
                };
                
                // Buscar el registro que corresponde con el reporte
                // y crear un registro vista a partir del mismo
                this.registro = this.contexto.Registros
                .Include(r => r.producto)
                .Include(r => r.creador)
                .Where(r =>
                    r.creacion == this.reporteActual.creacionRegistro &&
                    r.usuarioCreador == this.reporteActual.usuarioCreadorRegistro)
                .Select(r => new RegistroVM
                {
                    producto = r.productoAsociado,
                    precio = r.precio,
                    tienda = r.nombreTienda,
                    fecha = r.creacion,
                    unidad = r.producto.nombreUnidad,
                    provincia = r.nombreProvincia,
                    usuario = r.usuarioCreador,
                    marca = r.producto.marca,
                    canton = r.nombreCanton,
                    calificacionCreador = r.creador.calificacion,
                    categoria = r.producto.nombreCategoria,
                    descripcion = r.descripcion,
                    calificacionRegistro = r.calificacion
                })
                .FirstOrDefault();

                // Buscar las fotografías del registro
                this.fotografias = this.contexto.Fotografias
                .Where(r =>
                    r.creacion == this.registro.fecha &&
                    r.usuarioCreador == this.registro.usuario)
                .ToList();
            }
        }
    }
}