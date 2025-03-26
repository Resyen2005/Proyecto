namespace Proyecto_Datos
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSuscribirse = new Button();
            btnDesuscribirse = new Button();
            btnPublicar = new Button();
            btnObtenerMensaje = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtBrokerIP = new TextBox();
            txtBrokerPort = new TextBox();
            txtAppID = new TextBox();
            txtTema = new TextBox();
            txtContenidoPublicacion = new TextBox();
            dgvMensajes = new DataGridView();
            colTema = new DataGridViewTextBoxColumn();
            colContenido = new DataGridViewTextBoxColumn();
            btnCrearCliente = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMensajes).BeginInit();
            SuspendLayout();
            // 
            // btnSuscribirse
            // 
            btnSuscribirse.Location = new Point(65, 278);
            btnSuscribirse.Name = "btnSuscribirse";
            btnSuscribirse.Size = new Size(75, 23);
            btnSuscribirse.TabIndex = 0;
            btnSuscribirse.Text = "Suscribirse ";
            btnSuscribirse.UseVisualStyleBackColor = true;
            btnSuscribirse.Click += btnSuscribirse_Click_1;
            // 
            // btnDesuscribirse
            // 
            btnDesuscribirse.Location = new Point(194, 278);
            btnDesuscribirse.Name = "btnDesuscribirse";
            btnDesuscribirse.Size = new Size(84, 23);
            btnDesuscribirse.TabIndex = 1;
            btnDesuscribirse.Text = "Desuscribirse ";
            btnDesuscribirse.UseVisualStyleBackColor = true;
            btnDesuscribirse.Click += btnDesuscribirse_Click_1;
            // 
            // btnPublicar
            // 
            btnPublicar.Location = new Point(390, 278);
            btnPublicar.Name = "btnPublicar";
            btnPublicar.Size = new Size(75, 23);
            btnPublicar.TabIndex = 2;
            btnPublicar.Text = "Publicar ";
            btnPublicar.UseVisualStyleBackColor = true;
            btnPublicar.Click += btnPublicar_Click_1;
            // 
            // btnObtenerMensaje
            // 
            btnObtenerMensaje.Location = new Point(646, 278);
            btnObtenerMensaje.Name = "btnObtenerMensaje";
            btnObtenerMensaje.Size = new Size(124, 23);
            btnObtenerMensaje.TabIndex = 3;
            btnObtenerMensaje.Text = "Obtener Mensajes";
            btnObtenerMensaje.UseVisualStyleBackColor = true;
            btnObtenerMensaje.Click += btnObtenerMensaje_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 47);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 4;
            label1.Text = "MQ Broker IP";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 89);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 5;
            label2.Text = "MQ Broker Port";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 129);
            label3.Name = "label3";
            label3.Size = new Size(40, 15);
            label3.TabIndex = 6;
            label3.Text = "AppID";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 173);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 7;
            label4.Text = "Tema ";
            // 
            // txtBrokerIP
            // 
            txtBrokerIP.Location = new Point(107, 47);
            txtBrokerIP.Name = "txtBrokerIP";
            txtBrokerIP.Size = new Size(100, 23);
            txtBrokerIP.TabIndex = 8;
            
            // 
            // txtBrokerPort
            // 
            txtBrokerPort.Location = new Point(107, 89);
            txtBrokerPort.Name = "txtBrokerPort";
            txtBrokerPort.Size = new Size(100, 23);
            txtBrokerPort.TabIndex = 9;
            
            // 
            // txtAppID
            // 
            txtAppID.Location = new Point(107, 129);
            txtAppID.Name = "txtAppID";
            txtAppID.Size = new Size(100, 23);
            txtAppID.TabIndex = 10;
            
            // 
            // txtTema
            // 
            txtTema.Location = new Point(107, 173);
            txtTema.Name = "txtTema";
            txtTema.Size = new Size(100, 23);
            txtTema.TabIndex = 11;
            
            // 
            // txtContenidoPublicacion
            // 
            txtContenidoPublicacion.Location = new Point(344, 89);
            txtContenidoPublicacion.Multiline = true;
            txtContenidoPublicacion.Name = "txtContenidoPublicacion";
            txtContenidoPublicacion.Size = new Size(173, 128);
            txtContenidoPublicacion.TabIndex = 12;
            
            // 
            // dgvMensajes
            // 
            dgvMensajes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMensajes.Columns.AddRange(new DataGridViewColumn[] { colTema, colContenido });
            dgvMensajes.Location = new Point(544, 89);
            dgvMensajes.Name = "dgvMensajes";
            dgvMensajes.Size = new Size(244, 166);
            dgvMensajes.TabIndex = 13;
            
            // 
            // colTema
            // 
            colTema.HeaderText = "TEMA";
            colTema.Name = "colTema";
            // 
            // colContenido
            // 
            colContenido.HeaderText = "CONTENIDO";
            colContenido.Name = "colContenido";
            // 
            // btnCrearCliente
            // 
            btnCrearCliente.Location = new Point(238, 43);
            btnCrearCliente.Name = "btnCrearCliente";
            btnCrearCliente.Size = new Size(88, 23);
            btnCrearCliente.TabIndex = 14;
            btnCrearCliente.Text = "CrearCliente";
            btnCrearCliente.UseVisualStyleBackColor = true;
            btnCrearCliente.Click += btnCrearCliente_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCrearCliente);
            Controls.Add(dgvMensajes);
            Controls.Add(txtContenidoPublicacion);
            Controls.Add(txtTema);
            Controls.Add(txtAppID);
            Controls.Add(txtBrokerPort);
            Controls.Add(txtBrokerIP);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnObtenerMensaje);
            Controls.Add(btnPublicar);
            Controls.Add(btnDesuscribirse);
            Controls.Add(btnSuscribirse);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvMensajes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSuscribirse;
        private Button btnDesuscribirse;
        private Button btnPublicar;
        private Button btnObtenerMensaje;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtBrokerIP;
        private TextBox txtBrokerPort;
        private TextBox txtAppID;
        private TextBox txtTema;
        private TextBox txtContenidoPublicacion;
        private DataGridView dgvMensajes;
        private DataGridViewTextBoxColumn colTema;
        private DataGridViewTextBoxColumn colContenido;
        private Button btnCrearCliente;
    }
}
