using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoCoMPro.Pages.AgregarTienda
{
    public class AgregarTiendaModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPostSiguiente()
        {
            // TODO(Angie): hacer acci�n de seguir a agregar producto

        }

        // Acci�n al presionar cancelar
        public IActionResult OnPostCancelar()
        {
            // Cuando se cancela agregar un producto, se devuelve a la pantalla principal
            return RedirectToPage("/Home/Index");
        }
    }
}
