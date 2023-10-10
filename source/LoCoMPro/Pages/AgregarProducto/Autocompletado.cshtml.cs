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
        public JsonResult OnGet(string term)
        {
            IList<string> resultados = resultados = contexto.Productos.Where(p => p.nombre.Contains(term)).Select(p => p.nombre).ToList();
            return new JsonResult(resultados);
        }
    }
}
