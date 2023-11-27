using LoCoMPro.Utils.Clustering;

namespace LoCoMProTests.Utils.Clustering
{
    // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 3
    [TestClass]
    public class ComparadorTest
    {
        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 3
        [TestMethod]
        public void comparador_ValidacionComparacionIgual_DeberiaDevolverUno()
        {
            // Preparación
            string primerParametro = "iphone 14";
            string segundoParametro = "iphone 14";
            Comparador comparador = new Comparador();
            double resultado = -1.0;

            // Acción
            resultado = comparador.comparacion(primerParametro, segundoParametro);

            // Verificación
            Assert.AreEqual(1.0, resultado);
        }

    }
}
