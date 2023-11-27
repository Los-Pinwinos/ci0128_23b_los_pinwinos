using LoCoMPro.Utils.Clustering;

namespace LoCoMProTests.Utils.Clustering
{
    public class Agrupador
    {
        private Comparador comparador;
        public Agrupador()
        {
            comparador = new Comparador();
        }

        public List<String> agrupar(List<String> cadenas)
        {
            List<String> resultado = new List<String>();
            int totalCadenas = cadenas.Count;
            if (cadenas == null || totalCadenas <= 1)
            {
                return resultado;
            }

            for (int i = 0; i < totalCadenas; ++i)
            {
                foreach (String cadena in rangoPalabras(cadenas, i))
                {
                    double distancia = comparador.comparacion(cadenas[i], cadena);
                    if (distancia >= 0.7)
                    {
                        // Insertar en lista de cadenas[i]
                    }
                }
            }

            return resultado;
        }

        private IEnumerable<String> rangoPalabras(List<String> cadenas, int indiceInicial) {
            int totalCadenas = cadenas.Count;
            if (indiceInicial > totalCadenas)
            {
                yield break;
            }
            for (int i = indiceInicial; i < totalCadenas; i++)
            {
                yield return cadenas[i];
            }

        }
    }
}
