using LoCoMProTests.Mocks;
using LoCoMPro.Models;
using LoCoMPro.Data;
using LoCoMPro.Utils.Buscadores;

namespace LoCoMProTests.Utils.Buscadores
{
    // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 2
    [TestClass]
    public class BuscadorDeAportesTest
    {

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 2
        [TestMethod]
        public void buscadorDeAportes_ValidacionBusquedaAporteExistente_DeberiaDevolverSoloUnResultado()
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

            BuscadorDeAportes buscador = new BuscadorDeAportes(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 2
        [TestMethod]
        public void buscadorDeAportes_ValidacionBusquedaUsuarioConVariosAportes_DeberiaDevolverCincoResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro3 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro4 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro5 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            // Crear lista de registros
            List<Registro> registros = new List<Registro>()
            {
                registro1,
                registro2,
                registro3,
                registro4,
                registro5
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeAportes buscador = new BuscadorDeAportes(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(5, resultados.Count());
        }

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 2
        [TestMethod]
        public void buscadorDeAportes_ValidacionBusquedaUsuarioConVariosAportes_DeberiaDevolverElMasReciente()
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

            // Crear lista de registros
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

            BuscadorDeAportes buscador = new BuscadorDeAportes(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            if (resultados.Count() > 0) Assert.AreEqual(resultados.FirstOrDefault().fecha, registros[0].creacion);
            else Assert.Fail();
        }

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 2
        [TestMethod]
        public void buscadorDeAportes_ValidacionBusquedaSinAportes_DeberiaDeberiaDevolverCeroResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            // Crear lista vacia
            List<Registro> registros = new List<Registro>()
            {
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeProductos buscador = new BuscadorDeProductos(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(0, resultados.Count());
        }

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 2
        [TestMethod]
        public void buscadorDeAportes_ValidacionBusquedaAporteDeOtroUsuario_DeberiaDeberiaDevolverCeroResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario1 = CreadorDeModelos.CrearUsuarioPorDefecto();
            Usuario usuario2 = CreadorDeModelos.CrearUsuarioPorDefecto();

            // Modificar nombres de usuario
            usuario1.nombreDeUsuario = "Usuario1";
            usuario2.nombreDeUsuario = "Usuario2";
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario2);

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

            BuscadorDeAportes buscador = new BuscadorDeAportes(mockContexto.ObtenerObjetoDeMock(), usuario1.nombreDeUsuario);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(0, resultados.Count());
        }

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 2
        [TestMethod]
        public void buscadorDeAportes_ValidacionBusquedaConUsuarioNulo_DeberiaDeberiaDevolverCeroResultados()
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

            BuscadorDeAportes buscador = new BuscadorDeAportes(mockContexto.ObtenerObjetoDeMock(), null);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(0, resultados.Count());
        }

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 2
        [TestMethod]
        public void buscadorDeAportes_ValidacionBusquedaConAportesOcultos_DeberiaDeberiaDevolverDosResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro3 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);

            // Modificar la visibilidad
            registro2.visible = false;

            // Crear registro especifico
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

            BuscadorDeAportes buscador = new BuscadorDeAportes(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(2, resultados.Count());
        }

        // Hecho por: Kenneth Daniel Villalobos Sólís - C18548 - Sprint 2
        [TestMethod]
        public void buscadorDeAportes_ValidacionBusquedaConAportesDeMultiplesUsuarios_DeberiaDeberiaDevolverCuatroResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario1 = CreadorDeModelos.CrearUsuarioPorDefecto();
            Usuario usuario2 = CreadorDeModelos.CrearUsuarioPorDefecto();
            Usuario usuario3 = CreadorDeModelos.CrearUsuarioPorDefecto();

            // Modificar nombres de usuario
            usuario1.nombreDeUsuario = "Usuario1";
            usuario2.nombreDeUsuario = "Usuario2";
            usuario3.nombreDeUsuario = "Usuario3";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario1);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario1);
            Registro registro3 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario2);
            Registro registro4 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario3);
            Registro registro5 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario1);
            Registro registro6 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario1);
            Registro registro7 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario3);
            Registro registro8 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario2);
            Registro registro9 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario3);

            // Crear registro especifico
            List<Registro> registros = new List<Registro>()
            {
                registro1,
                registro2,
                registro3,
                registro4,
                registro5,
                registro6,
                registro7,
                registro8,
                registro9
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            BuscadorDeAportes buscador = new BuscadorDeAportes(mockContexto.ObtenerObjetoDeMock(), usuario1.nombreDeUsuario);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(4, resultados.Count());
        }
    }
}