using System;
using System.Windows.Forms;
using MQClientLibrary;
namespace Proyecto_Datos

{
    public partial class Form1 : Form
    {
        private MQClient client;

        public Form1()
        {
            InitializeComponent();
        }

        // Evento para inicializar el MQClient cuando se haga clic, por ejemplo, en un bot�n
        // o cuando desees crear la conexi�n.
        // Si deseas que se cree autom�ticamente cuando arranca la app, podr�as hacerlo en el Load del formulario.
        private void btnCrearCliente_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Convertir el texto de AppID a Guid
                Guid appId = Guid.Parse(txtAppID.Text);

                // Crear el cliente con los datos ingresados
                client = new MQClient(txtBrokerIP.Text, int.Parse(txtBrokerPort.Text), appId);

                MessageBox.Show("Cliente MQ creado exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el cliente MQ: " + ex.Message);
            }
        }

        private void btnSuscribirse_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (client == null)
                {
                    MessageBox.Show("Primero crea el cliente MQ (faltan IP, Puerto o AppID).");
                    return;
                }

                // Crear un objeto Topic con el texto ingresado en txtTema
                Topic topic = new Topic(txtTema.Text);

                // Llamar al m�todo Subscribe
                bool result = client.Subscribe(topic);

                if (result)
                {
                    MessageBox.Show("Suscripci�n exitosa al tema: " + topic.Name);
                }
                else
                {
                    MessageBox.Show("Ya estabas suscrito a este tema.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en suscripci�n: " + ex.Message);
            }
        }

        private void btnDesuscribirse_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (client == null)
                {
                    MessageBox.Show("Primero crea el cliente MQ (faltan IP, Puerto o AppID).");
                    return;
                }

                Topic topic = new Topic(txtTema.Text);

                bool result = client.Unsubscribe(topic);

                if (result)
                {
                    MessageBox.Show("Desuscripci�n exitosa del tema: " + topic.Name);
                }
                else
                {
                    MessageBox.Show("No estabas suscrito a este tema.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al desuscribirse: " + ex.Message);
            }
        }

        private void btnPublicar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (client == null)
                {
                    MessageBox.Show("Primero crea el cliente MQ (faltan IP, Puerto o AppID).");
                    return;
                }

                Topic topic = new Topic(txtTema.Text);
                MQClientLibrary.Message message = new MQClientLibrary.Message(txtContenidoPublicacion.Text);

                bool result = client.Publish(message, topic);

                if (result)
                {
                    MessageBox.Show("Mensaje publicado exitosamente.");
                }
                else
                {
                    MessageBox.Show("No existen suscriptores para este tema. Publicaci�n ignorada.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al publicar: " + ex.Message);
            }
        }

        private void btnObtenerMensaje_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (client == null)
                {
                    MessageBox.Show("Primero crea el cliente MQ (faltan IP, Puerto o AppID).");
                    return;
                }

                Topic topic = new Topic(txtTema.Text);

                // Llamamos a Receive para obtener un mensaje
                MQClientLibrary.Message receivedMsg = client.Receive(topic);

                // Agregamos el mensaje recibido al DataGridView
                // Asumiendo que dgvMensajes tiene dos columnas: "Tema" y "Contenido"
                dgvMensajes.Rows.Add(topic.Name, receivedMsg.Content);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al recibir mensaje: " + ex.Message);
            }
        }



    }
}
