using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LoCoMPro.Models;
using System.Drawing;

namespace LoCoMProTests.Models
{
    [TestClass]
    public class CantonTest
    {
        // Hecho por: Luis David Solano Santamaría - C17634
        [TestMethod]
        public void canton_Validacion_DeberiaSerValido()
        {
            // Crear cantón válido
            var canton = new Canton
            {
                nombre = "Alajuela",
                nombreProvincia = "Alajuela"
            };
            // Establecer condiciones de prueba
            var contexto = new ValidationContext(canton);
            var esValido = Validator.TryValidateObject(canton, contexto, null, true);
            // Revisar condiciones de prueba
            Assert.IsTrue(esValido);
        }

        // Hecho por: Luis David Solano Santamaría - C17634
        [TestMethod]
        public void nombreCanton_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Debe de tener al menos tres elementos en el nombre
            var canton = new Canton
            {
                nombre = "Al",
                nombreProvincia = "Alajuela"
            };
            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(canton.nombre,
                new ValidationContext(canton) { MemberName = "nombre" }, null);
            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Luis David Solano Santamaría - C17634
        [TestMethod]
        public void nombreCanton_ValidacionRegex_DeberiaSerInvalido()
        {
            // No puede tener números en su nombre
            var canton = new Canton
            {
                nombre = "Alajuela 25",
                nombreProvincia = "Alajuela"
            };
            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(canton.nombre,
                new ValidationContext(canton) { MemberName = "nombre" }, null);
            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Luis David Solano Santamaría - C17634
        [TestMethod]
        public void nombreProvincia_ValidacionLongitud_DeberiaSerInvalido()
        {
            // No puede tener menos de 5 elementos en el nombre de la provincia
            var canton = new Canton
            {
                nombre = "Alajuela",
                nombreProvincia = "Al"
            };
            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(canton.nombreProvincia,
                new ValidationContext(canton) { MemberName = "nombreProvincia" }, null);
            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Luis David Solano Santamaría - C17634
        [TestMethod]
        public void nombreProvincia_ValidacionRegex_DeberiaSerInvalido()
        {
            // No puede tener números en su nombre
            var canton = new Canton
            {
                nombre = "Alajuela",
                nombreProvincia = "Alajuela 25"
            };
            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(canton.nombreProvincia,
                new ValidationContext(canton) { MemberName = "nombreProvincia" }, null);
            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }
    }
}