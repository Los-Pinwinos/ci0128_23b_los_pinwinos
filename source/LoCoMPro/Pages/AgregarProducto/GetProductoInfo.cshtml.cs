using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace LoCoMPro.Pages.AgregarProducto
{
    public class GetProductoInfo : PageModel
    {
        private readonly Data.LoCoMProContext contexto;

        public GetProductoInfo(Data.LoCoMProContext contexto_base)
        {
            contexto = contexto_base;
        }

        public JsonResult OnGet(string productoNombre)
        {
            var respuesta = new RespuestaProducto(false, "", "", "");
            // Sacar el producto que coincide
            var producto = contexto.Productos.FirstOrDefault(p => p.nombre == productoNombre);
            if (producto != null)
            {
                respuesta.exito = true;
                respuesta.categoria = producto.nombreCategoria;
                respuesta.unidad = producto.nombreUnidad;
                respuesta.marca = "";
                if (producto.marca != null)
                {
                    respuesta.marca = producto.marca;
                }

            }
            return new JsonResult(respuesta);
        }

        // Clase para representar la respuesta solicitada
        public class RespuestaProducto
        {
            public RespuestaProducto(bool exito, string categoria, string unidad, string marca)
            {
                this.exito = exito;
                this.categoria = categoria;
                this.unidad = unidad;
                this.marca = marca;
            }
            public bool exito { get; set; }
            public string categoria { get; set; }
            public string unidad { get; set; }
            public string marca { get; set; }
        }
    }
}
