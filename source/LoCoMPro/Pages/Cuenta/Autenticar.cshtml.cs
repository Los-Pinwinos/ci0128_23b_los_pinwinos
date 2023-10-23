using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoCoMPro.Data;

namespace LoCoMPro.Pages.Cuenta
{
    // Clase modelo para manejar la p�gina de registrar cuenta
    public class ModeloAutenticar : PageModel
    {
        // Contexto para interactuar con la base de datos
        private readonly LoCoMProContext contexto;
        // Booleano para indicar si el usuario con el que
        // se ingres� a la p�gina se acaba de autenticar
        public bool seAutentico { get; set; }

        // Constructor del modelo de la p�gina
        public ModeloAutenticar(LoCoMProContext contexto)
        {
            // Obtiene el contexto
            this.contexto = contexto;
            // Asume que el usuario ya
            // estaba autenticado
            this.seAutentico = false;
        }

        // M�todo para manejar los GET http requests de la p�gina
        public IActionResult OnGet(string nombreUsuario)
        {
            // Obtiene el usuario de la base de datos
            var usuario = this.contexto.Usuarios.FirstOrDefault(
                    u => u.nombreDeUsuario == nombreUsuario);

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
                // Muestra la p�gina
                return Page();
            }

            // Se actualiza el booleano indicando
            // que no se acaba de actualizar
            this.seAutentico = false;
            // Retorna a la p�gina de error
            return RedirectToPage("/Error");
        }
    }
}
