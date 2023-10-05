using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoCoMPro.Pages.Cuenta
{
    // Clase modelo para manejar la p�gina parcial de salir del sistema
    public class ModeloSalirParcial : PageModel
    {
        // Constructor del modelo de la p�gina
        public ModeloSalirParcial()
        {
        }

        // M�todo OnGet para cerrar sesi�n (Logout)
        public async Task<IActionResult> OnGetAsync()
        {
            // Remueve la informaci�n guardada en la sesi�n
            HttpContext.Session.Remove("NombreDeUsuario");
            // Limpia la sesi�n
            HttpContext.Session.Clear();
            // Cierra sesi�n en el contexto http
            await HttpContext.SignOutAsync();
            // Redirecciona a la p�gina de inicio
            return RedirectToPage("/Index");
        }
    }
}
