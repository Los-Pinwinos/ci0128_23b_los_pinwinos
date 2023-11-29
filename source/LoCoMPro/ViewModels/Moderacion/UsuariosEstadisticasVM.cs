using System.ComponentModel.DataAnnotations;

namespace LoCoMPro.ViewModels.Moderacion
{
    public class UsuarioEstadisticasVM
    {
        // En esta clase no se necesitan anotaciones dado que solo corresponde a un contenedor para mostrar datos al
        // usuario, no se necesita obtener datos de este.
        public string NombreUsuario { get; set; }
        public double Calificacion { get; set; }
        public int CantidadContribuciones { get; set; }
        public int CantidadReportes { get; set; }
        public int CantidadVerificados { get; set; }

    }
}