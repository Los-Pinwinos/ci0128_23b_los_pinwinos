using LoCoMProTests.Mocks;
using LoCoMPro.Models;
using LoCoMPro.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using LoCoMPro.Pages.VerRegistros;
using LoCoMPro.Pages.Cuenta;
using LoCoMPro.ViewModels.VerRegistros;
using System.Collections;
using LoCoMPro.Pages.Moderacion;
using LoCoMPro.ViewModels.Moderacion;
using Microsoft.Win32;
using LoCoMPro.ViewModels.Cuenta;
using System.Diagnostics;

namespace LoCoMProTests.Pages.Moderacion
{
    // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
    [TestClass]
    public class EstadisticasUsuarioTest
    {

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void estadisticasUsuarios_buscarUsuarioReportadores_UnReporte_DebeRetornar1()
        {
            // Preparación
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Fotografia foto = CreadorDeModelos.CrearFotografiaPorDefecto();
            Reporte reporte = CreadorDeModelos.CrearReportePorDefecto(usuario, registro);
            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario
            };

            MockDeModelo<Usuario> mockModeloUsuario = new MockDeModelo<Usuario>();
            mockModeloUsuario.AgregarRangoInstanciasDeModelo(usuarios);
            mockModeloUsuario.Configurar();
            List<Registro> registros = new List<Registro>()
            {
                registro
            };

            MockDeModelo<Registro> mockModeloRegistro = new MockDeModelo<Registro>();
            mockModeloRegistro.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistro.Configurar();
            List<Reporte> reportes = new List<Reporte>()
            {
                reporte
            };

            MockDeModelo<Reporte> mockModeloReporte = new MockDeModelo<Reporte>();
            mockModeloReporte.AgregarRangoInstanciasDeModelo(reportes);
            mockModeloReporte.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuario.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistro.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Reportes, mockModeloReporte.ObtenerObjetoDeMock());

