using System;
using System.Net.Sockets;
using System.Text;
namespace MQClientLibrary
{
    public class MQClient
    {
        private readonly string serverIp;
        private readonly int serverPort;
        private readonly Guid appId;

        /// <summary>
        /// Constructor que inicia el cliente para comunicarse con el MQBroker.
        /// </summary>
        /// <param name="ip">IP del broker</param>
        /// <param name="port">Puerto del broker</param>
        /// <param name="appId">AppID generado por la aplicación cliente</param>
        public MQClient(string ip, int port, Guid appId)
        {
            if (string.IsNullOrWhiteSpace(ip))
                throw new ArgumentException("La IP no puede ser nula o vacía.", nameof(ip));

            serverIp = ip;
            serverPort = port;
            this.appId = appId;
        }

        /// <summary>
        /// Envía la petición al MQBroker y retorna la respuesta.
        /// </summary>
        /// <param name="request">Petición en formato string</param>
        /// <returns>Respuesta del broker</returns>
        private string SendRequest(string request)
        {
            try
            {
                using (TcpClient client = new TcpClient(serverIp, serverPort))
                {
                    NetworkStream stream = client.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes(request);
                    stream.Write(data, 0, data.Length);

                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    return Encoding.UTF8.GetString(buffer, 0, bytesRead);
                }
            }
            catch (SocketException ex)
            {
                throw new Exception("Error al conectarse con el MQBroker.", ex);
            }
        }

        /// <summary>
        /// Realiza la suscripción al tema indicado.
        /// </summary>
        /// <param name="topic">Objeto Topic que encapsula el nombre del tema</param>
        /// <returns>true si se suscribió correctamente, false si ya estaba suscrito</returns>
        public bool Subscribe(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            string request = $"Subscribe|{appId}|{topic.Name}";
            string response = SendRequest(request);

            if (response.Contains("Suscrito al tema"))
                return true;
            else if (response.Contains("ya está suscrito"))
                return false;
            else
                throw new Exception("Error en la suscripción: " + response);
        }

        /// <summary>
        /// Realiza la desuscripción del tema indicado.
        /// </summary>
        /// <param name="topic">Objeto Topic que encapsula el nombre del tema</param>
        /// <returns>true si se desuscribió correctamente, false si no estaba suscrito</returns>
        public bool Unsubscribe(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            string request = $"Unsubscribe|{appId}|{topic.Name}";
            string response = SendRequest(request);

            if (response.Contains("Se eliminó la suscripción"))
                return true;
            else if (response.Contains("no está suscrito"))
                return false;
            else
                throw new Exception("Error al desuscribir: " + response);
        }

        /// <summary>
        /// Publica un mensaje en el tema indicado.
        /// </summary>
        /// <param name="message">Objeto Message que encapsula el contenido</param>
        /// <param name="topic">Objeto Topic que encapsula el nombre del tema</param>
        /// <returns>true si se publicó correctamente, false si no existen suscriptores</returns>
        public bool Publish(Message message, Topic topic)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            string request = $"Publish|{appId}|{topic.Name}|{message.Content}";
            string response = SendRequest(request);

            if (response.Contains("Mensaje publicado"))
                return true;
            else if (response.Contains("No existen suscriptores"))
                return false;
            else
                throw new Exception("Error al publicar: " + response);
        }

        /// <summary>
        /// Recibe un mensaje de la cola correspondiente al tema indicado.
        /// </summary>
        /// <param name="topic">Objeto Topic que encapsula el nombre del tema</param>
        /// <returns>Objeto Message con el contenido del mensaje recibido</returns>
        public Message Receive(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            string request = $"Receive|{appId}|{topic.Name}";
            string response = SendRequest(request);

            if (response.Contains("no está suscrito"))
                throw new Exception($"El AppID {appId} no está suscrito al tema {topic.Name}.");
            else if (response.Contains("No hay mensajes"))
                throw new Exception("No hay mensajes en la cola.");
            else
            {
                // La respuesta es el contenido del mensaje.
                return new Message(response);
            }
        }
    }

    /// <summary>
    /// Clase que encapsula la información de un tema.
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// Nombre del tema.
        /// </summary>
        public string Name { get; private set; }

        public Topic(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del tema no puede ser vacío.", nameof(name));
            // Se pueden agregar validaciones o transformaciones adicionales.
            Name = name;
        }

        public override string ToString() => Name;
    }

    /// <summary>
    /// Clase que encapsula el contenido de un mensaje.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Contenido del mensaje.
        /// </summary>
        public string Content { get; set; }

        public Message(string content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));
            Content = content;
        }
    }
}
