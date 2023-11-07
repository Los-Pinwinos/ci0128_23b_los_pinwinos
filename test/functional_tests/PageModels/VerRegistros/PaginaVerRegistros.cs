using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using LoCoMProTestFuncionales.PageModels.Busqueda;

namespace LoCoMProTestFuncionales.PageModels.VerRegistros
{
    public class PaginaVerRegistros : PaginaBase
    {

        protected By checkboxDia = By.XPath("//div[1]/label/span");
        protected By checkboxSemana = By.XPath("//div[2]/label/span");
        protected By checkboxMes = By.XPath("//div[3]/label/span");
        protected By checkboxAno = By.XPath("//div[4]/label/span");
        protected List<By> titulos;

        public PaginaVerRegistros(IWebDriver driver) : base(driver)
        {
            this.titulos = new List<By>();
            this.IncializarTitulos();
        }

        private void IncializarTitulos()
        {
            this.titulos.Add(By.XPath("//th[1]/div"));
            this.titulos.Add(By.XPath("//th[2]/div"));
            this.titulos.Add(By.XPath("//th[3]/div"));
            this.titulos.Add(By.XPath("//th[4]/div"));
            this.titulos.Add(By.XPath("//th[5]/div"));
        }

        public void PresionarAgrupamientoDia()
        {
            this.driver.FindElement(checkboxDia).Click();
        }

        public void PresionarAgrupamientoSemana()
        {
            this.driver.FindElement(checkboxSemana).Click();
        }

        public void PresionarAgrupamientoMes()
        {
            this.driver.FindElement(checkboxMes).Click();
        }

        public void PresionarAgrupamientoAno()
        {
            this.driver.FindElement(checkboxAno).Click();
        }

        public string ObtenerValorTitulo(int titulo)
        {
            if (titulo < 5)
            {
                return driver.FindElement(this.titulos[titulo]).Text;
            }
            else
            {
                return "";
            }
        }

        new public string ObtenerURL()
        {
            return base.ObtenerURL();
        }
    }
}
