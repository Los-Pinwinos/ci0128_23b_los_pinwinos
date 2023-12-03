using OpenQA.Selenium;

namespace LoCoMProTestFuncionales.PageModels.DetallesRegistros
{
    public class PaginaDetallesRegistro : PaginaBase
    {
        protected By Calificacion = By.Id("CalificacionTotal");
        protected By CantidadCalificaciones = By.Id("CantidadCalificacionesTotal");
        protected By MensajeUltimaCalificacion = By.Id("UltimaCalificacion");
        protected By Estrella5 = By.CssSelector("label:nth-child(11)");
        protected By Estrella4 = By.CssSelector("label:nth-child(9)");
        protected By Estrella3 = By.CssSelector("label:nth-child(7)");
        protected By Estrella2 = By.CssSelector("label:nth-child(5)");
        protected By Estrella1 = By.CssSelector("label:nth-child(3)");

        public PaginaDetallesRegistro(IWebDriver driver) : base(driver) { }

        public string ObtenerCalificacion()
        {
            return driver.FindElement(Calificacion).Text;
        }

        public string ObtenerCantidadCalificaciones()
        {
            return driver.FindElement(CantidadCalificaciones).Text;
        }

        public string ObtenerMensajeCalificacion()
        {
            return driver.FindElement(MensajeUltimaCalificacion).Text;
        }

        public void clickEstrella(int calificacion)
        {
            switch(calificacion)
            {
                case 1:
                    driver.FindElement(Estrella1).Click();
                    break;
                case 2:
                    driver.FindElement(Estrella2).Click();
                    break;
                case 3:
                    driver.FindElement(Estrella3).Click();
                    break;
                case 4:
                    driver.FindElement(Estrella4).Click();
                    break;
                case 5:
                    driver.FindElement(Estrella5).Click();
                    break;
            }
        }
    }
}
