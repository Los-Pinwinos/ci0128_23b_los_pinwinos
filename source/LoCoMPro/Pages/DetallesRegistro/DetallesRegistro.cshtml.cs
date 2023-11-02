using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.Utils.SQL;
using LoCoMPro.ViewModels.DetallesRegistro;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace LoCoMPro.Pages.DetallesRegistro
{
    public class DetallesRegistroModel : PageModel
    {
        private readonly LoCoMProContext contexto;

        [BindProperty]
        public DetallesRegistroVM registro { get; set; }

        [BindProperty]
        public decimal ultimaCalificacion { get; set; }

        public DetallesRegistroModel(LoCoMProContext contexto)
        {
            this.contexto = contexto;
            this.registro = new DetallesRegistroVM {
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
                DateTime fecha = DateTime.Parse(fechaHora);
                this.registro = ActualizarRegistro(fecha, usuario);

                AlmacenarTempData(this.registro.usuarioCreador, this.registro.creacion.ToString());

                ActualizarCantidadCalificaciones(fecha, usuario);
                
                ActualizarUltimaCalificacion(fecha, usuario);

                return Page();
            } else
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
            string usuarioCreador = TempData["calificarRegistroUsuario"]?.ToString() ?? "";
            string creacionStr = TempData["calificarRegistroCreacion"]?.ToString() ?? "";

            AlmacenarTempData(usuarioCreador, creacionStr);

            DateTime creacion = DateTime.Parse(creacionStr);

            ActualizarTablaCalificaciones(usuario, usuarioCreador, creacionStr, calificacion);
            ActualizarCalificacionUsuario(usuarioCreador, calificacion);
            ActualizarCalificacionRegistro(creacion, usuarioCreador, calificacion);

            return Page();
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
            var ultimaCalificacion = this.contexto.Calificaciones
                                                .Where(r => r.creacionRegistro == fecha && r.usuarioCreadorRegistro
                                                .Equals(usuario) && r.calificacion != 0).FirstOrDefault();
            this.ultimaCalificacion = 0;
            if (ultimaCalificacion != null)
            {
                this.ultimaCalificacion = ultimaCalificacion.calificacion;
            }
        }

        private void AlmacenarTempData(string usuario, string creacion)
        {
            TempData["calificarRegistroUsuario"] = usuario;
            TempData["calificarRegistroCreacion"] = creacion;
        }

        private static void ActualizarTablaCalificaciones(string usuario, string usuarioCreador
            , string creacion, int calificacion)
        {
            ControladorComandosSql comandoInsertarCalificacion = new ControladorComandosSql();
            comandoInsertarCalificacion.ConfigurarNombreComando("calificarRegistro");
            comandoInsertarCalificacion.ConfigurarParametroComando("usuarioCalificador", usuario);
            comandoInsertarCalificacion.ConfigurarParametroComando("usuarioCreadorRegistro", usuarioCreador);
            comandoInsertarCalificacion.ConfigurarParametroComando("creacionRegistro", creacion);
            comandoInsertarCalificacion.ConfigurarParametroComando("calificacion", calificacion);
            comandoInsertarCalificacion.EjecutarProcedimiento();
        }

        private static void ActualizarCalificacionUsuario(string usuario, int calificacion)
        {
            ControladorComandosSql comandoActualizarUsuario = new ControladorComandosSql();
            comandoActualizarUsuario.ConfigurarNombreComando("actualizarCalificacionDeUsuario");
            comandoActualizarUsuario.ConfigurarParametroComando("nombreDeUsuario", usuario);
            comandoActualizarUsuario.ConfigurarParametroComando("calificacion", calificacion);
            comandoActualizarUsuario.EjecutarProcedimiento();
        }

        private static void ActualizarCalificacionRegistro(DateTime creacion, string usuario, int calificacion)
        {
            ControladorComandosSql comandoActualizarRegistro = new ControladorComandosSql();
            comandoActualizarRegistro.ConfigurarNombreComando("actualizarCalificacionDeRegistro");
            comandoActualizarRegistro.ConfigurarParametroComando("creacionDeRegistro", creacion);
            comandoActualizarRegistro.ConfigurarParametroComando("usuarioCreadorDeRegistro", usuario);
            comandoActualizarRegistro.ConfigurarParametroComando("nuevaCalificacion", calificacion);
            comandoActualizarRegistro.EjecutarProcedimiento();
        }
    }
}
