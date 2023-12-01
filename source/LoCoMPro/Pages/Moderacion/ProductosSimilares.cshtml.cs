using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.ViewModels.Moderacion;
using LoCoMPro.Data;

namespace LoCoMPro.Pages.Moderacion
{
    public class ProductosSimilares : PageModel
    {
        protected readonly LoCoMProContext contexto;
        protected readonly IConfiguration configuracion;
        public string? resultados;
        public ProductosSimilaresVM productosSimilaresVM { get; set; }

        // Constructor
        public ProductosSimilares(LoCoMProContext contexto, IConfiguration configuracion)
        {
            this.productosSimilaresVM = new ProductosSimilaresVM
            {
                nombreProducto = "",
                nombreCategoria = "",
                nombreMarca = "",
                unidad = ""
            };
            this.contexto = contexto;
            this.configuracion = configuracion;
        }

        public IActionResult OnGet()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                resultados = "1";
            }
            else
            {
                ViewData["MensajeError"] = "Por favor ingrese al sistema.";
            }
            return Page();
        }
    }
}
