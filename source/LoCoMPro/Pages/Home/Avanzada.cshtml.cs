using Humanizer;
using LoCoMPro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace LoCoMPro.Pages.Home
{
    public class AvanzadaModel : PageModel
    {

        private readonly LoCoMPro.Data.LoCoMProContext contexto;

        // Usuario
        public double latitudUsuario { get; set; }
        public double longitudUsuario { get; set; }

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

        // Obtencion de las coordenadas de un usuario
        private void ObtenerDireccionUsuario()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                // Colocar 0s, pues el usuario no ha ingresado en el sistema
                this.latitudUsuario = 0;
                this.longitudUsuario = 0;
            }
            else
            {
                // Obtener una tupla con los resultados de la longitud y latitud del usuario actual
                IQueryable<Tuple<double, double>> resultadosIQ = this.contexto.Usuarios
                    .Where(r => r.nombreDeUsuario == User.Identity.Name)
                    .Select(u => new Tuple<double, double>(u.latitudVivienda, u.longitudVivienda));

                this.latitudUsuario = resultadosIQ.ToList()[0].Item1;
                this.longitudUsuario = resultadosIQ.ToList()[0].Item2;
            }
        }

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

            // Obtener la direccion del usuario
            this.ObtenerDireccionUsuario();

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
