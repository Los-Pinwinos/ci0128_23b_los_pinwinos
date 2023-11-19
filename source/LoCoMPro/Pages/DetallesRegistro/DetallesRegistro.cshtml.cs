using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.Utils.SQL;
using LoCoMPro.ViewModels.DetallesRegistro;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Web;

namespace LoCoMPro.Pages.DetallesRegistro
{
    public class DetallesRegistroModel : PageModel
    {
        private readonly LoCoMProContext contexto;

        [BindProperty]
        public DetallesRegistroVM registro { get; set; }

        [BindProperty]
        public decimal ultimaCalificacion { get; set; }

        [BindProperty]
        public string reportePopup { get; set; }

        public DetallesRegistroModel(LoCoMProContext contexto)
        {
            this.contexto = contexto;
            this.registro = new DetallesRegistroVM
            {
                creacion = DateTime.Now,
                usuarioCreador = " ",
                precio = 0,
                nombreUnidad = " ",
                productoAsociado = " "
            };
        }

        public IActionResult OnGet(string fechaHora, string usuario)
        {
            if (!string.IsNullOrEmpty(fechaHora) || !string.IsNullOrEmpty(usuario))
            {
                if (!fechaHora.Contains("."))
                {
                    fechaHora += ".0000000";
                }
                else
                {
                    for (int i = fechaHora.IndexOf('.'); i <= fechaHora.IndexOf('.') + 7; i++)
                    {
                        if (i == fechaHora.Length)
                        {
                            fechaHora += "0";
                        }
                    }
                }
                DateTime fecha = DateTime.ParseExact(fechaHora, "yyyy-MM-ddTHH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);

                this.registro = ActualizarRegistro(fecha, usuario);

                AlmacenarTempData(this.registro.usuarioCreador, fecha);

                ActualizarCantidadCalificaciones(fecha, usuario);

                ActualizarUltimaCalificacion(fecha, usuario);

                return Page();
            }
            else
            {
                return RedirectToPage("/Home/Index");
            }
        }

        public string SepararPrecio(string separador)
        {
            char[] numeroTexto = Math.Truncate(this.registro.precio).ToString().ToCharArray();
            Array.Reverse(numeroTexto);
            string numeroAlReves = new string(numeroTexto);
            StringBuilder resultado = new StringBuilder();

            for (int i = 0; i < numeroAlReves.Length; i++)
            {
                if (i > 0 && i % 3 == 0)
                {
                    resultado.Append(separador);
                }
                resultado.Append(numeroAlReves[i]);
            }

            numeroTexto = resultado.ToString().ToCharArray();
            Array.Reverse(numeroTexto);
            return new string(numeroTexto);
        }

        public async Task<IActionResult> OnGetCalificar(int calificacion)
        {
            string usuario = User.Identity?.Name ?? "desconocido";
            string usuarioCreador = TempData["RegistroUsuario"]?.ToString() ?? "";
            int conteo = 0;
            double promedio = 0.0;

            if (TempData.ContainsKey("RegistroCreacion")
                && TempData["RegistroCreacion"] is DateTime creacion)
            {
                AlmacenarTempData(usuarioCreador, creacion);
                ActualizarTablaCalificaciones(usuario, usuarioCreador, creacion, calificacion);
                ActualizarCalificacionUsuario(usuarioCreador);
                ActualizarCalificacionRegistro(creacion, usuarioCreador, calificacion);
                ActualizarModeracionUsuario(usuarioCreador);

                // TODO(Angie): obtener los datos reales
                conteo = 1;
                promedio = 2.3;
            }
            
            return new JsonResult(new { Conteo = conteo, Calificacion = promedio.ToString() });
        }

        private DetallesRegistroVM ActualizarRegistro(DateTime fecha, string usuario)
        {
            var detallesIQ = contexto.Registros
                    .Include(r => r.producto)
                    .Include(r => r.fotografias)
                    .Where(r => r.creacion == fecha && r.usuarioCreador.Equals(usuario))
                    .Select(r => new DetallesRegistroVM
                    {
                        creacion = r.creacion,
                        usuarioCreador = r.usuarioCreador,
                        precio = r.precio,
                        calificacion = r.calificacion,
                        descripcion = r.descripcion,
                        productoAsociado = r.productoAsociado,
                        nombreUnidad = r.producto.nombreUnidad,
                        fotografias = r.fotografias
                    }).ToList();
            return detallesIQ.FirstOrDefault();
        }

        private void ActualizarCantidadCalificaciones(DateTime fecha, string usuario)
        {
            this.registro.cantidadCalificaciones = this.contexto.Calificaciones
                                                .Where(r => r.creacionRegistro == fecha && r.usuarioCreadorRegistro
                                                .Equals(usuario) && r.calificacion != 0).Count();
        }

        private void ActualizarUltimaCalificacion(DateTime fecha, string usuario)
        {
            string usuarioCalificador = User.Identity?.Name ?? "desconocido";
            var ultimaCalificacion = this.contexto.Calificaciones
                                                .Where(r => r.creacionRegistro == fecha
                                                    && r.usuarioCreadorRegistro.Equals(usuario)
                                                    && r.usuarioCalificador.Equals(usuarioCalificador))
                                                .FirstOrDefault();
            this.ultimaCalificacion = 0;
            if (ultimaCalificacion != null)
            {
                this.ultimaCalificacion = ultimaCalificacion.calificacion;
            }
        }

        private void AlmacenarTempData(string usuario, DateTime creacion)
        {
            TempData["RegistroUsuario"] = usuario;
            TempData["RegistroCreacion"] = creacion;
        }

        private static void ActualizarTablaCalificaciones(string usuario, string usuarioCreador
            , DateTime creacion, int calificacion)
        {
            ControladorComandosSql comandoInsertarCalificacion = new ControladorComandosSql();
            comandoInsertarCalificacion.ConfigurarNombreComando("calificarRegistro");
            comandoInsertarCalificacion.ConfigurarParametroComando("usuarioCalificador", usuario);
            comandoInsertarCalificacion.ConfigurarParametroComando("usuarioCreadorRegistro", usuarioCreador);
            comandoInsertarCalificacion.ConfigurarParametroDateTimeComando("creacionRegistro", creacion);
            comandoInsertarCalificacion.ConfigurarParametroComando("calificacion", calificacion);
            comandoInsertarCalificacion.EjecutarProcedimiento();
        }

        private static void ActualizarCalificacionUsuario(string usuario)
        {
            ControladorComandosSql comandoActualizarUsuario = new ControladorComandosSql();
            comandoActualizarUsuario.ConfigurarNombreComando("actualizarCalificacionDeUsuario");
            comandoActualizarUsuario.ConfigurarParametroComando("nombreDeUsuario", usuario);
            comandoActualizarUsuario.EjecutarProcedimiento();
        }

        private static void ActualizarCalificacionRegistro(DateTime creacion, string usuario, int calificacion)
        {
            ControladorComandosSql comandoActualizarRegistro = new ControladorComandosSql();
            comandoActualizarRegistro.ConfigurarNombreComando("actualizarCalificacionDeRegistro");
            comandoActualizarRegistro.ConfigurarParametroDateTimeComando("creacionDeRegistro", creacion);
            comandoActualizarRegistro.ConfigurarParametroComando("usuarioCreadorDeRegistro", usuario);
            comandoActualizarRegistro.ConfigurarParametroComando("nuevaCalificacion", calificacion);
            comandoActualizarRegistro.EjecutarProcedimiento();
        }

        private static void ActualizarModeracionUsuario(string usuario)
        {
            ControladorComandosSql controlador = new ControladorComandosSql();
            controlador.ConfigurarNombreComando("actualizarModeracion");
            controlador.ConfigurarParametroComando("nombreUsuario", usuario);
            controlador.EjecutarProcedimiento();
        }

        public IActionResult OnPostReportar()
        {
            string usuarioCreador = TempData["RegistroUsuario"]?.ToString() ?? "";
            if (TempData.ContainsKey("RegistroCreacion")
                && TempData["RegistroCreacion"] is DateTime creacion)
            {
                AlmacenarTempData(usuarioCreador, creacion);

                if (reportePopup != "" && reportePopup != null)
                {
                    CrearReporte(usuarioCreador, creacion);
                }
                return OnGet(creacion.ToString("yyyy-MM-ddTHH:mm:ss.fffffff"), usuarioCreador);
            }
            return RedirectToPage("/Home/Index");
        }

        private void CrearReporte(string usuarioCreador, DateTime creacion)
        {
            if (User.Identity != null && User.Identity.Name != null)
            {
                string usuarioReportador = User.Identity.Name;
                Reporte reporte = new Reporte
                {
                    usuarioCreadorReporte = usuarioReportador,
                    usuarioCreadorRegistro = usuarioCreador,
                    creacionRegistro = creacion,
                    comentario = reportePopup,
                    creacion = DateTime.Now,
                    verificado = false
                };
                contexto.Reportes.Add(reporte);
                contexto.SaveChanges();
            }
        }
    }
}