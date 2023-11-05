using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using LoCoMPro.Models;

namespace LoCoMProTests.Models
{
    [TestClass]
    public class RegistroTests
    {
        // Hecho por: Kenneth Daniel Villalobos Solís - C18548
        [TestMethod]
        public void registro_Validacion_DeberiaSerValido()
        {
            // Crear registro correcto de prueba
            var registro = new Registro
            {
                // La fecha de creación debe estar entre 1/1/2000 y 1/1/2200
                creacion = DateTime.Now,
                // 12 caracteres representa una longitud válida
                usuarioCreador = "Usuario1212*",
                // 23 caracteres representa una longitud válida
                descripcion = "Esta es una descripción",
                // Un número con 4 dígitos enteros y 2 dígitos decimales es válido
                precio = 5000.50m,
                // Un número entre 5 y 0
                calificacion = 5,
                // 11 caracteres representa una longitud válida
                productoAsociado = "Tennissitas",
                // 17 caracteres representa una longitud válida
                // No presenta números ni caracteres especiales
                nombreTienda = "Tienda tus tennis",
                // 22 caracteres representa una longitud válida
                // No presenta números ni caracteres especiales
                nombreDistrito = "San Isidro del Géneral",
                // 13 caracteres representa una longitud válida
                // No presenta números ni caracteres especiales
                nombreCanton = "Pérez Zeledón",
                // 8 caracteres representa una longitud válida
                // No presenta números ni caracteres especiales
                nombreProvincia = "San José"
            };

            // Establecer condiciones de prueba
            var contexto = new ValidationContext(registro);
            var esValido = Validator.TryValidateObject(registro, contexto, null, true);

            // Revisar condiciones de prueba
            Assert.IsTrue(esValido);
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548
        [TestMethod]
        public void creacion_ValidacionRango_DeberiaSerValido()
        {
            // Crear registro con fecha de creación incorrecta de prueba
            var registro = new Registro
            {
                // La fecha de creación debe estar entre 1/1/2000 y 1/1/2200
                creacion = new DateTime(3000, 1, 1),
                usuarioCreador = "Usuario1212*",
                descripcion = "Esta es una descripción",
                precio = 5000.50m,
                calificacion = 5,
                productoAsociado = "Tennissitas",
                nombreTienda = "Tienda tus tennis",
                nombreDistrito = "San Isidro del Géneral",
                nombreCanton = "Pérez Zeledón",
                nombreProvincia = "San José"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(registro.creacion,
                new ValidationContext(registro) { MemberName = "creacion" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548
        [TestMethod]
        public void usuario_ValidacionLongitud_DeberiaSerValido()
        {
            // Crear registro con usuario incorrecto de prueba
            var registro = new Registro
            {
                creacion = DateTime.Now,
                // El nombre de usuario debe tener entre 5 y 20 caracteres
                usuarioCreador = "Us0*",
                descripcion = "Esta es una descripción",
                precio = 5000.50m,
                calificacion = 5,
                productoAsociado = "Tennissitas",
                nombreTienda = "Tienda tus tennis",
                nombreDistrito = "San Isidro del Géneral",
                nombreCanton = "Pérez Zeledón",
                nombreProvincia = "San José"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(registro.usuarioCreador,
                new ValidationContext(registro) { MemberName = "usuarioCreador" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548
        [TestMethod]
        public void descripcion_ValidacionLongitud_DeberiaSerValido()
        {
            // Crear registro con descripción incorrecta de prueba
            var registro = new Registro
            {
                creacion = DateTime.Now,
                usuarioCreador = "Usuario1212*",
                // La descripción debe tener entre 1 y 150 caracteres
                descripcion = "Tengo que escribir una descripción de " +
                "más de ciento cincuenta caracteres para confirmar si " +
                "esta propiedad funciona de forma correcta y de ahí este " +
                "texto extremadamente largo",
                precio = 5000.50m,
                calificacion = 5,
                productoAsociado = "Tennissitas",
                nombreTienda = "Tienda tus tennis",
                nombreDistrito = "San Isidro del Géneral",
                nombreCanton = "Pérez Zeledón",
                nombreProvincia = "San José"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(registro.descripcion,
                new ValidationContext(registro) { MemberName = "descripcion" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548
        [TestMethod]
        public void calificacion_ValidacionRango_DeberiaSerValido()
        {
            // Crear registro con descripción incorrecta de prueba
            var registro = new Registro
            {
                creacion = DateTime.Now,
                usuarioCreador = "Usuario1212*",
                descripcion = "Esta es una descripción",
                precio = 5000.50m,
                // La calificación debe estar entre 0 y 5 puntos
                calificacion = -1,
                productoAsociado = "Tennissitas",
                nombreTienda = "Tienda tus tennis",
                nombreDistrito = "San Isidro del Géneral",
                nombreCanton = "Pérez Zeledón",
                nombreProvincia = "San José"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(registro.calificacion,
                new ValidationContext(registro) { MemberName = "calificacion" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548
        [TestMethod]
        public void productoAsociado_ValidacionLongitud_DeberiaSerValido()
        {
            // Crear registro con nombre de producto incorrecto de prueba
            var registro = new Registro
            {
                creacion = DateTime.Now,
                usuarioCreador = "Usuario1212*",
                descripcion = "Esta es una descripción",
                precio = 5000.50m,
                calificacion = 5,
                // El nombre del producto asociado debe tener entre 1 y
                // 256 caracteres
                productoAsociado = "",
                nombreTienda = "Tienda tus tennis",
                nombreDistrito = "San Isidro del Géneral",
                nombreCanton = "Pérez Zeledón",
                nombreProvincia = "San José"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(registro.productoAsociado,
                new ValidationContext(registro) { MemberName = "productoAsociado" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548
        [TestMethod]
        public void nombreTienda_ValidacionLongitud_DeberiaSerValido()
        {
            // Crear registro con nombre de tienda incorrecto de prueba
            var registro = new Registro
            {
                creacion = DateTime.Now,
                usuarioCreador = "Usuario1212*",
                descripcion = "Esta es una descripción",
                precio = 5000.50m,
                calificacion = 5,
                productoAsociado = "Tennissitas",
                // El nombre de la tienda debe tener entre 1 y 256
                // caracteres
                nombreTienda = "",
                nombreDistrito = "San Isidro del Géneral",
                nombreCanton = "Pérez Zeledón",
                nombreProvincia = "San José"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(registro.nombreTienda,
                new ValidationContext(registro) { MemberName = "nombreTienda" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548
        [TestMethod]
        public void nombreTienda_ValidacionRegex_DeberiaSerValido()
        {
            // Crear registro con nombre de tienda incorrecto de prueba
            var registro = new Registro
            {
                creacion = DateTime.Now,
                usuarioCreador = "Usuario1212*",
                descripcion = "Esta es una descripción",
                precio = 5000.50m,
                calificacion = 5,
                productoAsociado = "Tennissitas",
                // El nombre de la tienda debe estar formado por letras
                // solamente
                nombreTienda = "Supermercado Cool!",
                nombreDistrito = "San Isidro del Géneral",
                nombreCanton = "Pérez Zeledón",
                nombreProvincia = "San José"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(registro.nombreTienda,
                new ValidationContext(registro) { MemberName = "nombreTienda" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548
        [TestMethod]
        public void nombreDistrito_ValidacionLongitud_DeberiaSerValido()
        {
            // Crear registro con nombre de distrito incorrecto de prueba
            var registro = new Registro
            {
                creacion = DateTime.Now,
                usuarioCreador = "Usuario1212*",
                descripcion = "Esta es una descripción",
                precio = 5000.50m,
                calificacion = 5,
                productoAsociado = "Tennissitas",
                nombreTienda = "Tienda tus tennis",
                // El nombre del distrito tener entre 3 y 30 caracteres
                nombreDistrito = "Di",
                nombreCanton = "Pérez Zeledón",
                nombreProvincia = "San José"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(registro.nombreDistrito,
                new ValidationContext(registro) { MemberName = "nombreDistrito" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548
        [TestMethod]
        public void nombreDistrito_ValidacionRegex_DeberiaSerValido()
        {
            // Crear registro con nombre de distrito incorrecto de prueba
            var registro = new Registro
            {
                creacion = DateTime.Now,
                usuarioCreador = "Usuario1212*",
                descripcion = "Esta es una descripción",
                precio = 5000.50m,
                calificacion = 5,
                productoAsociado = "Tennissitas",
                nombreTienda = "Tienda tus tennis",
                // El nombre del distrito debe estar formado por letras
                // solamente
                nombreDistrito = "¡El mejor distrito!",
                nombreCanton = "Pérez Zeledón",
                nombreProvincia = "San José"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(registro.nombreDistrito,
                new ValidationContext(registro) { MemberName = "nombreDistrito" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548
        [TestMethod]
        public void nombreCanton_ValidacionLongitud_DeberiaSerValido()
        {
            // Crear registro con nombre de cantón incorrecto de prueba
            var registro = new Registro
            {
                creacion = DateTime.Now,
                usuarioCreador = "Usuario1212*",
                descripcion = "Esta es una descripción",
                precio = 5000.50m,
                calificacion = 5,
                productoAsociado = "Tennissitas",
                nombreTienda = "Tienda tus tennis",
                nombreDistrito = "San Isidro del Géneral",
                // El nombre del cantón debe tener entre 3 y 20 caracteres
                nombreCanton = "Ca",
                nombreProvincia = "San José"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(registro.nombreCanton,
                new ValidationContext(registro) { MemberName = "nombreCanton" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548
        [TestMethod]
        public void nombreCanton_ValidacionRegex_DeberiaSerValido()
        {
            // Crear registro con nombre de cantón incorrecto de prueba
            var registro = new Registro
            {
                creacion = DateTime.Now,
                usuarioCreador = "Usuario1212*",
                descripcion = "Esta es una descripción",
                precio = 5000.50m,
                calificacion = 5,
                productoAsociado = "Tennissitas",
                nombreTienda = "Tienda tus tennis",
                nombreDistrito = "San Isidro del Géneral",
                // El nombre del cantón debe estar formado por letras
                // solamente
                nombreCanton = "Pérez 2",
                nombreProvincia = "San José"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(registro.nombreCanton,
                new ValidationContext(registro) { MemberName = "nombreCanton" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548
        [TestMethod]
        public void nombreProvincia_ValidacionLongitud_DeberiaSerValido()
        {
            // Crear registro con nombre de provincia incorrecto de prueba
            var registro = new Registro
            {
                creacion = DateTime.Now,
                usuarioCreador = "Usuario1212*",
                descripcion = "Esta es una descripción",
                precio = 5000.50m,
                calificacion = 5,
                productoAsociado = "Tennissitas",
                nombreTienda = "Tienda tus tennis",
                nombreDistrito = "San Isidro del Géneral",
                nombreCanton = "Pérez Zeledón",
                // El nombre de la provincia debe tener entre 5 y 10 caracteres
                nombreProvincia = "La provincia independiente de Pérez"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(registro.nombreProvincia,
                new ValidationContext(registro) { MemberName = "nombreProvincia" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Kenneth Daniel Villalobos Solís - C18548
        [TestMethod]
        public void nombreProvincia_ValidacionRegex_DeberiaSerValido()
        {
            // Crear registro con nombre de provincia incorrecto de prueba
            var registro = new Registro
            {
                creacion = DateTime.Now,
                usuarioCreador = "Usuario1212*",
                descripcion = "Esta es una descripción",
                precio = 5000.50m,
                calificacion = 5,
                productoAsociado = "Tennissitas",
                nombreTienda = "Tienda tus tennis",
                nombreDistrito = "San Isidro del Géneral",
                nombreCanton = "Pérez Zeledón",
                // El nombre de la provincia debe estar formado por letras
                nombreProvincia = "PR0V1NC1A"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(registro.nombreProvincia,
                new ValidationContext(registro) { MemberName = "nombreProvincia" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }
    }
}
