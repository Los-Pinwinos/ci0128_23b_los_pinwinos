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

        // [BindProperty]
        public string NombreProducto { get; set; }

       
        public string marca { get; set; }

        
        public string nombreUnidad { get; set; }

       
        public string nombreCategoria { get; set; }

        public string? resultadoRegistros { get; set; }

        public async Task OnGetAsync(string productName)
        {
            IQueryable<VerRegistrosVM> registrosIQ = contexto.Registros
                .Include(r => r.producto)
                .Where(r => r.productoAsociado.Equals(productName))
                .GroupBy(r => new
                {
                    creacionDate = new DateTime(r.creacion.Year, r.creacion.Month, r.creacion.Day),
                    r.usuarioCreador,
                    r.precio,
                    r.calificacion,
                    r.descripcion
                })
                .Select(group => new VerRegistrosVM
                {
                    creacion = group.Key.creacionDate,
                    usuarioCreador = group.Key.usuarioCreador,
                    precio = group.Key.precio,
                    calificacion = group.Key.calificacion,
                    descripcion = group.Key.descripcion
                });

            Registros = await registrosIQ.ToListAsync();
            this.resultadoRegistros = JsonConvert.SerializeObject(Registros);
        }



    }
}
