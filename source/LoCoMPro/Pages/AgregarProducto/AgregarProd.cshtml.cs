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
        public AgregarProdVM ViewModel { get; set; }

        // Utilizados para cargar el combobox de unidad y categoría
        public SelectList OpcionesUnidad { get; set; }
        public SelectList OpcionesCategoria { get; set; }
        
        public AgregarProdModel(LoCoMPro.Data.LoCoMProContext contexto)
        {
            this.contexto = contexto;
            this.ViewModel = new AgregarProdVM
            {
                nombreProducto = "",
                marcaProducto = "",
                nombreUnidad = "",
                nombreCategoria = "",
                descripcion = "",
                etiqueta = "",
                precio = "",
            };
        }

        public IActionResult OnGet()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated) {
                // Establece mensaje para redireccionar si el usuario no está ingresado
                // en el sistema
                ViewData["RedirectMessage"] = "usuario";
            }
            else if (!TempData.ContainsKey("nombreTienda")) {
                // Establece mensaje para redireccionar si el usuario no escogió una tienda
                ViewData["RedirectMessage"] = "tienda";
            } else
            {
                // Rellena las select list de unidad y categoría
                this.RellenarSelectList();
            }
            return Page();
        }

        public IActionResult OnPostAceptar()
        {
            string usuarioCreador = User.Identity?.Name ?? "desconocido";
            // Verificar si el producto ya existe en la base de datos
            var existingProduct = this.contexto.Productos.FirstOrDefault(p => p.nombre == this.ViewModel.nombreProducto);
            // Si el producto no existía, se agrega
            if (existingProduct == null)
            {
                this.AgregarProducto();
            }
            // Revisar si tiene tienda
            string tiendaTemporal = TempData["nombreTienda"]?.ToString() ?? "";
            if (tiendaTemporal == "")
            {
                // Enviar error de tipo tienda
                ViewData["ErrorMessage"] = "tienda";
            }
            else
            {
                // Insertar registro a la base de datos y obtener su tiempo
                var tiempoActual = this.AgregarRegistro(usuarioCreador, tiendaTemporal);
                // Agregar fotografías a la base de datos
                this.AgregarFotografias(usuarioCreador, tiempoActual);
            }
            this.RellenarSelectList();
            this.LimpiarViewModel();
            return RedirectToPage("/Home/Index");
        }

        private void AgregarProducto()
        {
            var nuevoProducto = new Producto
            {
                nombre = this.ViewModel.nombreProducto,
                marca = this.ViewModel.marcaProducto,
                nombreUnidad = this.ViewModel.nombreUnidad,
                nombreCategoria = this.ViewModel.nombreCategoria
            };
            this.contexto.Productos.Add(nuevoProducto);
            this.contexto.SaveChanges();
        }

        private DateTime AgregarRegistro(string usuarioCreador, string tiendaTemporal)
        {
            var tiempoActual = DateTime.Now;
            var nuevoRegistro = new Registro
            {
                creacion = tiempoActual,
                usuarioCreador = usuarioCreador,
                descripcion = this.ViewModel.descripcion,
                precio = decimal.Parse(this.ViewModel.precio),
                calificacion = 0,
                productoAsociado = this.ViewModel.nombreProducto,
                nombreTienda = tiendaTemporal,
                nombreDistrito = TempData["distritoTienda"]?.ToString() ?? "",
                nombreCanton = TempData["cantonTienda"]?.ToString() ?? "",
                nombreProvincia = TempData["provinciaTienda"]?.ToString() ?? ""
            };
            contexto.Registros.Add(nuevoRegistro);
            contexto.SaveChanges();
            // Requerido para crear fotografías, debido a que es parte de la
            // llave primaria del registro
            return tiempoActual;
        }

        private void AgregarFotografias(string usuarioCreador, DateTime tiempoActual)
        {
            foreach (var archivo in Request.Form.Files)
            {
                string nombreArchivo = archivo.FileName;
                MemoryStream memoriaTemporal = new MemoryStream();
                archivo.CopyTo(memoriaTemporal);
                var fotografia = new Fotografia
                {
                    fotografia = memoriaTemporal.ToArray(),
                    nombreFotografia = nombreArchivo,
                    creacion = tiempoActual,
                    usuarioCreador = usuarioCreador
                };
                // Limpiar memoria temporal
                memoriaTemporal.Close();
                memoriaTemporal.Dispose();
                contexto.Fotografias.Add(fotografia);
                contexto.SaveChanges();
            }
        }

        public IActionResult OnPostCancelar()
        {
            this.RellenarSelectList();
            this.LimpiarViewModel();
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
            this.OpcionesCategoria = new SelectList(listaCategoria);
            var listaUnidad = new List<string>();
            // Crea una lista del nombre de todas las unidades
            foreach (var entradaUnidad in contexto.Unidades)
            {
                listaUnidad.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            this.OpcionesUnidad = new SelectList(listaUnidad);
        }

        private void LimpiarViewModel()
        {
            this.ViewModel.nombreProducto = "";
            this.ViewModel.marcaProducto = "";
            this.ViewModel.nombreUnidad = "";
            this.ViewModel.nombreCategoria = "";
            this.ViewModel.descripcion = "";
            this.ViewModel.etiqueta = "";
            this.ViewModel.precio = "";
        }
    }
}