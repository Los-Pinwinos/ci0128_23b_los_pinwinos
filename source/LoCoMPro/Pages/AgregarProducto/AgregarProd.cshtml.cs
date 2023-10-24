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

        // ViewModel de la p�gina
        [BindProperty]
        public AgregarProdVM viewModel { get; set; }

        // Utilizados para cargar el combobox de unidad y categor�a
        public SelectList opcionesUnidad { get; set; }
        public SelectList opcionesCategoria { get; set; }

        // Constructor del modelo de la p�gina
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
            // Inserta categor�as en combobox
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
            // Revisar si el usuario est� loggeado
            if (User.Identity == null || !User.Identity.IsAuthenticated) {
                ViewData["RedirectMessage"] = "usuario";
            }
            else if (!TempData.ContainsKey("nombreTienda")) {
                ViewData["RedirectMessage"] = "tienda";
            }
            // Inserta categor�as en combobox
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
            // Si el producto no exist�a, agr�guelo
            if (existingProduct == null)
            {
                // El producto no existe, agr�galo a la base de datos
                agregarProducto();
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
                var tiempoActual = agregarRegistro(usuarioCreador, tiendaTemporal);
                // Agregar fotograf�as a la base de datos
                agregarFotografias(usuarioCreador, tiempoActual);
            }
            RellenarSelectList();
            // Limpia los datos del view model
            LimpiarViewModel();
            // Redirige a otra p�gina o realiza cualquier otra acci�n despu�s de agregar el producto
            return RedirectToPage("/Home/Index");
        }

        private void agregarProducto()
        {
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

        private DateTime agregarRegistro(string usuarioCreador, string tiendaTemporal)
        {
            var tiempoActual = DateTime.Now;
            var nuevoRegistro = new Registro
            {
                // Indicar el tiempo de creaci�n
                creacion = tiempoActual,
                usuarioCreador = usuarioCreador,
                descripcion = viewModel.descripcion,
                // Convertir a decimal
                precio = decimal.Parse(viewModel.precio),
                calificacion = null,
                productoAsociado = viewModel.nombreProducto,
                nombreTienda = tiendaTemporal,
                nombreDistrito = TempData["distritoTienda"]?.ToString() ?? "",
                nombreCanton = TempData["cantonTienda"]?.ToString() ?? "",
                nombreProvincia = TempData["provinciaTienda"]?.ToString() ?? ""
            };
            // Agregar el nuevo registro a la base de datos
            contexto.Registros.Add(nuevoRegistro);
            contexto.SaveChanges();
            // Requerido para asociar im�genes al registro
            return tiempoActual;
        }

        private void agregarFotografias(string usuarioCreador, DateTime tiempoActual)
        {
            foreach (var archivo in Request.Form.Files)
            {
                // Obtener el nombre del archivo
                string nombreArchivo = archivo.FileName;
                // Crear memoria temporal para transformar la im�gen
                MemoryStream memoriaTemporal = new MemoryStream();
                // Copiar del archivo a la memoria temporal
                archivo.CopyTo(memoriaTemporal);
                // Crear instancia de fotograf�a
                var fotografia = new Fotografia
                {
                    fotografia = memoriaTemporal.ToArray(),
                    nombreFotografia = nombreArchivo,
                    // Creaci�n del registro asociado
                    creacion = tiempoActual,
                    // Usuario que agreg� la imagen
                    usuarioCreador = usuarioCreador
                };
                // Limpiar memoria temporal
                memoriaTemporal.Close();
                memoriaTemporal.Dispose();
                // Agregar fotograf�a a base de datos
                contexto.Fotografias.Add(fotografia);
                contexto.SaveChanges();
            }
        }

        public IActionResult OnPostCancelar()
        {
            RellenarSelectList();
            // Limpia los datos del view model
            LimpiarViewModel();
            // Dirigir a la p�gina de inicio
            return RedirectToPage("/Home/Index");
        }

        public void RellenarSelectList()
        {
            // Inserta categor�as en combobox
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