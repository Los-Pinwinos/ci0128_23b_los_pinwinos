
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using LoCoMProTestFuncionales.PageModels.Busqueda;
using LoCoMProTestFuncionales.PageModels.Home;

namespace LoCoMProTestFuncionales.Pages.Busqueda
{
    [TestFixture]
    public class BusquedaTests
    {
        private IWebDriver driver;
        private PaginaBusqueda paginaBusqueda;

        [SetUp]
        public void Setup()
        {
            // Preparación
            driver = new ChromeDriver();
        }

        // Alumno: Enrique Guillermo Vílchez Lizano C18477 - Sprint 2
        [Test]
        public void PaginaBusqueda_FiltradoPorProvinciaGuanacaste_FilasDebenTenerSoloProvinciaGuanacaste()
        {
            // Preparación
            PaginaHome paginaHome = new PaginaHome(this.driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());

            this.paginaBusqueda = paginaHome.Buscar("i");

            // Acción
            this.paginaBusqueda.FiltrarConCajasDeSeleccion("provincia", "Guanacaste");
            List<List<string>> resultados = this.paginaBusqueda.ObtenerTablaDeResultados();

            // Verificación
            bool filtradoCorrecto = true;
            foreach(var  resultado in resultados)
            {
                if (resultado[7] != "Guanacaste")
                {
                    filtradoCorrecto = false;
                }
            }
            
            Assert.IsTrue(filtradoCorrecto);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}

