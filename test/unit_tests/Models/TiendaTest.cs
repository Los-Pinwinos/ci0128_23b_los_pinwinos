using LoCoMPro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoCoMProTests.Models
{
    [TestClass]
    public class TiendaTest
    {
        // Hecho por: Angie Sofía Solís Manzano - C17686
        [TestMethod]
        public void tienda_Validacion_DeberiaSerValido()
        {
            // Crear tienda correcta de prueba
            var tienda = new Tienda
            {
                // 4 caracteres representa una longitud válida
                // No presenta números ni caracteres especiales
                nombre = "Pali",
                // 11 caracteres representa una longitud válida
                // No presenta números
                nombreDistrito = "San Vicente",
                // 7 caracteres representa una longitud válida
                // No presenta números
                nombreCanton = "Moravia",
                // 8 caracteres representa una longitud válida
                // No presenta números
                nombreProvincia = "San José",
                // Coordenadas
                latitud = 0,
                longitud = 0
            };

            // Establecer condiciones de prueba
            var contexto = new ValidationContext(tienda);
            var esValido = Validator.TryValidateObject(tienda, contexto, null, true);

            // Revisar condiciones de prueba
            Assert.IsTrue(esValido);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686
        [TestMethod]
        public void nombreTienda_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear tienda con nombre incorrecto de prueba
            var tienda = new Tienda
            {
                // El nombre de la tienda debe tener entre 1 y 256 caracteres
                nombre = "",
                nombreDistrito = "San Vicente",
                nombreCanton = "Moravia",
                nombreProvincia = "San José",
                latitud = 0,
                longitud = 0
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(tienda.nombre,
                new ValidationContext(tienda) { MemberName = "nombre" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686
        [TestMethod]
        public void nombreTienda_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear tienda con nombre incorrecto de prueba
            var tienda = new Tienda
            {
                // El nombre de la tienda debe estar formado por letras solamente
                nombre = "Supermercado Cool!",
                nombreDistrito = "San Vicente",
                nombreCanton = "Moravia",
                nombreProvincia = "San José",
                latitud = 0,
                longitud = 0
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(tienda.nombre,
                new ValidationContext(tienda) { MemberName = "nombre" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686
        [TestMethod]
        public void nombreDistrito_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear tienda con nombre de distrito incorrecto de prueba
            var tienda = new Tienda
            {
                nombre = "Pali",
                // El nombre del distrito tener entre 3 y 30 caracteres
                nombreDistrito = "Distrito Invalido Por No Estar Dentro Del " +
                    "Rango Del Largo de Distritos en Costa Rica",
                nombreCanton = "Moravia",
                nombreProvincia = "San José",
                latitud = 0,
                longitud = 0
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(tienda.nombreDistrito,
                new ValidationContext(tienda) { MemberName = "nombreDistrito" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686
        [TestMethod]
        public void nombreDistrito_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear tienda con nombre de distrito incorrecto de prueba
            var tienda = new Tienda
            {
                nombre = "Pali",
                // El nombre del distrito debe estar formado por letras
                // solamente
                nombreDistrito = "¡El mejor distrito!",
                nombreCanton = "Moravia",
                nombreProvincia = "San José",
                latitud = 0,
                longitud = 0
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(tienda.nombreDistrito,
                new ValidationContext(tienda) { MemberName = "nombreDistrito" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686
        [TestMethod]
        public void nombreCanton_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear tienda con nombre de cantón incorrecto de prueba
            var tienda = new Tienda
            {
                nombre = "Pali",
                nombreDistrito = "San Vicente",
                // El nombre del cantón debe tener entre 3 y 20 caracteres
                nombreCanton = "Canton Invalido Por No Estar Dentro Del Rango " +
                "Del Largo de Cantones en Costa Rica",
                nombreProvincia = "San José",
                latitud = 0,
                longitud = 0
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(tienda.nombreCanton,
                new ValidationContext(tienda) { MemberName = "nombreCanton" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686
        [TestMethod]
        public void nombreCanton_Validacionregex_DeberiaSerInvalido()
        {
            // Crear tienda con nombre de cantón incorrecto de prueba
            var tienda = new Tienda
            {
                nombre = "Pali",
                nombreDistrito = "San Vicente",
                // El nombre del cantón debe estar formado por letras solamente
                nombreCanton = "123",
                nombreProvincia = "San José",
                latitud = 0,
                longitud = 0
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(tienda.nombreCanton,
                new ValidationContext(tienda) { MemberName = "nombreCanton" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686
        [TestMethod]
        public void nombreProvincia_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear tienda con nombre de provincia incorrecto de prueba
            var tienda = new Tienda
            {
                nombre = "Pali",
                nombreDistrito = "San Vicente",
                nombreCanton = "Moravia",
                // El nombre de la provincia debe tener entre 5 y 10 caracteres
                nombreProvincia = "Provincia Invalida Por No Estar Dentro Del " +
                "Rango Del Lago de Provincias en Costa Rica",
                latitud = 0,
                longitud = 0
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(tienda.nombreProvincia,
                new ValidationContext(tienda) { MemberName = "nombreProvincia" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686
        [TestMethod]
        public void nombreProvincia_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear tienda con nombre de provincia incorrecto de prueba
            var tienda = new Tienda
            {
                nombre = "Pali",
                nombreDistrito = "San Vicente",
                nombreCanton = "Moravia",
                // El nombre de la provincia debe estar formado por letras
                nombreProvincia = " ",
                latitud = 0,
                longitud = 0
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(tienda.nombreProvincia,
                new ValidationContext(tienda) { MemberName = "nombreProvincia" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }
    }
}
