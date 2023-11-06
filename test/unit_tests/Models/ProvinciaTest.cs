using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LoCoMPro.Models;

namespace LoCoMProTests.Models
{
    [TestClass]
    public class ProvinciaTest
    {
        // Hecho por: Luis David Solano Santamaría - C17634
        [TestMethod]
        public void provincia_Validacion_DeberiaSerValido()
        {
            // Crear provincia válida
            var provincia = new Provincia
            {
                nombre = "Alajuela"
            };
            // Establecer condiciones de prueba
            var contexto = new ValidationContext(provincia);
            var esValido = Validator.TryValidateObject(provincia, contexto, null, true);
            // Revisar condiciones de prueba
            Assert.IsTrue(esValido);
        }

        // Hecho por: Luis David Solano Santamaría - C17634
        [TestMethod]
        public void nombreProvincia_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear provincia que no cumple con el tamaño mínimo
            var provincia = new Provincia
            {
                nombre = "Al"
            };
            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(provincia.nombre,
                new ValidationContext(provincia) { MemberName = "nombre" }, null);
            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Luis David Solano Santamaría - C17634
        [TestMethod]
        public void nombreProvincia_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear provincia que no cumple con el regex establecido
            var provincia = new Provincia
            {
                nombre = "Alajuela 25"
            };
            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(provincia.nombre,
                new ValidationContext(provincia) { MemberName = "nombre" }, null);
            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }
    }
}