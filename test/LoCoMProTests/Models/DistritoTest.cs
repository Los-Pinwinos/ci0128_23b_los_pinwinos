using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using LoCoMPro.Models;

namespace PruebasProvisionales
{
    [TestClass]
    public class DistritoTest
    {
        // Hecho por: Emilia María Víquez Mora - C18625
        [TestMethod]
        public void distrito_Validacion_DeberiaSerValido()
        {
            // Crear distrito correcta de prueba
            var distrito = new Distrito
            {
                // 5 caracteres representa una longitud válida
                // No presenta números
                nombre = "Ulloa",
                // 7 caracteres representa una longitud válida
                // No presenta números
                nombreCanton = "Heredia",
                // 7 caracteres representa una longitud válida
                // No presenta números
                nombreProvincia = "Heredia"
            };

            // Establecer condiciones de prueba
            var contexto = new ValidationContext(distrito);
            var esValido = Validator.TryValidateObject(distrito, contexto, null, true);

            // Revisar condiciones de prueba
            Assert.IsTrue(esValido);
        }

        // Hecho por: Emilia María Víquez Mora - C18625
        [TestMethod]
        public void nombre_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear distrito con nombre incorrecto de prueba
            var distrito = new Distrito
            {
                // El nombre debe tener entre 3 y 30 caracteres
                nombre = "D",
                nombreCanton = "Heredia",
                nombreProvincia = "Heredia"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(distrito.nombre,
                new ValidationContext(distrito) { MemberName = "nombre" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Emilia María Víquez Mora - C18625
        [TestMethod]
        public void nombre_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear distrito con nombre incorrecto de prueba
            var distrito = new Distrito
            {
                // El nombre debe estar formado por letras solamente
                nombre = "Distrito2",
                nombreCanton = "Heredia",
                nombreProvincia = "Heredia"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(distrito.nombre,
                new ValidationContext(distrito) { MemberName = "nombre" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Emilia María Víquez Mora - C18625
        [TestMethod]
        public void nombreCanton_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear distrito con nombre de cantón incorrecto de prueba
            var distrito = new Distrito
            {
                nombre = "Ulloa",
                // El nombre del cantón debe tener entre 3 y 20 caracteres
                nombreCanton = "C",
                nombreProvincia = "Heredia"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(distrito.nombreCanton,
                new ValidationContext(distrito) { MemberName = "nombreCanton" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Emilia María Víquez Mora - C18625
        [TestMethod]
        public void nombreCanton_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear distrito con nombre de cantón incorrecto de prueba
            var distrito = new Distrito
            {
                nombre = "Ulloa",
                // El nombre del cantón debe estar formado por letras solamente
                nombreCanton = "Cantón0",
                nombreProvincia = "Heredia"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(distrito.nombreCanton,
                new ValidationContext(distrito) { MemberName = "nombreCanton" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Emilia María Víquez Mora - C18625
        [TestMethod]
        public void nombreProvincia_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear distrito con nombre de provincia incorrecto de prueba
            var distrito = new Distrito
            {
                nombre = "Ulloa",
                nombreCanton = "Heredia",
                // El nombre de la provincia debe tener entre 5 y 10 caracteres
                nombreProvincia = "Herediaaaaaa"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(distrito.nombreProvincia,
                new ValidationContext(distrito) { MemberName = "nombreProvincia" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Emilia María Víquez Mora - C18625
        [TestMethod]
        public void nombreProvincia_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear distrito con nombre de provincia incorrecto de prueba
            var distrito = new Distrito
            {
                nombre = "Ulloa",
                nombreCanton = "Heredia",
                // El nombre de la provincia debe estar formado por letras solamente
                nombreProvincia = "Heredia1"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(distrito.nombreProvincia,
                new ValidationContext(distrito) { MemberName = "nombreProvincia" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }
    }
}