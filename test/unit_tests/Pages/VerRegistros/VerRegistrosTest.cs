using LoCoMProTests.Mocks;
using LoCoMPro.Models;
using LoCoMPro.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using LoCoMPro.Pages.VerRegistros;
using LoCoMPro.Pages.Cuenta;
using LoCoMPro.ViewModels.VerRegistros;
using System.Collections;

namespace LoCoMProTests.Pages.VerRegistros
{
    // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
    [TestClass]
    public class VerRegistrosTest
    {

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [TestMethod]
        public void verReportes_obtenerRegistros_ProductoConUnRegistro_DebeRetornarUno()
        {           
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Fotografia foto = CreadorDeModelos.CrearFotografiaPorDefecto();
            registro.fotografias = new List<Fotografia>();
            registro.fotografias.Add(foto);

            List<Registro> registros = new List<Registro>()
            {
                registro
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            VerRegistrosModel buscadorRegistros = new VerRegistrosModel(mockContexto.ObtenerObjetoDeMock(), registro.productoAsociado, registro.nombreTienda, registro.nombreProvincia, registro.nombreCanton);
            IQueryable<VerRegistrosVM> resultados = buscadorRegistros.ObtenerRegistros();

            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [TestMethod]
        public void verReportes_obtenerRegistros_ProductoConVariosRegistros_DebeRetornarMasDeUno()
        {
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Fotografia foto = CreadorDeModelos.CrearFotografiaPorDefecto();
            registro.fotografias = new List<Fotografia>();
            registro.fotografias.Add(foto);
            registro2.fotografias = new List<Fotografia>();
            registro2.fotografias.Add(foto);


            List<Registro> registros = new List<Registro>()
            {
                registro,
                registro2
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            VerRegistrosModel buscadorRegistros = new VerRegistrosModel(mockContexto.ObtenerObjetoDeMock(), registro.productoAsociado, registro.nombreTienda, registro.nombreProvincia, registro.nombreCanton);
            IQueryable<VerRegistrosVM> resultados = buscadorRegistros.ObtenerRegistros();

            Assert.IsTrue(resultados.Count() > 1);
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [TestMethod]
        public void verReportes_obtenerRegistros_RegistrosConProvinciasDiferente_DebeRetornarUno()
        {
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Fotografia foto = CreadorDeModelos.CrearFotografiaPorDefecto();
            registro.fotografias = new List<Fotografia>();
            registro.fotografias.Add(foto);
            registro2.fotografias = new List<Fotografia>();
            registro2.fotografias.Add(foto);
            registro2.nombreProvincia = "OtraProvincia";

            List<Registro> registros = new List<Registro>()
            {
                registro,
                registro2
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            VerRegistrosModel buscadorRegistros = new VerRegistrosModel(mockContexto.ObtenerObjetoDeMock(), registro.productoAsociado, registro.nombreTienda, registro.nombreProvincia, registro.nombreCanton);
            IQueryable<VerRegistrosVM> resultados = buscadorRegistros.ObtenerRegistros();

            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [TestMethod]
        public void verReportes_obtenerRegistros_RegistrosConCantonDiferente_DebeRetornarUno()
        {
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Fotografia foto = CreadorDeModelos.CrearFotografiaPorDefecto();
            registro.fotografias = new List<Fotografia>();
            registro.fotografias.Add(foto);
            registro2.fotografias = new List<Fotografia>();
            registro2.fotografias.Add(foto);
            registro2.nombreCanton = "OtraProvincia";

            List<Registro> registros = new List<Registro>()
            {
                registro,
                registro2
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            VerRegistrosModel buscadorRegistros = new VerRegistrosModel(mockContexto.ObtenerObjetoDeMock(), registro.productoAsociado, registro.nombreTienda, registro.nombreProvincia, registro.nombreCanton);
            IQueryable<VerRegistrosVM> resultados = buscadorRegistros.ObtenerRegistros();

            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [TestMethod]
        public void verReportes_obtenerRegistros_RegistrosConTiendasDiferentes_DebeRetornarUno()
        {
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Tienda tienda2 = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            tienda2.nombre = "OtraTienda";
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda2, usuario);
            Fotografia foto = CreadorDeModelos.CrearFotografiaPorDefecto();
            registro.fotografias = new List<Fotografia>();
            registro.fotografias.Add(foto);
            registro2.fotografias = new List<Fotografia>();
            registro2.fotografias.Add(foto);

            List<Registro> registros = new List<Registro>()
            {
                registro,
                registro2
            };

            MockDeModelo<Registro> mockModelo = new MockDeModelo<Registro>();
            mockModelo.AgregarRangoInstanciasDeModelo(registros);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModelo.ObtenerObjetoDeMock());

            VerRegistrosModel buscadorRegistros = new VerRegistrosModel(mockContexto.ObtenerObjetoDeMock(), registro.productoAsociado, registro.nombreTienda, registro.nombreProvincia, registro.nombreCanton);
            IQueryable<VerRegistrosVM> resultados = buscadorRegistros.ObtenerRegistros();

            Assert.AreEqual(1, resultados.Count());
        }

    }
}