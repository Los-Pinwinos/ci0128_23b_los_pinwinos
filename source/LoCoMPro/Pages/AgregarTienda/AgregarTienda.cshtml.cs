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
            // TODO(Angie): hacer acci�n de aceptar
            Console.WriteLine("Aceptar");

        }
        public void OnPostCancelar()
        {
            // TODO(Angie): hacer acci�n de aceptar
            Console.WriteLine("Cancelar");
        }

        // TODO(Angie): ubicaci�n
        // TODO(Angie): fecha actual
    }
}
