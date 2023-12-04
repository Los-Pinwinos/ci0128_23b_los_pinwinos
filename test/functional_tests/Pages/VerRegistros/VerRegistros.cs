using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using LoCoMProTestFuncionales.PageModels.Busqueda;
using LoCoMProTestFuncionales.PageModels.Home;
using LoCoMProTestFuncionales.PageModels.VerRegistros;

namespace LoCoMProTestFuncionales.Pages.VerRegistros
{
    public class VerRegistrosTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
        }


        public PaginaVerRegistros BuscarElemento(string elemento)
        {
            PaginaHome paginaHome = new PaginaHome(driver);
            driver.Navigate().GoToUrl(paginaHome.ObtenerURL());
            PaginaBusqueda paginaBusqueda = paginaHome.Buscar(elemento);
            return paginaBusqueda.SeleccionarResultado(0);
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [Test]
        public void VerRegistros_RevisarTituloSemana_DebeCambiar()
        {
            // Preparación
            PaginaVerRegistros paginaVerRegistros = this.BuscarElemento("Camisa");

            // Acción
            paginaVerRegistros.PresionarAgrupamientoSemana();

            // Verificación
            Assert.That(paginaVerRegistros.ObtenerValorTitulo(0), Is.EqualTo("Semana"));
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [Test]
        public void VerRegistros_RevisarTituloMes_DebeCambiar()
        {
            // Preparación
            PaginaVerRegistros paginaVerRegistros = this.BuscarElemento("Camisa");

            // Acción
            paginaVerRegistros.PresionarAgrupamientoMes();

            // Verificación
            Assert.That(paginaVerRegistros.ObtenerValorTitulo(0), Is.EqualTo("Mes"));
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [Test]
        public void VerRegistros_RevisarTituloAno_DebeCambiar()
        {
            // Preparación
            PaginaVerRegistros paginaVerRegistros = this.BuscarElemento("Camisa");

            // Acción
            paginaVerRegistros.PresionarAgrupamientoAno();

            // Verificación
            Assert.That(paginaVerRegistros.ObtenerValorTitulo(0), Is.EqualTo("Año"));
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [Test]
        public void VerRegistros_RevisarTituloPrecioMin_DebeCambiar()
        {
            // Preparación
            PaginaVerRegistros paginaVerRegistros = this.BuscarElemento("Camisa");

            // Acción
            paginaVerRegistros.PresionarAgrupamientoDia();

            // Verificación
            Assert.That(paginaVerRegistros.ObtenerValorTitulo(1), Is.EqualTo("Precio Mínimo"));
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [Test]
        public void VerRegistros_RevisarTituloPrecioPromedio_DebeCambiar()
        {
            // Preparación
            PaginaVerRegistros paginaVerRegistros = this.BuscarElemento("Camisa");

            // Acción
            paginaVerRegistros.PresionarAgrupamientoDia();

            // Verificación
            Assert.That(paginaVerRegistros.ObtenerValorTitulo(2), Is.EqualTo("Precio Promedio"));
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [Test]
        public void VerRegistros_RevisarTituloPrecioMax_DebeCambiar()
        {
            // Preparación
            PaginaVerRegistros paginaVerRegistros = this.BuscarElemento("Camisa");

            // Acción
            paginaVerRegistros.PresionarAgrupamientoDia();

            // Verificación
            Assert.That(paginaVerRegistros.ObtenerValorTitulo(3), Is.EqualTo("Precio Máximo"));
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [Test]
        public void VerRegistros_RevisarTituloCalifPromedio_DebeCambiar()
        {
            // Preparación
            PaginaVerRegistros paginaVerRegistros = this.BuscarElemento("Camisa");

            // Acción
            paginaVerRegistros.PresionarAgrupamientoDia();

            // Verificación
            Assert.That(paginaVerRegistros.ObtenerValorTitulo(4), Is.EqualTo("Calificación Promedio"));
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [Test]
        public void VerRegistros_RevisarTituloFechaInicial_PorDefecto()
        {
            // Preparación y Acción (se quiere verificar que los títulos inicien como se debe, por lo que la preparación también corresponde a la acción inicial)
            PaginaVerRegistros paginaVerRegistros = this.BuscarElemento("Camisa");

            // Verificación
            Assert.That(paginaVerRegistros.ObtenerValorTitulo(0), Is.EqualTo("Fecha"));
        }


        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [Test]
        public void VerRegistros_RevisarTituloPrecioInicial_PorDefecto()
        {
            // Preparación y Acción 
            PaginaVerRegistros paginaVerRegistros = this.BuscarElemento("Camisa");

            // Verificación
            Assert.That(paginaVerRegistros.ObtenerValorTitulo(1), Is.EqualTo("Precio"));
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [Test]
        public void VerRegistros_RevisarTituloCalificacionInicial_PorDefecto()
        {
            // Preparación y Acción 
            PaginaVerRegistros paginaVerRegistros = this.BuscarElemento("Camisa");

            // Verificación
            Assert.That(paginaVerRegistros.ObtenerValorTitulo(2), Is.EqualTo("Calificación"));
        }

        // Hecho por: Emilia María Víquez Mora - C18625 - Sprint 2
        [Test]
        public void VerRegistros_RevisarTituloDescripcioneInicial_PorDefecto()
        {
            // Preparación y Acción 
            PaginaVerRegistros paginaVerRegistros = this.BuscarElemento("Camisa");

            // Verificación
            Assert.That(paginaVerRegistros.ObtenerValorTitulo(3), Is.EqualTo("Descripción"));
        }


        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}