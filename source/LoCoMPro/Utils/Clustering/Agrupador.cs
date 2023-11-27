using LoCoMPro.Utils.Clustering;
using System.Collections.Generic;
using System.Drawing;

namespace LoCoMProTests.Utils.Clustering
{
    public class Agrupador
    {
        private Comparador comparador;

        public const double VALOR_MINIMO = 0.7;
        public Agrupador()
        {
            comparador = new Comparador();
        }

        public Dictionary<string, List<string>> agrupar(List<string> cadenas)
        {
            Dictionary<string, List<string>> resultado = new Dictionary<string, List<string>>();
            int totalCadenas = cadenas.Count;
            if (cadenas == null || totalCadenas <= 1)
            {
                return resultado;
            }

            bool guardado = false;
            for (int i = 0; i < totalCadenas; ++i)
            {
                List<string> guardados = new List<string>();
                foreach (String cadena in rangoPalabras(cadenas, i))
                {
                    double distancia = comparador.comparacion(cadenas[i], cadena);
                    if (distancia >= VALOR_MINIMO)
                    {
                        guardados.Add(cadena);
                        guardado = true;
                        cadenas.Remove(cadena);
                    }
                }
                if (guardado)
                {
                    resultado.Add(cadenas[i], guardados);
                }
                guardado = false;
            }

            return resultado;
        }

        private IEnumerable<string> rangoPalabras(List<string> cadenas, int indiceInicial) {
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
