using LoCoMPro.Data;
using LoCoMPro.Models;
using LoCoMPro.Utils.Buscadores;
using LoCoMProTests.Mocks;

namespace LoCoMProTests.Utils.Buscadores
{
    [TestClass]
    public class BuscadorDeOutliersPrecioTest
    {
        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [TestMethod]
        public void buscadorDeOutliersPrecio_ValidacionBusquedaSinRegistros_DeberiaDevolverNinguno()
        {
            // Preparación
            MockDeModelo<Registro> mockModelo = new();
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            // Acción
            BuscadorDeOutliersPrecio buscador = new(mockContexto.ObtenerObjetoDeMock());
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(0, resultados.Count());
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [TestMethod]
        public void buscadorDeOutliersPrecio_ValidacionBusquedaRegistrosVisibles_DeberiaDevolverNinguno()
        {
            // Preparación
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            registro1.visible = false;
            registro2.visible = false;

            List<Registro> registros = new()
            {
                registro1, registro2
            };

            MockDeModelo<Registro> mockModelo = new();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            // Acción
            BuscadorDeOutliersPrecio buscador = new(mockContexto.ObtenerObjetoDeMock());
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(0, resultados.Count());
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [TestMethod]
        public void buscadorDeOutliersPrecio_ValidacionBusquedaOutliers_DeberiaDevolverCero()
        {
            // Preparación
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro3 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro4 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro5 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            registro1.precio = 1;
            registro2.precio = 1;
            registro3.precio = 1;
            registro4.precio = 1;
            registro5.precio = 1;

            List<Registro> registros = new()
            {
                registro1, registro2, registro3, registro4, registro5
            };

            MockDeModelo<Registro> mockModelo = new();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            // Acción
            BuscadorDeOutliersPrecio buscador = new(mockContexto.ObtenerObjetoDeMock());
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(0, resultados.Count());
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [TestMethod]
        public void buscadorDeOutliersPrecio_ValidacionBusquedaOutliersCantidadImpar_DeberiaDevolverUno()
        {
            // Preparación
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro3 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro4 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro5 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            registro1.precio = 150;
            registro2.precio = 15;
            registro3.precio = 20;
            registro4.precio = 25;
            registro5.precio = 10;

            List<Registro> registros = new()
            {
                registro1, registro2, registro3, registro4, registro5
            };

            MockDeModelo<Registro> mockModelo = new();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            // Acción
            BuscadorDeOutliersPrecio buscador = new(mockContexto.ObtenerObjetoDeMock());
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [TestMethod]
        public void buscadorDeOutliersPrecio_ValidacionBusquedaOutliersCantidadPar_DeberiaDevolverUno()
        {
            // Preparación
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro3 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro4 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro5 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro6 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            registro1.precio = 1;
            registro2.precio = 2;
            registro3.precio = 3;
            registro4.precio = 4;
            registro5.precio = 5;
            registro6.precio = 100;

            List<Registro> registros = new()
            {
                registro1, registro2, registro3, registro4, registro5, registro6
            };

            MockDeModelo<Registro> mockModelo = new();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            // Acción
            BuscadorDeOutliersPrecio buscador = new(mockContexto.ObtenerObjetoDeMock());
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [TestMethod]
        public void buscadorDeOutliersPrecio_ValidacionBusquedaOutliersCantidadImpar_DeberiaDevolverDos()
        {
            // Preparación
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro3 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro4 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro5 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro6 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro7 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            registro1.precio = 1;
            registro2.precio = 101;
            registro3.precio = 102;
            registro4.precio = 103;
            registro5.precio = 104;
            registro6.precio = 105;
            registro7.precio = 206;

            List<Registro> registros = new()
            {
                registro1, registro2, registro3, registro4, registro5, registro6, registro7
            };

            MockDeModelo<Registro> mockModelo = new();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            // Acción
            BuscadorDeOutliersPrecio buscador = new(mockContexto.ObtenerObjetoDeMock());
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(2, resultados.Count());
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [TestMethod]
        public void buscadorDeOutliersPrecio_ValidacionBusquedaOutliersCantidadPar_DeberiaDevolverDos()
        {
            // Preparación
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro3 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro4 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro5 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro6 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro7 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro8 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            registro1.precio = 1;
            registro2.precio = 101;
            registro3.precio = 102;
            registro4.precio = 103;
            registro5.precio = 104;
            registro6.precio = 105;
            registro7.precio = 106;
            registro8.precio = 207;

            List<Registro> registros = new()
            {
                registro1, registro2, registro3, registro4, registro5, registro6, registro7, registro8
            };

            MockDeModelo<Registro> mockModelo = new();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            // Acción
            BuscadorDeOutliersPrecio buscador = new(mockContexto.ObtenerObjetoDeMock());
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(2, resultados.Count());
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [TestMethod]
        public void buscadorDeOutliersPrecio_ValidacionBusquedaOutliersDosTiendas_DeberiaDevolverUno()
        {
            // Preparación
            Tienda tienda1 = CreadorDeModelos.CrearTiendaPorDefecto();
            Tienda tienda2 = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro11 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda1, usuario);
            Registro registro12 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda1, usuario);
            Registro registro13 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda1, usuario);
            Registro registro14 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda1, usuario);
            Registro registro15 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda1, usuario);
            Registro registro21 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda2, usuario);
            Registro registro22 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda2, usuario);
            Registro registro23 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda2, usuario);
            Registro registro24 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda2, usuario);
            Registro registro25 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda2, usuario);

            registro11.precio = 1;
            registro12.precio = 100;
            registro13.precio = 3;
            registro14.precio = 4;
            registro15.precio = 2;
            registro21.precio = 1;
            registro22.precio = 5;
            registro23.precio = 4;
            registro24.precio = 3;
            registro25.precio = 2;

            List<Registro> registros = new()
            {
                registro11, registro12, registro13, registro14, registro15,
                registro21, registro22, registro23, registro24, registro25
            };

            MockDeModelo<Registro> mockModelo = new();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeOutliersPrecio buscador = new(mockContexto.ObtenerObjetoDeMock());

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [TestMethod]
        public void buscadorDeOutliersPrecio_ValidacionBusquedaOutliersDosTiendas_DeberiaDevolverDos()
        {
            // Preparación
            Tienda tienda1 = CreadorDeModelos.CrearTiendaPorDefecto();
            Tienda tienda2 = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro11 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda1, usuario);
            Registro registro12 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda1, usuario);
            Registro registro13 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda1, usuario);
            Registro registro14 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda1, usuario);
            Registro registro15 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda1, usuario);
            Registro registro21 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda2, usuario);
            Registro registro22 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda2, usuario);
            Registro registro23 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda2, usuario);
            Registro registro24 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda2, usuario);
            Registro registro25 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda2, usuario);

            registro11.precio = 1;
            registro12.precio = 100;
            registro13.precio = 3;
            registro14.precio = 4;
            registro15.precio = 2;
            registro21.precio = 1;
            registro22.precio = 5;
            registro23.precio = 4;
            registro24.precio = 3;
            registro25.precio = 100;

            List<Registro> registros = new()
            {
                registro11, registro12, registro13, registro14, registro15,
                registro21, registro22, registro23, registro24, registro25
            };

            MockDeModelo<Registro> mockModelo = new();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeOutliersPrecio buscador = new(mockContexto.ObtenerObjetoDeMock());

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(2, resultados.Count());
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [TestMethod]
        public void buscadorDeOutliersPrecio_ValidacionBusquedaOutliersDosProductos_DeberiaDevolverUno()
        {
            // Preparación
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro11 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda, usuario);
            Registro registro12 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda, usuario);
            Registro registro13 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda, usuario);
            Registro registro14 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda, usuario);
            Registro registro15 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda, usuario);
            Registro registro21 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda, usuario);
            Registro registro22 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda, usuario);
            Registro registro23 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda, usuario);
            Registro registro24 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda, usuario);
            Registro registro25 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda, usuario);

            registro11.precio = 1;
            registro12.precio = 100;
            registro13.precio = 3;
            registro14.precio = 4;
            registro15.precio = 2;
            registro21.precio = 1;
            registro22.precio = 5;
            registro23.precio = 4;
            registro24.precio = 3;
            registro25.precio = 2;

            List<Registro> registros = new()
            {
                registro11, registro12, registro13, registro14, registro15,
                registro21, registro22, registro23, registro24, registro25
            };

            MockDeModelo<Registro> mockModelo = new();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeOutliersPrecio buscador = new(mockContexto.ObtenerObjetoDeMock());

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 3
        [TestMethod]
        public void buscadorDeOutliersPrecio_ValidacionBusquedaOutliersDosProductos_DeberiaDevolverDos()
        {
            // Preparación
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro11 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda, usuario);
            Registro registro12 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda, usuario);
            Registro registro13 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda, usuario);
            Registro registro14 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda, usuario);
            Registro registro15 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda, usuario);
            Registro registro21 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda, usuario);
            Registro registro22 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda, usuario);
            Registro registro23 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda, usuario);
            Registro registro24 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda, usuario);
            Registro registro25 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda, usuario);

            registro11.precio = 1;
            registro12.precio = 100;
            registro13.precio = 3;
            registro14.precio = 4;
            registro15.precio = 2;
            registro21.precio = 1;
            registro22.precio = 5;
            registro23.precio = 4;
            registro24.precio = 100;
            registro25.precio = 2;

            List<Registro> registros = new()
            {
                registro11, registro12, registro13, registro14, registro15,
                registro21, registro22, registro23, registro24, registro25
            };

            MockDeModelo<Registro> mockModelo = new();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeOutliersPrecio buscador = new(mockContexto.ObtenerObjetoDeMock());

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(2, resultados.Count());
        }
    }
}
