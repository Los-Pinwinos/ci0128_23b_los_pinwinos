using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoCoMPro.Pages.Home
{
    public class Autocompletado : PageModel
    {
        private readonly Data.LoCoMProContext contexto;

        public Autocompletado(Data.LoCoMProContext contexto_base)
        {
            this.contexto = contexto_base;
        }
        public JsonResult OnGet(string term)
        {
            IList<string> resultados = new List<string>();

            resultados = contexto.Productos
                .Where(p => p.marca.StartsWith(term))
                .Select(p => p.marca)
                .Distinct()
                .OrderBy(p => p)
                .ToList();

            return new JsonResult(resultados);
        }
    }
}
