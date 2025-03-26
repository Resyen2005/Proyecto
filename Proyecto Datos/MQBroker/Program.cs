using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MQBroker
{
    class MQBroker
    {
        private TcpListener server;
        private SubscriptionList subscriptions;

        public MQBroker(int port)
        {
            server = new TcpListener(IPAddress.Any, port);
            subscriptions = new SubscriptionList();
        }

        public void Start()
        {
            server.Start();
            Console.WriteLine("MQBroker iniciado...");
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                HandleClient(client);
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            string response = ProcessRequest(request);
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            stream.Write(responseBytes, 0, responseBytes.Length);
            client.Close();
        }

        private string ProcessRequest(string request)
        {
            Console.WriteLine("Petición recibida: " + request);
            string[] parts = request.Split('|');
            if (parts.Length < 3)
                return "Formato de petición inválido.";

            string command = parts[0];
            Guid appId;
            if (!Guid.TryParse(parts[1], out appId))
                return "AppID inválido.";

            string topic = parts[2];

            switch (command)
            {
                case "Subscribe":
                    if (subscriptions.Find(appId, topic) != null)
                        return $"El AppID {appId} ya está suscrito al tema {topic}.";
                    subscriptions.Add(appId, topic);
                    return $"Suscrito al tema {topic} con AppID {appId}.";

                case "Unsubscribe":
                    if (subscriptions.Find(appId, topic) == null)
                        return $"El AppID {appId} no está suscrito al tema {topic}.";
                    subscriptions.Remove(appId, topic);
                    return $"Se eliminó la suscripción al tema {topic} para AppID {appId}.";

                case "Publish":
                    if (parts.Length < 4)
                        return "Falta contenido en la publicación.";
                    string content = parts[3];
                    Message msg = new Message(content);
                    // Verificar si existen suscriptores para el tema
                    bool found = false;
                    subscriptions.ForEachSubscriptionByTopic(topic, sub =>
                    {
                        found = true;
                        sub.MessageQueue.Enqueue(msg);
                    });
                    if (!found)
                        return $"No existen suscriptores para el tema {topic}. Publicación ignorada.";
                    return "Mensaje publicado.";

                case "Receive":
                    Subscription subFound = subscriptions.Find(appId, topic);
                    if (subFound == null)
                        return $"El AppID {appId} no está suscrito al tema {topic}.";
                    if (subFound.MessageQueue.IsEmpty())
                        return "No hay mensajes en la cola.";
                    Message receivedMsg = subFound.MessageQueue.Dequeue();
                    return receivedMsg.Content;

                default:
                    return "Comando inválido.";
            }
        }

        static void Main()
        {
            MQBroker broker = new MQBroker(5000);
            broker.Start();
        }
    }

    // Cola personalizada (FIFO)
    class CustomQueue<T>
    {
        private class Node
        {
            public T Data;
            public Node Next;
            public Node(T data) { Data = data; Next = null; }
        }

        private Node head, tail;

        public void Enqueue(T item)
        {
            Node newNode = new Node(item);
            if (tail != null)
                tail.Next = newNode;
            tail = newNode;
            if (head == null)
                head = tail;
        }

        public T Dequeue()
        {
            if (head == null)
                throw new InvalidOperationException("La cola está vacía");
            T value = head.Data;
            head = head.Next;
            if (head == null)
                tail = null;
            return value;
        }

        public bool IsEmpty()
        {
            return head == null;
        }
    }

    // Clase que representa una suscripción con su cola exclusiva
    class Subscription
    {
        public Guid AppId;
        public string Topic;
        public CustomQueue<Message> MessageQueue;

        public Subscription(Guid appId, string topic)
        {
            AppId = appId;
            Topic = topic;
            MessageQueue = new CustomQueue<Message>();
        }
    }

    // Lista enlazada personalizada para manejar las suscripciones
    class SubscriptionList
    {
        private class SubscriptionNode
        {
            public Subscription Data;
            public SubscriptionNode Next;
            public SubscriptionNode(Subscription subscription)
            {
                Data = subscription;
                Next = null;
            }
        }

        private SubscriptionNode head;

        // Agregar una nueva suscripción (si no existe)
        public void Add(Guid appId, string topic)
        {
            Subscription newSub = new Subscription(appId, topic);
            SubscriptionNode newNode = new SubscriptionNode(newSub);
            newNode.Next = head;
            head = newNode;
        }

        // Remover una suscripción en base a AppId y topic
        public void Remove(Guid appId, string topic)
        {
            SubscriptionNode current = head, prev = null;
            while (current != null)
            {
                if (current.Data.AppId == appId && current.Data.Topic == topic)
                {
                    if (prev == null)
                        head = current.Next;
                    else
                        prev.Next = current.Next;
                    return;
                }
                prev = current;
                current = current.Next;
            }
        }

        // Buscar una suscripción por AppId y topic
        public Subscription Find(Guid appId, string topic)
        {
            SubscriptionNode current = head;
            while (current != null)
            {
                if (current.Data.AppId == appId && current.Data.Topic == topic)
                    return current.Data;
                current = current.Next;
            }
            return null;
        }

        // Ejecuta una acción para cada suscripción que coincida con el tema
        public void ForEachSubscriptionByTopic(string topic, Action<Subscription> action)
        {
            SubscriptionNode current = head;
            while (current != null)
            {
                if (current.Data.Topic == topic)
                    action(current.Data);
                current = current.Next;
            }
        }
    }

    // Clase que representa un mensaje
    class Message
    {
        public string Content;
        public Message(string content) { Content = content; }
    }
}