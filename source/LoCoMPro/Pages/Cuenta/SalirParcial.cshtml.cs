using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoCoMPro.Pages.Cuenta
{
    // Clase modelo para manejar la página parcial de salir del sistema
    public class ModeloSalirParcial : PageModel
    {
        // Constructor del modelo de la página
        public ModeloSalirParcial()
        {
        }

        // Método OnGet para cerrar sesión (Logout)
        public async Task<IActionResult> OnGetAsync()
        {
            // Remueve la información guardada en la sesión
            HttpContext.Session.Remove("NombreDeUsuario");
            // Limpia la sesión
            HttpContext.Session.Clear();
            // Cierra sesión en el contexto http
            await HttpContext.SignOutAsync();
            // Redirecciona a la página de inicio
            return RedirectToPage("/Index");
        }
    }
}
