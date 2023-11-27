using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.VerRegistros;
using LoCoMPro.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LoCoMPro.Pages.VerRegistros
{
    public class VerRegistrosModel : PageModel
    {
        private readonly LoCoMProContext contexto;

        public IList<VerRegistrosVM> Registros { get; set; } = new List<VerRegistrosVM>();

        public string? NombreProducto { get; set; }

        public string? NombreCategoria { get; set; }

        public string? NombreMarca { get; set; }

        public string? NombreUnidad { get; set; }

        public string? NombreTienda { get; set; }

        public string? NombreProvincia { get; set; }

        public string? NombreCanton { get; set; }
        public string? NombreUsuario { get; set; }

        public int? EsFavorito { get; set; }

        public string? resultadoRegistros { get; set; }

        public ICollection<Fotografia>? fotografias { get; set; }


        public VerRegistrosModel(LoCoMProContext context, string? productoNombre = null, string? tiendaNombre = null, string? provinciaNombre = null, string? cantonNombre = null)
        {
            contexto = context;
            NombreProducto = productoNombre;
            NombreTienda = tiendaNombre;
            NombreProvincia = provinciaNombre;
            NombreCanton = cantonNombre;
            EsFavorito = 0;
        }

        public int EsProductoFavorito(string productoNombre)
        {
            bool productoEncontrado = false;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                Usuario? usuario = contexto.Usuarios.Include(u => u.favoritos).FirstOrDefault(u => u.nombreDeUsuario == User.Identity.Name);
                productoEncontrado = usuario?.favoritos.Any(producto => producto.nombre == productoNombre) ?? false;
            }

            return productoEncontrado ? 1 : 0;
        }

        public void OnGetAgregarAFavoritos(string nombreProducto)
        {

            // Revisar que el usuario esté loggeado
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                Usuario? usuario = contexto.Usuarios.Include(u => u.favoritos).FirstOrDefault(u => u.nombreDeUsuario == User.Identity.Name);
                if (usuario != null)
                {
                    // Obtener el producto de la base de datos
                    Producto? producto = contexto.Productos.FirstOrDefault(p => p.nombre == nombreProducto);
                    if (producto != null)
                    {
                        usuario.favoritos.Add(producto);
                        contexto.SaveChanges();
                    }
                }
            }
        }

        public void OnGetRemoverDeFavoritos(string nombreProducto)
        {

            // Revisar que el usuario esté loggeado
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                Usuario? usuario = contexto.Usuarios.Include(u => u.favoritos).FirstOrDefault(u => u.nombreDeUsuario == User.Identity.Name);
                if (usuario != null)
                {
                    // Obtener el producto de la base de datos
                    Producto? producto = contexto.Productos.FirstOrDefault(p => p.nombre == nombreProducto);
                    if (producto != null)
                    {
                        usuario.favoritos.Remove(producto);
                        contexto.SaveChanges();
                    }
                }
            }
        }

        public IQueryable<VerRegistrosVM> ObtenerRegistros()
        {
            IQueryable<VerRegistrosVM> registrosIQ = contexto.Registros
                .Include(r => r.fotografias)
                .Where(r => r.productoAsociado.Equals(NombreProducto) &&
                       r.nombreTienda.Equals(NombreTienda) &&
                       r.nombreProvincia.Equals(NombreProvincia) &&
                       r.nombreCanton.Equals(NombreCanton) &&
                       r.visible)
                .GroupBy(r => new
                {
                    r.creacion,
                    r.usuarioCreador,
                    r.precio,
                    r.calificacion,
                    r.descripcion
                })
                .Select(group => new VerRegistrosVM
                {
                    creacion = group.Key.creacion,
                    usuarioCreador = group.Key.usuarioCreador,
                    precio = group.Key.precio,
                    calificacion = group.Key.calificacion,
                    descripcion = group.Key.descripcion,
                    fotografias = group.SelectMany(registro => registro.fotografias).ToList()
                })
             .OrderByDescending(r => r.creacion);

            return registrosIQ;
        }

        public async Task<IActionResult> OnGetAsync(string productoNombre, string categoriaNombre
            , string marcaNombre, string unidadNombre, string tiendaNombre
            , string provinciaNombre, string cantonNombre)
        {
            NombreProducto = productoNombre;
            NombreCategoria = categoriaNombre;
            NombreMarca = marcaNombre;
            NombreUnidad = unidadNombre;
            NombreTienda = tiendaNombre;
            NombreProvincia = provinciaNombre;
            NombreCanton = cantonNombre;
            EsFavorito = EsProductoFavorito(productoNombre);

            IQueryable<VerRegistrosVM> registrosIQ = this.ObtenerRegistros();

            Registros = await registrosIQ.ToListAsync();
            this.resultadoRegistros = JsonConvert.SerializeObject(Registros);

            // Actualizar el atributo de fotografías para poder trabajar con todas las imagenes asociadas al registro
            var fotografiasEnlazadas = contexto.Fotografias
                .AsEnumerable()
                .Where(f => Registros.Any(r => r.fotografias.Contains(f)))
                .ToList();

            fotografias = fotografiasEnlazadas;

            return Page();
        }
    }
}
