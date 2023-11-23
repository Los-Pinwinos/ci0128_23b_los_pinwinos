using LoCoMPro.Data;
using LoCoMPro.Utils.Buscadores;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.ViewModels.Cuenta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using LoCoMPro.Models;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Pages.Cuenta
{
    public class ModeloFavoritos : PageModel
    {
        protected readonly LoCoMProContext contexto;
        protected readonly IConfiguration configuracion;

        // Constructor
        public ModeloFavoritos(LoCoMProContext contexto, IConfiguration configuracion)
        {
            this.contexto = contexto;
            this.configuracion = configuracion;
            // Inicializar
            this.Inicializar();
        }

        public string? favoritos { get; set; }
        public FavoritoVM favoritoVM { get; set; } = default!;

        // Paginación
        public int paginaDefault { get; set; }
        public int resultadosPorPagina { get; set; }

        // Inicializar atributos
        public void Inicializar()
        {

            this.favoritoVM = new FavoritoVM
            {
                nombreProducto = "",
                nombreCategoria = "",
                nombreMarca = ""
            };
            this.paginaDefault = 1;
            this.resultadosPorPagina = this.configuracion.GetValue("TamPaginaCuenta", 10);
        }

        public void OnGetRemoverDeFavoritos(string nombreProducto)
        {

            // Revisar que el usuario esté loggeado
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                Usuario? usuario = contexto.Usuarios.Include(u => u.favoritos).FirstOrDefault(u => u.nombreDeUsuario == User.Identity.Name);
                if (usuario != null)
                {
                    // Obtener el producto de la base de datos
                    Producto? producto = contexto.Productos.FirstOrDefault(p => p.nombre == nombreProducto);
                    if (producto != null)
                    {
                        usuario.favoritos.Remove(producto);
                        contexto.SaveChanges();
                    }
                }
            }
        }

        // Método On get para cargar los resultados o redireccionar
        public IActionResult OnGet()
        {
            // Si el usuario no está loggeado
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                // Establece mensaje para redireccionar
                ViewData["MensajeRedireccion"] = "Por favor ingrese al sistema.";
            }
            else
            {
                // Configurar buscador
                IBuscador<FavoritoVM> buscador = new BuscadorDeFavoritos(this.contexto, User.Identity.Name);
                // Consultar la base de datos
                IQueryable<FavoritoVM> busqueda = buscador.buscar();

                // Si la busqueda tuvo resultados
                List<FavoritoVM> resultados = busqueda.ToList();
                if (resultados.Count != 0)
                {
                    // Asignar data de JSON
                    this.favoritos = JsonConvert.SerializeObject(resultados);
                }
                else
                {
                    this.favoritos = "Sin resultados";
                }
            }
            return Page();
        }
    }
}
