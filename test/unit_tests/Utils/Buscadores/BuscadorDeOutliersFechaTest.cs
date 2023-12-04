using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.Utils.Buscadores;
using LoCoMProTests.Mocks;

namespace LoCoMProTests.Utils.Buscadores
{
    // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 3
    [TestClass]
    public class BuscadorDeOutliersTest
    {
        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 3
        [TestMethod]
        public void buscadorDeOutliersFecha_ValidacionBusquedaSinRegistros_DeberiaDevolverCeroResultados()
        {
            // Preparación

            // Crear lista de registros
            List<Registro> registros = new List<Registro>()
            {
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeOutliersFecha buscador = new BuscadorDeOutliersFecha(mockContexto.ObtenerObjetoDeMock());

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(0, resultados.Count());
        }

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 3
        [TestMethod]
        public void buscadorDeOutliersFecha_ValidacionBusquedaSinOutliers_DeberiaDevolverCeroResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            // Crear lista de registros
            List<Registro> registros = new List<Registro>()
            {
                registro
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeOutliersFecha buscador = new BuscadorDeOutliersFecha(mockContexto.ObtenerObjetoDeMock());

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(0, resultados.Count());
        }
    }
}