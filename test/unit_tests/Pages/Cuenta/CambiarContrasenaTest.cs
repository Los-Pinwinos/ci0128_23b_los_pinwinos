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

namespace LoCoMProTests.Pages.Cuenta.CambiarContrasena
{
    // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
    [TestClass]
    public class CambiarContrasenaTests
    {

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void cambioContrasena_usuarioInvalido_DebeRetornar3()
        {
            // Preparación
            Usuario usuario = new Usuario
            {
                nombreDeUsuario = "Usuario",
                correo = "usuario@example.com",
                hashContrasena = "AQAAAAIAAYagAAAAEKsU2+AMT85bnzhsCNuFBikWncWXvbzB+a1mkc5MX7GnEcXY0F+4TNoLD45JU7c+WQ==", // Usuario1*
                estado = 'A',
                calificacion = 4.2,
                distritoVivienda = "Distrito de prueba",
                cantonVivienda = "Cantón de prueba",
                provinciaVivienda = "Provincia de prueba",
                esAdministrador = false,
                esModerador = true,
                latitudVivienda = 9.1234,
                longitudVivienda = -84.5678,
                registros = new List<Registro>(),
                reportes = new List<Reporte>(),
                calificaciones = new List<Calificacion>()
            };

            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario
            };

            MockDeModelo<Usuario> mockModelo = new MockDeModelo<Usuario>();
            mockModelo.AgregarRangoInstanciasDeModelo(usuarios);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModelo.ObtenerObjetoDeMock());


            // Acción
            ModeloContrasena cambiador = new ModeloContrasena(mockContexto.ObtenerObjetoDeMock(), "Usuaaaario", "Usuario1*", "Usuario2*", "Usuario2*");
            int resultado = cambiador.cambiarContrasena();

            // Verificación
            Assert.AreEqual(3, resultado); // 3 es error del sistema por usuario inválido
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void cambioContrasena_usuarioVacio_DebeRetornar3()
        {
            // Preparación
            Usuario usuario = new Usuario
            {
                nombreDeUsuario = "Usuario",
                correo = "usuario@example.com",
                hashContrasena = "AQAAAAIAAYagAAAAEKsU2+AMT85bnzhsCNuFBikWncWXvbzB+a1mkc5MX7GnEcXY0F+4TNoLD45JU7c+WQ==", // Usuario1*
                estado = 'A',
                calificacion = 4.2,
                distritoVivienda = "Distrito de prueba",
                cantonVivienda = "Cantón de prueba",
                provinciaVivienda = "Provincia de prueba",
                esAdministrador = false,
                esModerador = true,
                latitudVivienda = 9.1234,
                longitudVivienda = -84.5678,
                registros = new List<Registro>(),
                reportes = new List<Reporte>(),
                calificaciones = new List<Calificacion>()
            };

            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario
            };

