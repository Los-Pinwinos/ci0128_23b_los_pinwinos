using LoCoMPro.Data;
using LoCoMPro.ViewModels.DetallesRegistro;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
            

            // TODO(Angie): cantidad de calificaciones de registros
        }
    }
}
