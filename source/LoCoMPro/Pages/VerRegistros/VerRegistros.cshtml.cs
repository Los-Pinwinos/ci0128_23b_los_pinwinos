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

        public async Task OnGetAsync()
        {
            // Search for products related to "Pañales"
            IQueryable<VerRegistrosVM> registrosIQ = contexto.Registros
                .Include(r => r.producto)
                .Where(r => r.productoAsociado.Equals("Aceite de Oliva"))
                .GroupBy(r => new
                {
                    r.creacion,
                    r.usuarioCreador,
                    r.precio,
                    // r.calificacion,
                    r.descripcion

                })
                .Select(group => new VerRegistrosVM
                {
                    creacion = group.Key.creacion,
                    usuarioCreador = group.Key.usuarioCreador,
                    precio = group.Key.precio,
                    // calificacion = group.calificacion,
                    descripcion = group.Key.descripcion
                });

            // You can add additional filters, sorting, and pagination if needed

            Registros = await registrosIQ.ToListAsync();
        }
    }
}
