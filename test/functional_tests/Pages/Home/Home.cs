
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using LoCoMProTestFuncionales.PageModels.Busqueda;
using LoCoMProTestFuncionales.PageModels.Home;

namespace LoCoMProFunctionalTests.Pages.Home
{
    [TestFixture]
    public class HomeTests
    {
        private IWebDriver driver;
        private PaginaHome paginaHome;

        [SetUp]
        public void Setup()
        {
            // Preparación
            driver = new ChromeDriver();
            paginaHome = new PaginaHome(driver);
        }

        // Alumno: Enrique Guillermo Vílchez Lizano C18477 - Sprint 2
        [Test]
        public void PaginaHome_BuscarProductoI_FilasDebenTenerSoloProductosQueContienenEnElNombreI()
        {
            // Preparación
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());

            // Acción
            PaginaBusqueda paginaBusqueda = paginaHome.Buscar("i");

            List<List<string>> resultados = paginaBusqueda.ObtenerTablaDeResultados();

            // Verificación
            bool resultadosCorrectos = true;
            foreach (var resultado in resultados)
            {
                if (!resultado[0].ToLower().Contains("i"))
                {
                    resultadosCorrectos = false;

                }
            }
            Assert.IsTrue(resultadosCorrectos);

        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}

