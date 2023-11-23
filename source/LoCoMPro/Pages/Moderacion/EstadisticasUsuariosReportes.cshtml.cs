using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Moderacion;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Import the necessary namespace for Task

namespace LoCoMPro.Pages.Moderacion
{
    public class EstadisticasUsuariosReportesModel : PageModel
    {
        private readonly LoCoMProContext contexto;

        public string resultadosEstadisticas { get; set; }

        public IList<UsuarioEstadisticasVM> Usuarios { get; set; } = new List<UsuarioEstadisticasVM>();

        public EstadisticasUsuariosReportesModel(LoCoMProContext context)
        {
            contexto = context;
        }

        public IQueryable<UsuarioEstadisticasVM> buscarUsuariosReportadores()
        {
            var topUsers = contexto.Reportes
                .Where(r => r.usuarioCreadorReporte != null)
                .GroupBy(r => r.usuarioCreadorReporte)
                .Select(g => new
                {
                    UsuarioCreadorReporte = g.Key,
                    CountReports = g.Count(),
                    CountVerifiedReports = g.Count(r => r.verificado),
                })
                .OrderByDescending(x => x.CountReports)
                .Take(10)
                .Join(contexto.Usuarios,
                    report => report.UsuarioCreadorReporte,
                    usuario => usuario.nombreDeUsuario,
                    (report, usuario) => new UsuarioEstadisticasVM
                    {
                        NombreUsuario = usuario.nombreDeUsuario,
                        Calificacion = usuario.calificacion,
                        CantidadReportes = report.CountReports,
                        CantidadVerificados = report.CountVerifiedReports,
                    });

            return topUsers;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var topUsers = buscarUsuariosReportadores();
            this.Usuarios = await topUsers.ToListAsync();
            this.resultadosEstadisticas = JsonConvert.SerializeObject(Usuarios);

            return Page();
        }
    }
}
