
using Newtonsoft.Json;

namespace LoCoMPro.Utils
{
    // Clase que permite parsear a formato JSON
    public static class ControladorJson
    {
        private static string AgregarCaracteresDeEscape(string json)
        {
            // Duplicar el carácter de escape '\'
            json = json.Replace("\\", "\\\\");

            // Duplicar los caracteres de escape específicos
            json = json.Replace("\b", "\\b")
                       .Replace("\n", "\\n")
                       .Replace("\t", "\\t")
                       .Replace("\r", "\\r");

            return json;
        }
        public static string ConvertirAJson(object? objecto)
        {
            string resultadoParseado = JsonConvert.SerializeObject(objecto);
            return AgregarCaracteresDeEscape(resultadoParseado);
        }
    }
}
