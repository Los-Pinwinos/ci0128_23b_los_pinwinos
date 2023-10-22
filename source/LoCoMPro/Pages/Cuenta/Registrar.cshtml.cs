using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Cuenta;
using LoCoMPro.Utils;
using LoCoMPro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Pages.Cuenta
{
    // Clase modelo para manejar la página de registrar cuenta
    public class ModeloRegistrar : PageModel
    {
        // Contexto para interactuar con la base de datos
        private readonly LoCoMProContext contexto;

        // Propiedad para hashear contraseñas
        private readonly PasswordHasher<Usuario> hasheador;

        // Controlador para el envio de correos de bienvenida
        private ControladorCorreos controladorCorreos { get; set; }

        // Propieda ligada para validaciones con el modelo de vista
        [BindProperty]
        public CrearUsuarioVM usuarioActual { get; set; }

        // Propiedad para guardar la lista de provincias
        public List<Provincia> provincias { get; set; }

        // Constructor del modelo de la página
        public ModeloRegistrar(LoCoMProContext contexto)
        {
            // Obtiene el contexto
            this.contexto = contexto;
            // Crea un hasheador de contraseñas
            this.hasheador = new PasswordHasher<Usuario>();
            // Crea un controlador de correos
            this.controladorCorreos = new ControladorCorreos();
            // Crea un CrearUsuarioVM dummy para no tener nulo
            this.usuarioActual = new CrearUsuarioVM
            {
                nombreDeUsuario = "",
                correo = "",
                contrasena = "+",
                confirmarContrasena = "-"
            };
            // Crea una lista para guardar las provincias
            this.provincias = new List<Provincia>();
        }

        // Método para manejar los GET http requests de la página
        public IActionResult OnGet()
        {
            // Cargar toda la información de provincias de la base de datos
            this.provincias = this.contexto.Provincias.ToList();

            // Retorna la página
            return Page();
        }

        // Método para obtener los cantones de una provincia específica
        public async Task<IActionResult> OnGetCantonesPorProvincia(string provincia)
        {
            // Pide a la base de datos los cantones que presentan el nombre de la
            // provincia indicado
            var cantones = await this.contexto.Cantones
                .Where(c => c.nombreProvincia == provincia)
                .ToListAsync();

            // Retorna un JSON con los cantones de la provincia específica
            return new JsonResult(cantones);
        }

        // Método para obtener los distritos de un provincia y cantón especificos
        public async Task<IActionResult> OnGetDistritosPorCanton(string provincia, string canton)
        {
            // Pide a la base de datos los distritos que presentan el nombre del
            // provincia y canton indicados
            var distritos = await this.contexto.Distritos
                .Where(d => d.nombreProvincia == provincia && d.nombreCanton == canton)
                .ToListAsync();

            // Retorna un JSON con los distritos del provincia y cantón específicos
            return new JsonResult(distritos);
        }

        // Método cuando se presiona el botón para crear una cuenta
        public async Task<IActionResult> OnPostCrearCuenta()
        {
            // Si los datos recopilados son válidos (cumplen con las propiedades de
            // validación del modelo de vista del lado del servidor)
            if (ModelState.IsValid)
            {
                // Busca si ese nombre de usuario o correo ya existen en la base de datos
                var usuarioExistente = this.contexto.Usuarios.FirstOrDefault(
                    p => p.nombreDeUsuario == this.usuarioActual.nombreDeUsuario ||
                    p.correo == this.usuarioActual.correo);

                // Si el nombre de usuario y correo no existen en el sistema
                if (usuarioExistente == null)
                {
                    // Intente enviar un correo de bienvenida al correo indicado
                    const string asunto = "Bienvenido a LoCoMPro!";
                    string titulo = "Buenas " + this.usuarioActual.nombreDeUsuario + "!";
                    const string cuerpo = "<br>Nos complace recibirte como parte de nuestro" +
                        " equipo en LoComPro.<br>Esperamos que la plataforma te sea útil y " +
                        "de tu agrado.<br><br>Para autenticar tu cuenta y tener acceso a " +
                        "todas las funcionalidades de un usuario registrado, presiona el " +
                        "siguiente botón:<br>";

                    string enlace = PageContext.HttpContext.Request.Scheme + "://" +
                        PageContext.HttpContext.Request.Host.Host + ":" +
                        PageContext.HttpContext.Request.Host.Port +
                        "/Cuenta/Ingresar";

                    // Si logra enviar el correo
                    if (this.controladorCorreos.enviarCorreoHtml(this.usuarioActual.correo,
                        asunto, titulo, cuerpo, enlace,"Autenticar cuenta"))
                    {
                        // Crea un nuevo usuario con los datos del modelo vista
                        var nuevoUsuario = new Usuario
                        {
                            nombreDeUsuario = this.usuarioActual.nombreDeUsuario,
                            correo = this.usuarioActual.correo,
                            hashContrasena = this.usuarioActual.contrasena,
                            estado = 'A',
                            calificacion = 0,
                            distritoVivienda = this.usuarioActual.distritoVivienda,
                            cantonVivienda = this.usuarioActual.cantonVivienda,
                            provinciaVivienda = this.usuarioActual.provinciaVivienda
                        };
                        // Agrega la contraseña hasheada
                        nuevoUsuario.hashContrasena =
                            this.hasheador.HashPassword(nuevoUsuario, this.usuarioActual.contrasena);
                        // Agrega el nuevo usuario al contexto
                        this.contexto.Add(nuevoUsuario);
                        // Envía los datos a la base de datos
                        await this.contexto.SaveChangesAsync();
                        // Retorna a la página
                        return RedirectToPage("Ingresar");
                    }
                    // Si falla enviando el correo
                    else
                    {
                        // Establece el error para enviar un mensaje
                        HttpContext.Items["error"] = "No se logró comunicar con la cuenta de correo," +
                            " inténtelo más tarde";
                    }
                }
                // Si el nombre de usuario o correo ya existen en el sistema
                else
                {
                    // Establece el error para enviar un mensaje
                    HttpContext.Items["error"] = "Nombre de usuario o correo ya existentes en el sistema";
                }
            }
            // Si los datos no son válidos
            else
            {
                // Establece el error para enviar un mensaje
                HttpContext.Items["error"] = "No se ingresó datos válidos";
            }

            // Recargar la información de provincias de la base de datos
            this.provincias = this.contexto.Provincias.ToList();

            // Retorna a la página
            return Page();
        }
    }
}
