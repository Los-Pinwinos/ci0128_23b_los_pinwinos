using OpenQA.Selenium;
using LoCoMProTestFuncionales.PageModels.Busqueda;

namespace LoCoMProTestFuncionales.PageModels.Home
{
    public class PaginaHome : PaginaBase
    {
        protected By CajaDeTextoProducto = By.Id("CajaDeTextoProducto");
        protected By BotonDeBusqueda = By.Id("BotonBuscar");

        public PaginaHome(IWebDriver driver) : base(driver) { }

        public PaginaBusqueda Buscar(string nombreDeProducto)
        {
            this.driver.FindElement(CajaDeTextoProducto).SendKeys(nombreDeProducto);
            this.driver.FindElement(BotonDeBusqueda).Click();

            return new PaginaBusqueda(this.driver);
        }

        new public string ObtenerURL()
        {
            return base.ObtenerURL();
        }
    }
}
