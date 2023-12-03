using LoCoMPro.ViewModels.Moderacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoCoMPro.Pages.Moderacion.Clustering
{
    public class ResultadoProductosSimilaresModel : PageModel
    {
        protected readonly IConfiguration configuracion;
        public string? resultadosJson;
        public string? nombreProducto;
        public int paginaDefault { get; set; }
        public int resultadosPorPagina { get; set; }
        public ProductosSimilaresVM productosSimilaresVM { get; set; }

        public ResultadoProductosSimilaresModel(IConfiguration configuracion)
        {
            this.productosSimilaresVM = new ProductosSimilaresVM
            {
                nombreProducto = "",
                nombreCategoria = "",
                nombreMarca = "",
                unidad = ""
            };
            this.paginaDefault = 1;
            this.configuracion = configuracion;
            this.resultadosPorPagina = this.configuracion.GetValue("TamPagina", 5);
        }

        public IActionResult OnGet(string resultadosJson, string nombreProducto)
        {
            this.resultadosJson = "0";
            this.nombreProducto = "0";
            if (User.Identity != null && User.Identity.IsAuthenticated && User.IsInRole("moderador"))
            {
                this.resultadosJson = resultadosJson;
                this.nombreProducto = nombreProducto;
            }
            else
            {
                ViewData["MensajeError"] = "Por favor ingrese al sistema como moderador.";
            }
            return Page();
        }
    }
}
