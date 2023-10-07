using LoCoMPro.ViewModels.Tienda;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        public JsonResult OnGet(String term)
        {
            // IList para el resultado
            IList<string> tiendas = new List<string>();

            // Actualizar provincia, cantón y distrito
            if (!TempData.ContainsKey("provinciaAutocompletado")
                    || !TempData.ContainsKey("cantonAutocompletado")
                    || !TempData.ContainsKey("distritoAutocompletado"))
            {
                Console.Error.WriteLine("Los datos para el autcompletado no se encontraron.");

                // Hay un error, por lo que no debe intentar obtener los datos temporales
                return new JsonResult(tiendas);
            }

            // Se obtienen los datos temporales
            string provincia = TempData["provinciaAutocompletado"]?.ToString() ?? "";
            string canton = TempData["cantonAutocompletado"]?.ToString() ?? "";
            string distrito = TempData["distritoAutocompletado"]?.ToString() ?? "";

            // Se vuelven a escrbir los datos para que siempre existan
            TempData["provinciaAutocompletado"] = provincia;
            TempData["cantonAutocompletado"] = canton;
            TempData["distritoAutocompletado"] = distrito;

            // Obtiene los resultados de la base de datos
            IList<AgregarTiendaVM> resultados = resultados
                = contexto.Tiendas
                .Where(r => r.nombre.Contains(term))
                .Select(r => new AgregarTiendaVM
                {
                    nombre = r.nombre,
                    nombreDistrito = r.nombreDistrito,
                    nombreCanton = r.nombreCanton,
                    nombreProvincia = r.nombreProvincia
                }).ToList();

            // Obtiene solo nos nombres de las tiendas que cumplen
            for (int i = 0; i < resultados.Count(); ++i)
            {
                if (resultados[i].nombreDistrito == distrito
                    && resultados[i].nombreCanton == canton
                    && resultados[i].nombreProvincia == provincia)
                {
                    tiendas.Add(resultados[i].nombre);
                }
            }

            // Retorna un JSON con las tiendas válidas
            return new JsonResult(tiendas);
        }
    }
}
