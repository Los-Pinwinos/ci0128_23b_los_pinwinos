using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using LoCoMPro.Models;

namespace LoCoMProTests.Models
{
    [TestClass]
    public class ProductoTests
    {
        // Hecho por: Emilia María Víquez Mora - C18625
        [TestMethod]
        public void producto_Validacion_DeberiaSerValido()
        {
            // Crear producto correcto de prueba
            var producto = new Producto
            {
                // El nombre debe tener entre 1 y 256 caracteres
                nombre = "Camisa",
                // El nombre de la marca debe tener entre 1 y 256 caracteres
                marca = "Gucci",
                // El nombre de la unidad debe tener entre 1 y 20 caracteres
                // No presenta números
                nombreUnidad = "Cantidad",
                // El nombre de la categoria debe tener entre 1 y 256 caracteres
                // No presenta números
                nombreCategoria = "Ropa"
            };

            // Establecer condiciones de prueba
            var contexto = new ValidationContext(producto);
            var esValido = Validator.TryValidateObject(producto, contexto, null, true);

            // Revisar condiciones de prueba
            Assert.IsTrue(esValido);
        }

        // Hecho por: Emilia María Víquez Mora - C18625
        [TestMethod]
        public void nombre_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear producto con nombre incorrecto de prueba
            var producto = new Producto
            {
                // El nombre debe tener entre 1 y 256 caracteres
                nombre = "",
                marca = "Gucci",
                nombreUnidad = "Cantidad",
                nombreCategoria = "Ropa"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(producto.nombre,
                new ValidationContext(producto) { MemberName = "nombre" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Emilia María Víquez Mora - C18625
        [TestMethod]
        public void marca_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear producto con nombre de marca incorrecto de prueba
            var producto = new Producto
            {
                nombre = "Camisa",
                // El nombre de la marca debe tener entre 1 y 256 caracteres
                marca = "",
                nombreUnidad = "Cantidad",
                nombreCategoria = "Ropa"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(producto.marca,
                new ValidationContext(producto) { MemberName = "marca" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Emilia María Víquez Mora - C18625
        [TestMethod]
        public void nombreUnidad_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear producto con nombre de unidad incorrecto de prueba
            var producto = new Producto
            {
                nombre = "Camisa",
                marca = "Gucci",
                // El nombre de la unidad debe tener entre 1 y 20 caracteres
                nombreUnidad = "LaMejorUnidadDeRopaQueHaExistidoEnTodoElMundoSoloEnLoCoMPro",
                nombreCategoria = "Ropa"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(producto.nombreUnidad,
                new ValidationContext(producto) { MemberName = "nombreUnidad" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Emilia María Víquez Mora - C18625
        [TestMethod]
        public void nombreUnidad_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear producto con nombre de unidad incorrecto de prueba
            var producto = new Producto
            {
                nombre = "Camisa",
                marca = "Gucci",
                // El nombre de la unidad no debe tener numeros
                nombreUnidad = "Cantidad1",
                nombreCategoria = "Ropa"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(producto.nombreUnidad,
                new ValidationContext(producto) { MemberName = "nombreUnidad" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Emilia María Víquez Mora - C18625
        [TestMethod]
        public void nombreCategoria_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear producto con nombre de categoría incorrecto de prueba
            var producto = new Producto
            {
                nombre = "Camisa",
                marca = "Gucci",
                nombreUnidad = "Cantidad",
                // El nombre de la categoria debe tener entre 1 y 256 caracteres
                nombreCategoria = ""
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(producto.nombreCategoria,
                new ValidationContext(producto) { MemberName = "nombreCategoria" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Emilia María Víquez Mora - C18625
        [TestMethod]
        public void nombreCategoria_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear producto con nombre de categoría incorrecto de prueba
            var producto = new Producto
            {
                nombre = "Camisa",
                marca = "Gucci",
                nombreUnidad = "Cantidad",
                // El nombre de la categoria no debe tener numeros
                nombreCategoria = "Categoria1"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(producto.nombreCategoria,
                new ValidationContext(producto) { MemberName = "nombreCategoria" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }
    }
}