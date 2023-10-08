using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.ViewModels.Tienda;
using System.Drawing;
using LoCoMPro.Models;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using LoCoMPro.Data.CR;

namespace LoCoMPro.Pages.AgregarTienda
{
    public class AgregarTiendaModel : PageModel
    {

        // Cantón
        [BindProperty]
        public required string Canton { get; set; }

        // Provincia
        [BindProperty]
        public required string Provincia { get; set; }

        // Nombre
        [BindProperty]
        public required string Nombre { get; set; }

        // View model de tienda
        [BindProperty]
        public required AgregarTiendaVM Tienda { get; set; }

        // Lista para guardar las provincias
        public List<LoCoMPro.Models.Provincia> ListaProvincias { get; set; }

        // Contexto
        private readonly LoCoMPro.Data.LoCoMProContext contexto;

        //Constructor
        public AgregarTiendaModel(LoCoMPro.Data.LoCoMProContext contexto)
        {
            this.contexto = contexto;
            this.Tienda = new AgregarTiendaVM
            {  // Crea una tienda vacía para trabajar con ella más adelante
                nombre = " ",
                nombreDistrito = " ",
                nombreCanton = " ",
                nombreProvincia = " "
            };

            // Crea una lista para guardar las provincias
            this.ListaProvincias = new List<LoCoMPro.Models.Provincia>();
        }

        // OnGet de la página
        public IActionResult OnGet()
        {
            // Revisar si el usuario está loggeado
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                ViewData["RedirectMessage"] = "usuario";
            }

            // Cargar toda la información de provincias de la base de datos
            this.ListaProvincias = this.contexto.Provincias.ToList();

            return Page();
        }

        // Método para obtener los cantones de una provincia específica
        public async Task<IActionResult> OnGetCantonesPorProvincia(string provincia)
        {
            // Obtiene los cantones de la provincia
            var cantones = await this.contexto.Cantones
                .Where(c => c.nombreProvincia == provincia)
                .ToListAsync();

            // Retorna un JSON con los cantones de la provincia específica
            return new JsonResult(cantones);
        }

        // Método para obtener los cantones de una provincia específica
        public void OnGetColocarDatosTemporales(string provincia, string canton)
        {
            // Se obtiene el distrito
            string distrito = this.ObtenerDistritos(provincia, canton)[0].nombre;

            // Se colocan los datos temporales para autocompletado
            AgregarDatosAutocompletado(provincia, canton, distrito);
        }

        private List<LoCoMPro.Models.Distrito> ObtenerDistritos(string provincia, string canton)
        {
            // Obtiene los distritos de esa provincia y cantón
            return this.contexto.Distritos
                .Where(d => d.nombreProvincia == provincia && d.nombreCanton == canton)
                .ToList();
        }

        // Datos para el autocompletado
        public void AgregarDatosAutocompletado(string provincia, string canton, string distrito)
        {
            TempData["provinciaAutocompletado"] = provincia;
            TempData["cantonAutocompletado"] = canton;
            TempData["distritoAutocompletado"] = distrito;
        }

        // Acción al presionar siguiente
        public IActionResult OnPostSiguiente()
        {
            // Leer datos del usuario
            if (!AnalizarDatos())
            {
                // Los datos no son válidos
                return Page();
            }
            // Preparar datos para la siguiente ventana
            AgregarDatosTienda();
            // Redirigir a la siguiente ventana de agregar producto
            return RedirectToPage("/AgregarProducto/AgregarProd");
        }

        // Método para leer los datos que el usuario ingresó
        private bool AnalizarDatos()
        {
            // Guarda los nuevos datos en la tienda
            this.Tienda.nombreProvincia = Provincia;
            this.Tienda.nombreCanton = Canton;
            this.Tienda.nombreDistrito  // se debe encontrar el distrito correspondiente
                = ObtenerDistritos(this.Tienda.nombreProvincia, this.Tienda.nombreCanton)[0].nombre;
            this.Tienda.nombre = Nombre;

            // Se verifica si la tienda es válida
            return VerificarValida();
        }

        // Método para verificar validez de la tienda
        private bool VerificarValida()
        {
            var tiendaExistenteEnDistrito = contexto.Tiendas.FirstOrDefault
                (p => p.nombre == this.Tienda.nombre
                && p.nombreDistrito == this.Tienda.nombreDistrito
                && p.nombreCanton == this.Tienda.nombreCanton
                && p.nombreProvincia == this.Tienda.nombreProvincia);

            // Se revisa si la tienda existe
            if (tiendaExistenteEnDistrito == null)
            {  // La tienda no existe y el nombre está disponible
                CrearNuevaTienda();
            }

            // La tienda ya existe
            return true;
        }

        // Se agrega una nueva tienda en la base de datos
        private void CrearNuevaTienda()
        {
            // Si es nueva, se debe crear
            var nuevaTienda = new Tienda
            {
                nombre = this.Tienda.nombre,
                nombreDistrito = this.Tienda.nombreDistrito,
                nombreCanton = this.Tienda.nombreCanton,
                nombreProvincia = this.Tienda.nombreProvincia
            };

            // Agrega la nueva tienda a la base de datos
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

        // Acción al presionar cancelar
        public IActionResult OnPostCancelar()
        {
            // Dirigir a la página de inicio
            return RedirectToPage("/Home/Index");
        }
    }
}