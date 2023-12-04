using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.ViewModels.Moderacion;
using LoCoMPro.Data;
using LoCoMPro.Utils.Buscadores;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.Utils;

namespace LoCoMPro.Pages.Moderacion
{
    public class ProductosSimilares : PageModel
    {
        protected readonly LoCoMProContext contexto;
        protected readonly IConfiguration configuracion;
        public string? principalesGrupo;
        public string? gruposJson;
        public int paginaDefault { get; set; }
        public int resultadosPorPagina { get; set; }
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
            this.paginaDefault = 1;
            this.resultadosPorPagina = this.configuracion.GetValue("TamPagina", 5);
        }

        public IActionResult OnGet()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated && User.IsInRole("moderador"))
            {
                BuscadorDeAgrupaciones buscador = new BuscadorDeAgrupaciones(this.contexto);
                IQueryable<ProductosSimilaresVM> busqueda = buscador.buscar();
                List<ProductosSimilaresVM> resultados = busqueda.ToList();
                // Se asume que no hay resultados
                this.principalesGrupo = "0";
                this.gruposJson = "0";
                var resultadosCluster = buscador.obtenerResultadosCluster();
                if (resultados.Count > 0 && resultadosCluster != null && resultadosCluster.Count > 0)
                {
                    // Se actualizan si hay resultados
                    this.principalesGrupo = ControladorJson.ConvertirAJson(resultados);
                    this.gruposJson = ControladorJson.ConvertirAJson(resultadosCluster);
                }
            }
            else
            {
                ViewData["MensajeError"] = "Por favor ingrese al sistema como moderador.";
            }
            return Page();
        }
    }
}
