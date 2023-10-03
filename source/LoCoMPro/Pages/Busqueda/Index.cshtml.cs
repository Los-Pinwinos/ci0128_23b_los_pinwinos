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
using Microsoft.Data.SqlClient;
using System.Globalization;
using Microsoft.IdentityModel.Tokens;

namespace LoCoMPro.Pages.Busqueda
{
    public class BusquedaVMModel : PageModel
    {
        private readonly LoCoMPro.Data.LoCoMProContext _context;
        private readonly IConfiguration _configuration;

        public BusquedaVMModel(LoCoMPro.Data.LoCoMProContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            // Inicializar
            Inicializar();
        }

        // Busquedas
        [BindProperty(SupportsGet = true)]
        public string? producto { get; set; }

        // Visual
        [BindProperty]
        public IList<BusquedaVM> productosVM { get; set; } = default!;

        // Paginacion
        public ListaPaginada<Producto> productosPaginados { get; set; } = default!;

        // Para ordernar por precio

        public string ordenarPor { get; set; }

        // Inicializar atributos
        public void Inicializar()
        {
            // Inicializar
            productosVM = new List<BusquedaVM>();
            productosPaginados = new ListaPaginada<Producto>();
        }

        // ON GET buscar
        public async Task<IActionResult> OnGetAsync(int? indicePagina, string? nombreProducto, string? filtroProducto, string ordenado)
        {
            if ((!string.IsNullOrEmpty(nombreProducto) || !string.IsNullOrEmpty(filtroProducto)) && _context.Productos != null)
            {
                // Verificar parámetros y asignar índice de página correcto
                indicePagina = verificarParametros(indicePagina, nombreProducto, filtroProducto);

                // Hacer la consulta de productos con registros
                IQueryable<Producto> productosIQ = buscarProductos();

                // Ordenamiento
                ordenarPor = String.IsNullOrEmpty(ordenado) ? "creacion" : ordenado;
                switch (ordenarPor)
                {
                    case "precio":
                        productosIQ = productosIQ.OrderBy(p => p.registros.Min(r => r.precio));
                        break;
                    default:
                        productosIQ = productosIQ.OrderByDescending(p => p.registros.Min(r => r.creacion));
                        break;
                }

                // Paginar
                await paginarProductos(productosIQ, indicePagina);
            }

            return Page();
        }

        // Verificar parámetros de ON GET Buscar
        private int? verificarParametros(int? indicePagina
            , string? nombreProducto, string? filtroProducto)
        {
            // Revisar si hay que regresar numero de página
            if (!string.IsNullOrEmpty(nombreProducto))
            {
                indicePagina = 1;
            }
            else
            {
                nombreProducto = filtroProducto;
            }
            producto = nombreProducto;

            return indicePagina;
        }

        // Buscar productos
        private IQueryable<Producto> buscarProductos()
        {
            // Consultar la base de datos
            IQueryable<Producto> productosIQ = _context.Productos
                    .Where(p => p.registros != null)
                    .Include(p => p.registros.OrderByDescending(r => r.creacion));
            // Buscar por nombre
            productosIQ = buscarNombre(productosIQ);
            // Retornar busqueda
            return productosIQ;
        }

        // Buscar por nombre
        private IQueryable<Producto> buscarNombre(IQueryable<Producto> productosIQ)
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

        // Paginar productos
        private async Task paginarProductos(IQueryable<Producto> productosFinales, int? indicePagina)
        {
            // Obtener tamaño de página
            var tamPagina = _configuration.GetValue("TamPagina", 4);
            // Crear productos paginados
            productosPaginados = await ListaPaginada<Producto>.CrearAsync(
                productosFinales.AsNoTracking(), indicePagina ?? 1, tamPagina);

            // Por cada resultado
            foreach (var producto in productosPaginados)
            {
                // Verificar que el producto tenga registros asociados
                if (producto.registros != null)
                {
                    // Convertir a lista
                    IList<Registro> listaRegistros = producto.registros.ToList();
                    // Ver si ya no hay resultados
                    if (listaRegistros.Count > 0)
                    {
                        // Obtener el registro mas reciente
                        Registro registroMasReciente = listaRegistros.First();
                        // Crear producto VM
                        var nuevoProductoVM = new BusquedaVM
                        {
                            nombre = producto.nombre,
                            precio = registroMasReciente.precio,
                            unidad = producto.nombreUnidad,
                            fecha = registroMasReciente.creacion,
                            tienda = registroMasReciente.nombreTienda,
                            provincia = registroMasReciente.nombreProvincia,
                            canton = registroMasReciente.nombreCanton,
                            marca = producto.marca
                        };
                        // Agregar a productos
                        productosVM.Add(nuevoProductoVM);
                    }
                }
            }
        }
    }
}
