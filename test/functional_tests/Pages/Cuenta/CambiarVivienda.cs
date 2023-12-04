using OpenQA.Selenium.Chrome;
using LoCoMProTestFuncionales.PageModels.Home;

namespace LoCoMProFunctionalTests.Pages.Home
{
    // Hecho por: Kenneth Daniel Villalobos Solís - C18548 - Sprint 2
    // Modificado por: Kenneth Daniel Villalobos Solís - C18548 - Sprint 3
    [TestFixture]
    public class CambiarViviendaTests
    {
        ChromeDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        private PaginaPerfil LlegarAPerfil()
        {
            PaginaHome paginaHome = new PaginaHome(driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());
            PaginaIngresar ingresar = paginaHome.IrAIngresarUsuario();
            paginaHome = ingresar.IniciarSesion("Usuario1*", "Usuario1*");
            
            return paginaHome.IrAPerfil();
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548 - Sprint 3
        [Test]
        public void CambioVivienda_CambioProvincia_ProvinciaActualizada()
        {
            // Preparación
            PaginaPerfil paginaCambio = LlegarAPerfil();

            // Acción
            paginaCambio.seleccionarProvincia("Alajuela");

            // Verificación
            string resultado = paginaCambio.ObtenerProvincia();
            Assert.That(resultado, Is.EqualTo("Alajuela"));
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548 - Sprint 3
        [Test]
        public void CambioVivienda_CambioCanton_CantonActualizado()
        {
            // Preparación
            PaginaPerfil paginaCambio = LlegarAPerfil();

            // Acción
            paginaCambio.seleccionarCanton("Desamparados");

            // Verificación
            string resultado = paginaCambio.ObtenerCanton();
            Assert.That(resultado, Is.EqualTo("Desamparados"));
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548 - Sprint 3
        [Test]
        public void CambioVivienda_CambioDistrito_DistritoActualizado()
        {
            // Preparación
            PaginaPerfil paginaCambio = LlegarAPerfil();

            // Acción
            paginaCambio.seleccionarDistrito("Mata Redonda");

            // Verificación
            string resultado = paginaCambio.ObtenerDistrito();
            Assert.That(resultado, Is.EqualTo("Mata Redonda"));
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548 - Sprint 3
        [Test]
        public void CambioVivienda_CambioCompleto_ProvinciaActualizada()
        {
            // Preparación
            PaginaPerfil paginaCambio = LlegarAPerfil();

            // Acción
            paginaCambio.seleccionarProvincia("Limón");
            paginaCambio.seleccionarCanton("Matina");
            paginaCambio.seleccionarDistrito("Carrandi");

            // Verificación
            string resultado = paginaCambio.ObtenerProvincia();
            Assert.That(resultado, Is.EqualTo("Limón"));
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548 - Sprint 3
        [Test]
        public void CambioVivienda_CambioCompleto_CantonActualizado()
        {
            // Preparación
            PaginaPerfil paginaCambio = LlegarAPerfil();

            // Acción
            paginaCambio.seleccionarProvincia("Heredia");
            paginaCambio.seleccionarCanton("Santa Barbara");
            paginaCambio.seleccionarDistrito("Puraba");

            // Verificación
            string resultado = paginaCambio.ObtenerCanton();
            Assert.That(resultado, Is.EqualTo("Santa Barbara"));
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548 - Sprint 2
        // Modificado por: Kenneth Daniel Villalobos Solís - C18548 - Sprint 3
        [Test]
        public void CambioVivienda_CambioCompleto_DistritoActualizado()
        {
            // Preparación
            PaginaPerfil paginaCambio = LlegarAPerfil();

            // Acción
            paginaCambio.seleccionarProvincia("Guanacaste");
            paginaCambio.seleccionarCanton("Hojancha");
            paginaCambio.seleccionarDistrito("Huacas");

            // Verificación
            string resultado = paginaCambio.ObtenerDistrito();
            Assert.That(resultado, Is.EqualTo("Huacas"));
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548 - Sprint 3
        [Test]
        public void CambioVivienda_SinCambio_BotonNoVisible()
        {
            // Preparación
            PaginaPerfil paginaCambio = LlegarAPerfil();

            // Acción
            paginaCambio.seleccionarProvincia("San José");

            // Verificación
            bool resultado = paginaCambio.GuardarCambiosEsVisible();
            Assert.That(!resultado);
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548 - Sprint 3
        [Test]
        public void CambioVivienda_SinCambio_BotonVisible()
        {
            // Preparación
            PaginaPerfil paginaCambio = LlegarAPerfil();

            // Acción
            paginaCambio.seleccionarProvincia("Heredia");

            // Verificación
            bool resultado = paginaCambio.GuardarCambiosEsVisible();
            Assert.That(resultado);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}