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
    public class EtiquetaTest
    {
        // Hecho por: Angie Sofía Solís Manzano - C17686
        [TestMethod]
        public void etiqueta_Validacion_DeberiaSerValido()
        {
            // Crear etiqueta correcta de prueba
            var etiqueta = new Etiqueta
            {
                // 15 caracteres representa una longitud válida
                // No presenta números
                etiqueta = "comidaSaludable",
                // La fecha de creación debe estar entre 1/1/2000 y 1/1/2200
                creacion = DateTime.Now,
                // 12 caracteres representa una longitud válida
                // Presenta mayuscula, minuscula, digito y caractér especial
                usuarioCreador = "Usuario1212*"
            };

            // Establecer condiciones de prueba
            var contexto = new ValidationContext(etiqueta);
            var esValido = Validator.TryValidateObject(etiqueta, contexto, null, true);

            // Revisar condiciones de prueba
            Assert.IsTrue(esValido);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686
        [TestMethod]
        public void nombreEtiqueta_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear etiqueta con nombre incorrecto de prueba
            var etiqueta = new Etiqueta
            {
                // El nombre de la etiqueta debe tener entre 1 y 256 caracteres
                etiqueta = "",
                creacion = DateTime.Now,
                usuarioCreador = "Usuario1212*"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(etiqueta.etiqueta,
                new ValidationContext(etiqueta) { MemberName = "etiqueta" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686
        [TestMethod]
        public void nombreEtiqueta_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear etiqueta con nombre incorrecto de prueba
            var etiqueta = new Etiqueta
            {
                // El nombre de la etiqueta debe estar formado por letras solamente
                etiqueta = "comidaSaludable1",
                creacion = DateTime.Now,
                usuarioCreador = "Usuario1212*"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(etiqueta.etiqueta,
                new ValidationContext(etiqueta) { MemberName = "etiqueta" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686
        [TestMethod]
        public void creacion_ValidacionRango_DeberiaSerInvalido()
        {
            // Crear etiqueta con fecha incorrecta de prueba
            var etiqueta = new Etiqueta
            {
                etiqueta = "",
                // La fecha de creación debe estar entre 1/1/2000 y 1/1/2200
                creacion = new DateTime(3000, 1, 1),
                usuarioCreador = "Usuario1212*"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(etiqueta.creacion,
                new ValidationContext(etiqueta) { MemberName = "creacion" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686
        [TestMethod]
        public void usuarioCreador_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear etiqueta con usuario incorrecto de prueba
            var etiqueta = new Etiqueta
            {
                etiqueta = "comidaSaludable",
                creacion = DateTime.Now,
                // El nombre del creador debe tener entre 5 y 20 caracteres
                usuarioCreador = "Us0*"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(etiqueta.usuarioCreador,
                new ValidationContext(etiqueta) { MemberName = "usuarioCreador" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Angie Sofía Solís Manzano - C17686
        [TestMethod]
        public void usuarioCreador_ValidacionRegex_DeberiaSerInvalido()
        {
            // Crear etiqueta con usuario incorrecto de prueba
            var etiqueta = new Etiqueta
            {
                etiqueta = "comidaSaludable",
                creacion = DateTime.Now,
                // El nombre del creador debe tener al menos un digito
                usuarioCreador = "Us-uario"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(etiqueta.usuarioCreador,
                new ValidationContext(etiqueta) { MemberName = "usuarioCreador" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }
    }
}
