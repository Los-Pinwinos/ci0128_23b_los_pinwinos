using OpenQA.Selenium;
using LoCoMProTestFuncionales.PageModels.Busqueda;

namespace LoCoMProTestFuncionales.PageModels.Cuenta
{
    public class PaginaFavoritos : PaginaBase
    {
        private By CuerpoDeTablaDeResultados = By.TagName("tbody");

        public PaginaFavoritos(IWebDriver driver) : base(driver) { }

        public List<List<string>> ObtenerTablaDeResultados()
        {
            List<List<string>> resultados = new List<List<string>>();

            IWebElement cuerpoTabla = driver.FindElement(this.CuerpoDeTablaDeResultados);

            IList<IWebElement> filas = cuerpoTabla.FindElements(By.TagName("tr"));

            foreach (IWebElement fila in filas)
            {
                List<string> datosFila = new List<string>();

                IList<IWebElement> celdas = fila.FindElements(By.TagName("td"));

                foreach (IWebElement celda in celdas)
                {
                    datosFila.Add(celda.Text);
                }

                resultados.Add(datosFila);
            }

            return resultados;

        }

        public void EliminarFavorito(string producto)
        {
            IWebElement cuerpoTabla = driver.FindElement(this.CuerpoDeTablaDeResultados);

            IList<IWebElement> filas = cuerpoTabla.FindElements(By.TagName("tr"));

            foreach (IWebElement fila in filas)
            {
                List<string> datosFila = new List<string>();

                IList<IWebElement> celdas = fila.FindElements(By.TagName("td"));

                foreach (IWebElement celda in celdas)
                {
                    // Se encontró el elemento
                    if (celda.Text == producto)
                    {
                        IWebElement corazon = fila.FindElement(By.ClassName("corazon-lleno"));
                        // Remover de favoritos
                        corazon.Click();
                    }
                }
            }
        }

        new public string ObtenerURL()
        {
            return base.ObtenerURL() + "/Cuenta/Favoritos/";
        }
    }
}
