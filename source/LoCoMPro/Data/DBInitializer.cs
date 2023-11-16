using Azure;
using LoCoMPro.Data.CR;
using LoCoMPro.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LoCoMPro.Data
{
    public class DBInitializer
    {
        public static async Task Initialize(LoCoMProContext contexto)
        {
            // Si no tiene distritos, cantones ni provincias
            if (!contexto.Distritos.Any() &&
                !contexto.Cantones.Any() &&
                !contexto.Provincias.Any())
            {
                // Cargar las provincias, cantones y distritos de Costa Rica
                DBInitializer.CargarProvinciasCantonesDistritos(contexto);
            }

            // Si no tiene unidades
            if (!contexto.Unidades.Any())
            {
                // Crear unidades base
                var unidades = new Unidad[]
                {
                    new Unidad {nombre= "Unidad"},
                    new Unidad {nombre= "Kilogramos"},
                    new Unidad {nombre = "Litros"}
                };
                contexto.Unidades.AddRange(unidades);
                contexto.SaveChanges();
            }

            // Si no tiene categorias
            if (!contexto.Categorias.Any())
            {
                // Crear categorias base
                var categorias = new Categoria[]
                {
                new Categoria { nombre = "Alimentos" },
                new Categoria { nombre = "Electrónicos" },
                new Categoria { nombre = "Entretenimiento" },
                new Categoria { nombre = "Salud" },
                new Categoria { nombre = "Limpieza" },
                new Categoria { nombre = "Ropa" },
                new Categoria { nombre = "Otros" }
                };
                contexto.Categorias.AddRange(categorias);
                contexto.SaveChanges();
            }

            // Si no tiene tiendas, productos ni registros
            if (!contexto.Tiendas.Any() &&
                !contexto.Productos.Any() &&
                !contexto.Registros.Any())
            {
                // TODO(Pinwinos): Agregar usuarios para cada miembro
                // Crear Usuario base
                var usuario = new Usuario
                {
                    nombreDeUsuario = "Usuario1*"
                    ,
                    correo = "prueba@gmail.com"
                    ,
                    hashContrasena = "AQAAAAIAAYagAAAAEKsU2+AMT85bnzhsCNuFBikWncWXvbzB+a1mkc5MX7GnEcXY0F+4TNoLD45JU7c+WQ=="
                    ,
                    estado = 'A'
                    ,
                    calificacion = 0
                    ,
                    distritoVivienda = "Garita"
                    ,
                    cantonVivienda = "Alajuela"
                    ,
                    provinciaVivienda = "Alajuela"
                    ,
                    esModerador = true
                };
                contexto.Usuarios.Add(usuario);
                contexto.SaveChanges();

                // Cargar tiendas base
                await DBInitializer.CargarTiendas(contexto);

                // Cargar productos base
                DBInitializer.CargarProductos(contexto);

                // Cargar registros base
                DBInitializer.CargarRegistros(contexto);
            }
        }

        // Cargar provincias cantones y distritos
        private static void CargarProvinciasCantonesDistritos(LoCoMProContext contexto)
        {
            // Cargar JSON
            string pathArchivoJson = "./Data/ArchivosJSON/ProvinciasCantonesDistritosCR.json";
            string datosJson = File.ReadAllText(pathArchivoJson);

            // Deserializar
            CR.CostaRica? costaRica = JsonConvert.DeserializeObject<CR.CostaRica>(datosJson);

            // Agregar los datos
            if (costaRica != null)
            {
                AgregarDatos(contexto, costaRica);
                contexto.SaveChanges();
            }
        }

        private static void AgregarDatos(LoCoMProContext contexto, CR.CostaRica costaRica)
        {
            if (costaRica.Provincias != null)
                foreach (var entradaProvincia in costaRica.Provincias.Values)
                {
                    AgregarProvincias(contexto, entradaProvincia);
                }
        }

        private static void AgregarProvincias(LoCoMProContext contexto, CR.Provincia entradaProvincia)
        {
            // Si ya está
            var provincia = contexto.Provincias.FirstOrDefault(p => p.nombre == entradaProvincia.Nombre);
            if (provincia == null && entradaProvincia.Nombre != null)
            {
                provincia = new LoCoMPro.Models.Provincia { nombre = entradaProvincia.Nombre };
                contexto.Provincias.Add(provincia);

                // Guarda los cambios en la base de datos
                contexto.SaveChanges();
            }

            if (entradaProvincia.Cantones != null && entradaProvincia.Nombre != null)
                foreach (var entradaCanton in entradaProvincia.Cantones.Values)
                {
                    AgregarCantones(contexto, entradaProvincia.Nombre, entradaCanton);
                }
        }

        private static void AgregarCantones(LoCoMProContext contexto, string nombreProv, CR.Canton entradaCanton)
        {
            // Accesar a las propiedades de canton
            string? nombreCant = entradaCanton.Nombre;
            // Ver si es central
            if (nombreCant == "Central")
                nombreCant = nombreProv;

            // Si ya está
            var canton = contexto.Cantones.FirstOrDefault(p => p.nombre == nombreCant && p.nombreProvincia == nombreProv);
            if (canton == null && nombreCant != null)
            {
                canton = new LoCoMPro.Models.Canton { nombre = nombreCant, nombreProvincia = nombreProv };
                contexto.Cantones.Add(canton);

                // Guarda los cambios en la base de datos
                contexto.SaveChanges();
            }

            if (entradaCanton.Distritos != null && nombreCant != null)
                foreach (var entradaDistrito in entradaCanton.Distritos.Values)
                {
                    AgregarDistrito(contexto, nombreProv, nombreCant, entradaDistrito);
                }
        }

        private static void AgregarDistrito(LoCoMProContext contexto, string nombreProv, string nombreCant, string nombreDistrito)
        {
            // Accesar a las propiedades de canton
            string nombreDistr = nombreDistrito;

            // Si ya está
            var distrito = contexto.Distritos.FirstOrDefault(p => p.nombre == nombreDistr && p.nombreCanton == nombreCant && p.nombreProvincia == nombreProv);
            if (distrito == null)
            {
                distrito = new LoCoMPro.Models.Distrito { nombre = nombreDistr, nombreCanton = nombreCant, nombreProvincia = nombreProv };
                contexto.Distritos.Add(distrito);

                // Guarda los cambios en la base de datos
                contexto.SaveChanges();
            }
        }

        private static void CargarProductos(LoCoMProContext contexto)
        {
            // Cargar JSON
            string pathArchivoJson = "./Data/ArchivosJSON/Productos.json";
            string datosJson = File.ReadAllText(pathArchivoJson);

            // Deserializar
            var productos = JsonConvert.DeserializeObject<Producto[]>(datosJson);

            // Agregar los datos
            if (productos != null)
            {
                contexto.Productos.AddRange(productos);
                contexto.SaveChanges();
            }
        }

        private static async Task CargarTiendas(LoCoMProContext contexto)
        {
            // Cargar JSON
            string pathArchivoJson = "./Data/ArchivosJSON/Tiendas.json";
            string datosJson = File.ReadAllText(pathArchivoJson);

            // Deserializar
            var tiendas = JsonConvert.DeserializeObject<Tienda[]>(datosJson);
            // Crear un cliente
            using (HttpClient cliente = new HttpClient())
            {
                if (tiendas != null)
                {
                    foreach (Tienda tienda in tiendas)
                    {
                        // Obtener las coordenadas de la tienda
                        string apiURL = ObtenerUrlLocalizacion(tienda.nombreProvincia, tienda.nombreCanton, tienda.nombreDistrito, tienda.nombre);
                        var (latitud, longitud) = await ObtenerCoordenadas(cliente, apiURL);

                        // Verificar si se obtuvieron las coordenadas con éxito
                        if (longitud != 0 && latitud != 0)
                        {
                            tienda.latitud = latitud;
                            tienda.longitud = longitud;

                            contexto.Tiendas.Add(tienda);
                        }

                    }
                }
                contexto.SaveChanges();
            }
        }

        private static async Task<(double, double)> ObtenerCoordenadas(HttpClient cliente, string apiUrl)
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

        private static string ObtenerUrlLocalizacion(string provincia, string canton, string distrito, string? tienda = null)
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

        private static void CargarRegistros(LoCoMProContext contexto)
        {
            // Cargar JSON
            string pathArchivoJson = "./Data/ArchivosJSON/Registros.json";
            string datosJson = File.ReadAllText(pathArchivoJson);

            // Deserializar
            var registros = JsonConvert.DeserializeObject<Registro[]>(datosJson);

            // Agregar los datos
            if (registros != null)
            {
                contexto.Registros.AddRange(registros);
                contexto.SaveChanges();
            }
        }
    }
}