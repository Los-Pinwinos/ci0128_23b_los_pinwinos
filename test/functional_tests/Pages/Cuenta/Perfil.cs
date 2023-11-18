using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LoCoMProFunctionalTests.Pages.Home
{
    [TestFixture]
    public class PerfilTests
    {
        ChromeDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        private void iniciarSesion()
        {
            // Ir a la p�gina de ingreso
            IWebElement botonLayoutIngresar = driver.FindElement(By.Id("BotonIngresarLayout"));
            botonLayoutIngresar.Click();


            // Escibir credenciales
            IWebElement cajaDeTextoUsuario = driver.FindElement(By.Id("CajaDeTextoUsuario"));
            cajaDeTextoUsuario.SendKeys("Usuario1*");

            // TODO: Cambiar
            IWebElement cajaDeTextoContrasena = driver.FindElement(By.Id("CajaDeTextoContrasena"));
            cajaDeTextoContrasena.SendKeys("Usuario1*");


            // Ingresar
            IWebElement botonIngresar = driver.FindElement(By.Id("botonIngresar"));
            botonIngresar.Click();
        }

        private void cambiarVivienda()
        {
            // Ir a la p�gina del perfil
            IWebElement dropdownPerfil = driver.FindElement(By.Id("perfilDropdown"));
            dropdownPerfil.Click();
            IWebElement botonLayoutPerfil = driver.FindElement(By.Id("BotonPerfilLayout"));
            botonLayoutPerfil.Click();


            // Seleccionar distintas opciones en las cajas
            // de selecci�n de vivienda
            IWebElement cajaSeleccionProvincia = driver.FindElement(By.Id("CajaDeSeleccionProvincia"));
            var selectorProvincia = new SelectElement(cajaSeleccionProvincia);
            selectorProvincia.SelectByValue("Guanacaste");

            IWebElement cajaSeleccionCanton = driver.FindElement(By.Id("CajaDeSeleccionCanton"));
            var selectorCanton = new SelectElement(cajaSeleccionCanton);
            selectorCanton.SelectByValue("Hojancha");

            IWebElement cajaSeleccionDistrito = driver.FindElement(By.Id("CajaDeSeleccionDistrito"));
            var selectorDistrito = new SelectElement(cajaSeleccionDistrito);
            selectorDistrito.SelectByValue("Huacas");


            // Guardar cambios
            IWebElement botonGuardar = driver.FindElement(By.Id("BotonGuardarCambios"));
            botonGuardar.Click();
        }

        [Test]
        public void CambiarViviendaUsuario()
        {
            // Ir a la p�gina home
            driver.Navigate().GoToUrl("http://localhost:5150");

            iniciarSesion();

            cambiarVivienda();

            // Verificar
            // Obtener distrito de vivienda
            IWebElement cajaSeleccionDistrito = driver.FindElement(By.Id("CajaDeSeleccionDistrito"));
            var selectorDistrito = new SelectElement(cajaSeleccionDistrito);
            IList<IWebElement> distritoSeleccionado = selectorDistrito.AllSelectedOptions;

            // Crear valor esperado
            var elementoHuacas = driver.FindElement(By.CssSelector("option[value='Huacas']"));
            IWebElement[] distritoEsperado = { elementoHuacas };

            // Assert
            CollectionAssert.AreEqual(distritoSeleccionado, distritoEsperado.ToArray());
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}