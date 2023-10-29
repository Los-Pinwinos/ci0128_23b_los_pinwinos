using LoCoMPro.Data;
using LoCoMPro.ViewModels.DetallesRegistro;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LoCoMPro.Pages.DetallesRegistro
{
    public class DetallesRegistroModel : PageModel
    {
        private readonly LoCoMProContext contexto;

        [BindProperty]
        public DetallesRegistroVM registro { get; set; }
        [BindProperty]
        public int cantidadCalificaciones { get; set; }

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

        public void OnGet(string fechaHora, string usuario, string producto)
        {
            DateTime fecha = DateTime.Parse(fechaHora);

            var detallesIQ = contexto.Registros
                .Include(r => r.producto)
                .Include(r => r.fotografias)
                .Where(r => r.creacion==fecha && r.usuarioCreador.Equals(usuario))
                .Select(r => new DetallesRegistroVM
                {
                    creacion = r.creacion,
                    usuarioCreador = r.usuarioCreador,
                    precio = r.precio,
                    calificacion = r.calificacion,
                    descripcion = r.descripcion,
                    productoAsociado = producto,
                    nombreUnidad = r.producto.nombreUnidad,
                    fotografias = r.fotografias
                }).ToList();

            this.registro = detallesIQ.FirstOrDefault();

            this.cantidadCalificaciones = this.contexto.Calificaciones
                                            .Where(r => r.creacionRegistro == fecha && r.usuarioCreadorRegistro.Equals(usuario) && r.calificacion != 0).Count();
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
    }
}
