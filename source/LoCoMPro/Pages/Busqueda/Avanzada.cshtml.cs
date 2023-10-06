using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.ViewModels.Busqueda;
using Microsoft.IdentityModel.Tokens;


namespace LoCoMPro.Pages.Busqueda
{
    public class BusquedaAvanzadaModel : BusquedaModel
    {
        // Constructor
        public BusquedaAvanzadaModel(LoCoMProContext context, IConfiguration configuration) 
            : base(context, configuration)
        {
            // Inicializar
            InicializarAvanzado();
        }

        // Avanzado
        [BindProperty(SupportsGet = true)]
        public string? marca { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? provincia { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? canton { get; set; }

        // Inicializar avanzado
        private void InicializarAvanzado()
        {
            // Inicializar
            producto = "";
            marca = "";
            provincia = "";
            canton = "";
        }

        // On GET avanzado
        public async Task<IActionResult> OnGetBuscarAvanzadoAsync(int? indicePagina
            , string? nombreProducto, string? filtroProducto
            , string? nombreMarca, string? filtroMarca
            , string? nombreProvincia, string? filtroProvincia
            , string? nombreCanton, string? filtroCanton
            , string? nombresProvincias, string? filtrosProvincias
            , string? nombresCantones, string? filtrosCantones
            , string? ordenadoPrecio, string? sentidoPrecio)
        {
            if ((!string.IsNullOrEmpty(nombreProducto) || !string.IsNullOrEmpty(filtroProducto)
                || !string.IsNullOrEmpty(nombreMarca)|| !string.IsNullOrEmpty(filtroMarca)
                || !string.IsNullOrEmpty(nombreProvincia)|| !string.IsNullOrEmpty(filtroProvincia)
                || !string.IsNullOrEmpty(nombreCanton) || !string.IsNullOrEmpty(filtroCanton))
                && _context.Productos != null)
            {
                // Verificar par�metros y asignar �ndice de p�gina correcto
                indicePagina = verificarParametros(indicePagina
                    , nombreProducto, filtroProducto
                    , nombresProvincias, filtrosProvincias
                    , nombresCantones, filtrosCantones
                    , ordenadoPrecio, sentidoPrecio);

                // Verificar par�metros y asignar �ndice de p�gina correcto
                indicePagina = verificarParametrosAvanzados(indicePagina
                    , nombreMarca, filtroMarca
                    , nombreProvincia, filtroProvincia
                    , nombreCanton, filtroCanton);

                // Hacer la consulta de productos con registros
                IQueryable<BusquedaVM> productosIQ = buscarProductos();

                // Cargar filtros
                cargarFiltros(productosIQ);

                // Filtrar
                productosIQ = filtrarProductos(productosIQ);

                // Ordenar por precio
                productosIQ = ordenarProducto(productosIQ);

                // Paginar
                await paginarProductos(productosIQ, indicePagina);
            }
            return Page();
        }

        // Verificar par�metros
        private int? verificarParametrosAvanzados(int? indicePagina
            , string? nombreMarca, string? filtroMarca
            , string? nombreProvincia, string? filtroProvincia
            , string? nombreCanton, string? filtroCanton)
        {
            // Revisar si hay que regresar numero de p�gina
            if (!string.IsNullOrEmpty(nombreMarca))
            {
                indicePagina = 1;
            }
            else
            {
                nombreMarca = filtroMarca;
            }
            marca = nombreMarca;

            if (!string.IsNullOrEmpty(nombreProvincia))
            {
                indicePagina = 1;
            }
            else
            {
                nombreProvincia = filtroProvincia;
            }
            provincia = nombreProvincia;

            if (!string.IsNullOrEmpty(nombreCanton))
            {
                indicePagina = 1;
            }
            else
            {
                nombreCanton = filtroCanton;
            }
            canton = nombreCanton;

            return indicePagina;
        }

        // Sobrecarga de buscar productos
        new protected IQueryable<BusquedaVM> buscarProductos()
        {
            IQueryable<BusquedaVM> productosIQ = _context.Registros
                        .Include(r => r.producto)
                        .OrderByDescending(r => r.creacion)
                        .GroupBy(r => new { r.productoAsociado, r.nombreTienda, r.nombreProvincia, r.nombreCanton, r.nombreDistrito })
                        .Select(group => new BusquedaVM
                        {
                            nombre = group.First().productoAsociado,
                            precio = group.First().precio,
                            unidad = group.First().producto.nombreUnidad,
                            fecha = group.First().creacion,
                            tienda = group.First().nombreTienda,
                            provincia = group.First().nombreProvincia,
                            canton = group.First().nombreCanton,
                            marca = !string.IsNullOrEmpty(group.First().producto.marca) ?
                                    group.First().producto.marca : "Sin marca"
                        });
            // Buscar por nombre
            productosIQ = buscarNombre(productosIQ);
            // Buscar por marca
            productosIQ = buscarMarca(productosIQ);
            // Buscar por provincia
            productosIQ = buscarProvincia(productosIQ);
            // Buscar por canton
            productosIQ = buscarCanton(productosIQ);
            // Retornar busqueda
            return productosIQ;
        }

        // Buscar por marca
        private IQueryable<BusquedaVM> buscarMarca(IQueryable<BusquedaVM> productosIQ)
        {
            // Ver si se usa el nombre de busqueda
            if (!string.IsNullOrEmpty(marca))
            {
                return productosIQ.Where(r => r.marca.Contains(marca));
            }
            else
            {
                return productosIQ;
            }
        }

        // Buscar por provincia
        private IQueryable<BusquedaVM> buscarProvincia(IQueryable<BusquedaVM> productosIQ)
        {
            // Ver si se usa el nombre de busqueda
            if (!string.IsNullOrEmpty(provincia))
            {
                return productosIQ.Where(r => r.provincia.Contains(provincia));
            }
            else
            {
                return productosIQ;
            }
        }

        // Buscar por canton
        private IQueryable<BusquedaVM> buscarCanton(IQueryable<BusquedaVM> productosIQ)
        {
            // Ver si se usa el nombre de busqueda
            if (!string.IsNullOrEmpty(canton))
            {
                return productosIQ.Where(r => r.canton.Contains(canton));
            }
            else
            {
                return productosIQ;
            }
        }
    }
}