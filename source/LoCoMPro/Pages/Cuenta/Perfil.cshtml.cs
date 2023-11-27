using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Cuenta;
using LoCoMPro.Utils;
using LoCoMPro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Web;
using LoCoMPro.Utils.SQL;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace LoCoMPro.Pages.Cuenta
{
    public class ModeloPerfil : PageModel
    {
        private readonly LoCoMProContext contexto;
        private readonly IConfiguration configuracion;

        public Usuario usuario { get; set; }

        [BindProperty]
        public ModificarUsuarioVM usuarioActual { get; set; }

        public List<Provincia> provincias { get; set; }
        
        public List<Canton> cantones { get; set; }

        public List<Distrito> distritos { get; set; }

        public int cantidadAportes { get; set; }

        public string calificacionUsuario { get; set; }

        public ModeloPerfil(LoCoMProContext contexto)
        {
            this.contexto = contexto;

            // Accede al archivo de configuración
            this.configuracion = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            // Crea un usuario con datos vacíos para no tener nulo
            this.usuario = new Usuario
            {
                nombreDeUsuario = "",
                correo = "",
                hashContrasena = "",
                calificacion = 0.0,
                distritoVivienda = "",
                cantonVivienda = "",
                provinciaVivienda = "",
                latitudVivienda = 0,
                longitudVivienda = 0
            };
            // Crea un ModificarUsuarioVM con datos vacíos para no tener nulo
            this.usuarioActual = new ModificarUsuarioVM
            {
            };
            this.provincias = new List<Provincia>();
            this.cantones = new List<Canton>();
            this.distritos = new List<Distrito>();
        }

        public IActionResult OnGet()
        {
            // Verifica si la página OnPost generó un error
            if (TempData.ContainsKey("ErrorCambiarUsuario"))
            {
                // Muestra el error
                ModelState.AddModelError(string.Empty, TempData["ErrorCambiarUsuario"]!.ToString()!);
            }

            this.provincias = this.contexto.Provincias.ToList();

            // Si el usuario está loggeado
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                // Obtiene la instancia del usuario
                this.usuario = this.contexto.Usuarios.FirstOrDefault(
                    p => p.nombreDeUsuario == User.Identity.Name) ?? this.usuario;

                this.calificacionUsuario = (Math.Floor(this.usuario.calificacion * 10) / 10).ToString("0.0", new CultureInfo("fr-FR"));

                // Actualiza el modelo vista con el usuario obtenido
                this.usuarioActual.nombreDeUsuario = this.usuario.nombreDeUsuario;
                this.usuarioActual.correo = this.usuario.correo;
                this.usuarioActual.provinciaVivienda = this.usuario.provinciaVivienda;
                this.usuarioActual.cantonVivienda = this.usuario.cantonVivienda;
                this.usuarioActual.distritoVivienda = this.usuario.distritoVivienda;

                this.CargarCantonYDistrito();

                // Consigue la cantidad de aportes hechos por el usuario
                this.cantidadAportes = this.contexto.Registros.Where(
                    c => c.usuarioCreador == this.usuario.nombreDeUsuario &&
                    c.calificacion != 0 &&
                    c.visible).Count();

            } else
            {
                // Generar un mensaje para redireccionar a la página de inicio
                ViewData["Redireccionar"] = "Porfavor ingrese al sistema.";
            }

            return Page();
        }

        private void CargarCantonYDistrito()
        {
            // Actualiza los cantones y distritos iniciales de acuerdo
            // a la vivienda del usuario obtenido
            this.cantones = this.contexto.Cantones.Where
                (c => c.nombreProvincia == this.usuario.provinciaVivienda).ToList();

            this.distritos = this.contexto.Distritos.Where(
                c => c.nombreProvincia == this.usuario.provinciaVivienda &&
                c.nombreCanton == this.usuario.cantonVivienda).ToList();
        }

        public async Task<IActionResult> OnGetCantonesPorProvincia(string provincia)
        {
            // Pide a la base de datos los cantones que presentan el nombre de la
            // provincia indicada
            var listaCantones = await this.contexto.Cantones
                .Where(c => c.nombreProvincia == provincia)
                .ToListAsync();

            return new JsonResult(listaCantones);
        }

        public async Task<IActionResult> OnGetDistritosPorCanton(string provincia, string canton)
        {
            // Pide a la base de datos los distritos que presentan el nombre del
            // provincia y canton indicados
            var listaDistritos = await this.contexto.Distritos
                .Where(d => d.nombreProvincia == provincia && d.nombreCanton == canton)
                .ToListAsync();

            return new JsonResult(listaDistritos);
        }

        public async Task<IActionResult> OnPostActualizarUsuario()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                // Obtiene la instancia del usuario
                this.usuario = this.contexto.Usuarios.FirstOrDefault(
                    p => p.nombreDeUsuario == User.Identity.Name) ?? this.usuario;

                
                if (ModelState.IsValid)
                {
                    // Actualiza el usuario con los datos del modelo vista
                    this.usuario.provinciaVivienda = this.usuarioActual.provinciaVivienda ?? "";
                    this.usuario.cantonVivienda = this.usuarioActual.cantonVivienda ?? "";
                    this.usuario.distritoVivienda = this.usuarioActual.distritoVivienda ?? "";

                    // Crear un cliente para consultar las coordenadas a un API
                    using (HttpClient cliente = new HttpClient())
                    {
                        // Consultar a la API las coordenadas de la ubicación del usuario
                        string apiURL = Localizador.ObtenerUrlLocalizacion(this.usuario.provinciaVivienda, this.usuario.cantonVivienda, this.usuario.distritoVivienda);
                        var (latitud, longitud) = await Localizador.ObtenerCoordenadas(cliente, apiURL);

                        if (latitud != 0 && longitud != 0)
                        {
                            this.usuario.latitudVivienda = latitud;
                            this.usuario.longitudVivienda = longitud;
                            this.contexto.SaveChanges();
                        }
                        else
                        {
                            // Guarda el error para mostrarlo en la página principal
                            TempData["ErrorCambiarUsuario"] = "Hubieron problemas al procesar su ubicación. Inténtelo más tarde";
                        }
                    }
                    

                    // Si el nombre de usuario cambió
                    if (this.usuario.nombreDeUsuario != this.usuarioActual.nombreDeUsuario &&
                        this.usuarioActual.nombreDeUsuario != null)
                    {
                        Usuario? nuevoUsuario = this.contexto.Usuarios.FirstOrDefault(
                            p => p.nombreDeUsuario == this.usuarioActual.nombreDeUsuario);
                        if (nuevoUsuario == null)
                        {
                            // Llamar al procedimiento para cambiar el nombre de usuario
                            ControladorComandosSql controlador = new ControladorComandosSql();
                            controlador.ConfigurarNombreComando("cambiarNombreUsuario");
                            controlador.ConfigurarParametroComando("anteriorNombre", this.usuario.nombreDeUsuario);
                            controlador.ConfigurarParametroComando("nuevoNombre", this.usuarioActual.nombreDeUsuario);
                            controlador.EjecutarProcedimiento();


                            // Reinicia la sesión para actualizar los claims
                            await this.reiniciarSesion();

                        } else
                        {
                            // Guarda el error para mostrarlo en la página principal
                            TempData["ErrorCambiarUsuario"] = "Usuario ya existente en el sistema";
                        }
                    }
                }
            }
            
            return RedirectToPage("/Cuenta/Perfil");
        }

        private async Task reiniciarSesion()
        {
            // Cierra la swsión
            await this.cerrarSesion();
            // Vuelve a iniciar la sesión
            await this.iniciarSesion();
        }

        private async Task cerrarSesion()
        {
            // Remueve la información guardada en la sesión
            HttpContext.Session.Remove("NombreDeUsuario");
            // Limpia la sesión
            HttpContext.Session.Clear();
            // Cierra sesión
            await HttpContext.SignOutAsync();
        }

        private async Task iniciarSesion()
        {
            // Obtiene el usuario de la base de datos
            var usuarioEncontrado = this.contexto.Usuarios.FirstOrDefault(
                    u => u.nombreDeUsuario == this.usuarioActual.nombreDeUsuario);

            // Si encuentra al usuario
            if (usuarioEncontrado != null)
            {
                // Guarda la información del nombre de usuario en la sesión actual
                HttpContext.Session.SetString("NombreDeUsuario", usuarioEncontrado.nombreDeUsuario);

                // Establece los Claims para guardar los datos del usuario en las páginas
                var claims = new List<Claim>
                {
                    // Crea un claim con el nombre de usuario
                    new Claim(ClaimTypes.Name, usuarioEncontrado.nombreDeUsuario),
                    // Crea un claim con el rol del usuario
                    new Claim(ClaimTypes.Role, usuarioEncontrado.esModerador? "moderador":"regular")
                };

                // Agrega los claims a la autentificación con cookies
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Inicia sesión en el contexto http con los nuevos claims
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(
                            this.configuracion.GetValue<int>("minutosTimeout"))
                    });
            }
        }
    }
}
