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
            this.viewModel = new AgregarProdVM
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
            foreach (var entradaUnidad in this.contexto.Categorias)
            {
                listaCategoria.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            this.opcionesCategoria = new SelectList(listaCategoria);
            // Inserta unidades en combobox
            var listaUnidad = new List<string>();
            // Crea una lista del nombre de todas las unidades
            foreach (var entradaUnidad in this.contexto.Unidades)
            {
                listaUnidad.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            this.opcionesUnidad = new SelectList(listaUnidad);
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
            foreach (var entradaUnidad in this.contexto.Categorias)
            {
                listaCategoria.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            this.opcionesCategoria = new SelectList(listaCategoria);

            // Inserta unidades en combobox
            var listaUnidad = new List<string>();
            // Crea una lista del nombre de todas las unidades
            foreach (var entradaUnidad in this.contexto.Unidades)
            {
                listaUnidad.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            this.opcionesUnidad = new SelectList(listaUnidad);
            return Page();
        }
        public IActionResult OnPostAceptar()
        {
            string usuarioCreador = User.Identity?.Name ?? "desconocido";
            // Verificar si el producto ya existe en la base de datos
            var existingProduct = this.contexto.Productos.FirstOrDefault(p => p.nombre == this.viewModel.nombreProducto);
            // Si el producto no existía, agréguelo
            if (existingProduct == null)
            {
                // El producto no existe, agrégalo a la base de datos
                this.agregarProducto();
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
                var tiempoActual = this.agregarRegistro(usuarioCreador, tiendaTemporal);
                // Agregar fotografías a la base de datos
                this.agregarFotografias(usuarioCreador, tiempoActual);
            }
            this.RellenarSelectList();
            // Limpia los datos del view model
            this.LimpiarViewModel();
            // Redirige a otra página o realiza cualquier otra acción después de agregar el producto
            return RedirectToPage("/Home/Index");
        }

        private void agregarProducto()
        {
            var nuevoProducto = new Producto
            {
                nombre = this.viewModel.nombreProducto,
                marca = this.viewModel.marcaProducto,
                nombreUnidad = this.viewModel.nombreUnidad,
                nombreCategoria = this.viewModel.nombreCategoria
            };

            // Agrega el nuevo producto a la base de datos
            this.contexto.Productos.Add(nuevoProducto);
            this.contexto.SaveChanges();
        }

        private DateTime agregarRegistro(string usuarioCreador, string tiendaTemporal)
        {
            var tiempoActual = DateTime.Now;
            var nuevoRegistro = new Registro
            {
                // Indicar el tiempo de creación
                creacion = tiempoActual,
                usuarioCreador = usuarioCreador,
                descripcion = this.viewModel.descripcion,
                // Convertir a decimal
                precio = decimal.Parse(this.viewModel.precio),
                calificacion = null,
                productoAsociado = this.viewModel.nombreProducto,
                nombreTienda = tiendaTemporal,
                nombreDistrito = TempData["distritoTienda"]?.ToString() ?? "",
                nombreCanton = TempData["cantonTienda"]?.ToString() ?? "",
                nombreProvincia = TempData["provinciaTienda"]?.ToString() ?? ""
            };
            // Agregar el nuevo registro a la base de datos
            contexto.Registros.Add(nuevoRegistro);
            contexto.SaveChanges();
            // Requerido para asociar imágenes al registro
            return tiempoActual;
        }

        private void agregarFotografias(string usuarioCreador, DateTime tiempoActual)
        {
            foreach (var archivo in Request.Form.Files)
            {
                // Obtener el nombre del archivo
                string nombreArchivo = archivo.FileName;
                // Crear memoria temporal para transformar la imágen
                MemoryStream memoriaTemporal = new MemoryStream();
                // Copiar del archivo a la memoria temporal
                archivo.CopyTo(memoriaTemporal);
                // Crear instancia de fotografía
                var fotografia = new Fotografia
                {
                    fotografia = memoriaTemporal.ToArray(),
                    nombreFotografia = nombreArchivo,
                    // Creación del registro asociado
                    creacion = tiempoActual,
                    // Usuario que agregó la imagen
                    usuarioCreador = usuarioCreador
                };
                // Limpiar memoria temporal
                memoriaTemporal.Close();
                memoriaTemporal.Dispose();
                // Agregar fotografía a base de datos
                contexto.Fotografias.Add(fotografia);
                contexto.SaveChanges();
            }
        }

        public IActionResult OnPostCancelar()
        {
            this.RellenarSelectList();
            // Limpia los datos del view model
            this.LimpiarViewModel();
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
            this.opcionesCategoria = new SelectList(listaCategoria);

            // Inserta unidades en combobox
            var listaUnidad = new List<string>();
            // Crea una lista del nombre de todas las unidades
            foreach (var entradaUnidad in contexto.Unidades)
            {
                listaUnidad.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            this.opcionesUnidad = new SelectList(listaUnidad);
        }
        private void LimpiarViewModel()
        {
            this.viewModel.nombreProducto = "";
            this.viewModel.marcaProducto = "";
            this.viewModel.nombreUnidad = "";
            this.viewModel.nombreCategoria = "";
            this.viewModel.descripcion = "";
            this.viewModel.etiqueta = "";
            this.viewModel.precio = "";
        }
    }
}