            // Acción
            EstadisticasUsuariosReportesModel buscadorUsuarios = new EstadisticasUsuariosReportesModel(mockContexto.ObtenerObjetoDeMock());
            IQueryable<UsuarioEstadisticasVM> resultados = buscadorUsuarios.buscarUsuariosReportadores();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }


        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void estadisticasUsuarios_buscarUsuarioReportados_UnReporte_DebeRetornar1()
        {
            // Preparación
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Fotografia foto = CreadorDeModelos.CrearFotografiaPorDefecto();
            Reporte reporte = CreadorDeModelos.CrearReportePorDefecto(usuario, registro);
            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario
            };

            MockDeModelo<Usuario> mockModeloUsuario = new MockDeModelo<Usuario>();
            mockModeloUsuario.AgregarRangoInstanciasDeModelo(usuarios);
            mockModeloUsuario.Configurar();
            List<Registro> registros = new List<Registro>()
            {
                registro
            };

            MockDeModelo<Registro> mockModeloRegistro = new MockDeModelo<Registro>();
            mockModeloRegistro.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistro.Configurar();
            List<Reporte> reportes = new List<Reporte>()
            {
                reporte
            };

            MockDeModelo<Reporte> mockModeloReporte = new MockDeModelo<Reporte>();
            mockModeloReporte.AgregarRangoInstanciasDeModelo(reportes);
            mockModeloReporte.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuario.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistro.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Reportes, mockModeloReporte.ObtenerObjetoDeMock());

            // Acción
            EstadisticasUsuariosReportesModel buscadorUsuarios = new EstadisticasUsuariosReportesModel(mockContexto.ObtenerObjetoDeMock());
            IQueryable<UsuarioEstadisticasVM> resultados = buscadorUsuarios.buscarUsuariosReportados();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }



        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void estadisticasUsuarios_buscarUsuarioReportadores_UnUsuarioVariosReportes_DebeRetornar1()
        {
            // Preparación
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            usuario.nombreDeUsuario = "Usuario2";
            Usuario usuario2 = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            registro.creacion = DateTime.Now;
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario2);
            Fotografia foto = CreadorDeModelos.CrearFotografiaPorDefecto();
            Reporte reporte = CreadorDeModelos.CrearReportePorDefecto(usuario, registro);
            Reporte reporte2 = CreadorDeModelos.CrearReportePorDefecto(usuario, registro2);
            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario,
                usuario2
            };

            MockDeModelo<Usuario> mockModeloUsuario = new MockDeModelo<Usuario>();
            mockModeloUsuario.AgregarRangoInstanciasDeModelo(usuarios);
            mockModeloUsuario.Configurar();
            List<Registro> registros = new List<Registro>()
            {
                registro,
                registro2
            };

            MockDeModelo<Registro> mockModeloRegistro = new MockDeModelo<Registro>();
            mockModeloRegistro.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistro.Configurar();
            List<Reporte> reportes = new List<Reporte>()
            {
                reporte,
                reporte2
            };

            MockDeModelo<Reporte> mockModeloReporte = new MockDeModelo<Reporte>();
            mockModeloReporte.AgregarRangoInstanciasDeModelo(reportes);
            mockModeloReporte.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuario.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistro.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Reportes, mockModeloReporte.ObtenerObjetoDeMock());

            // Acción
            EstadisticasUsuariosReportesModel buscadorUsuarios = new EstadisticasUsuariosReportesModel(mockContexto.ObtenerObjetoDeMock());
            IQueryable<UsuarioEstadisticasVM> resultados = buscadorUsuarios.buscarUsuariosReportadores();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void estadisticasUsuarios_buscarUsuarioReportados_UnUsuarioVariosReportes_DebeRetornar2()
        {
            // Preparación
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            usuario.nombreDeUsuario = "Usuario2";
            Usuario usuario2 = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            registro.creacion = DateTime.Now;
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario2);
            Fotografia foto = CreadorDeModelos.CrearFotografiaPorDefecto();
            Reporte reporte = CreadorDeModelos.CrearReportePorDefecto(usuario, registro);
            Reporte reporte2 = CreadorDeModelos.CrearReportePorDefecto(usuario, registro2);
            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario,
                usuario2
            };

            MockDeModelo<Usuario> mockModeloUsuario = new MockDeModelo<Usuario>();
            mockModeloUsuario.AgregarRangoInstanciasDeModelo(usuarios);
            mockModeloUsuario.Configurar();
            List<Registro> registros = new List<Registro>()
            {
                registro,
                registro2
            };

            MockDeModelo<Registro> mockModeloRegistro = new MockDeModelo<Registro>();
            mockModeloRegistro.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistro.Configurar();
            List<Reporte> reportes = new List<Reporte>()
            {
                reporte,
                reporte2
            };

            MockDeModelo<Reporte> mockModeloReporte = new MockDeModelo<Reporte>();
            mockModeloReporte.AgregarRangoInstanciasDeModelo(reportes);
            mockModeloReporte.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuario.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistro.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Reportes, mockModeloReporte.ObtenerObjetoDeMock());

            // Acción
            EstadisticasUsuariosReportesModel buscadorUsuarios = new EstadisticasUsuariosReportesModel(mockContexto.ObtenerObjetoDeMock());
            IQueryable<UsuarioEstadisticasVM> resultados = buscadorUsuarios.buscarUsuariosReportados();

            // Verificación
            Assert.AreEqual(2, resultados.Count());
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void estadisticasUsuarios_buscarUsuarioReportados_VariosReportesAUnUsuario_DebeRetornar1()
        {
            // Preparación
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            usuario.nombreDeUsuario = "Usuario2";
            Usuario usuario2 = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            registro.creacion = DateTime.Now;
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Fotografia foto = CreadorDeModelos.CrearFotografiaPorDefecto();
            Reporte reporte = CreadorDeModelos.CrearReportePorDefecto(usuario, registro);
            Reporte reporte2 = CreadorDeModelos.CrearReportePorDefecto(usuario2, registro2);
            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario,
                usuario2
            };

            MockDeModelo<Usuario> mockModeloUsuario = new MockDeModelo<Usuario>();
            mockModeloUsuario.AgregarRangoInstanciasDeModelo(usuarios);
            mockModeloUsuario.Configurar();
            List<Registro> registros = new List<Registro>()
            {
                registro,
                registro2
            };

            MockDeModelo<Registro> mockModeloRegistro = new MockDeModelo<Registro>();
            mockModeloRegistro.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistro.Configurar();
            List<Reporte> reportes = new List<Reporte>()
            {
                reporte,
                reporte2
            };

            MockDeModelo<Reporte> mockModeloReporte = new MockDeModelo<Reporte>();
            mockModeloReporte.AgregarRangoInstanciasDeModelo(reportes);
            mockModeloReporte.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuario.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistro.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Reportes, mockModeloReporte.ObtenerObjetoDeMock());

            // Acción
            EstadisticasUsuariosReportesModel buscadorUsuarios = new EstadisticasUsuariosReportesModel(mockContexto.ObtenerObjetoDeMock());
            IQueryable<UsuarioEstadisticasVM> resultados = buscadorUsuarios.buscarUsuariosReportados();

            // Verificación
            Assert.AreEqual(1, resultados.Count());
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void estadisticasUsuarios_buscarUsuarioReportadores_VariosReportesAUnUsuario_DebeRetornar2()
        {
            // Preparación
            Tienda tienda = CreadorDeModelos.CrearTiendaPorDefecto();
            Producto producto = CreadorDeModelos.CrearProductoPorDefecto();
            Usuario usuario = CreadorDeModelos.CrearUsuarioPorDefecto();
            usuario.nombreDeUsuario = "Usuario2";
            Usuario usuario2 = CreadorDeModelos.CrearUsuarioPorDefecto();
            Registro registro = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            registro.creacion = DateTime.Now;
            Registro registro2 = CreadorDeModelos.CrearRegistroPorDefecto(producto, tienda, usuario);
            Fotografia foto = CreadorDeModelos.CrearFotografiaPorDefecto();
            Reporte reporte = CreadorDeModelos.CrearReportePorDefecto(usuario, registro);
            Reporte reporte2 = CreadorDeModelos.CrearReportePorDefecto(usuario2, registro2);
            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario,
                usuario2
            };

            MockDeModelo<Usuario> mockModeloUsuario = new MockDeModelo<Usuario>();
            mockModeloUsuario.AgregarRangoInstanciasDeModelo(usuarios);
            mockModeloUsuario.Configurar();
            List<Registro> registros = new List<Registro>()
            {
                registro,
                registro2
            };

            MockDeModelo<Registro> mockModeloRegistro = new MockDeModelo<Registro>();
            mockModeloRegistro.AgregarRangoInstanciasDeModelo(registros);
            mockModeloRegistro.Configurar();
            List<Reporte> reportes = new List<Reporte>()
            {
                reporte,
                reporte2
            };

            MockDeModelo<Reporte> mockModeloReporte = new MockDeModelo<Reporte>();
            mockModeloReporte.AgregarRangoInstanciasDeModelo(reportes);
            mockModeloReporte.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModeloUsuario.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Registros, mockModeloRegistro.ObtenerObjetoDeMock());
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Reportes, mockModeloReporte.ObtenerObjetoDeMock());

            // Acción
            EstadisticasUsuariosReportesModel buscadorUsuarios = new EstadisticasUsuariosReportesModel(mockContexto.ObtenerObjetoDeMock());
            IQueryable<UsuarioEstadisticasVM> resultados = buscadorUsuarios.buscarUsuariosReportadores();

            // Verificación
            Assert.AreEqual(2, resultados.Count());
        }

    }
}