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

        // Inicializar atributos
        public void Inicializar()
        {
            // Inicializar
            productosVM = new List<BusquedaVM>();
        }

        // ON GET buscar
        public IActionResult OnGetBuscar(int? indicePagina
            , string? nombreProducto, string? filtroProducto)
        {
            if (!string.IsNullOrEmpty(nombreProducto) && !string.IsNullOrEmpty(filtroProducto) && _context.Productos != null)
            {
                // Verificar parámetros y asignar índice de página correcto
                indicePagina = verificarParametrosOnGetBuscar(indicePagina, nombreProducto, filtroProducto);

                // Hacer la consulta de productos con registros
                IQueryable<Producto> productosIQ = buscarProductos();

                // Paginar
                paginarProductos(productosIQ, indicePagina);
            }
            return Page();
        }

        // Verificar parámetros de ON GET Buscar
        private int? verificarParametrosOnGetBuscar(int? indicePagina
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
        private void paginarProductos(IQueryable<Producto> productosFinales, int? indicePagina)
        {
            
        }
    }
}
