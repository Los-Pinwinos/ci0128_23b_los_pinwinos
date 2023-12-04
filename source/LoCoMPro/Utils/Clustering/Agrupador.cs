using LoCoMPro.Utils.Clustering;
using System.Collections.Generic;
using System.Drawing;

namespace LoCoMProTests.Utils.Clustering;
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

        bool[] agrupado = new bool[totalCadenas];
        for (int i = 0; i < totalCadenas; ++i)
        {
            if (!agrupado[i])
            {
                identificarGrupo(cadenas, ref resultado, ref agrupado, i);
            }
        }

        return resultado;
    }

    private void identificarGrupo(List<string> cadenas, ref Dictionary<string, List<string>> resultado, ref bool[] agrupado, int i)
    {
        bool guardado = false;
        List<string> guardados = new List<string>();
        foreach (string cadena in rangoPalabras(cadenas, i + 1, agrupado))
        {
            double distancia = comparador.comparacion(cadenas[i], cadena);
            if (distancia >= VALOR_MINIMO)
            {
                guardado = guardarElemento(cadenas, ref agrupado, ref guardados, cadena);
            }
        }
        if (guardado)
        {
            resultado.Add(cadenas[i], guardados);
        }
    }

    private static bool guardarElemento(List<string> cadenas, ref bool[] agrupado, ref List<string> guardados, string cadena)
    {
        bool guardado;
        guardados.Add(cadena);
        guardado = true;
        var indice = cadenas.FindIndex(x => x.Equals(cadena));
        agrupado[indice] = true;
        return guardado;
    }

    private IEnumerable<string> rangoPalabras(List<string> cadenas, int indiceInicial, bool[] agrupado)
    {
        int totalCadenas = cadenas.Count;
        if (indiceInicial > totalCadenas)
        {
            yield break;
        }
        for (int i = indiceInicial; i < totalCadenas; i++)
        {
            if (!agrupado[i])
            {
                yield return cadenas[i];
            }
        }
    }
}

