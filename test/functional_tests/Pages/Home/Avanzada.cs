using LoCoMProTestFuncionales.PageModels.Busqueda;
using LoCoMProTestFuncionales.PageModels.Home;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LoCoMProFunctionalTests.Pages.Home
{
    [TestFixture]
    public class AvanzadaTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Preparación
            driver = new ChromeDriver();
        }

        // Alumno: Enrique Guillermo Vílchez Lizano C18477 - Sprint 3
        [Test]
        public void PaginaAvanzada_BuscarPorProvinciaYCantonHeredia_DeberiaTenerResultadosConProvinciaYCantonHerediaSolamente()
        {
            // Preparación
            PaginaHome paginaHome = new PaginaHome(driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());
            
            PaginaAvanzada paginaAvanzada = paginaHome.IrABuscarAvanzado();

            // Acción
            PaginaBusquedaAvanzada paginaBusquedaAvanzada = paginaAvanzada.Buscar(provincia: "Heredia", canton: "Heredia");

            List<List<string>> resultados = paginaBusquedaAvanzada.ObtenerTablaDeResultados();

            // Verificación
            bool resultadosCorrectos = true;
            foreach (var resultado in resultados)
            {
                // Comprobar si la columna de provincia y cantón cumplen el criterio
                if (!(resultado[7] == "Heredia" && resultado[8] == "Heredia"))
                {
                    resultadosCorrectos = false;
                }
            }

            Assert.IsTrue(resultadosCorrectos);
        }

        // Alumno: Enrique Guillermo Vílchez Lizano C18477 - Sprint 3
        [Test]
        public void PaginaAvanzada_BuscarPorMarcaApple_DeberiaTenerResultadosConMarcaAppleSolamente()
        {
            // Preparación
            PaginaHome paginaHome = new PaginaHome(driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());

            PaginaAvanzada paginaAvanzada = paginaHome.IrABuscarAvanzado();

            // Acción
            PaginaBusquedaAvanzada paginaBusquedaAvanzada = paginaAvanzada.Buscar(marca:"Apple");

            List<List<string>> resultados = paginaBusquedaAvanzada.ObtenerTablaDeResultados();

            // Verificación
            bool resultadosCorrectos = true;
            foreach (var resultado in resultados)
            {
                // Comprobar si la columna de marca cumple el criterio
                if (!(resultado[2] == "Apple"))
                {
                    resultadosCorrectos = false;
                }
            }

            Assert.IsTrue(resultadosCorrectos);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
