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
            // Preparación
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

            // Acción
            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, producto.marca);
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }


        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConMarca_ValidacionBusquedaConHileraVacia_DeberiaDeberiaDevolverSoloUnResultado()
        {
            // Preparación
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

            // Acción
            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, "");
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }


        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConMarca_ValidacionBusquedaConHileraNula_DeberiaDeberiaDevolverSoloUnResultado()
        {
            // Preparación
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

            // Acción
            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, null);
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConMarca_ValidacionBusquedaProductoConNombreVacio_DeberiaDeberiaDevolverCeroResultados()
        {
            // Preparación
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

            // Acción
            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, "    ");
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(0, resultados.Count());
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConMarca_ValidacionBusquedaVariosProductos_DeberiaDeberiaDevolverMasDeUnResultado()
        {
            // Preparación
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

            // Acción
            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, "Marca");
            var resultados = buscador.buscar();

            // Verificación
            Assert.IsTrue(resultados.Count() > 1);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConProvincia_ValidacionBusquedaVariosProductosMarcas_DeberiaDeberiaDevolverDosResultados()
        {
            // Preparación
            Tienda tienda1 = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto3 = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();

            producto1.nombre = "Producto1";
            producto1.marca = "Marca1";
            producto2.nombre = "Producto2";
            producto2.marca = "Marca2";
            producto3.nombre = "Producto3";
            producto3.marca = "Marca1";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda1, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda1, usuario);
            Registro registro3 = CreadorDeModelos.CrearRegistroPorDefecto(producto3, tienda1, usuario);

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

            // Acción
            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), "Producto", "Marca1");
            var resultados = buscador.buscar();

            // Verificación
            Assert.IsTrue(resultados.Count() == 2);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConProvincia_ValidacionBusquedaProvincia_DeberiaDevolverSoloUnResultado()
        {
            // Preparación
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

            // Acción
            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, null, registro.nombreProvincia);

            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConProvincia_ValidacionBusquedaProvinciaConHileraVacia_DeberiaDeberiaDevolverSoloUnResultado()
        {
            // Preparación
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

            // Acción
            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, null, "");
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConProvincia_ValidacionBusquedaProvinciaConHileraNula_DeberiaDeberiaDevolverSoloUnResultado()
        {
            // Preparación
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

            // Acción
            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, null, null);
            var resultados = buscador.buscar();

            // Verficación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConProvincia_ValidacionBusquedaProvinciaConNombreVacio_DeberiaDeberiaDevolverCeroResultados()
        {
            // Preparación
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

            // Acción
            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, null, "    ");
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(0, resultados.Count());
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConProvincia_ValidacionBusquedaVariasProvincias_DeberiaDeberiaDevolverTresResultados()
        {
            // Preparación
            Tienda tienda1 = CreadorDeModelos.CrearTiendaPorDefecto();
            Tienda tienda2 = CreadorDeModelos.CrearTiendaPorDefecto();
            Tienda tienda3 = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();

            tienda1.nombreProvincia = "Provincia1";
            tienda2.nombreProvincia = "Provincia2";
            tienda3.nombreProvincia = "Provincia3";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda1, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda2, usuario);
            Registro registro3 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda3, usuario);

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

            // Acción
            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, null, "Provincia");
            var resultados = buscador.buscar();

            // Verificación
            Assert.IsTrue(resultados.Count() == 3);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConProvincia_ValidacionBusquedaVariasProvincias_DeberiaDeberiaDevolverUnResultados()
        {
            // Preparación
            Tienda tienda1 = CreadorDeModelos.CrearTiendaPorDefecto();
            Tienda tienda2 = CreadorDeModelos.CrearTiendaPorDefecto();
            Tienda tienda3 = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();

            tienda1.nombreProvincia = "Provincia1";
            tienda2.nombreProvincia = "Provincia2";
            tienda3.nombreProvincia = "Provincia3";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda1, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda2, usuario);
            Registro registro3 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda3, usuario);

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

            // Acción
            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, null, "Provincia1");
            var resultados = buscador.buscar();

            // Verificación
            Assert.IsTrue(resultados.Count() == 1);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConProvincia_ValidacionBusquedaVariosProductosProvincias_DeberiaDeberiaDevolverUnResultado()
        {
            // Preparación
            Tienda tienda1 = CreadorDeModelos.CrearTiendaPorDefecto();
            Tienda tienda2 = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();

            producto1.nombre = "Producto1";
            producto2.nombre = "Producto2";
            tienda1.nombreProvincia = "Provincia1";
            tienda2.nombreProvincia = "Provincia2";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda1, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda2, usuario);
            Registro registro3 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda2, usuario);

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

            // Acción
            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), "Producto", null, "Provincia1");
            var resultados = buscador.buscar();

            // Verificación
            Assert.IsTrue(resultados.Count() == 1);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 2
        [TestMethod]
        public void buscadorDeProductosAvanzadoConProvincia_ValidacionBusquedaVariasMarcasProvincias_DeberiaDeberiaDevolverUnResultado()
        {
            // Preparación
            Tienda tienda1 = CreadorDeModelos.CrearTiendaPorDefecto();
            Tienda tienda2 = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();

            producto1.marca = "Marca1";
            producto2.marca = "Marca2";
            tienda1.nombreProvincia = "Provincia1";
            tienda2.nombreProvincia = "Provincia2";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda1, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda2, usuario);
            Registro registro3 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda2, usuario);

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

            // Acción
            BuscadorDeProductosAvanzado buscador = new BuscadorDeProductosAvanzado(mockContexto.ObtenerObjetoDeMock(), null, "Marca1", "Provincia1");
            var resultados = buscador.buscar();

            // Verificación
            Assert.IsTrue(resultados.Count() == 1);
        }
    }
}