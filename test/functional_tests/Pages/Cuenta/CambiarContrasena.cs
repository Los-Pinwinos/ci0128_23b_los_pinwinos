using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using LoCoMProTestFuncionales.PageModels.Home;
using LoCoMProTestFuncionales.PageModels.Cuenta.CambiarContrasena;

namespace LoCoMProFunctionalTests.Pages.Cuenta.CambiarContrasenaTest
{
    [TestFixture]

    // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3

    public class CambiarContrasenaTests
    {
        ChromeDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        private PaginaCambiarContrasena LlegarACambioContrasena()
        {
            PaginaHome paginaHome = new PaginaHome(driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());
            PaginaIngresar ingresar = paginaHome.IrAIngresarUsuario();
            paginaHome = ingresar.IniciarSesion("Usuario1*", "Usuario1*");
            PaginaPerfil perfil = paginaHome.IrAPerfil();
            return perfil.IrACambiarContrasena();
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [Test]
        public void DatosVacios()
        {
            // Preparación
            PaginaCambiarContrasena paginaCambio = LlegarACambioContrasena();

            // Acción
            paginaCambio.GuardarCambios();

            // Verificación
            string resultado = paginaCambio.ObtenerMensajeError();
            Assert.That(resultado, Is.EqualTo("Los datos brindados no son correctos. Recuerde que la contraseña debe contener al menos: una minúscula, una mayúscula, un dígito y un carácter especial. Además, debe estar entre 8 y 20 caracteres."));
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [Test]
        public void ActualConFormatoIncorrecto()
        {
            // Preparación
            PaginaCambiarContrasena paginaCambio = LlegarACambioContrasena();

            // Acción
            paginaCambio.IngresarContrasenaActual("abc");
            paginaCambio.IngresarContrasenaNueva("Usuario1*");
            paginaCambio.IngresarConfirmacionNueva("Usuario1*");
            paginaCambio.GuardarCambios();

            // Verificación
            string resultado = paginaCambio.ObtenerMensajeError();
            Assert.That(resultado, Is.EqualTo("Los datos brindados no son correctos. Recuerde que la contraseña debe contener al menos: una minúscula, una mayúscula, un dígito y un carácter especial. Además, debe estar entre 8 y 20 caracteres."));
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [Test]
        public void NuevaConFormatoIncorrecto()
        {
            // Preparación
            PaginaCambiarContrasena paginaCambio = LlegarACambioContrasena();

            // Acción
            paginaCambio.IngresarContrasenaActual("Usuario1*");
            paginaCambio.IngresarContrasenaNueva("abc");
            paginaCambio.IngresarConfirmacionNueva("abc");
            paginaCambio.GuardarCambios();

            // Verificación
            string resultado = paginaCambio.ObtenerMensajeError();
            Assert.That(resultado, Is.EqualTo("Los datos brindados no son correctos. Recuerde que la contraseña debe contener al menos: una minúscula, una mayúscula, un dígito y un carácter especial. Además, debe estar entre 8 y 20 caracteres."));
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [Test]
        public void ActualIncorrecta()
        {
            // Preparación
            PaginaCambiarContrasena paginaCambio = LlegarACambioContrasena();

            // Acción
            paginaCambio.IngresarContrasenaActual("Usuario2*"); // Incorrecta
            paginaCambio.IngresarContrasenaNueva("Usuario1*");
            paginaCambio.IngresarConfirmacionNueva("Usuario1*");
            paginaCambio.GuardarCambios();

            // Verificación
            string resultado = paginaCambio.ObtenerMensajeError();
            Assert.That(resultado, Is.EqualTo("Contraseña actual incorrecta."));
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 3
        [Test]
        public void ConfirmacionDiferente()
        {
            // Preparación
            PaginaCambiarContrasena paginaCambio = LlegarACambioContrasena();

            // Acción
            paginaCambio.IngresarContrasenaActual("Usuario1*"); // Correcta
            paginaCambio.IngresarContrasenaNueva("Usuario4*");
            paginaCambio.IngresarConfirmacionNueva("Usuario1*"); // No coincide con la nueva
            paginaCambio.GuardarCambios();

            // Verificación
            string resultado = paginaCambio.ObtenerMensajeError();
            Assert.That(resultado, Is.EqualTo("Las contraseñas no son iguales."));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}