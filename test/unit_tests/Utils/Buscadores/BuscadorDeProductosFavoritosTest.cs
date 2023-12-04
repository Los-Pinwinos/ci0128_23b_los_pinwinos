using LoCoMProTests.Mocks;
using LoCoMPro.Models;
using LoCoMPro.Data;
using LoCoMPro.Utils.Buscadores;
using LoCoMProTests;

namespace LoCoMProTests.Utils.Buscadores
{
    // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
    [TestClass]
    public class BuscadorDeProductosFavoritosTest
    {
        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
        [TestMethod]
        public void buscadorDeProductosFavoritos_ValidacionBusquedaSinFavoritos_DeberiaDevolverCeroResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto();

            // Crear registro especifico
            List<Registro> registros = new List<Registro>()
            {
                registro
            };

            // Crear los mocks
            MockDeModelo<Registro> mockModeloRegistros = new MockDeModelo<Registro>();
            mockModeloRegistros.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistros.Configurar();

            MockDeModelo<Usuario> mockModeloUsuarios = new MockDeModelo<Usuario>();
            mockModeloUsuarios.AgregarInstanciaDeModelo(usuario);
            mockModeloUsuarios.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistros.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuarios.ObtenerObjetoDeMock());

            BuscadorDeProductosFavoritos buscador = new BuscadorDeProductosFavoritos(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario);
            
            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(0, resultados.Count());
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
        [TestMethod]
        public void buscadorDeProductosFavoritos_ValidacionBusquedaVariosFavoritosEnUnaTienda_DeberiaDevolverUnResultado()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            usuario.nombreDeUsuario = "ElFavorito";

            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            producto1.nombre = "Producto1";
            producto2.nombre = "Producto2";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda);

            usuario.favoritos.Add(producto1);
            usuario.favoritos.Add(producto2);

            // Crear registros especificos
            List<Registro> registros = new List<Registro>()
            {
                registro1,
                registro2
            };

            // Crear los mocks
            MockDeModelo<Registro> mockModeloRegistros = new MockDeModelo<Registro>();
            mockModeloRegistros.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistros.Configurar();

            MockDeModelo<Usuario> mockModeloUsuarios = new MockDeModelo<Usuario>();
            mockModeloUsuarios.AgregarInstanciaDeModelo(usuario);
            mockModeloUsuarios.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistros.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuarios.ObtenerObjetoDeMock());

            BuscadorDeProductosFavoritos buscador = new BuscadorDeProductosFavoritos(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
        [TestMethod]
        public void buscadorDeProductosFavoritos_ValidacionBusquedaUnFavoritoEnVariasTiendas_DeberiaDevolverMasDeUnResultado()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda1 = CreadorDeModelos.CrearTiendaPorDefecto();
            Tienda tienda2 = CreadorDeModelos.CrearTiendaPorDefecto();
            tienda1.nombre = "Tienda1";
            tienda2.nombre = "Tienda2";

            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            usuario.nombreDeUsuario = "ElFavorito";

            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda1);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda2);

            usuario.favoritos.Add(producto);

            // Crear registros especificos
            List<Registro> registros = new List<Registro>()
            {
                registro1,
                registro2
            };

            // Crear los mocks
            MockDeModelo<Registro> mockModeloRegistros = new MockDeModelo<Registro>();
            mockModeloRegistros.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistros.Configurar();

            MockDeModelo<Usuario> mockModeloUsuarios = new MockDeModelo<Usuario>();
            mockModeloUsuarios.AgregarInstanciaDeModelo(usuario);
            mockModeloUsuarios.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistros.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuarios.ObtenerObjetoDeMock());

            BuscadorDeProductosFavoritos buscador = new BuscadorDeProductosFavoritos(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.IsTrue(resultados.Count() > 1);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
        [TestMethod]
        public void buscadorDeProductosFavoritos_ValidacionBusquedaVariosFavoritosEnVariasTiendas_DeberiaDevolverResultadosIgualesALaCantidadDeTiendas()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda1 = CreadorDeModelos.CrearTiendaPorDefecto();
            Tienda tienda2 = CreadorDeModelos.CrearTiendaPorDefecto();
            tienda1.nombre = "Tienda1";
            tienda2.nombre = "Tienda2";

            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            usuario.nombreDeUsuario = "ElFavorito";

            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            producto1.nombre = "Producto1";
            producto2.nombre = "Producto2";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda1);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda2);
            Registro registro3 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda2);
            Registro registro4 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda1);

            usuario.favoritos.Add(producto1);
            usuario.favoritos.Add(producto2);

            // Crear registros especificos
            List<Registro> registros = new List<Registro>()
            {
                registro1,
                registro2,
                registro3,
                registro4
            };

            List<Tienda> tiendas = new List<Tienda>()
            {
                tienda1,
                tienda2
            };

            // Crear los mocks
            MockDeModelo<Registro> mockModeloRegistros = new MockDeModelo<Registro>();
            mockModeloRegistros.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistros.Configurar();

            MockDeModelo<Usuario> mockModeloUsuarios = new MockDeModelo<Usuario>();
            mockModeloUsuarios.AgregarInstanciaDeModelo(usuario);
            mockModeloUsuarios.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistros.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuarios.ObtenerObjetoDeMock());

            BuscadorDeProductosFavoritos buscador = new BuscadorDeProductosFavoritos(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(tiendas.Count, resultados.Count());
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
        [TestMethod]
        public void buscadorDeProductosFavoritos_ValidacionPorcentajeConTodosFavoritosEnUnaTienda_DeberiaDevolverCienPorcientoDePertenencia()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();

            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            usuario.nombreDeUsuario = "ElFavorito";

            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            producto1.nombre = "Producto1";
            producto2.nombre = "Producto2";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda);

            usuario.favoritos.Add(producto1);
            usuario.favoritos.Add(producto2);

            // Crear registros especificos
            List<Registro> registros = new List<Registro>()
            {
                registro1,
                registro2
            };

            // Crear los mocks
            MockDeModelo<Registro> mockModeloRegistros = new MockDeModelo<Registro>();
            mockModeloRegistros.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistros.Configurar();

            MockDeModelo<Usuario> mockModeloUsuarios = new MockDeModelo<Usuario>();
            mockModeloUsuarios.AgregarInstanciaDeModelo(usuario);
            mockModeloUsuarios.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistros.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuarios.ObtenerObjetoDeMock());

            BuscadorDeProductosFavoritos buscador = new BuscadorDeProductosFavoritos(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(resultados.FirstOrDefault()!.porcentajeEncontrado, 100);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
        [TestMethod]
        public void buscadorDeProductosFavoritos_ValidacionCantidadConTodosFavoritosEnUnaTienda_DeberiaDevolverCantidadIgualANumeroDeFavoritos()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();

            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            usuario.nombreDeUsuario = "ElFavorito";

            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            producto1.nombre = "Producto1";
            producto2.nombre = "Producto2";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda);

            usuario.favoritos.Add(producto1);
            usuario.favoritos.Add(producto2);

            // Crear registros especificos
            List<Registro> registros = new List<Registro>()
            {
                registro1,
                registro2
            };

            // Crear los mocks
            MockDeModelo<Registro> mockModeloRegistros = new MockDeModelo<Registro>();
            mockModeloRegistros.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistros.Configurar();

            MockDeModelo<Usuario> mockModeloUsuarios = new MockDeModelo<Usuario>();
            mockModeloUsuarios.AgregarInstanciaDeModelo(usuario);
            mockModeloUsuarios.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistros.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuarios.ObtenerObjetoDeMock());

            BuscadorDeProductosFavoritos buscador = new BuscadorDeProductosFavoritos(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(resultados.FirstOrDefault()!.cantidadEncontrada, usuario.favoritos.Count);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
        [TestMethod]
        public void buscadorDeProductosFavoritos_ValidacionCantidadConFavoritosEnVariasTienda_DeberiaDevolverCantidadDistintaANumeroDeFavoritos()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda1 = CreadorDeModelos.CrearTiendaPorDefecto();
            Tienda tienda2 = CreadorDeModelos.CrearTiendaPorDefecto();
            tienda1.nombre = "Tienda1";
            tienda2.nombre = "Tienda2";

            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            usuario.nombreDeUsuario = "ElFavorito";

            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            producto1.nombre = "Producto1";
            producto2.nombre = "Producto2";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda1);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda2);

            usuario.favoritos.Add(producto1);
            usuario.favoritos.Add(producto2);

            // Crear registros especificos
            List<Registro> registros = new List<Registro>()
            {
                registro1,
                registro2
            };

            // Crear los mocks
            MockDeModelo<Registro> mockModeloRegistros = new MockDeModelo<Registro>();
            mockModeloRegistros.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistros.Configurar();

            MockDeModelo<Usuario> mockModeloUsuarios = new MockDeModelo<Usuario>();
            mockModeloUsuarios.AgregarInstanciaDeModelo(usuario);
            mockModeloUsuarios.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistros.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuarios.ObtenerObjetoDeMock());

            BuscadorDeProductosFavoritos buscador = new BuscadorDeProductosFavoritos(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreNotEqual(resultados.FirstOrDefault()!.cantidadEncontrada, usuario.favoritos.Count);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
        [TestMethod]
        public void buscadorDeProductosFavoritos_ValidacionPorcentajeConFavoritosEnVariasTienda_DeberiaDevolverDistintoACienPorciento()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda1 = CreadorDeModelos.CrearTiendaPorDefecto();
            Tienda tienda2 = CreadorDeModelos.CrearTiendaPorDefecto();
            tienda1.nombre = "Tienda1";
            tienda2.nombre = "Tienda2";

            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            usuario.nombreDeUsuario = "ElFavorito";

            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            producto1.nombre = "Producto1";
            producto2.nombre = "Producto2";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda1);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda2);

            usuario.favoritos.Add(producto1);
            usuario.favoritos.Add(producto2);

            // Crear registros especificos
            List<Registro> registros = new List<Registro>()
            {
                registro1,
                registro2
            };

            // Crear los mocks
            MockDeModelo<Registro> mockModeloRegistros = new MockDeModelo<Registro>();
            mockModeloRegistros.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistros.Configurar();

            MockDeModelo<Usuario> mockModeloUsuarios = new MockDeModelo<Usuario>();
            mockModeloUsuarios.AgregarInstanciaDeModelo(usuario);
            mockModeloUsuarios.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistros.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuarios.ObtenerObjetoDeMock());

            BuscadorDeProductosFavoritos buscador = new BuscadorDeProductosFavoritos(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreNotEqual(resultados.FirstOrDefault()!.porcentajeEncontrado, 100);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
        [TestMethod]
        public void buscadorDeProductosFavoritos_ValidacionBusquedaConUsuarioInexistente_DeberiaDevolverCeroResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda1 = CreadorDeModelos.CrearTiendaPorDefecto();
            Tienda tienda2 = CreadorDeModelos.CrearTiendaPorDefecto();
            tienda1.nombre = "Tienda1";
            tienda2.nombre = "Tienda2";

            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            usuario.nombreDeUsuario = "ElFavorito";

            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            producto1.nombre = "Producto1";
            producto2.nombre = "Producto2";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda1);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda2);

            usuario.favoritos.Add(producto1);
            usuario.favoritos.Add(producto2);

            // Crear registros especificos
            List<Registro> registros = new List<Registro>()
            {
                registro1,
                registro2
            };

            // Crear los mocks
            MockDeModelo<Registro> mockModeloRegistros = new MockDeModelo<Registro>();
            mockModeloRegistros.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistros.Configurar();

            MockDeModelo<Usuario> mockModeloUsuarios = new MockDeModelo<Usuario>();
            mockModeloUsuarios.AgregarInstanciaDeModelo(usuario);
            mockModeloUsuarios.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistros.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuarios.ObtenerObjetoDeMock());

            BuscadorDeProductosFavoritos buscador = new BuscadorDeProductosFavoritos(mockContexto.ObtenerObjetoDeMock(), "usuarioInexistente");

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(resultados.Count(), 0);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477 - Sprint 3
        [TestMethod]
        public void buscadorDeProductosFavoritos_ValidacionBusquedaConUsuarioNulo_DeberiaDevolverCeroResultados()
        {
            // Preparación
            // Crear modelos por defecto
            Tienda tienda1 = CreadorDeModelos.CrearTiendaPorDefecto();
            Tienda tienda2 = CreadorDeModelos.CrearTiendaPorDefecto();
            tienda1.nombre = "Tienda1";
            tienda2.nombre = "Tienda2";

            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            usuario.nombreDeUsuario = "ElFavorito";

            Producto producto1 = CreadorDeModelos.CrearProductoPorDefecto();
            Producto producto2 = CreadorDeModelos.CrearProductoPorDefecto();
            producto1.nombre = "Producto1";
            producto2.nombre = "Producto2";

            Registro registro1 = CreadorDeModelos.CrearRegistroPorDefecto(producto1, tienda1);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto2, tienda2);

            usuario.favoritos.Add(producto1);
            usuario.favoritos.Add(producto2);

            // Crear registros especificos
            List<Registro> registros = new List<Registro>()
            {
                registro1,
                registro2
            };

            // Crear los mocks
            MockDeModelo<Registro> mockModeloRegistros = new MockDeModelo<Registro>();
            mockModeloRegistros.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistros.Configurar();

            MockDeModelo<Usuario> mockModeloUsuarios = new MockDeModelo<Usuario>();
            mockModeloUsuarios.AgregarInstanciaDeModelo(usuario);
            mockModeloUsuarios.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistros.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuarios.ObtenerObjetoDeMock());

            BuscadorDeProductosFavoritos buscador = new BuscadorDeProductosFavoritos(mockContexto.ObtenerObjetoDeMock(), null);

            // Acción
            var resultados = buscador.buscar();

            // Verificación
            Assert.AreEqual(resultados.Count(), 0);
        }

    }
}