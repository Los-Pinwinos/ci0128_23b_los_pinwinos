using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// Se necesita crear un adaptador a modo de objeto para obtener la respuesta JSON del API ARGCIS, que determina las coordenadas de una localización específica
namespace LoCoMPro.Data
{
    public class ReferenciaEspacial
    {
        [JsonProperty("wkid")]
        public int Identificador { get; set; }

        [JsonProperty("latestWkid")]
        public int UltimoIdentificador { get; set; }
    }

    public class Ubicacion
    {
        [JsonProperty("x")]
        public double Longitud { get; set; }

        [JsonProperty("y")]
        public double Latitud { get; set; }
    }

    public class Extensión
    {
        [JsonProperty("xmin")]
        public double Xmin { get; set; }

        [JsonProperty("ymin")]
        public double Ymin { get; set; }

        [JsonProperty("xmax")]
        public double Xmax { get; set; }

        [JsonProperty("ymax")]
        public double Ymax { get; set; }
    }

    public class Candidato
    {
        [JsonProperty("address")]
        public string? Dirección { get; set; }

        [JsonProperty("location")]
        public Ubicacion? Coordenadas { get; set; }

        [JsonProperty("score")]
        public double Puntuación { get; set; }

        [JsonProperty("attributes")]
        public Dictionary<string, object>? AtributosAdicionales { get; set; }

        [JsonProperty("extent")]
        public Extensión? ExtensiónEspacial { get; set; }
    }

    public class AdaptadorArgcisJSON
    {
        [JsonProperty("spatialReference")]
        public ReferenciaEspacial? ReferenciaEspacial { get; set; }

        [JsonProperty("candidates")]
        public List<Candidato>? Candidatos { get; set; }
    }

}
