using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LoCoMPro.Models;


namespace LoCoMProTests.Models
{
    [TestClass]
    public class CategoriaTest
    {
        // Hecho por: Luis David Solano Santamaría - C17634
        [TestMethod]
        public void categoria_Validacion_DeberiaSerValido()
        {
            // Crear categoría válida
            var categoria = new Categoria
            {
                nombre = "Electrónicos"
            };
            // Establecer condiciones de prueba
            var contexto = new ValidationContext(categoria);
            var esValido = Validator.TryValidateObject(categoria, contexto, null, true);
            // Revisar condiciones de prueba
            Assert.IsTrue(esValido);
        }

        // Hecho por: Luis David Solano Santamaría - C17634
        [TestMethod]
        public void nombreCategoria_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear una categoría con tamaño que no cumple el rango establecido
            var categoria = new Categoria
            {
                nombre = ""
            };
            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(categoria.nombre,
                new ValidationContext(categoria) { MemberName = "nombre" }, null);
            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Luis David Solano Santamaría - C17634
        [TestMethod]
        public void nombreCategoria_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear una categoría con nombre que no cumple su expresión regular
            var categoria = new Categoria
            {
                nombre = "Entrada Invalida 25"
            };
            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(categoria.nombre,
                new ValidationContext(categoria) { MemberName = "nombre" }, null);
            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }
    }
}