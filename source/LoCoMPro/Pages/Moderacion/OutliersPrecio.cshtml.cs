using LoCoMPro.Data;
using LoCoMPro.ViewModels.Moderacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoCoMPro.Pages.Moderacion
{
    public class OutliersPrecioModel : PageModel
    {
        protected readonly LoCoMProContext contexto;
        protected readonly IConfiguration configuracion;
        public string? outliers { get; set; }
        public RegistroOutlierPrecioVM registrosVM { get; set; };
        public int paginaDefault { get; set; }
        public int resultadosPorPagina { get; set; }

        public OutliersPrecioModel(LoCoMProContext contexto, IConfiguration configuracion)
        {
            this.contexto = contexto;
            this.configuracion = configuracion;
            this.registrosVM = new RegistroOutlierPrecioVM
            {
                fecha = DateTime.Now,
                usuario = "",
                precio = 0,
                producto = "",
                tienda = "",
                provincia = "",
                canton = ""
            };
            this.paginaDefault = 1;
            this.resultadosPorPagina = this.configuracion.GetValue("TamPaginaCuenta", 10);
        }

        public void OnGet()
        {
        }

        public void OnGetEliminarRegistro(string fechaHora, string usuario)
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

                // TODO(Angie): seguir
            }
        }
    }
}
