using OpenQA.Selenium;
using LoCoMProTestFuncionales.PageModels.Busqueda;

namespace LoCoMProTestFuncionales.PageModels.Home
{
    public class PaginaIngresar : PaginaBase
    {
        protected By BotonIngresar = By.Id("BotonIngresarLayout");
        protected By UsuarioIngresar = By.Id("CajaDeTextoUsuario");
        protected By ContraseñaIngresar = By.Id("CajaDeTextoContrasena");
        protected By IniciarSesionBoton = By.Id("botonIngresar");

        public PaginaIngresar(IWebDriver driver) : base(driver) { }

        public PaginaHome IniciarSesion(string usuario, string contrasena)
        {
            IWebElement botonLayoutIngresar = driver.FindElement(BotonIngresar);
            botonLayoutIngresar.Click();

            IWebElement cajaDeTextoUsuario = driver.FindElement(UsuarioIngresar);
            cajaDeTextoUsuario.SendKeys(usuario);

            IWebElement cajaDeTextoContrasena = driver.FindElement(ContraseñaIngresar);
            cajaDeTextoContrasena.SendKeys(contrasena);

            IWebElement botonIngresar = driver.FindElement(IniciarSesionBoton);
            botonIngresar.Click();

            return new PaginaHome(this.driver);
        }

        new public string ObtenerURL()
        {
            return base.ObtenerURL();
        }
    }
}
