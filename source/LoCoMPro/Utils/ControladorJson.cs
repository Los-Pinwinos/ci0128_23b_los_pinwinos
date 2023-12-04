using Newtonsoft.Json;

namespace LoCoMPro.Utils
{
    // Clase que permite parsear a formato JSON
    public static class ControladorJson
    {
        private static string AgregarCaracteresEspeciales(string json)
        {
            // Duplicar el carácter de escape '\'
            json = json.Replace("\\", "\\\\");

            // Duplicar los caracteres de escape específicos
            json = json.Replace("\b", "\\b")
                       .Replace("\n", "\\n")
                       .Replace("\t", "\\t")
                       .Replace("\r", "\\r");

            // Duplicar los caracteres de string
            json = json.Replace("\"", "\\\"");

            return json;
        }
        public static string ConvertirAJson(object? objecto)
        {
            string resultadoParseado = JsonConvert.SerializeObject(objecto);
            return AgregarCaracteresEspeciales(resultadoParseado);
        }
    }
}