using LoCoMPro.Data;
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
                if (!fechaHora.Contains("."))
                {
                    fechaHora += ".0000000";
                } else
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
            if (TempData.ContainsKey("calificarRegistroCreacion") 
                && TempData["calificarRegistroCreacion"] is DateTime creacion)
            {
                AlmacenarTempData(usuarioCreador, creacion);
                ActualizarTablaCalificaciones(usuario, usuarioCreador, creacion, calificacion);
                ActualizarCalificacionUsuario(usuarioCreador, calificacion);

                Console.WriteLine("calif: " + calificacion);

                ActualizarCalificacionRegistro(creacion, usuarioCreador, calificacion);

                Console.WriteLine("di todo listo");
            }
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


            Console.WriteLine("ult: " + this.ultimaCalificacion);

        }

        private void AlmacenarTempData(string usuario, DateTime creacion)
        {
            TempData["calificarRegistroUsuario"] = usuario;
            TempData["calificarRegistroCreacion"] = creacion;
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
            comandoActualizarRegistro.ConfigurarParametroDateTimeComando("creacionDeRegistro", creacion);
            comandoActualizarRegistro.ConfigurarParametroComando("usuarioCreadorDeRegistro", usuario);
            comandoActualizarRegistro.ConfigurarParametroComando("nuevaCalificacion", calificacion);
            comandoActualizarRegistro.EjecutarProcedimiento();
        }
    }
}
