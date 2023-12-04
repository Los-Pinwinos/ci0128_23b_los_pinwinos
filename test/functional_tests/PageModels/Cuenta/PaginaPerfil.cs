using OpenQA.Selenium;
using LoCoMProTestFuncionales.PageModels.Busqueda;
using LoCoMProTestFuncionales.PageModels.Cuenta.CambiarContrasena;
using OpenQA.Selenium.Support.UI;

namespace LoCoMProTestFuncionales.PageModels.Home
{
    public class PaginaPerfil : PaginaBase
    {
        protected By PerfilDropdown = By.Id("perfilDropdown");
        protected By PerfilLayoutPerfil = By.Id("BotonPerfilLayout");
        protected By PerfilSeleccionProvincia = By.Id("CajaDeSeleccionProvincia");
        protected By PerfilSeleccionCanton = By.Id("CajaDeSeleccionCanton");
        protected By PerfilSeleccionDistrito = By.Id("CajaDeSeleccionDistrito");
        protected By PerfilBotonGuardar = By.Id("BotonGuardarCambios");
        protected By PerfilBotonContrasena = By.Id("botonCambioContrasena");

        public PaginaPerfil(IWebDriver driver) : base(driver) { }

        public void seleccionarProvincia(string provincia)
        {
            IWebElement cajaSeleccionProvincia = driver.FindElement(PerfilSeleccionProvincia);
            var selectorProvincia = new SelectElement(cajaSeleccionProvincia);
            selectorProvincia.SelectByValue(provincia);
        }

        public void seleccionarCanton(string canton)
        {
            IWebElement cajaSeleccionCanton = driver.FindElement(PerfilSeleccionCanton);
            var selectorCanton = new SelectElement(cajaSeleccionCanton);
            selectorCanton.SelectByValue(canton);
        }

        public void seleccionarDistrito(string distrito)
        {
            IWebElement cajaSeleccionDistrito = driver.FindElement(PerfilSeleccionDistrito);
            var selectorDistrito = new SelectElement(cajaSeleccionDistrito);
            selectorDistrito.SelectByValue(distrito);
        }

        public void GuardarCambios()
        {
            IWebElement botonGuardar = driver.FindElement(PerfilBotonGuardar);
            botonGuardar.Click();
        }

        public string ObtenerProvincia()
        {
            IWebElement cajaSeleccionProvincia = driver.FindElement(PerfilSeleccionProvincia);
            var selectorProvincia = new SelectElement(cajaSeleccionProvincia);
            IWebElement selectedOption = selectorProvincia.SelectedOption;

            return selectedOption.Text;
        }

        public string ObtenerCanton()
        {
            IWebElement cajaSeleccionCanton = driver.FindElement(PerfilSeleccionCanton);
            var selectorCanton = new SelectElement(cajaSeleccionCanton);
            IWebElement selectedOption = selectorCanton.SelectedOption;

            return selectedOption.Text;
        }

        public string ObtenerDistrito()
        {
            IWebElement cajaSeleccionDistrito = driver.FindElement(PerfilSeleccionDistrito);
            var selectorDistrito = new SelectElement(cajaSeleccionDistrito);
            IWebElement selectedOption = selectorDistrito.SelectedOption;

            return selectedOption.Text;
        }

        public bool GuardarCambiosEsVisible()
        {
            IWebElement botonGuardar = driver.FindElement(PerfilBotonGuardar);
            string display = botonGuardar.GetCssValue("display");

            return display.Equals("block", StringComparison.OrdinalIgnoreCase);
        }

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
