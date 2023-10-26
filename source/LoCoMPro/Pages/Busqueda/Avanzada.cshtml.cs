using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Busqueda;
using LoCoMPro.Utils.Busqueda;
using LoCoMPro.Utils.Interfaces;

namespace LoCoMPro.Pages.Busqueda
{
    public class BusquedaAvanzadaModel : BusquedaModel
    {
        // Constructor
        public BusquedaAvanzadaModel(LoCoMProContext contexto, IConfiguration configuracion)
            : base(contexto, configuracion)
        {
            // Inicializar
            this.InicializarAvanzado();
        }

        // Avanzado
        [BindProperty(SupportsGet = true)]
        public string? marca { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? provincia { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? canton { get; set; }

        // Inicializar avanzado
        private void InicializarAvanzado()
        {
            // Inicializar
            this.producto = "";
            this.marca = "";
            this.provincia = "";
            this.canton = "";
        }

        // On GET avanzado
        public IActionResult OnGetBuscarAvanzado(
            string? nombreProducto
            , string? nombreMarca
            , string? nombreProvincia
            , string? nombreCanton)
        {
            if ((!string.IsNullOrEmpty(nombreProducto)
                || !string.IsNullOrEmpty(nombreMarca)
                || !string.IsNullOrEmpty(nombreProvincia)
                || !string.IsNullOrEmpty(nombreCanton))
                && this.contexto.Productos != null)
            {
                // Asignar variables
                producto = string.IsNullOrEmpty(nombreProducto) ? "" : nombreProducto;
                marca = string.IsNullOrEmpty(nombreMarca) ? "" : nombreMarca;
                provincia = string.IsNullOrEmpty(nombreProvincia) ? "" : nombreProvincia;
                canton = string.IsNullOrEmpty(nombreProvincia) ? "" : nombreCanton;
                // Configurar buscador
                IBuscador<BusquedaVM> buscador = new BuscadorDeBusquedaAvanzada(this.contexto, nombreProducto, nombreMarca, nombreProvincia, nombreCanton);
                // Consultar la base de datos
                IQueryable<BusquedaVM> busqueda = buscador.buscar();
                // Cargar filtros
                this.cargarFiltros(busqueda);
                // Asignar data de JSON
                this.resultadosBusqueda = Newtonsoft.Json.JsonConvert.SerializeObject(busqueda.ToList());
            }
            return Page();
        }
    }
}