﻿using System.ComponentModel.DataAnnotations;
using LoCoMPro.Models;

namespace LoCoMProTests.Models
{
    [TestClass]
    public class FotografiaTests
    {
        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477
        [TestMethod]
        public void fotografia_Validacion_DeberiaSerValido()
        {
            // Crear fotografia correcta de prueba
            var foto = new Fotografia
            {   // Como no se prueba este atributo, no se inicializa como
                // una fotografía de verdad, solo un arreglo de bytes
                fotografia = BitConverter.GetBytes(12345),
                // La fecha de creación debe estar entre 1/1/2000 y 1/1/2200
                creacion = DateTime.Now,
                // 12 caracteres representa una longitud válida
                usuarioCreador = "Usuario1212*"
            };

            // Establecer condiciones de prueba
            var contexto = new ValidationContext(foto);
            var esValido = Validator.TryValidateObject(foto, contexto, null, true);

            // Revisar condiciones de prueba
            Assert.IsTrue(esValido);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477
        [TestMethod]
        public void creacion_ValidacionRango_DeberiaSerInvalido()
        {
            // Crear fotografia con fecha incorrecta de prueba
            var foto = new Fotografia
            {
                fotografia = BitConverter.GetBytes(12345),
                // La fecha de creación debe estar entre 1/1/2000 y 1/1/2200
                creacion = new DateTime(1999, 9, 2),
                usuarioCreador = "Usuario1212*"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(foto.creacion,
                new ValidationContext(foto) { MemberName = "creacion" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }

        // Hecho por: Enrique Guillermo Vílchez Lizano - C18477
        [TestMethod]
        public void usuarioCreador_ValidacionLongitud_DeberiaSerInvalido()
        {
            // Crear con usuario incorrecto de prueba
            var foto = new Fotografia
            {
                fotografia = BitConverter.GetBytes(12345),
                creacion = DateTime.Now,
                // El nombre del creador debe tener entre 5 y 20 caracteres
                usuarioCreador = "Us0*"
            };

            // Establecer condiciones de prueba
            var esValido = Validator.TryValidateProperty(foto.usuarioCreador,
                new ValidationContext(foto) { MemberName = "usuarioCreador" }, null);

            // Revisar condiciones de prueba
            Assert.IsFalse(esValido);
        }
    }
}