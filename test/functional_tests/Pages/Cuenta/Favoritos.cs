using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using LoCoMProTestFuncionales.PageModels.Busqueda;
using LoCoMProTestFuncionales.PageModels.Home;
using LoCoMProTestFuncionales.PageModels.VerRegistros;
using LoCoMProTestFuncionales.PageModels.Cuenta;

namespace LoCoMProFunctionalTests.Pages.Cuenta
{
    [TestFixture]
    public class FavoritosTests
    {
        ChromeDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        // Alumno: Enrique Guillermo Vílchez Lizano C18477 - Sprint 3
        [Test]
        public void PaginaFavoritos_AgregarAceiteAFavoritos_DeberiaEstarEnTablaDeVerFavoritos()
        {
            // Preparación
            PaginaHome paginaHome = new PaginaHome(this.driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());

            PaginaIngresar paginaIngresar = paginaHome.IrAIngresarUsuario();
            paginaIngresar.IniciarSesion("Envil0705!", "Envil0705!");
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());

            PaginaBusqueda paginaBusqueda = paginaHome.Buscar("aceite");
            string producto = paginaBusqueda.ObtenerTablaDeResultados()[0][0];
            // Obtener el primer resultado
            PaginaVerRegistros paginaVerRegistros = paginaBusqueda.SeleccionarResultado(0);
            
            // Acción
            paginaVerRegistros.AgregarProductoAFavoritos();

            // Verificación
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());
            PaginaFavoritos paginaFavoritos = paginaHome.IrAFavoritos();
            List<List<string>> resultados = paginaFavoritos.ObtenerTablaDeResultados();

            bool resultadosCorrectos = false;
            foreach (var resultado in resultados)
            {
                if (resultado[1].ToLower().Contains("aceite"))
                {
                    resultadosCorrectos = true;
                }
            }

            if (resultadosCorrectos)
            {
                // Devolver al estado orginal
                paginaFavoritos.EliminarFavorito(producto);
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