using System.Net.Mail;
using System.Net;
using System.Text;

namespace LoCoMPro.Utils
{
    // Clase para el manejo de correos y creación de pin aleatorios
    public class ControladorCorreos
    {
        // Instancia de configuración basada en el archivo "appsettings.json"
        // Se utiliza para establecer el serviodr de correos y las credenciales
        // de la cuenta
        private readonly IConfiguration configuracion = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Método para enviar correos
        // TODO(Kenneth): Volverlo más restrictivo
        public bool enviarCorreo(string recibidor, string asunto, string cuerpo)
        {
            // Define un booleano para indicar si el proceso fallo o funcionó
            bool resultado = true;

            // Obtiene las credenciales y servidor del archivo de configuración
            string? servidor = configuracion.GetValue<string>("serverCorreos");
            string? enviador = configuracion.GetValue<string>("correoEmpresa");
            string? contrasena = configuracion.GetValue<string>("contrasenaEmpresa");

            // Si las credenciales y servidor son válidas
            if (servidor != null && enviador != null && contrasena != null)
            {
                // Se conecta al servidor
                var cliente = new SmtpClient(servidor)
                {
                    Port = 587,
                    Credentials = new NetworkCredential(enviador, contrasena),
                    EnableSsl = true,
                };

                // Crea el correo
                var correo = new MailMessage
                {
                    From = new MailAddress(enviador),
                    Subject = asunto,
                    Body = cuerpo
                };
                correo.To.Add(recibidor);

                try
                {
                    // Envia el correo
                    cliente.Send(correo);
                }
                catch (Exception)
                {
                    // Actualiza el booleano para indicar que falló
                    resultado = false;
                }
            }
            // Si no existían credenciales o servidor válidos en el archivo de configuración
            else
            {
                // Actualiza el booleano para indicar que falló
                resultado = false;
            }

            // Retorna el booleano
            return resultado;
        }

        // Método para generar pins aleatorios
        public string generarPin(int longitudPin)
        {
            // Crea un constructor de string para crear el pin
            StringBuilder constructorPin = new StringBuilder();

            // Crea variables para el caracter a agregar y la posición
            // para agregarlo en cada iteración
            char caracter = '-';
            int posicion = 0;

            // Crea un generador de números pseudoaleatorios utilizando
            // el tiempo actual como semilla
            Random generadorAleatorio = new Random();

            // Ciclo para agregar todos los caracteres
            for (int limiteActual = 0; limiteActual < longitudPin; ++limiteActual)
            {
                // Obtiene una posición aleatoria entre las disponibles
                posicion = generadorAleatorio.Next(0, limiteActual);
                // Si es par, genere una letra mayuscula aleatoria, si es impar, genere un digito
                caracter = (posicion % 2 == 0 ?
                    (char)generadorAleatorio.Next(65, 90) : (char)generadorAleatorio.Next(48, 57));
                // Inserta el caracter aleatorio en la posición aleatoria
                constructorPin.Insert(posicion, caracter);
            }

            // Retorna el pin construído
            return constructorPin.ToString();
        }
    }
}
