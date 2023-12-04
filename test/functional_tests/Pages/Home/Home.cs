
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

        // Alumno: Luis David Solano Santamaría C17634 - Sprint 3
        [Test]
        public void PaginaHome_OpcionesModerador_ModeradorPuedeVerRegistrosReportados()
        {
            // Preparación
            PaginaHome paginaHome = IniciarSesionModerador();

            // Acción
            paginaHome.DesplegarDropdownModerador();

            // Verificación
            Assert.IsTrue(paginaHome.RegistrosReportadosVisibles());
        }

        // Alumno: Luis David Solano Santamaría C17634 - Sprint 3
        [Test]
        public void PaginaHome_OpcionesModerador_ModeradorPuedeVerOutliersPrecio()
        {
            // Preparación
            PaginaHome paginaHome = IniciarSesionModerador();

            // Acción
            paginaHome.DesplegarDropdownModerador();

            // Verificación
            Assert.IsTrue(paginaHome.OutliersPrecioVisible());
        }

        // Alumno: Luis David Solano Santamaría C17634 - Sprint 3
        [Test]
        public void PaginaHome_OpcionesModerador_ModeradorPuedeVerClustering()
        {
            // Preparación
            PaginaHome paginaHome = IniciarSesionModerador();

            // Acción
            paginaHome.DesplegarDropdownModerador();

            // Verificación
            Assert.IsTrue(paginaHome.ClusteringVisible());
        }

        // Alumno: Luis David Solano Santamaría C17634 - Sprint 3
        [Test]
        public void PaginaHome_OpcionesModerador_ModeradorPuedeVerMasReportan()
        {
            // Preparación
            PaginaHome paginaHome = IniciarSesionModerador();

            // Acción
            paginaHome.DesplegarDropdownModerador();

            // Verificación
            Assert.IsTrue(paginaHome.MasReportanVisible());
        }

        // Alumno: Luis David Solano Santamaría C17634 - Sprint 3
        [Test]
        public void PaginaHome_OpcionesModerador_ModeradorPuedeVerMasReportados()
        {
            // Preparación
            PaginaHome paginaHome = IniciarSesionModerador();

            // Acción
            paginaHome.DesplegarDropdownModerador();

            // Verificación
            Assert.IsTrue(paginaHome.MasReportadosVisible());
        }

        private PaginaHome IniciarSesionModerador()
        {
            PaginaHome paginaHome = new PaginaHome(this.driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());
            PaginaIngresar paginaIngresar = paginaHome.IrAIngresarUsuario();
            paginaHome = paginaIngresar.IniciarSesion("Usuario1*", "Usuario1*");
            return paginaHome;
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}

