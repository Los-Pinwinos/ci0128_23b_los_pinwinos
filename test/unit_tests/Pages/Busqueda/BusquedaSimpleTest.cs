using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.Pages.Busqueda;
using LoCoMProTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoCoMPro.ViewModels.Busqueda;
using Microsoft.Extensions.Configuration;

// buscadorDeProductos_ValidacionBusquedaProductoExistente_DeberiaDevolverSoloUnResultado

namespace LoCoMProTests.Pages.Busqueda
{
    // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 2
    [TestClass]
    public class BusquedaSimpleTest
    {
        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 2
        private IQueryable<BusquedaVM> generarBusquedaVM(int cantidad)
        {
            IList<BusquedaVM> resultados = new List<BusquedaVM>();
            for (int i = 0; i < cantidad; ++i)
            {
                BusquedaVM busquedaVM = new BusquedaVM
                {
                    nombre = "nombre" + i,
                    precio = i,
                    unidad = "unidad" + i,
                    fecha = DateTime.Now,
                    tienda = "tienda" + i,
                    provincia = "provincia" + i,
                    canton = "canton" + i,
                    marca = "marca" + i,
                    categoria = "categoria" + i
                };
                resultados.Add(busquedaVM);
            }
            return resultados.AsQueryable();
        }

        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 2
        [TestMethod]
        public void busquedaSimple_ValidacionCargarFiltroProvinciaNulo_DeberiaEstarVacio()
        {
            // Preparación
            MockDeContexto<LoCoMProContext> contexto = new MockDeContexto<LoCoMProContext>();
            IConfiguration configuracion = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            BusquedaModel pagina = new BusquedaModel(contexto.ObtenerObjetoDeMock(), configuracion);
            IQueryable<BusquedaVM> parametro = Enumerable.Empty<BusquedaVM>().AsQueryable();
            
            // Acción
            pagina.cargarFiltrosProvincia(parametro);

            // Verificación
            Assert.AreEqual(0, pagina.provinciasV.Count());
        }

        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 2
        [TestMethod]
        public void busquedaSimple_ValidacionCargarFiltroProvincia_DeberiaEstarLleno()
        {
            // Preparación
            MockDeContexto<LoCoMProContext> contexto = new MockDeContexto<LoCoMProContext>();
            IConfiguration configuracion = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            BusquedaModel pagina = new BusquedaModel(contexto.ObtenerObjetoDeMock(), configuracion);
            IQueryable<BusquedaVM> parametro = this.generarBusquedaVM(10);

            // Acción
            pagina.cargarFiltrosProvincia(parametro);

            // Verificación
            Assert.AreEqual(10, pagina.provinciasV.Count());
        }

        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 2
        [TestMethod]
        public void busquedaSimple_ValidacionCargarFiltroCantonNulo_DeberiaEstarVacio()
        {
            // Preparación
            MockDeContexto<LoCoMProContext> contexto = new MockDeContexto<LoCoMProContext>();
            IConfiguration configuracion = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            BusquedaModel pagina = new BusquedaModel(contexto.ObtenerObjetoDeMock(), configuracion);
            IQueryable<BusquedaVM> parametro = Enumerable.Empty<BusquedaVM>().AsQueryable();

            // Acción
            pagina.cargarFiltrosCanton(parametro);

            // Verificación
            Assert.AreEqual(0, pagina.cantonesV.Count());
        }

        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 2
        [TestMethod]
        public void busquedaSimple_ValidacionCargarFiltroCanton_DeberiaEstarLleno()
        {
            // Preparación
            MockDeContexto<LoCoMProContext> contexto = new MockDeContexto<LoCoMProContext>();
            IConfiguration configuracion = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            BusquedaModel pagina = new BusquedaModel(contexto.ObtenerObjetoDeMock(), configuracion);
            IQueryable<BusquedaVM> parametro = this.generarBusquedaVM(10);

            // Acción
            pagina.cargarFiltrosCanton(parametro);

            // Verificación
            Assert.AreEqual(10, pagina.cantonesV.Count());
        }

        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 2
        [TestMethod]
        public void busquedaSimple_ValidacionCargarFiltroTiendaNulo_DeberiaEstarVacio()
        {
            // Preparación
            MockDeContexto<LoCoMProContext> contexto = new MockDeContexto<LoCoMProContext>();
            IConfiguration configuracion = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            BusquedaModel pagina = new BusquedaModel(contexto.ObtenerObjetoDeMock(), configuracion);
            IQueryable<BusquedaVM> parametro = Enumerable.Empty<BusquedaVM>().AsQueryable();

            // Acción
            pagina.cargarFiltrosTienda(parametro);

            // Verificación
            Assert.AreEqual(0, pagina.tiendasV.Count());
        }

        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 2
        [TestMethod]
        public void busquedaSimple_ValidacionCargarFiltroTienda_DeberiaEstarLleno()
        {
            // Preparación
            MockDeContexto<LoCoMProContext> contexto = new MockDeContexto<LoCoMProContext>();
            IConfiguration configuracion = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            BusquedaModel pagina = new BusquedaModel(contexto.ObtenerObjetoDeMock(), configuracion);
            IQueryable<BusquedaVM> parametro = this.generarBusquedaVM(10);

            // Acción
            pagina.cargarFiltrosTienda(parametro);

            // Verificación
            Assert.AreEqual(10, pagina.tiendasV.Count());
        }

        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 2
        [TestMethod]
        public void busquedaSimple_ValidacionCargarFiltroMarcaNulo_DeberiaEstarVacio()
        {
            // Preparación
            MockDeContexto<LoCoMProContext> contexto = new MockDeContexto<LoCoMProContext>();
            IConfiguration configuracion = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            BusquedaModel pagina = new BusquedaModel(contexto.ObtenerObjetoDeMock(), configuracion);
            IQueryable<BusquedaVM> parametro = Enumerable.Empty<BusquedaVM>().AsQueryable();

            // Acción
            pagina.cargarFiltrosMarca(parametro);

            // Verificación
            Assert.AreEqual(0, pagina.marcasV.Count());
        }

        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 2
        [TestMethod]
        public void busquedaSimple_ValidacionCargarFiltroMarca_DeberiaEstarLleno()
        {
            // Preparación
            MockDeContexto<LoCoMProContext> contexto = new MockDeContexto<LoCoMProContext>();
            IConfiguration configuracion = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            BusquedaModel pagina = new BusquedaModel(contexto.ObtenerObjetoDeMock(), configuracion);
            IQueryable<BusquedaVM> parametro = this.generarBusquedaVM(10);

            // Acción
            pagina.cargarFiltrosMarca(parametro);

            // Verificación
            Assert.AreEqual(10, pagina.marcasV.Count());
        }

        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 2
        [TestMethod]
        public void busquedaSimple_ValidacionCargarFiltroCategoriaNulo_DeberiaEstarVacio()
        {
            // Preparación
            MockDeContexto<LoCoMProContext> contexto = new MockDeContexto<LoCoMProContext>();
            IConfiguration configuracion = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            BusquedaModel pagina = new BusquedaModel(contexto.ObtenerObjetoDeMock(), configuracion);
            IQueryable<BusquedaVM> parametro = Enumerable.Empty<BusquedaVM>().AsQueryable();

            // Acción
            pagina.cargarFiltrosCategoria(parametro);

            // Verificación
            Assert.AreEqual(0, pagina.categoriasV.Count());
        }

        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 2
        [TestMethod]
        public void busquedaSimple_ValidacionCargarFiltroCategoria_DeberiaEstarLleno()
        {
            // Preparación
            MockDeContexto<LoCoMProContext> contexto = new MockDeContexto<LoCoMProContext>();
            IConfiguration configuracion = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            BusquedaModel pagina = new BusquedaModel(contexto.ObtenerObjetoDeMock(), configuracion);
            IQueryable<BusquedaVM> parametro = this.generarBusquedaVM(10);

            // Acción
            pagina.cargarFiltrosCategoria(parametro);

            // Verificación
            Assert.AreEqual(10, pagina.categoriasV.Count());
        }
    }
}