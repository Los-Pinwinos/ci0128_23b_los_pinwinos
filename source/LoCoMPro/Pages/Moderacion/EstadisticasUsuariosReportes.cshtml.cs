using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Moderacion;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Import the necessary namespace for Task
using LoCoMPro.ViewModels.VerRegistros;

namespace LoCoMPro.Pages.Moderacion
{
    public class EstadisticasUsuariosReportesModel : PageModel
    {
        private readonly LoCoMProContext contexto;

        public string? resultadosEstadisticas { get; set; }

        public IList<UsuarioEstadisticasVM> Usuarios { get; set; } = new List<UsuarioEstadisticasVM>();

        public EstadisticasUsuariosReportesModel(LoCoMProContext context)
        {
            contexto = context;
        }
        public IQueryable<UsuarioEstadisticasVM> buscarUsuariosReportadores()
        {
            IQueryable<UsuarioEstadisticasVM> topUsers = contexto.Reportes
                .Where(r => r.usuarioCreadorReporte != null)
                .GroupBy(r => r.usuarioCreadorReporte)
                .Select(g => new
                {
                    UsuarioCreadorReporte = g.Key,
                    CountReports = g.Count(),
                    CountVerifiedReports = g.Count(report => report.registro.visible == false && report.verificado),
                    CountContributions = contexto.Registros.Count(registro => registro.usuarioCreador == g.Key)
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
                        CantidadContribuciones = report.CountContributions,
                        CantidadReportes = report.CountReports,
                        CantidadVerificados = report.CountVerifiedReports,
                    });

            return topUsers;
        }

        public async Task<IActionResult> OnGetAsync(string tipo)
        {
            if (tipo == "reportadores")
            {
                IQueryable<UsuarioEstadisticasVM> topUsers = buscarUsuariosReportadores();
                this.Usuarios = await topUsers.ToListAsync();
                this.resultadosEstadisticas = JsonConvert.SerializeObject(Usuarios);
            } else
            {
                // Se asume que tipo == reportados. Si se desea agregar estadísticas nuevas, se puede
                // agregar otro if/else acá.
                IQueryable<UsuarioEstadisticasVM> topUsers = buscarUsuariosReportadores();
                this.Usuarios = await topUsers.ToListAsync();
                this.resultadosEstadisticas = JsonConvert.SerializeObject(Usuarios);
            }
            return Page();
        }
    }
}
