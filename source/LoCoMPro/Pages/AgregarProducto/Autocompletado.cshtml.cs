using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Pages.AgregarProducto
{
    public class Autocompletado : PageModel
    {
        private readonly Data.LoCoMProContext contexto;
        private readonly IConfiguration configuracion;

        public Autocompletado(Data.LoCoMProContext contexto_base, IConfiguration configuracion_pagina)
        {
            contexto = contexto_base;
            configuracion = configuracion_pagina;
        }
        public JsonResult OnGet(string term)
        {
            IList<string> resultados = resultados = contexto.Productos.Where(p => p.nombre.Contains(term)).Select(p => p.nombre).ToList();
            return new JsonResult(resultados);
        }
    }
}
