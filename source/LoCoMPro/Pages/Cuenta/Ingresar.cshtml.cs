using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Cuenta;
using LoCoMPro.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace LoCoMPro.Pages.Cuenta
{
    // Clase modelo para manejar la p�gina de ingresar con una cuenta
    public class ModeloIngresar : PageModel
    {
        // Contexto para interactuar con la base de datos
        private readonly LoCoMProContext contexto;

        // Instancia de configuraci�n basada en el archivo "appsettings.json"
        // Se utiliza para establecer el timeout de ingreso a la cuenta
        private readonly IConfiguration configuracion = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

        // Propiedad para hashear contrase�as
        private readonly PasswordHasher<Usuario> hasheador;

        // Propiedad ligada para validaciones con el modelo de vista
        // (Ingresar presenta un modelo vista dado que �nicamente
        // requer�a dos verificaci�n de dos propiedades y una deb�a
        // ser una contrase�a, no un Hash)
        [BindProperty]
        public IngresarUsuarioVM usuarioActual { get; set; }

        // Constructor del modelo de la p�gina
        public ModeloIngresar(LoCoMProContext contexto)
        {
            // Obtiene el contexto
            this.contexto = contexto;
            // Crea un hasheador de contrase�as
            this.hasheador = new PasswordHasher<Usuario>();
            // Crea un IngresarUsuarioVM dummy para no tener nulo
            this.usuarioActual = new IngresarUsuarioVM
            {
                nombreDeUsuario = "",
                contrasena = "+"
            };
        }

        // M�todo para manejar los GET http requests de la p�gina
        public IActionResult OnGet()
        {
            // Retorna la p�gina
            return Page();
        }

        // M�todo cuando se presiona el bot�n de ingresar
        public async Task<IActionResult> OnPostIngresar()
        {
             if (ModelState.IsValid)
            {
                // Si los datos recopilados son v�lidos (cumplen con las propiedades de
                // validaci�n del modelo de vista del lado del servidor)
                // Busca al usuario en la base de datos
                var usuario = this.contexto.Usuarios.FirstOrDefault(
                    u => u.nombreDeUsuario == usuarioActual.nombreDeUsuario);
                // Si lo encuentra, est� activo y tiene el mismo hash de contrase�a
                if (usuario != null && usuario.estado == 'A' &&
                    this.hasheador.VerifyHashedPassword(
                        usuario, usuario.hashContrasena, this.usuarioActual.contrasena) ==
                        PasswordVerificationResult.Success)
                {
                    // Guarda la informaci�n del nombre de usuario en la sesi�n actual
                    HttpContext.Session.SetString("NombreDeUsuario", usuario.nombreDeUsuario);

                    // Establece los Claims para mapear los datos del usuario al elemento HTML
                    // para poder utilizar User.Identity y verificar usuarios ingresados
                    // Crea un claim con el nombre de usuario
                    // (No se agregaron m�s porque se pueden obtener de la base, pero es posible)
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.nombreDeUsuario)
                    };
                    // Agrega los claims a la autentificaci�n con cookies
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    // Inicia sesi�n en el contexto http con los claims
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(
                                this.configuracion.GetValue<int>("minutosTimeout"))
                        });

                    // Redirecciona a la p�gina home
                    return RedirectToPage("/Home/Index");
                }
                else
                {
                    // Muestra error por no cumplir requerimientos del View Model
                    ModelState.AddModelError(string.Empty, "Credenciales inv�lidas, int�ntelo de nuevo");
                }
            }
            else
            {
                // Muestra error por no cumplir requerimientos del View Model
                ModelState.AddModelError(string.Empty, "Credenciales inv�lidas, int�ntelo de nuevo");
            }

            // Recarga la misma p�gina
            return Page();
        }
    }
}
