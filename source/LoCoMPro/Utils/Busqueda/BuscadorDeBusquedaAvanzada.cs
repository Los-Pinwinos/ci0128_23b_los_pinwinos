using LoCoMPro.Data;
using LoCoMPro.Utils.Interfaces;
using LoCoMPro.ViewModels.Busqueda;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Utils.Busqueda
{
    // Buscador especializado para la pagina de busqueda avanzada
    public class BuscadorDeBusquedaAvanzada : BuscadorDeBusqueda
    {
        // Busquedas
        private string? marca { get; set; }
        private string? provincia { get; set; }
        private string? canton { get; set; }

        // Constructor
        public BuscadorDeBusquedaAvanzada(LoCoMProContext contexto
                                        , string? producto = null, string? marca = null, string? provincia = null, string? canton = null)
            : base(contexto, producto)
        {
            this.marca = marca;
            this.provincia = provincia;
            this.canton = canton;
        }

        // Setters
        public void setProvincia(string? provincia)
        {
            this.provincia = provincia;
        }
        public void setCanton(string? canton)
        {
            this.canton = canton;
        }
        public void setMarca(string? marca)
        {
            this.marca = marca;
        }

        // Buscar
        public override IQueryable<BusquedaVM> buscar()
        {
            IQueryable<BusquedaVM> resultadosIQ = base.buscar();
            // Buscar por marca
            resultadosIQ = this.buscarMarca(resultadosIQ);
            // Buscar por provincia
            resultadosIQ = this.buscarProvincia(resultadosIQ);
            // Buscar por canton
            resultadosIQ = this.buscarCanton(resultadosIQ);
            return resultadosIQ;
        }

        // Buscar por marca
        private IQueryable<BusquedaVM> buscarMarca(IQueryable<BusquedaVM> entradaIQ)
        {
            // Ver si se usa el nombre de busqueda
            if (!string.IsNullOrEmpty(this.marca))
            {
                return entradaIQ.Where(r => r.marca.Contains(this.marca));
            }
            else
            {
                return entradaIQ;
            }
        }

        // Buscar por provincia
        private IQueryable<BusquedaVM> buscarProvincia(IQueryable<BusquedaVM> entradaIQ)
        {
            // Ver si se usa el nombre de busqueda
            if (!string.IsNullOrEmpty(this.provincia))
            {
                return entradaIQ.Where(r => r.provincia.Contains(this.provincia));
            }
            else
            {
                return entradaIQ;
            }
        }

        // Buscar por canton
        private IQueryable<BusquedaVM> buscarCanton(IQueryable<BusquedaVM> entradaIQ)
        {
            // Ver si se usa el nombre de busqueda
            if (!string.IsNullOrEmpty(this.canton))
            {
                return entradaIQ.Where(r => r.canton.Contains(this.canton));
            }
            else
            {
                return entradaIQ;
            }
        }
    }
}