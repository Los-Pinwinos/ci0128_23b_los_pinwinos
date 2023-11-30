using OpenQA.Selenium;


namespace LoCoMProTestFuncionales.PageModels.Cuenta.CambiarContrasena
{
    public class PaginaCambiarContrasena : PaginaBase
    {
        protected By cajaActual = By.Id("CajaDeTextoActual");
        protected By cajaNueva = By.Id("CajaDeTextoNueva");
        protected By cajaConfirmacion = By.Id("CajaDeTextoConfirmacion");
        protected By botonConfirmarCambios = By.Id("BotonGuardarCambios");
        protected By mensajeAlerta = By.XPath("//form/div");

        public PaginaCambiarContrasena(IWebDriver driver) : base(driver) { }

        public void IngresarContrasenaActual(string texto)
        {
            IWebElement cajaTextoActual = driver.FindElement(cajaActual);
            cajaTextoActual.SendKeys(texto);
        }

        public void IngresarContrasenaNueva(string texto)
        {
            IWebElement cajaTextoNueva = driver.FindElement(cajaNueva);
            cajaTextoNueva.SendKeys(texto);
        }
        public void IngresarConfirmacionNueva(string texto)
        {
            IWebElement cajaTextoNueva = driver.FindElement(cajaConfirmacion);
            cajaTextoNueva.SendKeys(texto);
        }

        public void GuardarCambios()
        {
            IWebElement botonGuardar = driver.FindElement(botonConfirmarCambios);
            botonGuardar.Click();
        }

        public string ObtenerMensajeError()
        {
            return driver.FindElement(mensajeAlerta).Text;
        }
    }
}