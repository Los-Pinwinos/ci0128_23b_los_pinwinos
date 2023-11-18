using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using LoCoMProTestFuncionales.PageModels.VerRegistros;


namespace LoCoMProTestFuncionales.PageModels.Busqueda
{
    public class PaginaBusqueda : PaginaBase
    {
        protected By CajaDeTextoProducto = By.Id("CajaDeTextoProducto");
        protected By BotonDeBusqueda = By.ClassName("BusquedaIndice-boton-busqueda");

        protected By BotonAplicarFiltros = By.CssSelector(".BusquedaIndice-boton-filtros[onclick='aplicarFiltrosFuncion()']");
        protected By BotonLimpiarFiltros = By.CssSelector(".BusquedaIndice-boton-filtros[onclick='limpiarFiltrosFuncion()']");

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
            this.desplegablesDeFiltros.Add("provincia", By.Id("Dropdown1"));
            this.desplegablesDeFiltros.Add("canton", By.Id("Dropdown2"));
            this.desplegablesDeFiltros.Add("tienda", By.Id("Dropdown3"));
            this.desplegablesDeFiltros.Add("marca", By.Id("Dropdown4"));
            this.desplegablesDeFiltros.Add("categoria", By.Id("Dropdown5"));
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

            filtro.Click();

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

        public void OrdenarColumna(string columna)
        {
            IWebElement ordenador = driver.FindElement(By.XPath(columna));
            ordenador.Click();
        }

        public bool RevisarFlecha(string flecha)
        {
            return driver.FindElement(By.Id(flecha)).Displayed;
    }
    }
}
