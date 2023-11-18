using LoCoMPro.Data;
using LoCoMPro.ViewModels.Cuenta;
using LoCoMPro.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Humanizer;
using System.Runtime.CompilerServices;

namespace LoCoMPro.Pages.Cuenta
{
    public class ModeloContrasena : PageModel
    {

        // Contexto para interactuar con la base de datos
        private readonly LoCoMProContext contexto;

        [BindProperty]
        public CambiarContraseñaVM usuarioActual { get; set; }


        private readonly PasswordHasher<Usuario> hasheador;


        public ModeloContrasena(LoCoMProContext contexto)
        {
            this.contexto = contexto;
            this.hasheador = new PasswordHasher<Usuario>();
            // Crea un ModificarUsuarioVM con datos vacíos para no tener nulo
            this.usuarioActual = new CambiarContraseñaVM
            {
                nombreDeUsuario = "",
                contrasenaActual = "+",
                contrasenaNueva = "+",
                confirmarContrasena = "+",
            };
            this.hasheador = new PasswordHasher<Usuario>();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public void OnPostAceptar()
        {
            this.usuarioActual.nombreDeUsuario = User.Identity?.Name ?? "desconocido";

            if (ModelState.IsValid)
            {
                // Buscar usuario con el nombre de usaurio
                var usuario = this.contexto.Usuarios.FirstOrDefault(
                    u => u.nombreDeUsuario == usuarioActual.nombreDeUsuario);

                // Verificar la contraseña
                if (usuario != null && usuario.estado == 'A' &&
                   this.hasheador.VerifyHashedPassword(
                       usuario, usuario.hashContrasena, this.usuarioActual.contrasenaActual) ==
                       PasswordVerificationResult.Success)
                {
                    if (this.usuarioActual.contrasenaNueva == this.usuarioActual.confirmarContrasena) 
                    {
                        // Cambiar la contraseña del usuario por la indicada
                        usuario.hashContrasena = this.hasheador.HashPassword(usuario, this.usuarioActual.contrasenaNueva);
                        this.contexto.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Las contraseñas no son iguales.");

                        Console.WriteLine("Son diferentes");
                    }
                } 
                else
                {
                    ModelState.AddModelError(string.Empty, "Contraseña actual incorrecta.");

                    Console.WriteLine("Mala contraseña");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Los datos brindados no son correctos. Recuerde que la contraseña debe contener al menos: una minúscula, una mayúscula, un dígito y un carácter especial. Además, debe estar entre 8 y 20 caracteres.");
                Console.WriteLine("No cumple lo que se pide");
            }
            // return Page();
        }
    }
}
