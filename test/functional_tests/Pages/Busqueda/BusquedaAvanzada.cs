using LoCoMProTestFuncionales.PageModels.Busqueda;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoCoMProTestFuncionales.PageModels.Home;

namespace LoCoMProFunctionalTests.Pages.Busqueda
{
    [TestFixture]
    public class BusquedaAvanzada
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
        public void PaginaBusquedaAvanzada_BuscarPorProvinciaHerediaYCantonTodos_DeberiaTenerResultadosConProvinciaHeredia()
        {
            // Preparación
            PaginaHome paginaHome = new PaginaHome(driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());

            PaginaAvanzada paginaAvanzada = paginaHome.IrABuscarAvanzado();
            // Se hace una búsqueda no significativa para moverse entre ventanas
            PaginaBusquedaAvanzada paginaBusquedaAvanzada = paginaAvanzada.Buscar();
            paginaBusquedaAvanzada.EsperarSegundos(2);
            // Acción
            paginaBusquedaAvanzada.Buscar(provincia: "Heredia");
            List<List<string>> resultados = paginaBusquedaAvanzada.ObtenerTablaDeResultados();

            // Verificación
            bool resultadosCorrectos = true;
            foreach (var resultado in resultados)
            {
                // Comprobar si la columna de provincia cumple el criterio
                if (!(resultado[7] == "Heredia"))
                {
                    resultadosCorrectos = false;
                }
            }

            Assert.IsTrue(resultadosCorrectos);
        }

        // Alumno: Enrique Guillermo Vílchez Lizano C18477 - Sprint 3
        [Test]
        public void PaginaBusquedaAvanzada_BuscarPorProductoIPhone14YMarcaApple_DeberiaTenerResultadosSoloDeIphone14YMarcaApple()
        {
            // Preparación
            PaginaHome paginaHome = new PaginaHome(driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());

            PaginaAvanzada paginaAvanzada = paginaHome.IrABuscarAvanzado();
            // Se hace una búsqueda no significativa para moverse entre ventanas
            PaginaBusquedaAvanzada paginaBusquedaAvanzada = paginaAvanzada.Buscar();

            // Acción
            paginaBusquedaAvanzada.Buscar(producto: "Iphone 14", marca: "Apple");
            List<List<string>> resultados = paginaBusquedaAvanzada.ObtenerTablaDeResultados();

            // Verificación
            bool resultadosCorrectos = true;
            foreach (var resultado in resultados)
            {
                // Comprobar si la columna de provincia cumple el criterio
                if (!(resultado[0] == "Iphone 14" && resultado[2] == "Apple"))
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
