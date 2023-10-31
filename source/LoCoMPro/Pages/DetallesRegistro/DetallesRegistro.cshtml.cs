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

                this.registro = detallesIQ.FirstOrDefault();

                TempData["calificarRegistroCreacion"] = this.registro.creacion.ToString();
                TempData["calificarRegistroUsuario"] = this.registro.usuarioCreador;

                this.registro.cantidadCalificaciones = this.contexto.Calificaciones
                                                .Where(r => r.creacionRegistro == fecha && r.usuarioCreadorRegistro
                                                .Equals(usuario) && r.calificacion != 0).Count();

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

        public IActionResult OnGetCalificar(int calificacion)
        {

            // TODO(Angie): borrar
            Console.WriteLine("estoy en el metodo");
                
            string usuario = User.Identity?.Name ?? "desconocido";
            string usuarioCreador = TempData["calificarRegistroUsuario"]?.ToString() ?? "";
            string creacionStr = TempData["calificarRegistroCreacion"]?.ToString() ?? "";
            DateTime creacion = DateTime.Parse(creacionStr);

            Console.WriteLine("usuario: " + usuario + " usuarioCreador: " + usuarioCreador + " creacion: " + creacion + " calificacion " + calificacion);
                

            // Actualizar la tabla de calificaciones
            ControladorComandosSql comandoInsertarCalificacion = new ControladorComandosSql();
            comandoInsertarCalificacion.ConfigurarNombreComando("calificarRegistro");
            comandoInsertarCalificacion.ConfigurarParametroComando("usuarioCalificador", usuario);
            comandoInsertarCalificacion.ConfigurarParametroComando("usuarioCreadorRegistro", usuarioCreador);
            comandoInsertarCalificacion.ConfigurarParametroComando("creacionRegistro", creacion);
            comandoInsertarCalificacion.ConfigurarParametroComando("calificacion", calificacion);
            // comandoInsertarCalificacion.EjecutarProcedimiento();

            // TODO(Angie):
            Console.WriteLine("segundo procedimiento");

            // Actualizar la calificación del usuario
            ControladorComandosSql comandoActualizarUsuario = new ControladorComandosSql();
            comandoActualizarUsuario.ConfigurarNombreComando("actualizarCalificacionDeUsuario");
            comandoActualizarUsuario.ConfigurarParametroComando("nombreDeUsuario", usuarioCreador);
            comandoActualizarUsuario.ConfigurarParametroComando("calificacion", calificacion);
            comandoActualizarUsuario.EjecutarProcedimiento();

            // TODO(Angie):
            Console.WriteLine("tercer procedimiento");

            // Actualizar la calificación del registro
            ControladorComandosSql comandoActualizarRegistro = new ControladorComandosSql();
            comandoActualizarRegistro.ConfigurarNombreComando("actualizarCalificacionDeRegistro");
            comandoActualizarRegistro.ConfigurarParametroComando("creacionDeRegistro", creacion);
            comandoActualizarRegistro.ConfigurarParametroComando("usuarioCreadorDeRegistro", usuarioCreador);
            comandoActualizarRegistro.ConfigurarParametroComando("nuevaCalificacion", calificacion);
            comandoActualizarRegistro.EjecutarProcedimiento();

            return Page();
        }
    }
}
