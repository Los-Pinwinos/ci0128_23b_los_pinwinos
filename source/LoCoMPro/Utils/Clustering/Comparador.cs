using F23.StringSimilarity;

namespace LoCoMPro.Utils.Clustering;
public class Comparador
{
    private JaroWinkler algoritmo;

    public Comparador()
    {
        this.algoritmo = new JaroWinkler();
    }

    public double comparacion(string primeraCadena, string segundaCadena)
    {
        if (this.algoritmo == null || primeraCadena == null || segundaCadena == null || primeraCadena == "" || segundaCadena == "")
        {
            return -1.0;
        }

        double distancia = algoritmo.Similarity(primeraCadena, segundaCadena);

        return distancia;
    }
}
