using LoCoMPro.Models;
using LoCoMPro.ViewModels.AgregarProducto;
using LoCoMPro.ViewModels.Tienda;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Pages.AgregarProducto
{
    public class AgregarProdModel : PageModel
    {
        private readonly LoCoMPro.Data.LoCoMProContext contexto;

        // ViewModel de la página
        [BindProperty]
        public AgregarProdVM viewModel { get; set; }

        // Utilizados para cargar el combobox de unidad y categoría
        public SelectList opcionesUnidad { get; set; }
        public SelectList opcionesCategoria { get; set; }

        // Constructor del modelo de la página
        public AgregarProdModel(LoCoMPro.Data.LoCoMProContext contexto)
        {
            // Establece el contexto
            this.contexto = contexto;
            // Establece el registro
            viewModel = new AgregarProdVM
            {
                nombreProducto = "",
                marcaProducto = "",
                nombreUnidad = "",
                nombreCategoria = "",
                descripcion = "",
                etiqueta = "",
                precio = "",
            };
            // Inserta categorías en combobox
            var listaCategoria = new List<string>();
            // Crea una lista del nombre de todas las categorias
            foreach (var entradaUnidad in contexto.Categorias)
            {
                listaCategoria.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            opcionesCategoria = new SelectList(listaCategoria);
            // Inserta unidades en combobox
            var listaUnidad = new List<string>();
            // Crea una lista del nombre de todas las unidades
            foreach (var entradaUnidad in contexto.Unidades)
            {
                listaUnidad.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            opcionesUnidad = new SelectList(listaUnidad);
        }
        public IActionResult OnGet()
        {
            // Revisar si el usuario está loggeado
            if (User.Identity == null || !User.Identity.IsAuthenticated) {
                ViewData["RedirectMessage"] = "usuario";
            }
            else if (!TempData.ContainsKey("nombreTienda")) {
                ViewData["RedirectMessage"] = "tienda";
            }
            // Inserta categorías en combobox
            var listaCategoria = new List<string>();
            // Crea una lista del nombre de todas las categorias
            foreach (var entradaUnidad in contexto.Categorias)
            {
                listaCategoria.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            opcionesCategoria = new SelectList(listaCategoria);

            // Inserta unidades en combobox
            var listaUnidad = new List<string>();
            // Crea una lista del nombre de todas las unidades
            foreach (var entradaUnidad in contexto.Unidades)
            {
                listaUnidad.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            opcionesUnidad = new SelectList(listaUnidad);
            return Page();
        }
        public IActionResult OnPostAceptar()
        {
            string usuarioCreador = User.Identity?.Name ?? "desconocido";
            // Verificar si el producto ya existe en la base de datos
            var existingProduct = contexto.Productos.FirstOrDefault(p => p.nombre == viewModel.nombreProducto);
            // Si el producto no existía, agréguelo
            if (existingProduct == null)
            {
                // El producto no existe, agrégalo a la base de datos
                var nuevoProducto = new Producto
                {
                    nombre = viewModel.nombreProducto,
                    marca = viewModel.marcaProducto,
                    nombreUnidad = viewModel.nombreUnidad,
                    nombreCategoria = viewModel.nombreCategoria
                };

                // Agrega el nuevo producto a la base de datos
                contexto.Productos.Add(nuevoProducto);
                contexto.SaveChanges();
            }
            // Revisar si tiene tienda
            string tiendaTemporal = TempData["nombreTienda"]?.ToString() ?? "";
            if (tiendaTemporal == "")
            {
                ViewData["ErrorMessage"] = "tienda";
            }
            else
            {
                // Agregarle registros al producto encontrado
                var nuevoRegistro = new Registro
                {
                    // Indicar el tiempo de creación
                    creacion = DateTime.Now,
                    usuarioCreador = usuarioCreador,
                    descripcion = viewModel.descripcion,
                    // Convertir a decimal
                    precio = decimal.Parse(viewModel.precio),
                    productoAsociado = viewModel.nombreProducto,                    
                    nombreTienda = tiendaTemporal,
                    nombreDistrito = TempData["distritoTienda"]?.ToString() ?? "",
                    nombreCanton = TempData["cantonTienda"]?.ToString() ?? "",
                    nombreProvincia = TempData["provinciaTienda"]?.ToString() ?? ""
                };
                // Agregar el nuevo registro a la base de datos
                contexto.Registros.Add(nuevoRegistro);
                contexto.SaveChanges();
            }
            RellenarSelectList();
            // Limpia los datos del view model
            LimpiarViewModel();
            // Redirige a otra página o realiza cualquier otra acción después de agregar el producto
            return RedirectToPage("/Home/Index");
        }

        public IActionResult OnPostCancelar()
        {
            RellenarSelectList();
            // Limpia los datos del view model
            LimpiarViewModel();
            // Dirigir a la página de inicio
            return RedirectToPage("/Home/Index");
        }

        public void RellenarSelectList()
        {
            // Inserta categorías en combobox
            var listaCategoria = new List<string>();
            // Crea una lista del nombre de todas las categorias
            foreach (var entradaUnidad in contexto.Categorias)
            {
                listaCategoria.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            opcionesCategoria = new SelectList(listaCategoria);

            // Inserta unidades en combobox
            var listaUnidad = new List<string>();
            // Crea una lista del nombre de todas las unidades
            foreach (var entradaUnidad in contexto.Unidades)
            {
                listaUnidad.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            opcionesUnidad = new SelectList(listaUnidad);
        }
        private void LimpiarViewModel()
        {
            viewModel.nombreProducto = "";
            viewModel.marcaProducto = "";
            viewModel.nombreUnidad = "";
            viewModel.nombreCategoria = "";
            viewModel.descripcion = "";
            viewModel.etiqueta = "";
            viewModel.precio = "";
        }
    }
}