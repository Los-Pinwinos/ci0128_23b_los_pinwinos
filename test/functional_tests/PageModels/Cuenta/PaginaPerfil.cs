using OpenQA.Selenium;
using LoCoMProTestFuncionales.PageModels.Busqueda;
using LoCoMProTestFuncionales.PageModels.Cuenta.CambiarContrasena;

namespace LoCoMProTestFuncionales.PageModels.Home
{
    public class PaginaPerfil : PaginaBase
    {
        protected By PerfilDropdown = By.Id("perfilDropdown");
        protected By PerfilLayoutPerfil = By.Id("BotonPerfilLayout");
        protected By PerfilBotonContrasena = By.Id("botonCambioContrasena");

        public PaginaPerfil(IWebDriver driver) : base(driver) { }


        public PaginaCambiarContrasena IrACambiarContrasena()
        {
            IWebElement botonCambiarContrasena = driver.FindElement(PerfilBotonContrasena);
            botonCambiarContrasena.Click();

            return new PaginaCambiarContrasena(this.driver);
        }

        new public string ObtenerURL()
        {
            return base.ObtenerURL();
        }
    }
}
