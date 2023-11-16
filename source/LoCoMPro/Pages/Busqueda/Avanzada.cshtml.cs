using Microsoft.AspNetCore.Mvc;
using LoCoMPro.Data;
using LoCoMPro.ViewModels.Busqueda;
using LoCoMPro.Utils.Buscadores;
using LoCoMPro.Utils.Interfaces;
using Newtonsoft.Json;
using LoCoMPro.Models;
using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Pages.Busqueda
{
    public class BusquedaAvanzadaModel : BusquedaModel
    {
        // Constructor
        public BusquedaAvanzadaModel(LoCoMProContext contexto, IConfiguration configuracion)
            : base(contexto, configuracion)
        {
            this.InicializarAvanzado();
        }

        // Avanzado
        [BindProperty(SupportsGet = true)]
        public string? marca { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? provincia { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? canton { get; set; }
        public IList<string> resultadosAutocompletado { get; set; }
        public IList<Provincia>? provincias;
        public int buscarPorCanton { get; set; }

        private void InicializarAvanzado()
        {
            this.producto = "";
            this.marca = "";
            this.provincia = "";
            this.canton = "";
            this.provincias = new List<Provincia>();
            this.buscarPorCanton = 0;
        }

            // On GET avanzado
        public IActionResult OnGetBuscarAvanzado(
            string? nombreProducto
            , string? nombreMarca
            , string? nombreProvincia
            , string? nombreCanton)
        {
            // Cargar toda la información de provincias de la base de datos
            provincias = contexto.Provincias.ToList();

            if ((!string.IsNullOrEmpty(nombreProducto)
                || !string.IsNullOrEmpty(nombreMarca)
                || !string.IsNullOrEmpty(nombreProvincia)
                || !string.IsNullOrEmpty(nombreCanton))
                && this.contexto.Productos != null)
            {
                this.resultadosAutocompletado = new List<string>();
                // Asignar variables
                producto = string.IsNullOrEmpty(nombreProducto) ? "" : nombreProducto;
                marca = string.IsNullOrEmpty(nombreMarca) ? "" : nombreMarca;
                provincia = string.IsNullOrEmpty(nombreProvincia) ? "" : nombreProvincia;
                canton = string.IsNullOrEmpty(nombreCanton) ? "" : nombreCanton;
                // Configurar buscador
                IBuscador<BusquedaVM> buscador = new BuscadorDeProductosAvanzado(this.contexto, nombreProducto, nombreMarca, nombreProvincia, nombreCanton);
                // Consultar la base de datos
                IQueryable<BusquedaVM> busqueda = buscador.buscar();
                // Cargar filtros
                this.cargarFiltros(busqueda);
                // Asignar data de JSON
                this.resultadosBusqueda = JsonConvert.SerializeObject(busqueda.ToList());

                if (canton != null)
                {
                    buscarPorCanton = 1;
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAutocompletar(string hilera)
        {
            List<string> resultados = await contexto.Productos
                .Where(p => p.marca.StartsWith(hilera))
                .Select(p => p.marca)
                .Distinct()
                .OrderBy(p => p)
                .ToListAsync();

            return new JsonResult(resultados);
        }


        public IActionResult OnGetCantonesPorProvincia(string provincia)
        {
            var cantones = contexto.Cantones
                .Where(c => c.nombreProvincia == provincia)
                .ToList();

            // Retorna un JSON con los cantones de la provincia específica
            return new JsonResult(cantones);
        }
    }
}