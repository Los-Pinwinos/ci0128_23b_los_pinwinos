using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Cuenta;
using LoCoMPro.Utils;
using LoCoMPro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace LoCoMPro.Pages.Cuenta
{
    // Clase modelo para manejar la p�gina de registrar cuenta
    public class ModeloPerfil : PageModel
    {
        // Contexto para interactuar con la base de datos
        private readonly LoCoMProContext contexto;

        // Propiedad para manejar el usuario actual
        private Usuario usuario;

        // Propieda ligada para validaciones con el modelo de vista
        [BindProperty]
        public ModificarUsuarioVM usuarioActual { get; set; }

        // Propiedad para guardar la lista de provincias
        public List<Provincia> provincias { get; set; }

        // Constructor del modelo de la p�gina
        public ModeloPerfil(LoCoMProContext contexto)
        {
            // Obtiene el contexto
            this.contexto = contexto;
            // Crea un usuario dummy para no tener nulo
            this.usuario = new Usuario
            {
                nombreDeUsuario = "",
                correo = "",
                hashContrasena = ""
            };
            // Crea un ModificarUsuarioVM dummy para no tener nulo
            this.usuarioActual = new ModificarUsuarioVM
            {
                nombreDeUsuario = "",
                correo = ""
            };
            // Crea una lista para guardar las provincias
            this.provincias = new List<Provincia>();
        }

        // M�todo para manejar los GET http requests de la p�gina
        public IActionResult OnGet()
        {
            // Cargar toda la informaci�n de provincias de la base de datos
            this.provincias = this.contexto.Provincias.ToList();

            // Si el usuario est� loggeado
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                // Obtiene la instancia del usuario
                this.usuario = this.contexto.Usuarios.FirstOrDefault(
                    p => p.nombreDeUsuario == User.Identity.Name) ?? this.usuario;

                // Actualiza el modelo vista con el usuario obtenido
                this.usuarioActual.nombreDeUsuario = this.usuario.nombreDeUsuario;
                this.usuarioActual.correo = this.usuario.correo;
                this.usuarioActual.provinciaVivienda = this.usuario.provinciaVivienda;
                this.usuarioActual.cantonVivienda = this.usuario.cantonVivienda;
                this.usuarioActual.distritoVivienda = this.usuario.distritoVivienda;

            // Si el usuarion no est� loggeado
            } else
            {
                // Generar un mensaje para redireccionar a la p�gina de inicio
                ViewData["Redireccionar"] = "Porfavor ingrese al sistema.";
            }

            // Retorna la p�gina
            return Page();
        }

        // M�todo para obtener los cantones de una provincia espec�fica
        public async Task<IActionResult> OnGetCantonesPorProvincia(string provincia)
        {
            // Pide a la base de datos los cantones que presentan el nombre de la
            // provincia indicado
            var cantones = await this.contexto.Cantones
                .Where(c => c.nombreProvincia == provincia)
                .ToListAsync();

            // Retorna un JSON con los cantones de la provincia espec�fica
            return new JsonResult(cantones);
        }

        // M�todo para obtener los distritos de un provincia y cant�n especificos
        public async Task<IActionResult> OnGetDistritosPorCanton(string provincia, string canton)
        {
            // Pide a la base de datos los distritos que presentan el nombre del
            // provincia y canton indicados
            var distritos = await this.contexto.Distritos
                .Where(d => d.nombreProvincia == provincia && d.nombreCanton == canton)
                .ToListAsync();

            // Retorna un JSON con los distritos del provincia y cant�n espec�ficos
            return new JsonResult(distritos);
        }

        // M�todo cuando se presiona el bot�n para crear una cuenta
        public async Task<IActionResult> OnPostActualizarUsuario()
        {
            // Si los datos recopilados son v�lidos (cumplen con las propiedades de
            // validaci�n del modelo de vista del lado del servidor)
            if (ModelState.IsValid)
            {
                
            }

            // Recargar la informaci�n de provincias de la base de datos
            this.provincias = this.contexto.Provincias.ToList();

            // Retorna a la p�gina
            return Page();
        }
    }
}
