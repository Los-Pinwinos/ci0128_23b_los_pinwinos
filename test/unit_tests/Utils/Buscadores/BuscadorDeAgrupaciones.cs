using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.Utils.Buscadores;
using LoCoMProTests.Mocks;

namespace LoCoMProTests.Utils.Buscadores
{
    // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 3
    [TestClass]
    public class BuscadorDeAgrupacionesTest
    {
        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 3
        [TestMethod]
        public void buscadorDeAgrupaciones_ValidacionBusquedaSinRegistros_DeberiaDevolverCeroResultados()
        {
            // Preparación
            // Crear lista de productos
            List<Producto> productos = new List<Producto>()
            {
            };

            MockDeModelo<Producto> mockModelo = new MockDeModelo<Producto>();
            mockModelo.AgregarRangoInstanciasDeModelo(productos);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Productos, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeAgrupaciones buscador = new BuscadorDeAgrupaciones(mockContexto.ObtenerObjetoDeMock());

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(0, resultados.Count());
        }

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 3
        [TestMethod]
        public void buscadorDeAgrupaciones_ValidacionBusquedaSinGrupos_DeberiaDevolverCeroResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            producto1.nombre = "Almohada";
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            producto2.nombre = "Pijama";

            // Crear lista de productos
            List<Producto> productos = new List<Producto>()
            {
                producto1,
                producto2
            };

            MockDeModelo<Producto> mockModelo = new MockDeModelo<Producto>();
            mockModelo.AgregarRangoInstanciasDeModelo(productos);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Productos, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeAgrupaciones buscador = new BuscadorDeAgrupaciones(mockContexto.ObtenerObjetoDeMock());

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(0, resultados.Count());
        }

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 3
        [TestMethod]
        public void buscadorDeAgrupaciones_ValidacionBusquedaUnGrupoSimple_DeberiaDevolverUnResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            producto1.nombre = "Camisa XL";
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            producto2.nombre = "Camisa XS";

            // Crear lista de productos
            List<Producto> productos = new List<Producto>()
            {
                producto1,
                producto2
            };

            MockDeModelo<Producto> mockModelo = new MockDeModelo<Producto>();
            mockModelo.AgregarRangoInstanciasDeModelo(productos);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Productos, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeAgrupaciones buscador = new BuscadorDeAgrupaciones(mockContexto.ObtenerObjetoDeMock());

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 3
        [TestMethod]
        public void buscadorDeAgrupaciones_ValidacionBusquedaUnGrupoComplejo_DeberiaDevolverUnResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            producto1.nombre = "Camisa";
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            producto2.nombre = "Camiseta";
            Producto producto3 = CreadorDeModelos.CrearProductoPorDefecto();
            producto3.nombre = "Camison";
            Producto producto4 = CreadorDeModelos.CrearProductoPorDefecto();
            producto4.nombre = "Camis3ta";
            Producto producto5 = CreadorDeModelos.CrearProductoPorDefecto();
            producto5.nombre = "Camisita";

            // Crear lista de productos
            List<Producto> productos = new List<Producto>()
            {
                producto1,
                producto2,
                producto3,
                producto4,
                producto5
            };

            MockDeModelo<Producto> mockModelo = new MockDeModelo<Producto>();
            mockModelo.AgregarRangoInstanciasDeModelo(productos);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Productos, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeAgrupaciones buscador = new BuscadorDeAgrupaciones(mockContexto.ObtenerObjetoDeMock());

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 3
        [TestMethod]
        public void buscadorDeAgrupaciones_ValidacionBusquedaMulltiplesGruposSimples_DeberiaDevolverDosResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            producto1.nombre = "Camisa XL";
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            producto2.nombre = "Camisa XS";
            Producto producto3 = CreadorDeModelos.CrearProductoPorDefecto();
            producto3.nombre = "Pantalón XL";
            Producto producto4 = CreadorDeModelos.CrearProductoPorDefecto();
            producto4.nombre = "Pantalón XS";

            // Crear lista de productos
            List<Producto> productos = new List<Producto>()
            {
                producto1,
                producto2,
                producto3,
                producto4
            };

