using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Pages.AgregarTienda
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

        // Crea JSON
        public JsonResult OnGet(List<String> term)
        {
            IList<string> resultados = resultados
                = contexto.Tiendas
                .Where(p => p.nombre.Contains(term[0]))
                .Select(p => p.nombre).ToList();
            return new JsonResult(resultados);
        }
    }
}