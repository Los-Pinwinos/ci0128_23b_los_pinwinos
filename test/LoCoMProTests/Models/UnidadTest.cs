using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LoCoMPro.Models;


namespace LoCoMProTests.Models
{
    [TestClass]
    public class UnidadTest
    {
        // Hecho por: Luis David Solano Santamaría - C17634
        [TestMethod]
        public void unidad_Validacion_DeberiaSerValido()
        {
            // Crear una unidad con nombre que cumple su expresión regular
            var unidad = new Unidad
            {
                nombre = "Bolsa de papas"
            };
            // Establecer condiciones de prueba
            var contexto = new ValidationContext(unidad);
            var esValido = Validator.TryValidateObject(unidad, contexto, null, true);
            // Revisar condiciones de prueba
            Assert.IsTrue(esValido);
        }

        // Hecho por: Luis David Solano Santamaría - C17634
        [TestMethod]
        public void unidadNombre_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear una unidad con nombre longitud que no cumple el intervalo deseado
            var unidad = new Unidad
            {
                nombre = ""
            };
            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(unidad.nombre,
                new ValidationContext(unidad) { MemberName = "nombre" }, null);
            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Luis David Solano Santamaría - C17634
        [TestMethod]
        public void unidadNombre_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear una unidad con nombre que no cumple su expresión regular
            var unidad = new Unidad
            {
                nombre = "Entrada Invalida 25"
            };
            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(unidad.nombre,
                new ValidationContext(unidad) { MemberName = "nombre" }, null);
            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }
    }
}