using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace LoCoMProFunctionalTests.Pages.AgregarTienda
{
    // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 2
    internal class Tienda
    {
        private IWebDriver driver;

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 2
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 2
        [Test]
        public void RevisarCantonesDeLimonEnAgregarTienda()
        {
            // Preparación
            driver.Navigate().GoToUrl("http://localhost:5150");
            IniciarSesion();

            //  Acceder a agregar tienda
            var botonAgregarRegistro = driver.FindElement(By.LinkText("Agregar registro"));
            botonAgregarRegistro.Click();

            //  Variables de agregar tienda
            var provinciaDropdown = driver.FindElement(By.Id("Provincia"));
            var cantonDropdown = driver.FindElement(By.Id("Canton"));
            SelectElement provinciaSelector = new SelectElement(provinciaDropdown);
            SelectElement cantonSelector = new SelectElement(cantonDropdown);
            IList<IWebElement> opcionesCanton;
            bool correcto = true;

            // Acción
            provinciaSelector.SelectByText("Limón");
            opcionesCanton = cantonSelector.Options;
            if (opcionesCanton.Count == 6)
            {
                for (int i = 0; i < opcionesCanton.Count && correcto; ++i)
                {
                    correcto = VerficarOpcionCanton(i, opcionesCanton[i].Text);
                }
            }
            else
            {
                correcto = false;
            }

            // Verificación
            Assert.That(correcto, Is.True);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 2
        private void IniciarSesion()
        {
            var botonIniciarSesionHome = driver.FindElement(By.LinkText("Iniciar sesión"));
            botonIniciarSesionHome.Click();

            var usuario = driver.FindElement(By.Id("CajaDeTextoUsuario"));
            usuario.SendKeys("Usuario1*");

            var contrasena = driver.FindElement(By.Id("CajaDeTextoContrasena"));
            contrasena.SendKeys("Usuario1*");

            var botonIniciarSesion = driver.FindElement(By.CssSelector(".Ingresar-boton-basico"));
            botonIniciarSesion.Click();
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 2
        private static bool VerficarOpcionCanton(int i, string canton)
        {
            bool igualdad = false;

            switch (i)
            {
                case 0:
                    igualdad = (canton == "Guácimo");
                    break;
                case 1:
                    igualdad = (canton == "Limón");
                    break;
                case 2:
                    igualdad = (canton == "Matina");
                    break;
                case 3:
                    igualdad = (canton == "Pococí");
                    break;
                case 4:
                    igualdad = (canton == "Siquirres");
                    break;
                case 5:
                    igualdad = (canton == "Talamanca");
                    break;
            }

            return igualdad;
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686 - Sprint 2
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
