using OpenQA.Selenium;
using LoCoMProTestFuncionales.PageModels.Busqueda;
using LoCoMProTestFuncionales.PageModels;

namespace LoCoMProTestFuncionales.PageModels.Home
{
    public class PaginaHome : PaginaBase
    {
        private By CajaDeTextoProducto = By.Id("CajaDeTextoProducto");
        private By BotonDeBusqueda = By.Id("BotonBuscar");
        private By BotonBusquedaAvanzada = By.Id("BotonBusquedaAvanzada");
        private By BotonIngresar = By.Id("BotonIngresarLayout");
        private By PerfilDropdown = By.Id("perfilDropdown");
        private By PerfilLayoutPerfil = By.Id("BotonPerfilLayout");
        private By PerfilLayoutFavoritos = By.Id("BotonFavoritosLayout");

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

        public void DesplegarDropdownPefil()
        {
            IWebElement dropdownPerfil = driver.FindElement(PerfilDropdown);
            dropdownPerfil.Click();
        }

        public PaginaPerfil IrAPerfil()
        {
            DesplegarDropdownPefil();

            IWebElement botonLayoutPerfil = driver.FindElement(PerfilLayoutPerfil);
            botonLayoutPerfil.Click();

            return new PaginaPerfil(this.driver);
        }

        public Cuenta.PaginaFavoritos IrAFavoritos()
        {
            DesplegarDropdownPefil();

            IWebElement botonLayoutFavoritos = driver.FindElement(PerfilLayoutFavoritos);
            botonLayoutFavoritos.Click();

            return new Cuenta.PaginaFavoritos(this.driver);
        }

        public PaginaAvanzada IrABuscarAvanzado()
        {
            IWebElement botonBusquedaAvanzada = this.driver.FindElement(this.BotonBusquedaAvanzada);
            botonBusquedaAvanzada.Click();

            return new PaginaAvanzada(this.driver);
        }

        new public string ObtenerURL()
        {
            return base.ObtenerURL();
        }
    }
}
