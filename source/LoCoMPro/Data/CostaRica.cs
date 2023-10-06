using System.Collections.Generic;

namespace LoCoMPro.Data.CR
{
    public class Distrito
    {
        public string? Nombre { get; set; }
    }

    public class Canton
    {
        public string? Nombre { get; set; }
        public Dictionary<string, string>? Distritos { get; set; }
    }

    public class Provincia
    {
        public string? Nombre { get; set; }
        public Dictionary<string, Canton>? Cantones { get; set; }
    }

    public class CostaRica
    {
        public Dictionary<string, Provincia>? Provincias { get; set; }
    }
}

