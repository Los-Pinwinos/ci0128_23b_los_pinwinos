using LoCoMPro.Models;
using System.Diagnostics;

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

            // Crear provincias
            var provincias = new Provincia[]
            {
                new Provincia{nombre="Heredia"},
                new Provincia{nombre="San José"}
            };

            context.Provincias.AddRange(provincias);
            context.SaveChanges();

            // Crear cantones
            var cantones = new Canton[]
            {
                new Canton{nombre="Heredia",nombreProvincia="Heredia"},
                new Canton{nombre="San José",nombreProvincia="San José"}
            };

            context.Cantones.AddRange(cantones);
            context.SaveChanges();

            // Crear distritos
            var distritos = new Distrito[]
            {
                new Distrito {nombre="Heredia", nombreCanton="Heredia", nombreProvincia="Heredia"},
                new Distrito {nombre="Carmen", nombreCanton="San José", nombreProvincia="San José"}
            };

            context.Distritos.AddRange(distritos);
            context.SaveChanges();

            // Crear Usuario
            var usuario = new Usuario
            {
                nombreDeUsuario = "Usuario1",
                correo = "prueba@gmail.com"
    ,
                contrasena = "123456",
                estado = 'A',
                calificacion = 5,
                distritoVivienda = "Carmen"
    ,
                cantonVivienda = "San José",
                provinciaVivienda = "San José"
            };
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
                new Categoria{nombre="Alimentos"},
                new Categoria{nombre="Electrónicos"},
                new Categoria{nombre="Entretenimiento"},
                new Categoria{nombre="Salud"},
                new Categoria{nombre="Limpieza"},
                new Categoria{nombre="Ropa"},
                new Categoria{nombre="Otros"}
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
    }
}
