using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Moderacion;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoCoMPro.ViewModels.VerRegistros;

namespace LoCoMPro.Pages.Moderacion
{
    public class EstadisticasUsuariosReportesModel : PageModel
    {
        private readonly LoCoMProContext contexto;

        public string? resultadosEstadisticas { get; set; }

        public int cantidadValidos { get; set; }

        public string tipoPagina { get; set; }

        public IList<UsuarioEstadisticasVM> Usuarios { get; set; } = new List<UsuarioEstadisticasVM>();

        public EstadisticasUsuariosReportesModel(LoCoMProContext context)
        {
            contexto = context;
            cantidadValidos = 0;
            tipoPagina = "reportadores";
        }

        public int contarUsuariosValidos()
        {
            int cantidad = this.Usuarios.Count(user =>
                user.NombreUsuario != null);
            return cantidad;
        }

        public IQueryable<UsuarioEstadisticasVM> buscarUsuariosReportadores()
        {
            IQueryable<UsuarioEstadisticasVM> topUsuarios = contexto.Reportes
                .Where(r => r.usuarioCreadorReporte != null)
                .GroupBy(r => r.usuarioCreadorReporte)
                .Select(g => new
                {
                    UsuarioCreadorReporte = g.Key,
                    CantidadReportes = g.Count(),
                    CantidadAprobados = g.Count(report => report.registro.visible == false && report.verificado),
                    CantidadAportes = contexto.Registros.Count(registro => registro.usuarioCreador == g.Key)
                })
                .OrderByDescending(x => x.CantidadReportes)
                .Take(10)
                .Join(contexto.Usuarios,
                    report => report.UsuarioCreadorReporte,
                    usuario => usuario.nombreDeUsuario,
                    (report, usuario) => new UsuarioEstadisticasVM
                    {
                        NombreUsuario = usuario.nombreDeUsuario,
                        Calificacion = usuario.calificacion,
                        CantidadContribuciones = report.CantidadAportes,
                        CantidadReportes = report.CantidadReportes,
                        CantidadVerificados = report.CantidadAprobados,
                    });

            return topUsuarios;
        }

        public IQueryable<UsuarioEstadisticasVM> buscarUsuariosReportados()
        {
            IQueryable<UsuarioEstadisticasVM> topUsuarios = contexto.Reportes
                .Where(r => r.usuarioCreadorRegistro != null)
                .GroupBy(r => r.usuarioCreadorRegistro)
                .Select(g => new
                {
                    UsuarioCreadorReporte = g.Key,
                    CantidadReportes = g.Count(),
                    CantidadAprobados = g.Count(report => report.registro.visible == false && report.verificado),
                    CantidadAportes = contexto.Registros.Count(registro => registro.usuarioCreador == g.Key)
                })
                .OrderByDescending(x => x.CantidadReportes)
                .Take(10)
                .Join(contexto.Usuarios,
                    report => report.UsuarioCreadorReporte,
                    usuario => usuario.nombreDeUsuario,
                    (report, usuario) => new UsuarioEstadisticasVM
                    {
                        NombreUsuario = usuario.nombreDeUsuario,
                        Calificacion = usuario.calificacion,
                        CantidadContribuciones = report.CantidadAportes,
                        CantidadReportes = report.CantidadReportes,
                        CantidadVerificados = report.CantidadAprobados,
                    });

            return topUsuarios;
        }

        public async Task<IActionResult> OnGetAsync(string tipo)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated && User.IsInRole("moderador"))
            {
                this.tipoPagina = tipo;
                if (this.tipoPagina == "reportadores")
                {
                    IQueryable<UsuarioEstadisticasVM> topUsuarios = buscarUsuariosReportadores();
                    this.Usuarios = await topUsuarios.ToListAsync();
                    this.resultadosEstadisticas = JsonConvert.SerializeObject(Usuarios);
                }
                else
                {
                    // Se asume que tipo == reportados. Si se desea agregar estadísticas nuevas, se puede
                    // agregar otro if/else acá.
                    IQueryable<UsuarioEstadisticasVM> topUsuarios = buscarUsuariosReportados();
                    this.Usuarios = await topUsuarios.ToListAsync();
                    this.resultadosEstadisticas = JsonConvert.SerializeObject(Usuarios);
                }
                // En caso de que las búsquedas retornen Usuarios inválidos para cumplir
                // con los 10 usuarios en total, se realiza un conteo para
                // verificar si se debe o no mostrar la imagen del pinwino que indica
                // que no hay usuarios que califiquen para la estadística.
                this.cantidadValidos = contarUsuariosValidos();
            }
            else
            {
                ViewData["MensajeError"] = "Por favor ingrese al sistema como moderador.";
            }
            return Page();
        }
    }
}
