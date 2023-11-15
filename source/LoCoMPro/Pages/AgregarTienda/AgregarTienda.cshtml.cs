using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.ViewModels.Tienda;
using LoCoMPro.Models;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Pages.AgregarTienda
{
    public class AgregarTiendaModel : PageModel
    {
        [BindProperty]
        public required string Canton { get; set; }

        [BindProperty]
        public required string Provincia { get; set; }

        [BindProperty]
        public required AgregarTiendaVM Tienda { get; set; }

        public List<Provincia> ListaProvincias { get; set; }

        private readonly Data.LoCoMProContext contexto;

        public AgregarTiendaModel(Data.LoCoMProContext contexto)
        {
            this.contexto = contexto;
            this.Tienda = new AgregarTiendaVM
            {
                nombre = " ",
                nombreDistrito = " ",
                nombreCanton = " ",
                nombreProvincia = " "
            };

            this.ListaProvincias = new List<Provincia>();
        }

        public IActionResult OnGet()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                // Se redirige al usuario porque debe estar ingresado para esta funcionalidad
                ViewData["RedirectMessage"] = "usuario";
            }

            // Cargar toda la información de provincias de la base de datos
            this.ListaProvincias = this.contexto.Provincias.ToList();

            return Page();
        }

        public async Task<IActionResult> OnGetCantonesPorProvincia(string provincia)
        {
            var cantones = await this.contexto.Cantones
                .Where(c => c.nombreProvincia == provincia)
                .ToListAsync();

            return new JsonResult(cantones);
        }

        // Método que obtiene los datos para enviarlos al Autcompletado
        public void OnGetColocarDatosTemporales(string provincia, string canton)
        {
            string distrito = this.ObtenerDistritos(provincia, canton)[0].nombre;
            AgregarDatosAutocompletado(provincia, canton, distrito);
        }

        private List<Distrito> ObtenerDistritos(string provincia, string canton)
        {
            return this.contexto.Distritos
                .Where(d => d.nombreProvincia == provincia && d.nombreCanton == canton)
                .ToList();
        }

        // Agrega al TempData la provincia, cantón y distrito necesarias para el Autocompletado
        private void AgregarDatosAutocompletado(string provincia, string canton, string distrito)
        {
            // Guarda los datos en TempData para que puedan ser accesados después
            TempData["provinciaAutocompletado"] = provincia;
            TempData["cantonAutocompletado"] = canton;
            TempData["distritoAutocompletado"] = distrito;
        }

        public IActionResult OnPostVerificarDatos()
        {
            // Validar datos del usuario
            if (!AnalizarDatos())
            {
                return Page();
            }
            // Preparar datos para la siguiente ventana
            AgregarDatosTienda();
            // Redirigir a la siguiente ventana de agregar producto
            return RedirectToPage("/AgregarProducto/AgregarProd");
        }

        private bool AnalizarDatos()
        {
            this.Tienda.nombreProvincia = this.Provincia;
            this.Tienda.nombreCanton = this.Canton;
            // Se debe encontrar el distrito correspondiente
            this.Tienda.nombreDistrito
                = ObtenerDistritos(this.Provincia, this.Canton)[0].nombre;

            return VerificarTiendaValida();
        }

        private bool VerificarTiendaValida()
        {
            var tiendaExistenteEnDistrito = contexto.Tiendas.FirstOrDefault
                (p => p.nombre == this.Tienda.nombre
                && p.nombreDistrito == this.Tienda.nombreDistrito
                && p.nombreCanton == this.Tienda.nombreCanton
                && p.nombreProvincia == this.Tienda.nombreProvincia);

            if (tiendaExistenteEnDistrito == null)
            {  // La tienda no existe y el nombre está disponible
                CrearNuevaTienda();
            }

            // La tienda ya existe
            return true;
        }

        private void CrearNuevaTienda()
        {
            var nuevaTienda = new Tienda
            {
                nombre = this.Tienda.nombre,
                nombreDistrito = this.Tienda.nombreDistrito,
                nombreCanton = this.Tienda.nombreCanton,
                nombreProvincia = this.Tienda.nombreProvincia,
                latitud = 0,
                longitud = 0
            };
            this.contexto.Add(nuevaTienda);
            this.contexto.SaveChanges();
        }

        // Datos para siguiente ventana
        private void AgregarDatosTienda()
        {
            // Guarda los datos en TempData para que puedan ser accesados después
            TempData["nombreTienda"] = this.Tienda.nombre;
            TempData["provinciaTienda"] = this.Tienda.nombreProvincia;
            TempData["cantonTienda"] = this.Tienda.nombreCanton;
            TempData["distritoTienda"] = this.Tienda.nombreDistrito;
        }

        public IActionResult OnPostCancelar()
        {
            // Dirigir a la página de inicio
            return RedirectToPage("/Home/Index");
        }
    }
}