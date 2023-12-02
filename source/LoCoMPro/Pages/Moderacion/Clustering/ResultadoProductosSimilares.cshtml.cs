using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoCoMPro.Pages.Moderacion.Clustering
{
    public class ResultadoProductosSimilaresModel : PageModel
    {
        public string? resultadosJson;
        public string? nombreProducto;
        public void OnGet(string resultadosJson, string nombreProducto)
        {
            this.resultadosJson = resultadosJson;
            this.nombreProducto = nombreProducto;
        }
    }
}
