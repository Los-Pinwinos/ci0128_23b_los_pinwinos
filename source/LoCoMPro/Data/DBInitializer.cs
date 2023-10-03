using LoCoMPro.Data.CR;
using LoCoMPro.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;

namespace LoCoMPro.Data
{
    public class DBInitializer
    {
        public static void Initialize(LoCoMProContext context)
        {
            // Buscar registros
            if (context.Registros.Any())
            {
                return;   // DB ya tiene registros
            }

            // Cargar las provincias, cantones y distritos de Costa Rica
            DBInitializer.CargarProvinciasCantonesDistritos(context);
        }

        // Cargar provincias cantones y distritos
        public static void CargarProvinciasCantonesDistritos(LoCoMProContext context)
        {
            // Cargar JSON
            string jsonFilePath = "./Data/ProvinciasCantonesDistritosCR.json";
            string jsonData = File.ReadAllText(jsonFilePath);

            // Deserializar
            CR.CostaRica costaRica = JsonConvert.DeserializeObject<CR.CostaRica>(jsonData);

            // Agregar los datos
            AgregarDatos(context, costaRica);

            // Guarda los cambios en la base de datos
            context.SaveChanges();
        }

        // Agrega las provincias, cantones y distritos
        private static void AgregarDatos(LoCoMProContext context, CR.CostaRica costaRica)
        {
            foreach (var entradaProvincia in costaRica.Provincias.Values)
            {
                // Llama al método de agregar provincias para cada provincia
                AgregarProvincias(context, entradaProvincia);
            }
        }

        // Agrega las provincias
        private static void AgregarProvincias(LoCoMProContext context, CR.Provincia entradaProvincia)
        {
            // Se verifica si ya está
            var provincia = context.Provincias.FirstOrDefault(p => p.nombre == entradaProvincia.Nombre);
            if (provincia == null)
            {  // Si hay que agregarla, se agrega
                provincia = new LoCoMPro.Models.Provincia { nombre = entradaProvincia.Nombre };
                context.Provincias.Add(provincia);
            }

            // Para cada provincia, se agregan sus cantones
            foreach (var entradaCanton in entradaProvincia.Cantones.Values)
            {
                AgregarCantones(context, entradaProvincia.Nombre, entradaCanton);
            }
        }

        // Agrega los cantones
        private static void AgregarCantones(LoCoMProContext context, string nombreProv, CR.Canton entradaCanton)
        {
            // Accesa a las propiedades de canton
            string nombreCant = entradaCanton.Nombre;
            // Ver si es central
            if (nombreCant == "Central")
                nombreCant = nombreProv;

            // Si ya está
            var canton = context.Cantones.FirstOrDefault(p => p.nombre == nombreCant && p.nombreProvincia == nombreProv);
            if (canton == null)
            {  // Si hay que agregarlo, se agrega
                canton = new LoCoMPro.Models.Canton { nombre = nombreCant, nombreProvincia = nombreProv };
                context.Cantones.Add(canton);
            }

            // Para cada cantón, se agregan sus distritos
            foreach (var entradaDistrito in entradaCanton.Distritos.Values)
            {
                AgregarDistrito(context,nombreProv, nombreCant, entradaDistrito);
            }
        }

        // Agrega los distritos
        private static void AgregarDistrito(LoCoMProContext context, string nombreProv, string nombreCant, string nombreDistrito)
        {
            // Accesar a las propiedades de canton
            string nombreDistr = nombreDistrito;

            // Se revisa si ya está
            var distrito = context.Distritos.FirstOrDefault(p => p.nombre == nombreDistr && p.nombreCanton == nombreCant && p.nombreProvincia == nombreProv);
            if (distrito == null)
            {  // Hay que crearlo
                distrito = new LoCoMPro.Models.Distrito { nombre = nombreDistr, nombreCanton = nombreCant, nombreProvincia = nombreProv };
                context.Distritos.Add(distrito);
            }
        }
    }
}
