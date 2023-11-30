using LoCoMPro.Data;
using Newtonsoft.Json;

namespace LoCoMPro.Utils
{
    // Clase que permite localizar puntos geográficos
    public class Localizador
    {
        public static async Task<(double, double)> ObtenerCoordenadas(HttpClient cliente, string apiUrl)
        {
            double latitud = 0;
            double longitud = 0;

            HttpResponseMessage respuesta = await cliente.GetAsync(apiUrl);

            if (respuesta.IsSuccessStatusCode)
            {
                // Obtener el contenido JSON de la respuesta del fetch
                string contenidoJSON = await respuesta.Content.ReadAsStringAsync();
                AdaptadorArgcisJSON objetoJSON;

                if (contenidoJSON != null)
                {
                    // Convertir el contenido del JSON en un objeto conocido
                    objetoJSON = JsonConvert.DeserializeObject<AdaptadorArgcisJSON>(contenidoJSON);

                    if (objetoJSON != null && objetoJSON.Candidatos != null)
                    {
                        latitud = objetoJSON.Candidatos[0].Coordenadas.Latitud;
                        longitud = objetoJSON.Candidatos[0].Coordenadas.Longitud;
                    }
                }
            }
            return (latitud, longitud);
        }

        public static string ObtenerUrlLocalizacion(string provincia, string canton, string distrito, string? tienda = null)
        {
            // Crear URL para obtener las coordenadas de una tienda
            string urlBase = "https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates?f=pjson";
            string url = "";

            // Verificar si se solicitó tienda
            if (string.IsNullOrEmpty(tienda))
                url = $"{urlBase}&singleLine={tienda},{distrito},{canton},{provincia}";
            else
                url = $"{urlBase}&singleLine={distrito},{canton},{provincia}";

            return url;
        }

		// Cálculo de la distancia en kilómetros entre dos puntos geográficos usando la Fórmula del Haversine
		public static double DistanciaKm(double latOrigen, double lonOrigen, double latDestino, double lonDestino)
		{
			// Distancia a kilómetros
			double radioTierraKm = 6371.0;
			// Deltas
			double difLatitud = GradosARadianes(latDestino - latOrigen);
			double difLongitud = GradosARadianes(lonDestino - lonOrigen);

			double a = Math.Sin(difLatitud / 2) * Math.Sin(difLatitud / 2) +
					   Math.Cos(GradosARadianes(latOrigen)) * Math.Cos(GradosARadianes(latDestino)) *
					   Math.Sin(difLongitud / 2) * Math.Sin(difLongitud / 2);

			double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			// Distancia a kilómetros
			double distancia = radioTierraKm * c;
			return distancia;
		}

		// Pasar grados a radianes
		private static double GradosARadianes(double degrees)
		{
			return degrees * Math.PI / 180.0;
		}
	}
}
