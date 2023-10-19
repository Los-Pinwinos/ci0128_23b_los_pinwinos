using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Pages.AgregarProducto
{
    public class Autocompletado : PageModel
    {
        private readonly Data.LoCoMProContext contexto;

        public Autocompletado(Data.LoCoMProContext contexto_base)
        {
            contexto = contexto_base;
        }
        public JsonResult OnGet(string term, string attribute)
        {
            IList<string> resultados = new List<string>();

            if (attribute == "Producto")
            {
                resultados = contexto.Productos
                    .Where(p => p.nombre.StartsWith(term))
                    .Select(p => p.nombre)
                    .Distinct()
                    .OrderBy(p => p)
                    .ToList();
            }
            else if (attribute == "Marca")
            {
                resultados = contexto.Productos
                    .Where(p => p.marca.StartsWith(term))
                    .Select(p => p.marca)
                    .Distinct()
                    .OrderBy(p => p)
                    .ToList();
            }
            // Add more conditions for other attributes if needed

            return new JsonResult(resultados);
        }
    }
}
