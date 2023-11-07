
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

        // Alumno: Luis David Solano Santamaría C17634 - Sprint 2
        [Test]
        public void PaginaBusqueda_OrdenamientoAscendienteNombre_ColumnaNoDebeTenerFlecha()
        {
            // Preparación
            PaginaHome paginaHome = new PaginaHome(this.driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());

            // Acción
            this.paginaBusqueda = paginaHome.Buscar("a");

            // Verificación
            Assert.IsFalse(paginaBusqueda.RevisarFlecha("DescendenteNombre"));
        }

        // Alumno: Luis David Solano Santamaría C17634 - Sprint 2
        [Test]
        public void PaginaBusqueda_OrdenamientoAscendienteNombre_ColumnaDebeTenerFlecha()
        {
            // Preparación
            PaginaHome paginaHome = new PaginaHome(this.driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());
            this.paginaBusqueda = paginaHome.Buscar("a");

            // Acción
            this.paginaBusqueda.OrdenarColumna("//div[@id='FilaDeFiltrosYResultados']/div/table/thead/tr/th/label");

            // Verificación
            Assert.IsTrue(paginaBusqueda.RevisarFlecha("DescendenteNombre"));
        }

        // Alumno: Luis David Solano Santamaría C17634 - Sprint 2
        [Test]
        public void PaginaBusqueda_OrdenamientoDescendienteNombre_ColumnaDebeTenerFlecha()
        {
            // Preparación
            PaginaHome paginaHome = new PaginaHome(this.driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());
            this.paginaBusqueda = paginaHome.Buscar("a");

            // Acción
            this.paginaBusqueda.OrdenarColumna("//div[@id='FilaDeFiltrosYResultados']/div/table/thead/tr/th/label");
            this.paginaBusqueda.OrdenarColumna("//div[@id='FilaDeFiltrosYResultados']/div/table/thead/tr/th/label");

            // Verificación
            Assert.IsTrue(paginaBusqueda.RevisarFlecha("AscendenteNombre"));
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}

