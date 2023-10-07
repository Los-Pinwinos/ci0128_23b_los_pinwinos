using Humanizer;
using LoCoMPro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoCoMPro.Pages.Home
{
    public class AvanzadaModel : PageModel
    {

        private readonly LoCoMPro.Data.LoCoMProContext contexto;
        
        public AvanzadaModel(LoCoMPro.Data.LoCoMProContext contexto)
        {
            this.contexto = contexto;
            // Crea una lista para guardar las provincias
            this.provincias = new List<Provincia>();
        }

        // Mapa
        public IList<Provincia>? provincias;

        // Busqueda
        [BindProperty(SupportsGet = true)]
        public string? producto { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? marca { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? provincia { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? canton { get; set; } = default!;

        // On GET Buscar
        public IActionResult OnGetBuscar()
        {
            if (!string.IsNullOrWhiteSpace(producto)
                || !string.IsNullOrWhiteSpace(marca)
                || !string.IsNullOrWhiteSpace(provincia)
                || !string.IsNullOrWhiteSpace(canton))
            {
                // Redireccionar
                return RedirectToPage("/Busqueda/Avanzada", new
                {
                    handler = "BuscarAvanzado",
                    nombreProducto = this.producto,
                    nombreMarca = this.marca,
                    nombreProvincia = this.provincia,
                    nombreCanton = this.canton
                });
            }
            else
            {
                return Page();
            }
        }

        // On GET
        public IActionResult OnGet()
        {
            // Cargar toda la información de provincias de la base de datos
            provincias = contexto.Provincias.ToList();

            // Retorna la página
            return Page();
        }

        //On GET obtener los cantones de una provincia específica
        public IActionResult OnGetCantonesPorProvincia(string provincia)
        {
            // Pide a la base de datos los cantones que presentan el nombre de la
            // provincia indicado
            var cantones = contexto.Cantones
                .Where(c => c.nombreProvincia == provincia)
                .ToList();

            // Retorna un JSON con los cantones de la provincia específica
            return new JsonResult(cantones);
        }
    }
}
