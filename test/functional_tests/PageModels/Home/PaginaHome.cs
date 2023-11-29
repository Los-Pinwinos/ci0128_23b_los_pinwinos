using OpenQA.Selenium;
using LoCoMProTestFuncionales.PageModels.Busqueda;
using LoCoMProTestFuncionales.PageModels.Cuenta;

namespace LoCoMProTestFuncionales.PageModels.Home
{
    public class PaginaHome : PaginaBase
    {
        protected By CajaDeTextoProducto = By.Id("CajaDeTextoProducto");
        protected By BotonDeBusqueda = By.Id("BotonBuscar");
        protected By BotonIngresar = By.Id("BotonIngresarLayout");
        protected By PerfilDropdown = By.Id("perfilDropdown");
        protected By PerfilLayoutPerfil = By.Id("BotonPerfilLayout");


        public PaginaHome(IWebDriver driver) : base(driver) { }

        public PaginaBusqueda Buscar(string nombreDeProducto)
        {
            this.driver.FindElement(CajaDeTextoProducto).SendKeys(nombreDeProducto);
            this.driver.FindElement(BotonDeBusqueda).Click();

            return new PaginaBusqueda(this.driver);
        }

        public PaginaIngresar IrAIngresarUsuario()
        {
            IWebElement botonLayoutIngresar = driver.FindElement(BotonIngresar);
            botonLayoutIngresar.Click();

            return new PaginaIngresar(this.driver);
        }

        public PaginaPerfil IrAPerfil()
        {
            IWebElement dropdownPerfil = driver.FindElement(PerfilDropdown);
            dropdownPerfil.Click();
            IWebElement botonLayoutPerfil = driver.FindElement(PerfilLayoutPerfil);
            botonLayoutPerfil.Click();

            return new PaginaPerfil(this.driver);
        }

        new public string ObtenerURL()
        {
            return base.ObtenerURL();
        }
    }
}
