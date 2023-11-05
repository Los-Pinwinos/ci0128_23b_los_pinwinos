﻿using LoCoMPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoCoMProTests
{
    public class CreadorDeModelos
    {
        public static Usuario CrearUsuarioPorDefecto()
        {
            return new Usuario { nombreDeUsuario = "Usuario1", correo = "usuario1@gmail.com", hashContrasena = "hashContrasena" };
        }
        public static Provincia CrearProvinciaPorDefecto()
        {
            return new Provincia { nombre = "Provincia" };
        }

        public static Canton CrearCantonPorDefecto(Provincia? provinciaAsociada = null)
        {
            provinciaAsociada = provinciaAsociada ?? CrearProvinciaPorDefecto();
            return new Canton { nombre = "Canton", nombreProvincia = provinciaAsociada.nombre, provincia = provinciaAsociada };
        }

        public static Distrito CrearDistritoPorDefecto(Canton? cantonAsociado = null)
        {
            cantonAsociado = cantonAsociado ?? CrearCantonPorDefecto();
            return new Distrito { nombre = "Distrito", nombreCanton = cantonAsociado.nombre, nombreProvincia = cantonAsociado.nombreProvincia, canton = cantonAsociado };
        }

        public static Tienda CrearTiendaPorDefecto(Distrito? distritoAsociado = null)
        {
            distritoAsociado = distritoAsociado ?? CrearDistritoPorDefecto();
            return new Tienda { nombre = "Tienda", nombreDistrito = distritoAsociado.nombre, nombreCanton = distritoAsociado.nombreCanton, nombreProvincia = distritoAsociado.nombreProvincia, distrito = distritoAsociado };
        }

        public static Categoria CrearCategoriaPorDefecto()
        {
            return new Categoria { nombre = "Categoria" };
        }
        public static Unidad CrearUnidadPorDefecto()
        {
            return new Unidad { nombre = "Unidad" };
        }
        public static Producto CrearProductoPorDefecto(Categoria? categoriaAsociada = null, Unidad? unidadAsociada = null)
        {
            categoriaAsociada = categoriaAsociada ?? CrearCategoriaPorDefecto();
            unidadAsociada = unidadAsociada ?? CrearUnidadPorDefecto();
            return new Producto { nombre = "Producto", nombreCategoria = categoriaAsociada.nombre, nombreUnidad = unidadAsociada.nombre, categoria = categoriaAsociada, unidad = unidadAsociada };
        }

        public static Registro CrearRegistroPorDefecto(Producto? productoAsociado = null, Tienda? tiendaAsociada = null, Usuario? usuarioAsociado = null)
        {
            productoAsociado = productoAsociado ?? CrearProductoPorDefecto();
            tiendaAsociada = tiendaAsociada ?? CrearTiendaPorDefecto();
            usuarioAsociado = usuarioAsociado ?? CrearUsuarioPorDefecto();
            return new Registro { creacion = DateTime.Now, productoAsociado = productoAsociado.nombre, usuarioCreador = usuarioAsociado.nombreDeUsuario, precio = 999.99M, nombreTienda = tiendaAsociada.nombre, nombreDistrito = tiendaAsociada.nombreDistrito, nombreCanton = tiendaAsociada.nombreCanton, nombreProvincia = tiendaAsociada.nombreProvincia, tienda = tiendaAsociada, producto = productoAsociado, creador = usuarioAsociado };
        }
    }
}