            MockDeModelo<Usuario> mockModelo = new MockDeModelo<Usuario>();
            mockModelo.AgregarRangoInstanciasDeModelo(usuarios);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModelo.ObtenerObjetoDeMock());


            // Acción
            ModeloContrasena cambiador = new ModeloContrasena(mockContexto.ObtenerObjetoDeMock(), "", "Usuario1*", "Usuario2*", "Usuario2*");
            int resultado = cambiador.cambiarContrasena();

            // Verificación
            Assert.AreEqual(3, resultado); // 3 es error del sistema por usuario inválido
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void cambioContrasena_CambioCorrectoDiferentes_DebeRetornar0()
        {
            // Preparación
            Usuario usuario = new Usuario
            {
                nombreDeUsuario = "Usuario",
                correo = "usuario@example.com",
                hashContrasena = "AQAAAAIAAYagAAAAEKsU2+AMT85bnzhsCNuFBikWncWXvbzB+a1mkc5MX7GnEcXY0F+4TNoLD45JU7c+WQ==", // Usuario1*
                estado = 'A',
                calificacion = 4.2,
                distritoVivienda = "Distrito de prueba",
                cantonVivienda = "Cantón de prueba",
                provinciaVivienda = "Provincia de prueba",
                esAdministrador = false,
                esModerador = true,
                latitudVivienda = 9.1234,
                longitudVivienda = -84.5678,
                registros = new List<Registro>(),
                reportes = new List<Reporte>(),
                calificaciones = new List<Calificacion>()
            };

            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario
            };

            MockDeModelo<Usuario> mockModelo = new MockDeModelo<Usuario>();
            mockModelo.AgregarRangoInstanciasDeModelo(usuarios);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModelo.ObtenerObjetoDeMock());


            // Acción
            ModeloContrasena cambiador = new ModeloContrasena(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario, "Usuario1*", "Usuario2*", "Usuario2*");
            int resultado = cambiador.cambiarContrasena();

            // Verificación
            Assert.AreEqual(0, resultado); // 0 es cambio correcto
        }


        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void cambioContrasena_CambioCorrectoIguales_DebeRetornar0()
        {
            // Preparación
            Usuario usuario = new Usuario
            {
                nombreDeUsuario = "Usuario",
                correo = "usuario@example.com",
                hashContrasena = "AQAAAAIAAYagAAAAEKsU2+AMT85bnzhsCNuFBikWncWXvbzB+a1mkc5MX7GnEcXY0F+4TNoLD45JU7c+WQ==", // Usuario1*
                estado = 'A',
                calificacion = 4.2,
                distritoVivienda = "Distrito de prueba",
                cantonVivienda = "Cantón de prueba",
                provinciaVivienda = "Provincia de prueba",
                esAdministrador = false,
                esModerador = true,
                latitudVivienda = 9.1234,
                longitudVivienda = -84.5678,
                registros = new List<Registro>(),
                reportes = new List<Reporte>(),
                calificaciones = new List<Calificacion>()
            };

            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario
            };

            MockDeModelo<Usuario> mockModelo = new MockDeModelo<Usuario>();
            mockModelo.AgregarRangoInstanciasDeModelo(usuarios);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModelo.ObtenerObjetoDeMock());


            // Acción
            // Es válido cambiar una contraseña por sí misma
            ModeloContrasena cambiador = new ModeloContrasena(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario, "Usuario1*", "Usuario1*", "Usuario1*");
            int resultado = cambiador.cambiarContrasena();

            // Verificación
            Assert.AreEqual(0, resultado); // 0 es cambio correcto
        }


        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void cambioContrasena_ActualInvalida_DebeRetornar2()
        {
            // Preparación
            Usuario usuario = new Usuario
            {
                nombreDeUsuario = "Usuario",
                correo = "usuario@example.com",
                hashContrasena = "AQAAAAIAAYagAAAAEKsU2+AMT85bnzhsCNuFBikWncWXvbzB+a1mkc5MX7GnEcXY0F+4TNoLD45JU7c+WQ==", // Usuario1*
                estado = 'A',
                calificacion = 4.2,
                distritoVivienda = "Distrito de prueba",
                cantonVivienda = "Cantón de prueba",
                provinciaVivienda = "Provincia de prueba",
                esAdministrador = false,
                esModerador = true,
                latitudVivienda = 9.1234,
                longitudVivienda = -84.5678,
                registros = new List<Registro>(),
                reportes = new List<Reporte>(),
                calificaciones = new List<Calificacion>()
            };

            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario
            };

            MockDeModelo<Usuario> mockModelo = new MockDeModelo<Usuario>();
            mockModelo.AgregarRangoInstanciasDeModelo(usuarios);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModelo.ObtenerObjetoDeMock());


            // Acción
            ModeloContrasena cambiador = new ModeloContrasena(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario, "Usuario2*", "Usuario1*", "Usuario1*");
            int resultado = cambiador.cambiarContrasena();

            // Verificación
            Assert.AreEqual(2, resultado); // 2 es contraseña actual es incorrecta
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void cambioContrasena_ConfirmacionDiferente_DebeRetornar1()
        {
            // Preparación
            Usuario usuario = new Usuario
            {
                nombreDeUsuario = "Usuario",
                correo = "usuario@example.com",
                hashContrasena = "AQAAAAIAAYagAAAAEKsU2+AMT85bnzhsCNuFBikWncWXvbzB+a1mkc5MX7GnEcXY0F+4TNoLD45JU7c+WQ==", // Usuario1*
                estado = 'A',
                calificacion = 4.2,
                distritoVivienda = "Distrito de prueba",
                cantonVivienda = "Cantón de prueba",
                provinciaVivienda = "Provincia de prueba",
                esAdministrador = false,
                esModerador = true,
                latitudVivienda = 9.1234,
                longitudVivienda = -84.5678,
                registros = new List<Registro>(),
                reportes = new List<Reporte>(),
                calificaciones = new List<Calificacion>()
            };

            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario
            };

            MockDeModelo<Usuario> mockModelo = new MockDeModelo<Usuario>();
            mockModelo.AgregarRangoInstanciasDeModelo(usuarios);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModelo.ObtenerObjetoDeMock());


            // Acción
            ModeloContrasena cambiador = new ModeloContrasena(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario, "Usuario1*", "Usuario2*", "Usuario3*");
            int resultado = cambiador.cambiarContrasena();

            // Verificación
            Assert.AreEqual(1, resultado); // 1 es confirmación y nueva son diferentes
        }


        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void cambioContrasena_DatosVacios_DebeRetornar4()
        {
            // Preparación
            Usuario usuario = new Usuario
            {
                nombreDeUsuario = "Usuario",
                correo = "usuario@example.com",
                hashContrasena = "AQAAAAIAAYagAAAAEKsU2+AMT85bnzhsCNuFBikWncWXvbzB+a1mkc5MX7GnEcXY0F+4TNoLD45JU7c+WQ==", // Usuario1*
                estado = 'A',
                calificacion = 4.2,
                distritoVivienda = "Distrito de prueba",
                cantonVivienda = "Cantón de prueba",
                provinciaVivienda = "Provincia de prueba",
                esAdministrador = false,
                esModerador = true,
                latitudVivienda = 9.1234,
                longitudVivienda = -84.5678,
                registros = new List<Registro>(),
                reportes = new List<Reporte>(),
                calificaciones = new List<Calificacion>()
            };

            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario
            };

            MockDeModelo<Usuario> mockModelo = new MockDeModelo<Usuario>();
            mockModelo.AgregarRangoInstanciasDeModelo(usuarios);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModelo.ObtenerObjetoDeMock());


            // Acción
            ModeloContrasena cambiador = new ModeloContrasena(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario, "", "", "");
            int resultado = cambiador.cambiarContrasena();

            // Verificación
            Assert.AreEqual(4, resultado); // 4 es formato inválido
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void cambioContrasena_DatosNuevosVacios_DebeRetornar4()
        {
            // Preparación
            Usuario usuario = new Usuario
            {
                nombreDeUsuario = "Usuario",
                correo = "usuario@example.com",
                hashContrasena = "AQAAAAIAAYagAAAAEKsU2+AMT85bnzhsCNuFBikWncWXvbzB+a1mkc5MX7GnEcXY0F+4TNoLD45JU7c+WQ==", // Usuario1*
                estado = 'A',
                calificacion = 4.2,
                distritoVivienda = "Distrito de prueba",
                cantonVivienda = "Cantón de prueba",
                provinciaVivienda = "Provincia de prueba",
                esAdministrador = false,
                esModerador = true,
                latitudVivienda = 9.1234,
                longitudVivienda = -84.5678,
                registros = new List<Registro>(),
                reportes = new List<Reporte>(),
                calificaciones = new List<Calificacion>()
            };

            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario
            };

            MockDeModelo<Usuario> mockModelo = new MockDeModelo<Usuario>();
            mockModelo.AgregarRangoInstanciasDeModelo(usuarios);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModelo.ObtenerObjetoDeMock());


            // Acción
            ModeloContrasena cambiador = new ModeloContrasena(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario, "Usuario1*", "", "");
            int resultado = cambiador.cambiarContrasena();

            // Verificación
            Assert.AreEqual(4, resultado); // 4 es formato inválido
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void cambioContrasena_UsarContrasenaVieja_DebeRetornar2()
        {
            // Preparación
            Usuario usuario = new Usuario
            {
                nombreDeUsuario = "Usuario",
                correo = "usuario@example.com",
                hashContrasena = "AQAAAAIAAYagAAAAEKsU2+AMT85bnzhsCNuFBikWncWXvbzB+a1mkc5MX7GnEcXY0F+4TNoLD45JU7c+WQ==", // Usuario1*
                estado = 'A',
                calificacion = 4.2,
                distritoVivienda = "Distrito de prueba",
                cantonVivienda = "Cantón de prueba",
                provinciaVivienda = "Provincia de prueba",
                esAdministrador = false,
                esModerador = true,
                latitudVivienda = 9.1234,
                longitudVivienda = -84.5678,
                registros = new List<Registro>(),
                reportes = new List<Reporte>(),
                calificaciones = new List<Calificacion>()
            };

            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario
            };

            MockDeModelo<Usuario> mockModelo = new MockDeModelo<Usuario>();
            mockModelo.AgregarRangoInstanciasDeModelo(usuarios);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModelo.ObtenerObjetoDeMock());

            // Se cambia una vez la contraseña para confirmar que la contraseña anterior no se puede utilizar nuevamente
            ModeloContrasena cambiador = new ModeloContrasena(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario, "Usuario1*", "Usuario2*", "Usuario2*");
            cambiador.cambiarContrasena();

            // Acción
            ModeloContrasena nuevoCambiador = new ModeloContrasena(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario, "Usuario1*", "Usuario3*", "Usuario3*");
            int resultado = nuevoCambiador.cambiarContrasena();

            // Verificación
            Assert.AreEqual(2, resultado); // 2 es contraseña actual es incorrecta
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [TestMethod]
        public void cambioContrasena_UsarContrasenaNueva_DebeRetornar0()
        {
            // Preparación
            Usuario usuario = new Usuario
            {
                nombreDeUsuario = "Usuario",
                correo = "usuario@example.com",
                hashContrasena = "AQAAAAIAAYagAAAAEKsU2+AMT85bnzhsCNuFBikWncWXvbzB+a1mkc5MX7GnEcXY0F+4TNoLD45JU7c+WQ==", // Usuario1*
                estado = 'A',
                calificacion = 4.2,
                distritoVivienda = "Distrito de prueba",
                cantonVivienda = "Cantón de prueba",
                provinciaVivienda = "Provincia de prueba",
                esAdministrador = false,
                esModerador = true,
                latitudVivienda = 9.1234,
                longitudVivienda = -84.5678,
                registros = new List<Registro>(),
                reportes = new List<Reporte>(),
                calificaciones = new List<Calificacion>()
            };

            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario
            };

            MockDeModelo<Usuario> mockModelo = new MockDeModelo<Usuario>();
            mockModelo.AgregarRangoInstanciasDeModelo(usuarios);
            mockModelo.Configurar();

            MockDeContexto<LoCoMProContext> mockContexto = new MockDeContexto<LoCoMProContext>();
            mockContexto.ConfigurarParaInstanciasDeModelo(p => p.Usuarios, mockModelo.ObtenerObjetoDeMock());

            // Se cambia una vez la contraseña para confirmar que la contraseña anterior no se puede utilizar nuevamente
            ModeloContrasena cambiador = new ModeloContrasena(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario, "Usuario1*", "Usuario2*", "Usuario2*");
            cambiador.cambiarContrasena();

            // Acción
            ModeloContrasena nuevoCambiador = new ModeloContrasena(mockContexto.ObtenerObjetoDeMock(), usuario.nombreDeUsuario, "Usuario2*", "Usuario3*", "Usuario3*");
            int resultado = nuevoCambiador.cambiarContrasena();

            // Verificación
            Assert.AreEqual(0, resultado); // 0 es cambios aplicados correctamente
        }

    }
}