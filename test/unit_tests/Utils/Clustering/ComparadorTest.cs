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
            // El rango de resultados es de [0,1], causando que -1.0 sea imposible de retornar a menos de encontrar un error
            double resultado = -1.0;

            // Acción
            resultado = comparador.comparacion(primerParametro, segundoParametro);

            // Verificación
            Assert.AreEqual(1.0, resultado);
        }

        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 3
        [TestMethod]
        public void comparador_ValidacionComparacionDiferentes_DeberiaDevolverCorrecto()
        {
            // Preparación
            string primerParametro = "iphone";
            string segundoParametro = "iphones";
            Comparador comparador = new Comparador();
            // El rango de resultados es de [0,1], causando que -1.0 sea imposible de retornar a menos de encontrar un error
            double resultado = -1.0;

            // Acción
            resultado = comparador.comparacion(primerParametro, segundoParametro);

            // Verificación
            Assert.AreEqual(0.9809523820877075, resultado);
        }
    }
}
