using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.ViewModels.Tienda;
using LoCoMPro.Models;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Pages.AgregarTienda
{
    public class AgregarTiendaModel : PageModel
    {
        // Distrito
        [BindProperty]
        public required string Distrito { get; set; }

        // Cant�n
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
        public List<Provincia> ListaProvincias { get; set; }

        // Contexto
        private readonly LoCoMPro.Data.LoCoMProContext contexto;

        //Constructor
        public AgregarTiendaModel(LoCoMPro.Data.LoCoMProContext contexto)
        {
            this.contexto = contexto;
            this.Tienda = new AgregarTiendaVM
            {  // Crea una tienda vac�a para trabajar con ella m�s adelante
                nombre = " ",
                nombreDistrito = " ",
                nombreCanton = " ",
                nombreProvincia = " "
            };

            // Crea una lista para guardar las provincias
            this.ListaProvincias = new List<Provincia>();
        }

        // OnGet de la p�gina
        public IActionResult OnGet()
        {
            // Cargar toda la informaci�n de provincias de la base de datos
            this.ListaProvincias = this.contexto.Provincias.ToList();

            return Page();
        }

        // M�todo para obtener los cantones de una provincia espec�fica
        public async Task<IActionResult> OnGetCantonesPorProvincia(string provincia)
        {
            // Obtiene los cantones de la provincia
            var cantones = await this.contexto.Cantones
                .Where(c => c.nombreProvincia == provincia)
                .ToListAsync();

            // Retorna un JSON con los cantones de la provincia espec�fica
            return new JsonResult(cantones);
        }

        // Acci�n al presionar siguiente
        public IActionResult OnPostSiguiente()
        {
            // Leer datos del usuario
            if (!AnalizarDatos())
            {
                // Los datos no son v�lidos
                return Page();
            }

            // Preparar datos para la siguiente ventana
            AgregarDatosTienda();

            // Redirigir a la siguiente ventana de agregar producto
            return RedirectToPage("/Index");

            // TODO(Los Pinwinos): actualizar para ir a la p�gina de agregar producto
            // return RedirectToPage("/AgregarProducto/AgregarProd");
        }

        // M�todo para leer los datos que el usuario ingres�
        private bool AnalizarDatos()
        {
            // Guarda los nuevos datos en la tienda
            this.Tienda.nombreProvincia = Provincia;
            this.Tienda.nombreCanton = Canton;
            this.Tienda.nombreDistrito = Distrito;
            this.Tienda.nombre = Nombre;

            // Se verifica si la tienda es v�lida
            return VerificarValida();
        }

        // M�todo para verificar validez de la tienda
        private bool VerificarValida()
        {
            // Revisa si existe una tienda con ese nombre en el distrito, cant�n y provincia indicados
            var tiendaExistenteEnDistrito = contexto.Tiendas.FirstOrDefault
                (p => p.nombre == this.Tienda.nombre
                && p.nombreDistrito == this.Tienda.nombreDistrito
                && p.nombreCanton == this.Tienda.nombreCanton
                && p.nombreProvincia == this.Tienda.nombreProvincia);

            if (tiendaExistenteEnDistrito == null)
            {  // La tienda no existe y el nombre est� disponible
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
            // Guarda los datos en TempData para que puedan ser accesados despu�s
            TempData["nombreTienda"] = this.Tienda.nombre;
            TempData["provinciaTienda"] = this.Tienda.nombreProvincia;
            TempData["cantonTienda"] = this.Tienda.nombreCanton;
            TempData["distritoTienda"] = this.Tienda.nombreDistrito;
        }

        // Acci�n al presionar cancelar
        public IActionResult OnPostCancelar()
        {
            return RedirectToPage("/Index");

            // TODO(Los Pinwinos): actualizar para que se dirija a la p�gina de inicio
            // return RedirectToPage("/Home/Index");
        }
    }
}