using LoCoMPro.Models;
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
        public AgregarProdModel(LoCoMPro.Data.LoCoMProContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [BindProperty]
        public string Producto { get; set; }

        [BindProperty]
        public string Marca { get; set; }

        [BindProperty]
        public string Unidad { get; set; }

        [BindProperty]
        public string Categoria { get; set; }

        // Cargan combobox
        public SelectList opcionesUnidad { get; set; }
        public SelectList opcionesCategoria { get; set; }
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
        public void OnPostAceptar()
        {
            // Leer los valores ingresados por el usuario
            string productoTexto = Producto;
            string marcaTexto = Marca;
            string unidadTexto = Unidad;
            string categoriaTexto = Categoria;

            // Verificar si el producto ya existe en la base de datos
            var existingProduct = _context.Productos.FirstOrDefault(p => p.nombre == productoTexto);

            if (existingProduct != null)
            {
                // El producto ya existe, mostrar un mensaje de error al usuario
                ViewData["ErrorMessage"] = "El producto ya existe en el sistema. Favor volver a la p�gina anterior";
                return; // No agregar el producto
            }

            // El producto no existe, agr�galo a la base de datos
            var nuevoProducto = new Producto
            {
                nombre = productoTexto,
                marca = marcaTexto,
                nombreUnidad = unidadTexto,
                nombreCategoria = categoriaTexto
            };

            // Agrega el nuevo producto a la base de datos
            _context.Productos.Add(nuevoProducto);
            _context.SaveChanges();

            // Redirige a otra p�gina o realiza cualquier otra acci�n despu�s de agregar el producto
            // RedirectToPage("/AgregarRegistro");  // TODO(pinwinos): hacer que esta p�gina se devuelva a la p�gina de agregar registro
        }

        public void OnPostCancelar()
        {
            // L�gica para manejar la acci�n cuando se presiona el bot�n "Cancelar
            Console.WriteLine("Boton de cancelar fue presionado");
        }
    }
}