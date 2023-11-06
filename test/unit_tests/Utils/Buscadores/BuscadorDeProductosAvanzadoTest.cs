using LoCoMProTests.Mocks;
using LoCoMPro.Models;
using LoCoMPro.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using LoCoMPro.Pages.Cuenta;
using LoCoMPro.Utils.Buscadores;

namespace LoCoMProTests.Utils.Buscadores
{
    // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
    [TestClass]
    public class BuscadorDeProductosAvanzadoTest
    {

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConMarca_ValidacionBusquedaProductoExistente_DeberiaDevolverSoloUnResultado()
        {
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            List<Registro> registros = new List<Registro>()
            {
                registro
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, producto.marca);

            var resultados = buscador.buscar();

            Assert.AreEqual(1, resultados.Count());
        }


        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConMarca_ValidacionBusquedaConHileraVacia_DeberiaDeberiaDevolverSoloUnResultado()
        {
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            List<Registro> registros = new List<Registro>()
            {
                registro
            };

            producto.registros = registros;
            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, "");
            var resultados = buscador.buscar();

            Assert.AreEqual(1, resultados.Count());
        }


        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConMarca_ValidacionBusquedaConHileraNula_DeberiaDeberiaDevolverSoloUnResultado()
        {
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            List<Registro> registros = new List<Registro>()
            {
                registro
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, null);
            var resultados = buscador.buscar();

            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConMarca_ValidacionBusquedaProductoConNombreVacio_DeberiaDeberiaDevolverCeroResultados()
        {
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            producto.nombre = "    ";

            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            List<Registro> registros = new List<Registro>()
            {
                registro
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, "    ");
            var resultados = buscador.buscar();

            Assert.AreEqual(0, resultados.Count());
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [TestMethod]
        public void buscadorDeProductos_ValidacionBusquedaVariosProductos_DeberiaDeberiaDevolverMasDeUnResultado()
        {
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto3 = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();

            producto1.nombre = "Producto1";
            producto1.marca = "Marca1";
            producto2.nombre = "Producto2";
            producto2.marca = "Marca2";
            producto3.nombre = "Producto3";
            producto3.marca = "Marca3";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda, usuario);
            Registro registro3 = CreadorDeModelos.CrearRegistroPorDefecto(producto3, tienda, usuario);

            List<Registro> registros = new List<Registro>()
            {
                registro1,
                registro2,
                registro3
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, "Marca");
            var resultados = buscador.buscar();

            Assert.IsTrue(resultados.Count() > 1);
        }

    }
}