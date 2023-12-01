using LoCoMPro.Utils.Clustering;

namespace LoCoMProTests.Utils.Clustering
{
    // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 3
    [TestClass]
    public class AgrupadorTest
    {
        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 3
        [TestMethod]
        public void agrupador_ValidacionAgrupacionLista_DeberiaDevolverUnCluster()
        {
            // Preparación
            List<string> cadenas = new() {"zapato", "zapatos", "sapato", "sapatos"};
            Agrupador agrupador = new Agrupador();

            // Acción
            var resultado = agrupador.agrupar(cadenas);

            // Verificación
            Assert.AreEqual(1, resultado.Count);
        }

        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 3
        [TestMethod]
        public void agrupador_ValidacionAgrupacionLista_DeberiaDevolverLlaveCorrecta()
        {
            // Preparación
            List<string> cadenas = new() { "zapato", "zapatos", "sapato", "sapatos" };
            string llave = "zapato";
            Agrupador agrupador = new Agrupador();

            // Acción
            var clusterResultado = agrupador.agrupar(cadenas);

            // Verificación
            bool resultado = clusterResultado.ContainsKey(llave);
            Assert.IsTrue(resultado);
        }

        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 3
        [TestMethod]
        public void agrupador_ValidacionAgrupacionLista_DeberiaDevolverClusterCorrecto()
        {
            // Preparación
            List<string> cadenas = new() { "zapato", "zapatos", "sapato", "sapatos" };
            List<string>  resultadoEsperado = new() {"zapatos", "sapato", "sapatos" };
            string llave = "zapato";
            Agrupador agrupador = new Agrupador();

            // Acción
            var clusterResultado = agrupador.agrupar(cadenas);

            // Verificación
            bool resultado = clusterResultado[llave].SequenceEqual(resultadoEsperado);
            Assert.IsTrue(resultado);
        }

        // Hecho por: Luis David Solano Santamaría - C17634 - Sprint 3
        [TestMethod]
        public void agrupador_ValidacionAgrupacionListaVacia_DeberiaDevolverDiccionarioVacio()
        {
            // Preparación
            List<string> cadenas = new List<string>();
            Agrupador agrupador = new Agrupador();

            // Acción
            var resultado = agrupador.agrupar(cadenas);

            // Verificación
            Assert.AreEqual(0, resultado.Count);
        }
    }
}
