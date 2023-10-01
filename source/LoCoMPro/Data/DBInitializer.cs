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
                new Provincia{nombre="Alajuela"}
            };

            context.Provincias.AddRange(provincias);
            context.SaveChanges();

            // Crear cantones
            var cantones = new Canton[]
            {
                new Canton{nombre="Heredia",nombreProvincia="Heredia"},
                new Canton{nombre="Alajuela",nombreProvincia="Alajuela"}
            };

            context.Cantones.AddRange(cantones);
            context.SaveChanges();

            // Crear distritos
            var distritos = new Distrito[]
            {
                new Distrito {nombre="Mercedes", nombreCanton="Heredia", nombreProvincia="Heredia"},
                new Distrito {nombre="Garita", nombreCanton="Alajuela", nombreProvincia="Alajuela"}
            };

            context.Distritos.AddRange(distritos);
            context.SaveChanges();

            // Crear Usuario
            var usuario = new Usuario { nombreDeUsuario = "Usuario1", correo = "prueba@gmail.com", contrasena = "123456", estado = 'A', calificacion = 5, distritoVivienda = "Garita", cantonVivienda = "Alajuela", provinciaVivienda = "Alajuela" };

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
                new Producto {nombre="Camisa", marca="Nike", nombreCategoria="Ropa", nombreUnidad="Cantidad"},
                new Producto {nombre="Computadora", marca="Dell", nombreCategoria="Electrónicos", nombreUnidad="Cantidad"},
                new Producto {nombre="Camiseta", marca="Adiddas", nombreCategoria="Ropa", nombreUnidad="Cantidad"}
            };

            context.Productos.AddRange(productos);
            context.SaveChanges();

            // Crear Tiendas
            var tiendas = new Tienda[]
            {
                new Tienda {nombre="Walmart", nombreDistrito="Garita", nombreCanton="Alajuela", nombreProvincia="Alajuela"},
                new Tienda {nombre="Maxi Pali", nombreDistrito="Mercedes", nombreCanton="Heredia", nombreProvincia="Heredia"}
            };

            context.Tiendas.AddRange(tiendas);
            context.SaveChanges();

            // Crear registros
            var registros = new Registro[]
            {
                new Registro {creacion=DateTime.Parse("2019-09-01 10:30:45"), precio=13.2M, productoAsociado="Iphone 14", usuarioCreador="Usuario1", nombreTienda="Walmart", nombreDistrito="Garita", nombreCanton="Alajuela", nombreProvincia="Alajuela", descripcion="Muy bueno" },
                new Registro {creacion=DateTime.Parse("2019-09-01 10:31:45"), precio=12.2M, productoAsociado="Camisa", usuarioCreador="Usuario1", nombreTienda="Maxi Pali", nombreDistrito="Mercedes", nombreCanton="Heredia", nombreProvincia="Heredia", descripcion="Muy bueno tambien" },
                new Registro {creacion=DateTime.Parse("2019-09-02 10:31:45"), precio=13.2M, productoAsociado="Camisa", usuarioCreador="Usuario1", nombreTienda="Maxi Pali", nombreDistrito="Mercedes", nombreCanton="Heredia", nombreProvincia="Heredia", descripcion="Excelente" },
                new Registro {creacion=DateTime.Parse("2019-09-03 10:31:45"), precio=10.2M, productoAsociado="Camiseta", usuarioCreador="Usuario1", nombreTienda="Maxi Pali", nombreDistrito="Mercedes", nombreCanton="Heredia", nombreProvincia="Heredia", descripcion="Genial" },
                new Registro {creacion=DateTime.Parse("2019-09-04 10:31:45"), precio=150.2M, productoAsociado="Computadora", usuarioCreador="Usuario1", nombreTienda="Walmart", nombreDistrito="Garita", nombreCanton="Alajuela", nombreProvincia="Alajuela", descripcion="Maravilloso" },
            };

            context.Registros.AddRange(registros);
            context.SaveChanges();
        }
    }
}