using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoCoMPro.Models;
using LoCoMPro.ViewModels.Busqueda;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace LoCoMPro.Pages.Busqueda
{
    public class IndexVMModel : PageModel
    {
        private readonly LoCoMPro.Data.LoCoMProContext _context;
        private readonly IConfiguration _configuration;


        public IndexVMModel(LoCoMPro.Data.LoCoMProContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Busquedas
        [BindProperty(SupportsGet = true)]
        public string? producto { get; set; }

        // Visual
        [BindProperty]
        public IList<IndexVM> productosVM { get; set; } = default!;

        // On Get Async
        public async Task OnGetAsync(string? nombreProducto)
        {
            if (_context.Productos != null)
            {
                // Crear el arreglo de productos
                productosVM = new List<IndexVM>();

                // Asignar
                if (!string.IsNullOrEmpty(nombreProducto))
                {
                    producto = nombreProducto;
                }

                // Hacer la consulta
                IQueryable<Producto> productosIQ = (from s in _context.Productos
                                                    select s).Include(p => p.registros); ;
                // Ver si se usa el nombre de busqueda
                productosIQ = buscarProductos(productosIQ);

                // Mappee a la vista
                await listarProductos(productosIQ);
            }
        }

        // Buscar productos
        public IQueryable<Producto> buscarProductos(IQueryable<Producto> productosIQ)
        {
            // Ver si se usa el nombre de busqueda
            if (!string.IsNullOrEmpty(producto))
            {
                return productosIQ.Where(s => s.nombre.Contains(producto));
            }
            else
            {
                return productosIQ;
            }
        }

        // Listar productos
        public async Task listarProductos(IQueryable<Producto> productosIQ)
        {
            // Obtener resultados
            IList<Producto> productos = await productosIQ.ToListAsync();

            // Por cada resultado
            foreach (var producto in productos)
            {
                // Verificar que el producto tenga registros asociados
                if (producto.registros != null)
                {
                    // Obtener el registro mas reciente
                    Registro registroMasReciente = producto.registros
                        .OrderByDescending(r => r.creacion)
                        .ToList().First();
                    // Agregar a productos
                    productosVM.Add(
                        new IndexVM
                        {
                            nombre = producto.nombre,
                            precio = registroMasReciente.precio,
                            unidad = producto.nombreUnidad,
                            fecha = registroMasReciente.creacion,
                            tienda = registroMasReciente.nombreTienda,
                            provincia = registroMasReciente.nombreProvincia,
                            canton = registroMasReciente.nombreCanton
                        });
                }
            }
        }
    }
}

