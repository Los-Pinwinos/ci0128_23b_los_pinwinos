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
            // L�gica para manejar la acci�n cuando se presiona el bot�n "Aceptar"
            Console.WriteLine("Boton de aceptar fue presionado");

        }

        public void OnPostCancelar()
        {
            // L�gica para manejar la acci�n cuando se presiona el bot�n "Cancelar
             Console.WriteLine("Boton de cancelar fue presionado");
        }
    }
}
