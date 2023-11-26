using OpenQA.Selenium;
using LoCoMProTestFuncionales.PageModels.Busqueda;
using LoCoMProTestFuncionales.PageModels.Cuenta.CambiarContrasena;

namespace LoCoMProTestFuncionales.PageModels.Home
{
    public class PaginaHome : PaginaBase
    {
        protected By CajaDeTextoProducto = By.Id("CajaDeTextoProducto");
        protected By BotonDeBusqueda = By.Id("BotonBuscar");
        protected By BotonIngresar = By.Id("BotonIngresarLayout");
        protected By UsuarioIngresar = By.Id("CajaDeTextoUsuario");
        protected By ContraseñaIngresar = By.Id("CajaDeTextoContrasena");
        protected By IniciarSesionBoton = By.Id("botonIngresar");
        protected By PerfilDropdown = By.Id("perfilDropdown");
        protected By PerfilLayoutPerfil = By.Id("BotonPerfilLayout");
        protected By PerfilBotonContrasena = By.Id("bnotonCambioContrasena");


        public PaginaHome(IWebDriver driver) : base(driver) { }

        public PaginaBusqueda Buscar(string nombreDeProducto)
        {
            this.driver.FindElement(CajaDeTextoProducto).SendKeys(nombreDeProducto);
            this.driver.FindElement(BotonDeBusqueda).Click();

            return new PaginaBusqueda(this.driver);
        }

        public void IniciarSesion(string usuario, string contrasena)
        {
            IWebElement botonLayoutIngresar = driver.FindElement(BotonIngresar);
            botonLayoutIngresar.Click();

            IWebElement cajaDeTextoUsuario = driver.FindElement(UsuarioIngresar);
            cajaDeTextoUsuario.SendKeys(usuario);

            IWebElement cajaDeTextoContrasena = driver.FindElement(ContraseñaIngresar);
            cajaDeTextoContrasena.SendKeys(contrasena);

            IWebElement botonIngresar = driver.FindElement(IniciarSesionBoton);
            botonIngresar.Click();
        }

        public PaginaCambiarContrasena IrACambiarContrasena()
        {
            // Ir a la p�gina del cambio de contraseña
            IWebElement dropdownPerfil = driver.FindElement(PerfilDropdown);
            dropdownPerfil.Click();
            IWebElement botonLayoutPerfil = driver.FindElement(PerfilLayoutPerfil);
            botonLayoutPerfil.Click();
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
