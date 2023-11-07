using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using LoCoMProTestFuncionales.PageModels.VerRegistros;


namespace LoCoMProTestFuncionales.PageModels.Busqueda
{
    public class PaginaBusqueda : PaginaBase
    {
        protected By CajaDeTextoProducto = By.Id("CajaDeTextoProducto");
        protected By BotonDeBusqueda = By.ClassName("BusquedaIndice-boton-busqueda");

        protected By BotonAplicarFiltros = By.CssSelector(".BusquedaIndice-boton-filtros[onclick='filtrar()']");
        protected By BotonLimpiarFiltros = By.CssSelector(".BusquedaIndice-boton-filtros[onclick='limpiarFiltros()']");

        protected By CabezaDeTablaDeResultados = By.TagName("thead");
        protected By CuerpoDeTablaDeResultados = By.TagName("tbody");

        protected Dictionary<string, By> desplegablesDeFiltros;

        public PaginaBusqueda(IWebDriver driver) : base(driver)
        {
            this.desplegablesDeFiltros = new Dictionary<string, By>();

            InicializarFiltros();
        }
        private void InicializarFiltros()
        {
            this.desplegablesDeFiltros.Add("provincia", By.XPath("//div[@class='BusquedaIndice-dropdown' and @onmouseenter='mostrarDesplegable(1)' and @onmouseleave='ocultarDesplegable(1)']"));
            this.desplegablesDeFiltros.Add("canton", By.XPath("//div[@class='BusquedaIndice-dropdown' and @onmouseenter='mostrarDesplegable(2)' and @onmouseleave='ocultarDesplegable(2)']"));
            this.desplegablesDeFiltros.Add("tienda", By.XPath("//div[@class='BusquedaIndice-dropdown' and @onmouseenter='mostrarDesplegable(3)' and @onmouseleave='ocultarDesplegable(3)']"));
            this.desplegablesDeFiltros.Add("marca", By.XPath("//div[@class='BusquedaIndice-dropdown' and @onmouseenter='mostrarDesplegable(4)' and @onmouseleave='ocultarDesplegable(4)']"));
            this.desplegablesDeFiltros.Add("categoria", By.XPath("//div[@class='BusquedaIndice-dropdown' and @onmouseenter='mostrarDesplegable(5)' and @onmouseleave='ocultarDesplegable(5)']"));
        }

        public PaginaBusqueda Buscar(string nombreDeProducto)
        {
            this.driver.FindElement(CajaDeTextoProducto).SendKeys(nombreDeProducto);
            this.driver.FindElement(BotonDeBusqueda).Click();

            return new PaginaBusqueda(this.driver);
        }

        public List<List<string>> ObtenerTablaDeResultados()
        {
            List<List<string>> resultados = new List<List<string>>();

            IWebElement cuerpoTabla = driver.FindElement(this.CuerpoDeTablaDeResultados);

            IList<IWebElement> filas = cuerpoTabla.FindElements(By.TagName("tr"));

            foreach (IWebElement fila in filas)
            {
                List<string> datosFila = new List<string>();

                IList<IWebElement> celdas = fila.FindElements(By.TagName("td"));

                foreach (IWebElement celda in celdas)
                {
                    datosFila.Add(celda.Text);
                }

                resultados.Add(datosFila);
            }

            return resultados;

        }

        public PaginaVerRegistros SeleccionarResultado(int resultado)
        {
            IWebElement cuerpoTabla = driver.FindElement(this.CuerpoDeTablaDeResultados);

            IList<IWebElement> filas = cuerpoTabla.FindElements(By.TagName("tr"));

            if (filas.Count > resultado)
            {
                filas[resultado].Click();
                filas[resultado].Click();
            }
            return new PaginaVerRegistros(this.driver);
        }

        public void FiltrarConCajasDeSeleccion(string nombreDeFiltro, string valorDeFiltrado)
        {
            IWebElement filtro = driver.FindElement(this.desplegablesDeFiltros[nombreDeFiltro]);

            Actions action = new Actions(driver);
            action.MoveToElement(filtro).Perform();

            IReadOnlyCollection<IWebElement> opciones = driver.FindElements(By.Name(nombreDeFiltro));

            foreach (IWebElement opcion in opciones)
            {
                if(opcion.GetAttribute("value") ==  valorDeFiltrado)
                    opcion.Click();
            }

            driver.FindElement(this.BotonAplicarFiltros).Click();
        }
        new public string ObtenerURL()
        {
            return base.ObtenerURL() + "/Busqueda";
        }
    }
}
