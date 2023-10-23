using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace LoCoMPro.Utils
{
    // Clase para el encriptado de strings
    public class Encriptador
    {
        // Instancia de configuración basada en el archivo "appsettings.json"
        // Se utiliza para establecer la llave
        private readonly IConfiguration configuracion = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Llave por defecto utilizada al no encontrarse la llave en el archivo
        // de configuración
        private readonly string llavePorDefecto = "LlaveParaEncriptarNoMuySeguraxDD";

        // Método para encriptar hileras
        public string encriptar(string hilera)
        {
            // Crear un objeto para el algoritmo de encriptado
            using (Aes algoritmoEncriptado = Aes.Create())
            {
                // Obtener la llave de encriptado para el algoritmo a partir
                // del archivo de configuración, de no encontrarla, usar la
                // llave por defecto
                algoritmoEncriptado.Key = Encoding.UTF8.GetBytes(
                    this.configuracion.GetValue<string>("llaveEncriptado") ?? this.llavePorDefecto);
                // Generar IV aleatoria para mayor seguridad
                algoritmoEncriptado.GenerateIV();

                // Obtiene la hilera a encriptar como bytes
                byte[] hileraBytes = Encoding.UTF8.GetBytes(hilera);

                // Crear un objeto encriptor a partir del algoritmo de encriptado
                using (ICryptoTransform encriptor = algoritmoEncriptado.CreateEncryptor())
                {
                    // Encripta los bytes con el encriptor
                    byte[] encriptado = encriptor.TransformFinalBlock(hileraBytes, 0, hileraBytes.Length);
                    // Concatena el IV y el encriptado para mayor seguridad
                    byte[] encriptadoConIV = algoritmoEncriptado.IV.Concat(encriptado).ToArray();

                    // Retorna un string con la encripción generada
                    return Convert.ToBase64String(encriptadoConIV);
                }
            }
        }

        // Método para desencriptar hileras
        public string desencriptar(string hilera)
        {
            // Intenta interpretar la hilera como string de base 64 y desencriptarla
            try
            {
                // Corregir los errores de traducción en la hilera
                hilera = hilera.Replace(' ', '+');
                // Crear un objeto para el algoritmo de encriptado
                using (Aes algoritmoEncriptado = Aes.Create())
                {
                    // Obtener la llave de encriptado para el algoritmo a partir
                    // del archivo de configuración, de no encontrarla, usar la
                    // llave por defecto
                    algoritmoEncriptado.Key = Encoding.UTF8.GetBytes(
                        this.configuracion.GetValue<string>("llaveEncriptado") ?? this.llavePorDefecto);

         
                        // Obtener la hilera a desencriptar como bytes
                        byte[] encriptadoConIV = Convert.FromBase64String(hilera);

                        // Obtener el IV y hilera encriptada
                        algoritmoEncriptado.IV = encriptadoConIV.Take(16).ToArray();
                        byte[] encriptado = encriptadoConIV.Skip(16).ToArray();

                        // Crear un objeto desencriptor a partir del algoritmo de encriptado
                        using (ICryptoTransform desencriptor = algoritmoEncriptado.CreateDecryptor())
                        {
                            // Desencripta los bytes con el desencriptor
                            byte[] desencriptado = desencriptor.TransformFinalBlock(encriptado, 0, encriptado.Length);

                            // Retorna un string con la desencripción generada
                            return Encoding.UTF8.GetString(desencriptado);
                        }
                    }
                }

            // Si la hilera era inválida
            catch (Exception)
            {
                // Retorna string vacío
                return "";
            }
        }
    }
}
