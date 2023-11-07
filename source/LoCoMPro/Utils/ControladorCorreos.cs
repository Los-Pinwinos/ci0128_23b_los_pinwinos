using System.Net.Mail;
using System.Net;
using System.Text;

namespace LoCoMPro.Utils
{
    // Clase para el manejo de correos y creación de pin aleatorios
    public class ControladorCorreos
    {
        // Instancia de configuración basada en el archivo "appsettings.json"
        // Se utiliza para establecer el servidor de correos y las credenciales
        // de la cuenta
        private readonly IConfiguration configuracion = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Método para enviar correos
        public bool enviarCorreo(string recibidor, string asunto, string cuerpo, bool html = false)
        {
            // Define un booleano para indicar si el proceso fallo o funcionó
            bool resultado = true;

            // Obtiene las credenciales y servidor del archivo de configuración
            string servidor = this.configuracion.GetValue<string>("serverCorreos") ??
                throw new InvalidOperationException("Servidor de correos no encontrado");
            string enviador = this.configuracion.GetValue<string>("correoEmpresa") ??
                throw new InvalidOperationException("Cuenta de correos no encontrada");

            // Crea un encriptador para desencriptar la contraseña
            Encriptador encriptador = new Encriptador();
            // Obtiene la contraseña de la cuenta de correos desencriptada
            string contrasena = encriptador.desencriptar(
                this.configuracion.GetValue<string>("contrasenaEmpresa") ??
                throw new InvalidOperationException("Contraseña de correo no encontrada"));

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
                    Body = cuerpo,
                    IsBodyHtml = html
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

        // Método para enviar correos con formato HTML
        public bool enviarCorreoHtml(string recibidor, string asunto, string titulo, string cuerpo, string? enlace = null,
            string mensajeBoton = "Presionar")
        {
            string cuerpoHTML = "";

            // Si no se indicó un enlace
            if (string.IsNullOrEmpty(enlace))
            {
                // Construye un cuerpo HTML sin botón
                cuerpoHTML = "<html><body><h1 style=\"color: #033F63;\">" + titulo + "</h1>" +
                "<div style=\"color: black; font-size: 18px;\">" +
                "<p>" + cuerpo + "</p>" +
                "</div></body></html>";
            } else
            {
                // Construye un cuerpo HTML con botón que redirecciones al enlace
                cuerpoHTML = "<html><body><h1 style=\"color: #033F63;\">" + titulo + "</h1>" +
                    "<div style=\"color: #033F63; font-size: 13px;\">" + 
                    "<p>" + cuerpo + "</p>" +
                    "<a href='" + enlace + "'>" +
                        "<button " +
                            "style=\"background-color: #033F63; " +
                            "color: white; " +
                            "width: 240px; " +
                            "height: 25px; " +
                            "border: none; " +
                            "border-radius: 5px; " +
                            "cursor: pointer; " +
                            "font-size: 13px;\">" + mensajeBoton + "</button>" +
                    "</div></a></body></html>";
            }

            // Envía el correo con el cuerpo HTML
            return this.enviarCorreo(recibidor, asunto, cuerpoHTML, true);
        }

        // Método para generar pins aleatorios
        public string generarPin(int longitudPin)
        {
            // Crea un constructor de string para crear el pin
            var constructorPin = new StringBuilder();

            // Crea variables para el caracter a agregar y la posición
            // para agregarlo en cada iteración
            char caracter;
            int posicion;

            // Crea un generador de números pseudoaleatorios utilizando
            // el tiempo actual como semilla
            var generadorAleatorio = new Random();

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
