using LoCoMPro.Models;
using LoCoMPro.ViewModels.AgregarProducto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Pages.AgregarProducto
{
    public class AgregarProdModel : PageModel
    {
        private readonly LoCoMPro.Data.LoCoMProContext _context;
        private readonly IConfiguration _configuration;

        // ViewModel de la p�gina
        [BindProperty]
        public AgregarProdVM viewModel { get; set; }

        // Utilizados para cargar el combobox de unidad y categor�a
        public SelectList opcionesUnidad { get; set; }
        public SelectList opcionesCategoria { get; set; }

        // Constructor del modelo de la p�gina
        public AgregarProdModel(LoCoMPro.Data.LoCoMProContext contexto, IConfiguration configuration)
        {
            // Establece el contexto
            _context = contexto;
            // Establece la configuraci�n
            _configuration = configuration;
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
            foreach (var entradaUnidad in _context.Categorias)
            {
                listaCategoria.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            opcionesCategoria = new SelectList(listaCategoria);
            // Inserta unidades en combobox
            var listaUnidad = new List<string>();
            // Crea una lista del nombre de todas las unidades
            foreach (var entradaUnidad in _context.Unidades)
            {
                listaUnidad.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            opcionesUnidad = new SelectList(listaUnidad);
        }
        public void OnGet()
        {
            // Inserta categor�as en combobox
            var listaCategoria = new List<string>();
            // Crea una lista del nombre de todas las categorias
            foreach (var entradaUnidad in _context.Categorias)
            {
                listaCategoria.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            opcionesCategoria = new SelectList(listaCategoria);

            // Inserta unidades en combobox
            var listaUnidad = new List<string>();
            // Crea una lista del nombre de todas las unidades
            foreach (var entradaUnidad in _context.Unidades)
            {
                listaUnidad.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            opcionesUnidad = new SelectList(listaUnidad);
        }
        public IActionResult OnPostAceptar()
        {
            // TODO(pinwinos): Implementar usuarios
            string usuarioCreador = "Usuario1";

            Console.WriteLine("A ver");
            Console.WriteLine(viewModel.nombreProducto);

            // Verificar si el producto ya existe en la base de datos
            var existingProduct = _context.Productos.FirstOrDefault(p => p.nombre == viewModel.nombreProducto);

            // Si el producto no exist�a, agr�guelo
            if (existingProduct == null)
            {
                // El producto no existe, agr�galo a la base de datos
                var nuevoProducto = new Producto
                {
                    nombre = viewModel.nombreProducto,
                    marca = viewModel.marcaProducto,
                    nombreUnidad = viewModel.nombreUnidad,
                    nombreCategoria = viewModel.nombreCategoria
                };

                // Agrega el nuevo producto a la base de datos
                _context.Productos.Add(nuevoProducto);
                _context.SaveChanges();
            }

            // Agregarle registros al producto encontrado
            var nuevoRegistro = new Registro
            {
                // Indicar el tiempo de creaci�n
                creacion = DateTime.Now,
                usuarioCreador = usuarioCreador,
                descripcion = viewModel.descripcion,
                // Convertir a decimal
                precio = decimal.Parse(viewModel.precio),
                productoAsociado = viewModel.nombreProducto,
                // TODO(pinwinos): Reemplazar con el nombre de tienda
                nombreTienda = "Maxi Pali",
                // TODO(pinwinos): Reemplazar con el nombre del distrito
                nombreDistrito = "Heredia",
                // TODO(pinwinos): Reemplazar con el nombre del canton
                nombreCanton = "Heredia",
                // TODO(pinwinos): Reemplazar con el nombre de la provincia
                nombreProvincia = "Heredia"
            };

            // Add the new registro to the database
            _context.Registros.Add(nuevoRegistro);
            _context.SaveChanges();

            // TODO(Pinwinos): definir si borrar si se sale de esta p�gina
            RellenarSelectList();
            // Limpia los datos del view model
            limpiarViewModel();
            // Redirige a otra p�gina o realiza cualquier otra acci�n despu�s de agregar el producto
            return RedirectToPage("/AgregarProducto/AgregarProd");
        }

        // Se presion� el bot�n cancelar
        public void OnPostCancelar()
        {
            // TODO(Pinwinos): definir si borrar si se sale de esta p�gina
            RellenarSelectList();
            // Limpia los datos del view model
            limpiarViewModel();
            // L�gica para manejar la acci�n cuando se presiona el bot�n "Cancelar
            Console.WriteLine("Boton de cancelar fue presionado");
        }

        // Rellena los select list
        public void RellenarSelectList()
        {
            // Inserta categor�as en combobox
            var listaCategoria = new List<string>();
            // Crea una lista del nombre de todas las categorias
            foreach (var entradaUnidad in _context.Categorias)
            {
                listaCategoria.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            opcionesCategoria = new SelectList(listaCategoria);

            // Inserta unidades en combobox
            var listaUnidad = new List<string>();
            // Crea una lista del nombre de todas las unidades
            foreach (var entradaUnidad in _context.Unidades)
            {
                listaUnidad.Add(entradaUnidad.nombre);
            }
            // Inserta la lista de nombres en un SelectList
            opcionesUnidad = new SelectList(listaUnidad);
        }

        // Limpia los contenidos del view model
        private void limpiarViewModel()
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