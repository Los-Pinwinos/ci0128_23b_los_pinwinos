using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Moderacion
{
    public class UsuarioEstadisticasVM
    {
        public string NombreUsuario { get; set; }
        public double Calificacion { get; set; }
        public int CantidadReportes { get; set; }
        public int CantidadVerificados { get; set; }

    }
}