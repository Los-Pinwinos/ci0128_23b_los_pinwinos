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
    // Clase modelo para manejar la página de registrar cuenta
    public class ModeloPerfil : PageModel
    {
        // Contexto para interactuar con la base de datos
        private readonly LoCoMProContext contexto;

        // Propiedad para manejar el usuario actual
        public Usuario usuario { get; set; }

        // Propieda ligada para validaciones con el modelo de vista
        [BindProperty]
        public ModificarUsuarioVM usuarioActual { get; set; }

        // Propiedad para guardar la lista de provincias
        public List<Provincia> provincias { get; set; }

        // Propiedad para guardar la lista de cantones iniciales
        public List<Canton> cantones { get; set; }

        // Propiedad para guardar la lista de distritos iniciales
        public List<Distrito> distritos { get; set; }

        // Propiedad para guardar la cantidad de aportes que ha hecho el usuario
        public int cantidadAportes { get; set; }

        // Constructor del modelo de la página
        public ModeloPerfil(LoCoMProContext contexto)
        {
            // Obtiene el contexto
            this.contexto = contexto;
            // Crea un usuario dummy para no tener nulo
            this.usuario = new Usuario
            {
                nombreDeUsuario = "",
                correo = "",
                hashContrasena = "",
                calificacion = 0.0
            };
            // Crea un ModificarUsuarioVM dummy para no tener nulo
            this.usuarioActual = new ModificarUsuarioVM
            {
            };
            // Crea una lista para guardar las provincias
            this.provincias = new List<Provincia>();
            // Crea una lista para guardar las cantones iniciales
            this.cantones = new List<Canton>();
            // Crea una lista para guardar los distritos iniciales
            this.distritos = new List<Distrito>();
        }

        // Método para manejar los GET http requests de la página
        public IActionResult OnGet()
        {
            // Cargar toda la información de provincias de la base de datos
            this.provincias = this.contexto.Provincias.ToList();

            // Si el usuario está loggeado
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

                // Actualiza los cantones y distritos iniciales de acuerdo
                // a la vivienda del usuario obtenido
                this.cantones = this.contexto.Cantones.Where
                    (c => c.nombreProvincia == this.usuario.provinciaVivienda).ToList();

                this.distritos = this.contexto.Distritos.Where(
                    c => c.nombreProvincia == this.usuario.provinciaVivienda &&
                    c.nombreCanton == this.usuario.cantonVivienda).ToList();

                // Consigue la cantidad de aportes hechos por el usuario
                this.cantidadAportes = this.contexto.Registros.Where(
                    c => c.usuarioCreador == this.usuario.nombreDeUsuario && c.calificacion != 0).Count();

                // Si el usuarion no está loggeado
            } else
            {
                // Generar un mensaje para redireccionar a la página de inicio
                ViewData["Redireccionar"] = "Porfavor ingrese al sistema.";
            }

            // Retorna la página
            return Page();
        }

        // Método para obtener los cantones de una provincia específica
        public async Task<IActionResult> OnGetCantonesPorProvincia(string provincia)
        {
            // Pide a la base de datos los cantones que presentan el nombre de la
            // provincia indicado
            var listaCantones = await this.contexto.Cantones
                .Where(c => c.nombreProvincia == provincia)
                .ToListAsync();

            // Retorna un JSON con los cantones de la provincia específica
            return new JsonResult(listaCantones);
        }

        // Método para obtener los distritos de un provincia y cantón especificos
        public async Task<IActionResult> OnGetDistritosPorCanton(string provincia, string canton)
        {
            // Pide a la base de datos los distritos que presentan el nombre del
            // provincia y canton indicados
            var listaDistritos = await this.contexto.Distritos
                .Where(d => d.nombreProvincia == provincia && d.nombreCanton == canton)
                .ToListAsync();

            // Retorna un JSON con los distritos del provincia y cantón específicos
            return new JsonResult(listaDistritos);
        }

        // Método cuando se presiona el botón para crear una cuenta
        public IActionResult OnPostActualizarUsuario()
        {
            // Si el usuario está loggeado
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                // Obtiene la instancia del usuario
                this.usuario = this.contexto.Usuarios.FirstOrDefault(
                    p => p.nombreDeUsuario == User.Identity.Name) ?? this.usuario;

                // Si los datos recopilados son válidos (cumplen con las propiedades de
                // validación del modelo de vista del lado del servidor)
                if (ModelState.IsValid)
                {
                    // Actualiza el usuario con los datos del modelo vista
                    this.usuario.provinciaVivienda = this.usuarioActual.provinciaVivienda;
                    this.usuario.cantonVivienda = this.usuarioActual.cantonVivienda;
                    this.usuario.distritoVivienda = this.usuarioActual.distritoVivienda;

                    // Guarda los cambios en la base de datos
                    this.contexto.SaveChanges();
                }
            }

            // Retorna a la página
            return RedirectToPage("/Cuenta/Perfil");
        }
    }
}
