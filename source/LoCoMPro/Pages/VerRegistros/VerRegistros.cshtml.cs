using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Busqueda;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoCoMPro.ViewModels.VerRegistros;
using LoCoMPro.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LoCoMPro.Pages.VerRegistros
{
    public class VerRegistrosModel : PageModel
    {
        private readonly LoCoMProContext contexto;
        private readonly IConfiguration _configuration;

        public VerRegistrosModel(LoCoMProContext context, IConfiguration configuration)
        {
            contexto = context;
            _configuration = configuration;
        }

        public IList<VerRegistrosVM> Registros { get; set; } = new List<VerRegistrosVM>();

        public string NombreProducto { get; set; }
       
        public string NombreCategoria { get; set; }

        public string NombreMarca { get; set; }

        public string NombreUnidad { get; set; }

        public string NombreTienda { get; set; }

        public string NombreProvincia { get; set; }

        public string NombreCanton { get; set; }
        public string NombreUsuario {  get; set; } 

        public string? resultadoRegistros { get; set; }

        public ICollection<Fotografia>? fotografias { get; set; }

        public async Task OnGetAsync(string productName, string categoriaNombre
            , string marcaNombre, string unidadNombre, string tiendaNombre
            , string provinciaNombre, string cantonNombre)
        {
            NombreProducto = productName;
            NombreCategoria = categoriaNombre;
            NombreMarca = marcaNombre;
            NombreUnidad = unidadNombre;
            NombreTienda = tiendaNombre;
            NombreProvincia = provinciaNombre;
            NombreCanton = cantonNombre;

            IQueryable<VerRegistrosVM> registrosIQ = contexto.Registros
                .Include(r => r.fotografias)
                .Where(r => r.productoAsociado.Equals(productName) && r.nombreTienda.Equals(tiendaNombre) && r.nombreProvincia.Equals(provinciaNombre) && r.nombreCanton.Equals(cantonNombre))
                .GroupBy(r => new
                {
                    /*creacionDate = new DateTime(r.creacion.Year, r.creacion.Month, r.creacion.Day),*/
                    r.creacion,
                    r.usuarioCreador,
                    r.precio,
                    r.calificacion,
                    r.descripcion
                })
                .Select(group => new VerRegistrosVM
                {
                    /*creacion = group.Key.creacionDate,*/
                    creacion = group.Key.creacion,
                    usuarioCreador = group.Key.usuarioCreador,
                    precio = group.Key.precio,
                    calificacion = group.Key.calificacion,
                    descripcion = group.Key.descripcion,
                    fotografias = group.SelectMany(registro => registro.fotografias).ToList()
                });

            Registros = await registrosIQ.ToListAsync();
            this.resultadoRegistros = JsonConvert.SerializeObject(Registros);

            // Actualizar el atributo de fotografías para poder trabajar con todas las imagenes asociadas al registro
            var fotografiasEnlazadas = contexto.Fotografias
                .AsEnumerable()
                .Where(f => Registros.Any(r => r.fotografias.Contains(f)))
                .ToList();

            fotografias = fotografiasEnlazadas;
        }
    }
}