            MockDeModelo<Producto> mockModelo = new MockDeModelo<Producto>();
            mockModelo.AgregarRangoInstanciasDeModelo(productos);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Productos, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeAgrupaciones buscador = new BuscadorDeAgrupaciones(mockContexto.ObtenerObjetoDeMock());

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(2, resultados.Count());
        }

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 3
        [TestMethod]
        public void buscadorDeAgrupaciones_ValidacionBusquedaMulltiplesGruposComplejos_DeberiaDevolverDosResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            producto1.nombre = "Camisa";
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            producto2.nombre = "Camiseta";
            Producto producto3 = CreadorDeModelos.CrearProductoPorDefecto();
            producto3.nombre = "Camison";
            Producto producto4 = CreadorDeModelos.CrearProductoPorDefecto();
            producto4.nombre = "Camis3ta";
            Producto producto5 = CreadorDeModelos.CrearProductoPorDefecto();
            producto5.nombre = "Camisita";

            Producto producto6 = CreadorDeModelos.CrearProductoPorDefecto();
            producto6.nombre = "Pantalon";
            Producto producto7 = CreadorDeModelos.CrearProductoPorDefecto();
            producto7.nombre = "Pantalón";
            Producto producto8 = CreadorDeModelos.CrearProductoPorDefecto();
            producto8.nombre = "Pataloneta";
            Producto producto9 = CreadorDeModelos.CrearProductoPorDefecto();
            producto9.nombre = "Pantalones";
            Producto producto10 = CreadorDeModelos.CrearProductoPorDefecto();
            producto10.nombre = "Panta";

            // Crear lista de productos
            List<Producto> productos = new List<Producto>()
            {
                producto1,
                producto2,
                producto3,
                producto4,
                producto5,
                producto6,
                producto7,
                producto8,
                producto9,
                producto10
            };

            MockDeModelo<Producto> mockModelo = new MockDeModelo<Producto>();
            mockModelo.AgregarRangoInstanciasDeModelo(productos);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Productos, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeAgrupaciones buscador = new BuscadorDeAgrupaciones(mockContexto.ObtenerObjetoDeMock());

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(2, resultados.Count());
        }

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 3
        [TestMethod]
        public void buscadorDeAgrupaciones_ValidacionBusquedaGruposSimplesYProductosNormales_DeberiaDevolverDosResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            producto1.nombre = "Camisa XL";
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            producto2.nombre = "Camisa XS";
            Producto producto3 = CreadorDeModelos.CrearProductoPorDefecto();
            producto3.nombre = "Gorra S";
            Producto producto4 = CreadorDeModelos.CrearProductoPorDefecto();
            producto4.nombre = "Gorra M";
            Producto producto5 = CreadorDeModelos.CrearProductoPorDefecto();
            producto5.nombre = "Pantalón";

            // Crear lista de productos
            List<Producto> productos = new List<Producto>()
            {
                producto1,
                producto2,
                producto3,
                producto4,
                producto5
            };

            MockDeModelo<Producto> mockModelo = new MockDeModelo<Producto>();
            mockModelo.AgregarRangoInstanciasDeModelo(productos);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Productos, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeAgrupaciones buscador = new BuscadorDeAgrupaciones(mockContexto.ObtenerObjetoDeMock());

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(2, resultados.Count());
        }

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 3
        [TestMethod]
        public void buscadorDeAgrupaciones_ValidacionBusquedaGruposComplejosYProductosNormales_DeberiaDevolverDosResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Producto producto0 = CreadorDeModelos.CrearProductoPorDefecto();
            producto0.nombre = "Goma";

            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            producto1.nombre = "Camisa";
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            producto2.nombre = "Camiseta";
            Producto producto3 = CreadorDeModelos.CrearProductoPorDefecto();
            producto3.nombre = "Camison";
            Producto producto4 = CreadorDeModelos.CrearProductoPorDefecto();
            producto4.nombre = "Camis3ta";
            Producto producto5 = CreadorDeModelos.CrearProductoPorDefecto();
            producto5.nombre = "Camisita";

            Producto producto6 = CreadorDeModelos.CrearProductoPorDefecto();
            producto6.nombre = "Cuaderno";

            Producto producto7 = CreadorDeModelos.CrearProductoPorDefecto();
            producto7.nombre = "Pantalón";
            Producto producto8 = CreadorDeModelos.CrearProductoPorDefecto();
            producto8.nombre = "Pataloneta";
            Producto producto9 = CreadorDeModelos.CrearProductoPorDefecto();
            producto9.nombre = "Pantalones";
            Producto producto10 = CreadorDeModelos.CrearProductoPorDefecto();
            producto10.nombre = "Panta";
            Producto producto11 = CreadorDeModelos.CrearProductoPorDefecto();
            producto11.nombre = "Pantalon";

            Producto producto12 = CreadorDeModelos.CrearProductoPorDefecto();
            producto12.nombre = "Juguete";

            // Crear lista de productos
            List<Producto> productos = new List<Producto>()
            {
                producto0,
                producto1,
                producto2,
                producto3,
                producto4,
                producto5,
                producto6,
                producto7,
                producto8,
                producto9,
                producto10,
                producto11,
                producto12
            };

            MockDeModelo<Producto> mockModelo = new MockDeModelo<Producto>();
            mockModelo.AgregarRangoInstanciasDeModelo(productos);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Productos, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeAgrupaciones buscador = new BuscadorDeAgrupaciones(mockContexto.ObtenerObjetoDeMock());

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(2, resultados.Count());
        }
    }
}