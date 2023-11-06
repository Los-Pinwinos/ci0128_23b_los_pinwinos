using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Moderacion;
using LoCoMPro.Models;
using LoCoMPro.ViewModels.Cuenta;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Web;

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
            this.indiceReporteActual = 0;
            this.reporte = null;
            this.registro = null;
            this.fotografias = null;
        }

        // Reporte actual
        private Reporte? reporteActual;

        // Indice del reporte revisado actualmente
        public int indiceReporteActual = 0;

        // Bind properties para mostrar en la página
        [BindProperty]
        public ReporteVM? reporte { get; set; } = null;
        [BindProperty]
        public RegistroVM? registro { get; set; } = null;
        // Fotografías
        public ICollection<Fotografia>? fotografias { get; set; } = null;

        // Cultura francesa para usar comas para separa
        public CultureInfo culturaComas { get; set; } = new System.Globalization.CultureInfo("fr-FR");

        // On GET
        public IActionResult OnGet(int indiceActual = -1)
        {
            // Si se indicó un indice
            if (indiceActual != -1)
            {
                // Se actualiza el indice al indicado del
                // llamado anterior
                this.indiceReporteActual = indiceActual;
            }

            // Obtiene el reporte en el indice actual
            this.obtenerReporte();

            // Obtener la información de los modelos vista
            // a partir del reporte
            this.obtenerModelosVista();
            
            // Retorna la página
            return Page();
        }

        private void obtenerReporte()
        {
            // Crea una lista ordenada de más antiguo a más
            // nuevo con los reportes no verificados
            List<Reporte> reportesDisponiblesOrdenados = this.contexto.Reportes
                .Include(r => r.creadorReporte)
                .Include(r => r.registro)
                .Where(r => !r.verificado)
                .OrderBy(r => r.creacion)
                .ToList();

            // Si el indice actual es válido
            if (this.indiceReporteActual >= 0 &&
                this.indiceReporteActual < reportesDisponiblesOrdenados.Count)
            {
                // Obtener el reporte en ese indice
                this.reporteActual = reportesDisponiblesOrdenados[this.indiceReporteActual];
            }
            else
            {
                // Reiniciar el índice
                this.indiceReporteActual = 0;
            }
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

                // Redondear la calificación del usuario creador del reporte
                this.reporte.calificacionCreador = (Math.Floor(this.reporte.calificacionCreador * 10) / 10);

                // Obtener la cantidad de registros calificados del creador del reporte
                this.reporte.cantidadCalificacionesCreador = this.contexto.Registros
                    .Where(r =>
                    r.usuarioCreador == this.reporteActual.usuarioCreadorReporte &&
                    r.calificacion != 0 &&
                    r.visible).Count();

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

                // Redondear la calificación del usuario creador del registro
                this.registro.calificacionCreador = (Math.Floor(this.registro.calificacionCreador * 10) / 10);

                // Obtener la cantidad de registros calificados del creador del registro
                this.registro.cantidadCalificacionesCreador = this.contexto.Registros
                    .Where(r =>
                    r.usuarioCreador == this.reporteActual.usuarioCreadorRegistro &&
                    r.calificacion != 0 &&
                    r.visible).Count();

                // Redondear la calificación del registro
                this.registro.calificacionRegistro = (Math.Floor(this.registro.calificacionRegistro * 10) / 10);

                // Obtener la cantidad de calificaciones del registro
                this.registro.cantidadCalificacionesRegistro = this.contexto.Calificaciones
                    .Where(c =>
                    c.usuarioCreadorRegistro == this.reporteActual.usuarioCreadorRegistro &&
                    c.creacionRegistro == this.reporteActual.creacionRegistro).Count();

                // Buscar las fotografías del registro
                this.fotografias = this.contexto.Fotografias
                .Where(r =>
                    r.creacion == this.registro.fecha &&
                    r.usuarioCreador == this.registro.usuario)
                .ToList();
            }
        }

        public IActionResult OnPostRechazar(int indiceReporteActual)
        {
            // Obtener la instancia del reporte actual
            this.reporteActual = this.contexto.Reportes
                .Include(r => r.creadorReporte)
                .Include(r => r.registro)
                .Where(r =>
                      !r.verificado &&
                       r.usuarioCreadorReporte == this.reporte.creador &&
                       r.creacionRegistro == this.registro.fecha &&
                       r.usuarioCreadorRegistro == this.registro.usuario)
                .FirstOrDefault();

            // Si el reporte todavía existe
            if (this.reporteActual != null)
            {
                // Cambia el reporte indicando que ha sido verificado
                this.reporteActual.verificado = true;
                // Guarda los cambios en la base de datos
                this.contexto.SaveChanges();
            }

            // Propagar el mismo índice
            // (No se incrementa dado que se "eliminó" el reporte
            // en este índice)
            this.indiceReporteActual = indiceReporteActual;
            // Llamar al OnGet con el indice actual
            return RedirectToPage("/Moderacion/VerReportes", new
            {
                indiceActual = this.indiceReporteActual
            });
        }


        public IActionResult OnPostAceptar(int indiceReporteActual)
        {
            // Obtener la instancia del reporte actual
            this.reporteActual = this.contexto.Reportes
                .Include(r => r.creadorReporte)
                .Include(r => r.registro)
                .Where(r =>
                      !r.verificado &&
                       r.usuarioCreadorReporte == this.reporte.creador &&
                       r.creacionRegistro == this.registro.fecha &&
                       r.usuarioCreadorRegistro == this.registro.usuario)
                .FirstOrDefault();

            // Si el reporte todavía existe
            if (this.reporteActual != null)
            {
                // Obtener el registro reportado
                Registro? registroReportado = this.contexto.Registros
                .Where(r =>
                       r.usuarioCreador == this.registro.usuario &&
                       r.creacion == this.registro.fecha)
                .FirstOrDefault();

                // Si el registro reportado existe
                if (registroReportado != null)
                {
                    // Oculta el registro
                    registroReportado.visible = false;
                }

                // Cambia el reporte indicando que ha sido verificado
                this.reporteActual.verificado = true;
                // Guarda los cambios en la base de datos
                this.contexto.SaveChanges();
            }

            // Propagar el mismo índice
            // (No se incrementa dado que se "eliminó" el reporte
            // en este índice)
            this.indiceReporteActual = indiceReporteActual;
            // Llamar al OnGet con el indice actual
            return RedirectToPage("/Moderacion/VerReportes", new
            {
                indiceActual = this.indiceReporteActual
            });
        }

        public IActionResult OnPostPasar(int indiceReporteActual)
        {
            // Propagar el índice siguiente
            this.indiceReporteActual = indiceReporteActual + 1;

            // Llamar al OnGet con el indice actual
            return RedirectToPage("/Moderacion/VerReportes", new
            {
                indiceActual = this.indiceReporteActual
            });
        }
    }
}