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

            // Crear Usuario
            var usuario = new Usuario {
                nombreDeUsuario = "Usuario1"
                , correo = "prueba@gmail.com"
                , hashContrasena = "123456"
                , estado = 'A'
                , calificacion = 5
                , distritoVivienda = "Garita"
                , cantonVivienda = "Alajuela"
                , provinciaVivienda = "Alajuela" };
            context.Usuarios.Add(usuario);
            context.SaveChanges();

            // Crear Unidad
            var unidades = new Unidad[]
            {
                new Unidad {nombre= "Cantidad"},
                new Unidad {nombre= "Kilogramos"},
                new Unidad {nombre = "Litros"}
            };
            context.Unidades.AddRange(unidades);
            context.SaveChanges();

            // Crear Categorias
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
            context.Categorias.AddRange(categorias);
            context.SaveChanges();

            // Crear productos
            var productos = new Producto[]
            {
                new Producto {nombre="Iphone 14", marca="Apple", nombreCategoria="Electrónicos", nombreUnidad="Cantidad"},
                new Producto {nombre="Camisa", marca="Nike", nombreCategoria="Ropa", nombreUnidad="Cantidad"}
            };
            context.Productos.AddRange(productos);
            context.SaveChanges();

            // Crear Tiendas
            var tiendas = new Tienda[]
            {
                new Tienda {nombre="Walmart", nombreDistrito="Carmen", nombreCanton="San José", nombreProvincia="San José"},
                new Tienda {nombre="Maxi Pali", nombreDistrito="Heredia", nombreCanton="Heredia", nombreProvincia="Heredia"}
            };
            context.Tiendas.AddRange(tiendas);
            context.SaveChanges();

            // Crear registros
            var registros = new Registro[]
            {
                new Registro {creacion=DateTime.Parse("2019-09-01 10:30:45"), precio=13.2M
                    , productoAsociado="Iphone 14", usuarioCreador="Usuario1", nombreTienda="Walmart"
                    , nombreDistrito="Carmen", nombreCanton="San José", nombreProvincia="San José"
                    , descripcion="Muy bueno" },
                new Registro {creacion=DateTime.Parse("2019-09-01 10:31:45"), precio=12.2M
                    , productoAsociado="Camisa", usuarioCreador="Usuario1", nombreTienda="Maxi Pali"
                    , nombreDistrito="Heredia", nombreCanton="Heredia", nombreProvincia="Heredia"
                    , descripcion="Muy bueno tambien" },
            };
            context.Registros.AddRange(registros);
            context.SaveChanges();
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
        }

        private static void AgregarDatos(LoCoMProContext context, CR.CostaRica costaRica)
        {
            foreach (var entradaProvincia in costaRica.Provincias.Values)
            {
                AgregarProvincias(context, entradaProvincia);
            }
        }

        private static void AgregarProvincias(LoCoMProContext context, CR.Provincia entradaProvincia)
        {
            // Si ya está
            var provincia = context.Provincias.FirstOrDefault(p => p.nombre == entradaProvincia.Nombre);
            if (provincia == null)
            {
                provincia = new LoCoMPro.Models.Provincia { nombre = entradaProvincia.Nombre };
                context.Provincias.Add(provincia);

                // Guarda los cambios en la base de datos
                context.SaveChanges();
            }

            foreach (var entradaCanton in entradaProvincia.Cantones.Values)
            {
                AgregarCantones(context, entradaProvincia.Nombre, entradaCanton);
            }
        }

        private static void AgregarCantones(LoCoMProContext context, string nombreProv, CR.Canton entradaCanton)
        {
            // Accesar a las propiedades de canton
            string nombreCant = entradaCanton.Nombre;
            // Ver si es central
            if (nombreCant == "Central")
                nombreCant = nombreProv;

            // Si ya está
            var canton = context.Cantones.FirstOrDefault(p => p.nombre == nombreCant && p.nombreProvincia == nombreProv);
            if (canton == null)
            {
                canton = new LoCoMPro.Models.Canton { nombre = nombreCant, nombreProvincia = nombreProv };
                context.Cantones.Add(canton);

                // Guarda los cambios en la base de datos
                context.SaveChanges();
            }

            foreach (var entradaDistrito in entradaCanton.Distritos.Values)
            {
                AgregarDistrito(context, nombreProv, nombreCant, entradaDistrito);
            }
        }

        private static void AgregarDistrito(LoCoMProContext context, string nombreProv, string nombreCant, string nombreDistrito)
        {
            // Accesar a las propiedades de canton
            string nombreDistr = nombreDistrito;

            // Si ya está
            var distrito = context.Distritos.FirstOrDefault(p => p.nombre == nombreDistr && p.nombreCanton == nombreCant && p.nombreProvincia == nombreProv);
            if (distrito == null)
            {
                distrito = new LoCoMPro.Models.Distrito { nombre = nombreDistr, nombreCanton = nombreCant, nombreProvincia = nombreProv };
                context.Distritos.Add(distrito);

                // Guarda los cambios en la base de datos
                context.SaveChanges();
            }
        }
    }
}