using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoCoMPro.Pages.AgregarTienda
{
    public class AgregarTiendaModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPostAceptar()
        {
            // TODO(Angie): hacer acción de aceptar
            Console.WriteLine("Aceptar");

        }
        public void OnPostCancelar()
        {
            // TODO(Angie): hacer acción de aceptar
            Console.WriteLine("Cancelar");
        }

        // TODO(Angie): ubicación
        // TODO(Angie): fecha actual
    }
}
