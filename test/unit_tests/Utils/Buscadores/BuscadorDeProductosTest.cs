using LoCoMProTests.Mocks;
using LoCoMPro.Models;
using LoCoMPro.Data;
using LoCoMPro.Utils.Buscadores;
using LoCoMProTests;

namespace LoCoMProTests.Utils.Buscadores
{
    // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 2
    [TestClass]
    public class BuscadorDeProductosTest
    {
        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 2
        [TestMethod]
        public void buscadorDeProductos_ValidacionBusquedaProductoExistente_DeberiaDevolverSoloUnResultado()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            // Crear registro especifico
            List<Registro> registros = new List<Registro>()
            {
                registro
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeProductos buscador = new BuscadorDeProductos(mockContexto.ObtenerObjetoDeMock(), producto.nombre);
            
            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 2
        [TestMethod]
        public void buscadorDeProductos_ValidacionBusquedaProductoConVariosRegistros_DeberiaDevolverSoloUnResultado()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            // Crear registro especifico
            List<Registro> registros = new List<Registro>()
            {
                registro1,
                registro2
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeProductos buscador = new BuscadorDeProductos(mockContexto.ObtenerObjetoDeMock(), producto.nombre);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 2
        [TestMethod]
        public void buscadorDeProductos_ValidacionBusquedaProductoConVariosRegistros_DeberiaDevolverElMasReciente()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            // Modificar fechas
            registro1.creacion = DateTime.Parse("2023-11-02");
            registro2.creacion = DateTime.Parse("2023-11-01");

            // Crear registro especifico
            List<Registro> registros = new List<Registro>()
            {
                registro1,
                registro2
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeProductos buscador = new BuscadorDeProductos(mockContexto.ObtenerObjetoDeMock(), producto.nombre);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            if (resultados.Count() > 0) Assert.AreEqual(resultados.FirstOrDefault().fecha, registros[0].creacion);
            else Assert.Fail();
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 2
        [TestMethod]
        public void buscadorDeProductos_ValidacionBusquedaProductoSinRegistros_DeberiaDeberiaDevolverCeroResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            // Crear lista vacia
            List<Registro> registros = new List<Registro>()
            {
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeProductos buscador = new BuscadorDeProductos(mockContexto.ObtenerObjetoDeMock(), producto.nombre);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(0, resultados.Count());
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 2
        [TestMethod]
        public void buscadorDeProductos_ValidacionBusquedaConHileraVacia_DeberiaDeberiaDevolverSoloUnResultado()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            // Crear registro especifico
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

            BuscadorDeProductos buscador = new BuscadorDeProductos(mockContexto.ObtenerObjetoDeMock(), "");

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 2
        [TestMethod]
        public void buscadorDeProductos_ValidacionBusquedaConHileraNula_DeberiaDeberiaDevolverSoloUnResultado()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            // Crear registro especifico
            List<Registro> registros = new List<Registro>()
            {
                registro
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeProductos buscador = new BuscadorDeProductos(mockContexto.ObtenerObjetoDeMock(), null);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 2
        [TestMethod]
        public void buscadorDeProductos_ValidacionBusquedaProductoConNombreVacio_DeberiaDeberiaDevolverUnSoloResultado()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            producto.nombre = "    ";

            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);


            producto.nombre = "    ";
            // Crear registro especifico
            List<Registro> registros = new List<Registro>()
            {
                registro
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeProductos buscador = new BuscadorDeProductos(mockContexto.ObtenerObjetoDeMock(), "    ");

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 2
        [TestMethod]
        public void buscadorDeProductos_ValidacionBusquedaVariosProductos_DeberiaDeberiaDevolverMasDeUnResultado()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();

            producto1.nombre = "Producto1";
            producto2.nombre = "Producto2";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda, usuario);

            // Crear registros especifico
            List<Registro> registros = new List<Registro>()
            {
                registro1,
                registro2
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeProductos buscador = new BuscadorDeProductos(mockContexto.ObtenerObjetoDeMock(), "Producto");

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.IsTrue(resultados.Count() > 1);
        }
    }
}