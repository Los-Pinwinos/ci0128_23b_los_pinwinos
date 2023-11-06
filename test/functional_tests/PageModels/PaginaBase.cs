using OpenQA.Selenium;

namespace LoCoMProTestFuncionales.PageModels
{
    // Página base
    public class PaginaBase
    {
        protected IWebDriver driver;

        public PaginaBase(IWebDriver driver)
        {
            this.driver = driver;
        }

        virtual public string ObtenerURL()
        {
            return "http://localhost:5150";
        }

        public void EsperarMilisegundos(int milisegundos)
        {
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(milisegundos);
        }

        public void EsperarSegundos(int segundos)
        {
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(segundos);
        }
    }
}
