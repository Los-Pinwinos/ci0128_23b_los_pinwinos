using LoCoMProTestFuncionales.PageModels.Home;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace LoCoMProTestFuncionales.PageModels.Busqueda
{
    public class PaginaBusquedaAvanzada : PaginaBusqueda
    {
        private By CajaDeTextoMarca = By.Id("CajaDeTextoMarca");
        private By CajaDeSeleccionCanton = By.Id("CajaDeSeleccionCanton");
        private By CajaDeSeleccionProvincia = By.Id("CajaDeSeleccionProvincia");

        public PaginaBusquedaAvanzada(IWebDriver driver) : base(driver)
        {
            this.BotonDeBusqueda = By.ClassName("BusquedaAvanzada-boton-busqueda");
        }

        public void LlenarMarca(string marca)
        {
            IWebElement cajaMarca = this.driver.FindElement(this.CajaDeTextoMarca);
            cajaMarca.SendKeys(marca);
        }

        public void LlenarProducto(string producto)
        {
            IWebElement cajaProducto = this.driver.FindElement(this.CajaDeTextoProducto);
            cajaProducto.SendKeys(producto);
        }

        public void SeleccionarProvincia(string provincia)
        {
            IWebElement cajaProvincia = this.driver.FindElement(this.CajaDeSeleccionProvincia);

            SelectElement selectorProvincia = new SelectElement(cajaProvincia);
            try
            {
                selectorProvincia.SelectByValue(provincia);
            }
            // En caso de que el elemento no exista
            catch (NoSuchElementException)
            {
                throw new Exception("No existe la provincia seleccionada");
            }
        }

        public void SeleccionarCanton(string canton)
        {
            IWebElement cajaCanton = this.driver.FindElement(this.CajaDeSeleccionCanton);

            SelectElement selectorCanton = new SelectElement(cajaCanton);
            try
            {
                selectorCanton.SelectByValue(canton);
                // En caso de que el elemento no exista
            }
            catch (NoSuchElementException)
            {
                throw new Exception("No existe el cantón seleccionado");
            }
        }

        public PaginaBusquedaAvanzada Buscar(string? producto = null, string? marca = null, string? provincia = null, string? canton = null)
        {
            if (!string.IsNullOrEmpty(producto))
            {
                this.LlenarProducto(producto);
            }
            if (!string.IsNullOrEmpty(marca))
            {
                this.LlenarMarca(marca);
            }
            if (!string.IsNullOrEmpty(provincia))
            {
                this.SeleccionarProvincia(provincia);
            }
            if (!string.IsNullOrEmpty(canton))
            {
                this.SeleccionarCanton(canton);
            }
            // Hacer la búsqueda
            IWebElement botonBusqueda = this.driver.FindElement(this.BotonDeBusqueda);
            botonBusqueda.Click();

            return new PaginaBusquedaAvanzada(this.driver);
        }
    }
}
