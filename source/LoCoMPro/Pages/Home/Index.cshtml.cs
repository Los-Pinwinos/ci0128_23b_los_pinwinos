using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata;

namespace LoCoMPro.Pages.Home
{
    public class HomeModel : PageModel
    {
        // Busqueda
        [BindProperty(SupportsGet =true)]
        public string? producto { get; set; }
        // On GET
        public IActionResult OnGet()
        {
            if (!string.IsNullOrWhiteSpace(producto)) { 
                // Redireccionar
                return RedirectToPage("/Busqueda/Index", new { nombreProducto = this.producto });
            } else
            {
                return Page();
            }
        }
    }
}