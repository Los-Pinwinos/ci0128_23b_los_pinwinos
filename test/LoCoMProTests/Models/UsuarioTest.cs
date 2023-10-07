using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using LoCoMPro.Models;

namespace PruebasProvisionales
{
    [TestClass]
    public class UsuarioTests
    {
        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477
        [TestMethod]
        public void usuario_Validacion_DeberiaSerValido()
        {
            // Crear usuario correcto de prueba
            var usuario = new Usuario
            {
                // 12 caracteres representa una longitud válida
                // Presenta mayuscula, minuscula, digito y caractér especial
                nombreDeUsuario = "Usuario1212*",
                // El correo debe ser de tipo EmailAddress
                correo = "example@gmail.com",
                // Contrasena
                hashContrasena = "MiContrasena00+",
                // El estado debe ser 'A', 'B' o 'C'
                estado = 'A',
                // La calificación debe estar entre 0 y 5 puntos
                calificacion = 5
            };

            // Establecer condiciones de prueba
            var contexto = new ValidationContext(usuario);
            var esValido = Validator.TryValidateObject(usuario, contexto, null, true);

            // Revisar condiciones de prueba
            Assert.IsTrue(esValido);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477
        [TestMethod]
        public void nombreDeUsuario_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear usuario incorrecto de prueba
            var usuario = new Usuario
            {
                // El nombre de usuario debe tener entre 5 y 20 caracteres
                nombreDeUsuario = "Us0*",
                correo = "example@gmail.com",
                hashContrasena = "hashcontrasena",
                estado = 'A',
                calificacion = 5
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(usuario.nombreDeUsuario,
                new ValidationContext(usuario) { MemberName = "nombreDeUsuario" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477
        [TestMethod]
        public void nombreDeUsuario_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear usuario incorrecto de prueba
            var usuario = new Usuario
            {
                // El nombre de usuario debe tener al menos un digito
                nombreDeUsuario = "Us-uario",
                correo = "example@gmail.com",
                hashContrasena = "hashcontrasena",
                estado = 'A',
                calificacion = 5
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(usuario.nombreDeUsuario,
                new ValidationContext(usuario) { MemberName = "nombreDeUsuario" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477
        [TestMethod]
        public void correo_ValidacionTipo_DeberiaSerInvalido()
        {
            // Crear usuario correcto de prueba
            var usuario = new Usuario
            {
                nombreDeUsuario = "Usuario1212*",
                // El correo debe ser de tipo EmailAddress
                correo = "examplegmail.com",
                hashContrasena = "hashcontrasena",
                estado = 'A',
                calificacion = 5
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(usuario.correo,
                new ValidationContext(usuario) { MemberName = "correo" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477
        [TestMethod]
        public void estado_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear usuario correcto de prueba
            var usuario = new Usuario
            {
                nombreDeUsuario = "Usuario1212*",
                correo = "example@gmail.com",
                hashContrasena = "hashcontrasena",
                // El estado debe ser 'A', 'B' o 'C'
                estado = 'P',
                calificacion = 5
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(usuario.estado,
                new ValidationContext(usuario) { MemberName = "estado" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477
        [TestMethod]
        public void calificacion_ValidacionRango_DeberiaSerInvalido()
        {
            // Crear usuario correcto de prueba
            var usuario = new Usuario
            {
                nombreDeUsuario = "Usuario1212*",
                correo = "example@gmail.com",
                hashContrasena = "hashcontrasena",
                estado = 'A',
                // La calificación debe estar entre 0 y 5 puntos
                calificacion = -1
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(usuario.calificacion,
                new ValidationContext(usuario) { MemberName = "calificacion" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477
        [TestMethod]
        public void distritoVivienda_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear usuario correcto de prueba
            var usuario = new Usuario
            {
                nombreDeUsuario = "Usuario1212*",
                correo = "example@gmail.com",
                hashContrasena = "hashcontrasena",
                estado = 'A',
                calificacion = 5,
                // El distrito debe tener entre 3 y 30 caracteres
                distritoVivienda = "d1"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(usuario.distritoVivienda,
                new ValidationContext(usuario) { MemberName = "distritoVivienda" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477
        [TestMethod]
        public void distritoVivienda_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear usuario correcto de prueba
            var usuario = new Usuario
            {
                nombreDeUsuario = "Usuario1212*",
                correo = "example@gmail.com",
                hashContrasena = "hashcontrasena",
                estado = 'A',
                calificacion = 5,
                // El distrito debe estar formado por letras solamente
                distritoVivienda = "distrito1"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(usuario.distritoVivienda,
                new ValidationContext(usuario) { MemberName = "distritoVivienda" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477
        [TestMethod]
        public void cantonVivienda_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear usuario correcto de prueba
            var usuario = new Usuario
            {
                nombreDeUsuario = "Usuario1212*",
                correo = "example@gmail.com",
                hashContrasena = "hashcontrasena",
                estado = 'A',
                calificacion = 5,
                // El canton debe tener entre 3 y 20 caracteres
                cantonVivienda = "c1"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(usuario.cantonVivienda,
                new ValidationContext(usuario) { MemberName = "cantonVivienda" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477
        [TestMethod]
        public void cantonVivienda_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear usuario correcto de prueba
            var usuario = new Usuario
            {
                nombreDeUsuario = "Usuario1212*",
                correo = "example@gmail.com",
                hashContrasena = "hashcontrasena",
                estado = 'A',
                calificacion = 5,
                // El canton debe tener entre 3 y 20 caracteres
                cantonVivienda = "canton1"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(usuario.cantonVivienda,
                new ValidationContext(usuario) { MemberName = "cantonVivienda" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477
        [TestMethod]
        public void provinciaVivienda_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear usuario correcto de prueba
            var usuario = new Usuario
            {
                nombreDeUsuario = "Usuario1212*",
                correo = "example@gmail.com",
                hashContrasena = "hashcontrasena",
                estado = 'A',
                calificacion = 5,
                // La provincia debe tener entre 5 y 10 caracteres
                provinciaVivienda = "p1"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(usuario.provinciaVivienda,
                new ValidationContext(usuario) { MemberName = "provinciaVivienda" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477
        [TestMethod]
        public void provinciaVivienda_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear usuario correcto de prueba
            var usuario = new Usuario
            {
                nombreDeUsuario = "Usuario1212*",
                correo = "example@gmail.com",
                hashContrasena = "hashcontrasena",
                estado = 'A',
                calificacion = 5,
                // La provincia debe tener entre 5 y 10 caracteres
                provinciaVivienda = "provin1"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(usuario.provinciaVivienda,
                new ValidationContext(usuario) { MemberName = "provinciaVivienda" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }
    }
}
