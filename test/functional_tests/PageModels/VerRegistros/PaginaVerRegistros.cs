using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using LoCoMProTestFuncionales.PageModels.DetallesRegistros;

namespace LoCoMProTestFuncionales.PageModels.VerRegistros
{
    public class PaginaVerRegistros : PaginaBase
    {
        protected By iconoCorazon = By.Id("iconoCorazon");
        protected By checkboxDia = By.XPath("//div[1]/label/span");
        protected By checkboxSemana = By.XPath("//div[2]/label/span");
        protected By checkboxMes = By.XPath("//div[3]/label/span");
        protected By checkboxAno = By.XPath("//div[4]/label/span");
        protected By cuerpoDeTablaDeResultados = By.TagName("tbody");
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

        public void AgregarProductoAFavoritos()
        {
            IWebElement corazon = driver.FindElement(this.iconoCorazon);
            
            String[] clases = corazon.GetAttribute("class").Split(" ");
            if (!clases.Contains("corazon-lleno")) {
                corazon.Click();
            }
        }

        public void EliminarProductoDeFavoritos()
        {
            IWebElement corazon = driver.FindElement(this.iconoCorazon);

            String[] clases = corazon.GetAttribute("class").Split(" ");
            if (clases.Contains("corazon-lleno")) {
                corazon.Click();
            }
        }

        public PaginaDetallesRegistro SeleccionarResultado(int resultado)
        {
            IWebElement cuerpoTabla = driver.FindElement(this.cuerpoDeTablaDeResultados);

            IList<IWebElement> filas = cuerpoTabla.FindElements(By.TagName("tr"));

            if (filas.Count > resultado)
            {
                filas[resultado].Click();
            }
            return new PaginaDetallesRegistro(this.driver);
        }

        new public string ObtenerURL()
        {
            return base.ObtenerURL();
        }
    }
}
