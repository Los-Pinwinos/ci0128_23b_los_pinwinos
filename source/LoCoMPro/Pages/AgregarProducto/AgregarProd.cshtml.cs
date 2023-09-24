using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoCoMPro.Pages.AgregarProducto
{
    public class AgregarProdModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPostAceptar()
        {
            // Lógica para manejar la acción cuando se presiona el botón "Aceptar"
            Console.WriteLine("Boton de aceptar fue presionado");

        }

        public void OnPostCancelar()
        {
            // Lógica para manejar la acción cuando se presiona el botón "Cancelar
             Console.WriteLine("Boton de cancelar fue presionado");
        }
    }
}
