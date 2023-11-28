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

        private readonly LoCoMProContext contexto;

        [BindProperty]
        public CambiarContrasenaVM usuarioActual { get; set; }


        private readonly PasswordHasher<Usuario> hasheador;


        public ModeloContrasena(LoCoMProContext contexto, string nombreUsuario = "", string actual = "+", string nueva = "+", string confirmar = "+")
        {
            this.contexto = contexto;
            this.hasheador = new PasswordHasher<Usuario>();
            this.usuarioActual = new CambiarContrasenaVM
            {
                nombreDeUsuario = nombreUsuario,
                contrasenaActual = actual,
                contrasenaNueva = nueva,
                confirmarContrasena = confirmar,
            };
        }

        public int cambiarContrasena()
        {
            if (ModelState.IsValid && this.usuarioActual.contrasenaNueva != "" && usuarioActual.confirmarContrasena != "")
            {
                // Buscar usuario con el nombre de usaurio
                var usuario = this.contexto.Usuarios.FirstOrDefault(
                u => u.nombreDeUsuario == usuarioActual.nombreDeUsuario);

                // Verificar la contrase�a
                if (usuario != null && usuario.estado == 'A' && usuario.nombreDeUsuario != "")
                {

                    if (this.hasheador.VerifyHashedPassword(
                       usuario, usuario.hashContrasena, this.usuarioActual.contrasenaActual) ==
                       PasswordVerificationResult.Success)
                    {
                        if (this.usuarioActual.contrasenaNueva == this.usuarioActual.confirmarContrasena)
                        {
                            // Cambiar la contrase�a del usuario por la indicada
                            usuario.hashContrasena = this.hasheador.HashPassword(usuario, this.usuarioActual.contrasenaNueva);
                            this.contexto.SaveChanges();
                            return 0;
                        }
                        else
                        {
                            // Datos nuevos inv�lidos
                            return 1;
                        }
                    }
                    else
                    {
                        // Datos actuales inv�lidos
                        return 2;
                    }
                }
                else
                {
                    // Problema interno con el usuario
                    return 3;
                }
            }
            else
            {
                // Formato inv�lido
                return 4;
            }
        }

        public void OnPostAceptar()
        {
            this.usuarioActual.nombreDeUsuario = User.Identity?.Name ?? "desconocido";
            int resultado = cambiarContrasena();
            if (resultado == 0)
            {
                // Indicar que la transferencia fue exitosa
                TempData["ExitoContrasena"] = "Contrase�a cambiada exitosamente.";
            }
            else
            {
                if (resultado == 1)
                {
                    ModelState.AddModelError(string.Empty, "Las contrase�as no son iguales.");
                }
                else
                {
                    if (resultado == 2)
                    {
                        ModelState.AddModelError(string.Empty, "Contrase�a actual incorrecta.");
                    }
                    else
                    {
                        if (resultado == 3)
                        {
                            ModelState.AddModelError(string.Empty, "Hubo un problema con el sistema. Favor intentar m�s tarde.");

                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Los datos brindados no son correctos. Recuerde que la contrase�a debe contener al menos: una min�scula, una may�scula, un d�gito y un car�cter especial. Adem�s, debe estar entre 8 y 20 caracteres.");
                        }
                    }
                }
            }

        }
    }
}
