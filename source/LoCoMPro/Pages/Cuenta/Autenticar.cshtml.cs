using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.Data;
using LoCoMPro.Utils;
using System.Web;

namespace LoCoMPro.Pages.Cuenta
{
    // Clase modelo para manejar la página de registrar cuenta
    public class ModeloAutenticar : PageModel
    {
        // Contexto para interactuar con la base de datos
        private readonly LoCoMProContext contexto;
        // Propiedad para encriptar usuarios
        private Encriptador encriptador { get; set; }
        // Booleano para indicar si el usuario con el que
        // se ingresó a la página se acaba de autenticar
        public bool seAutentico { get; set; }

        // Constructor del modelo de la página
        public ModeloAutenticar(LoCoMProContext contexto)
        {
            // Obtiene el contexto
            this.contexto = contexto;
            // Crea un nuevo encriptador
            this.encriptador = new Encriptador();
            // Asume que el usuario ya
            // estaba autenticado
            this.seAutentico = false;
        }

        // Método para manejar los GET http requests de la página
        public IActionResult OnGet(string codigoUsuario)
        {
            // Obtiene el usuario de la base de datos a partir
            // del nombre de usuario dado desencriptado
            var usuario = this.contexto.Usuarios.FirstOrDefault(
              u => u.nombreDeUsuario == this.encriptador.desencriptar(HttpUtility.UrlDecode(codigoUsuario)));

            // Si el usuario existe
            if (usuario != null)
            {
                // Si el usuario estaba inactivo
                if (usuario.estado == 'I')
                {
                    // Lo activa
                    usuario.estado = 'A';
                    // Guarda los cambios en la base de datos
                    this.contexto.SaveChanges();
                    // Se actualiza el booleano indicando
                    // que se acaba de actualizar
                    this.seAutentico = true;
                }
                // Muestra la página
                return Page();
            }

            // Se actualiza el booleano indicando
            // que no se acaba de actualizar
            this.seAutentico = false;
            // Retorna a la página de error
            return RedirectToPage("/Error");
        }
    }
}
