namespace AppPorts
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtReceived = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBaud = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxNamePorts = new System.Windows.Forms.ComboBox();
            this.btnUpdateSaved = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panelConfi = new System.Windows.Forms.Panel();
            this.btnDetener = new System.Windows.Forms.Button();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtProcentaje = new System.Windows.Forms.Label();
            this.txtTamEnviando = new System.Windows.Forms.Label();
            this.txtTamTotal = new System.Windows.Forms.Label();
            this.progresoBarra = new System.Windows.Forms.ProgressBar();
            this.btnCrearArchivo = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHistorial = new System.Windows.Forms.TextBox();
            this.panelConfi.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtReceived
            // 
            this.txtReceived.Location = new System.Drawing.Point(81, 137);
            this.txtReceived.Name = "txtReceived";
            this.txtReceived.Size = new System.Drawing.Size(53, 20);
            this.txtReceived.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "RecBtsThres";
            // 
            // txtBaud
            // 
            this.txtBaud.Location = new System.Drawing.Point(81, 111);
            this.txtBaud.Name = "txtBaud";
            this.txtBaud.Size = new System.Drawing.Size(53, 20);
            this.txtBaud.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "BaudRate";
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(81, 82);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(53, 20);
            this.txtData.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = " DataBits";
            // 
            // cbxNamePorts
            // 
            this.cbxNamePorts.FormattingEnabled = true;
            this.cbxNamePorts.Location = new System.Drawing.Point(59, 44);
            this.cbxNamePorts.Name = "cbxNamePorts";
            this.cbxNamePorts.Size = new System.Drawing.Size(75, 21);
            this.cbxNamePorts.TabIndex = 16;
            this.cbxNamePorts.Text = "<Ports>";
            this.cbxNamePorts.SelectedIndexChanged += new System.EventHandler(this.cbxNamePorts_SelectedIndexChanged);
            // 
            // btnUpdateSaved
            // 
            this.btnUpdateSaved.Location = new System.Drawing.Point(10, 169);
            this.btnUpdateSaved.Name = "btnUpdateSaved";
            this.btnUpdateSaved.Size = new System.Drawing.Size(124, 23);
            this.btnUpdateSaved.TabIndex = 23;
            this.btnUpdateSaved.Text = "Editar";
            this.btnUpdateSaved.UseVisualStyleBackColor = true;
            this.btnUpdateSaved.Click += new System.EventHandler(this.btnUpdateSaved_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(8, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 64);
            this.button1.TabIndex = 24;
            this.button1.Text = " ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelConfi
            // 
            this.panelConfi.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelConfi.Controls.Add(this.btnDetener);
            this.panelConfi.Controls.Add(this.btnIniciar);
            this.panelConfi.Controls.Add(this.button2);
            this.panelConfi.Controls.Add(this.label1);
            this.panelConfi.Controls.Add(this.cbxNamePorts);
            this.panelConfi.Controls.Add(this.label2);
            this.panelConfi.Controls.Add(this.txtReceived);
            this.panelConfi.Controls.Add(this.btnUpdateSaved);
            this.panelConfi.Controls.Add(this.txtBaud);
            this.panelConfi.Controls.Add(this.label3);
            this.panelConfi.Controls.Add(this.txtData);
            this.panelConfi.Location = new System.Drawing.Point(8, 94);
            this.panelConfi.Name = "panelConfi";
            this.panelConfi.Size = new System.Drawing.Size(156, 280);
            this.panelConfi.TabIndex = 25;
            // 
            // btnDetener
            // 
            this.btnDetener.Location = new System.Drawing.Point(10, 211);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(56, 23);
            this.btnDetener.TabIndex = 26;
            this.btnDetener.Text = " Detener";
            this.btnDetener.UseVisualStyleBackColor = true;
            this.btnDetener.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(72, 211);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(62, 23);
            this.btnIniciar.TabIndex = 25;
            this.btnIniciar.Text = " Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(59, 38);
            this.button2.TabIndex = 24;
            this.button2.Text = " ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.txtProcentaje);
            this.panel1.Controls.Add(this.txtTamEnviando);
            this.panel1.Controls.Add(this.txtTamTotal);
            this.panel1.Controls.Add(this.progresoBarra);
            this.panel1.Controls.Add(this.btnCrearArchivo);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtMensaje);
            this.panel1.Controls.Add(this.btnEnviar);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtHistorial);
            this.panel1.Location = new System.Drawing.Point(177, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 464);
            this.panel1.TabIndex = 26;
            // 
            // txtProcentaje
            // 
            this.txtProcentaje.AutoSize = true;
            this.txtProcentaje.Location = new System.Drawing.Point(192, 384);
            this.txtProcentaje.Name = "txtProcentaje";
            this.txtProcentaje.Size = new System.Drawing.Size(15, 13);
            this.txtProcentaje.TabIndex = 33;
            this.txtProcentaje.Text = "%";
            // 
            // txtTamEnviando
            // 
            this.txtTamEnviando.AutoSize = true;
            this.txtTamEnviando.Location = new System.Drawing.Point(52, 426);
            this.txtTamEnviando.Name = "txtTamEnviando";
            this.txtTamEnviando.Size = new System.Drawing.Size(92, 13);
            this.txtTamEnviando.TabIndex = 32;
            this.txtTamEnviando.Text = "Cargando.............";
            // 
            // txtTamTotal
            // 
            this.txtTamTotal.AutoSize = true;
            this.txtTamTotal.Location = new System.Drawing.Point(21, 384);
            this.txtTamTotal.Name = "txtTamTotal";
            this.txtTamTotal.Size = new System.Drawing.Size(101, 13);
            this.txtTamTotal.TabIndex = 31;
            this.txtTamTotal.Text = "Tamaño del archivo";
            // 
            // progresoBarra
            // 
            this.progresoBarra.Location = new System.Drawing.Point(24, 400);
            this.progresoBarra.Name = "progresoBarra";
            this.progresoBarra.Size = new System.Drawing.Size(209, 23);
            this.progresoBarra.TabIndex = 29;
            // 
            // btnCrearArchivo
            // 
            this.btnCrearArchivo.Location = new System.Drawing.Point(30, 303);
            this.btnCrearArchivo.Name = "btnCrearArchivo";
            this.btnCrearArchivo.Size = new System.Drawing.Size(146, 33);
            this.btnCrearArchivo.TabIndex = 28;
            this.btnCrearArchivo.Text = "Enviar archivo";
            this.btnCrearArchivo.UseVisualStyleBackColor = true;
            this.btnCrearArchivo.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(30, 223);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(197, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Eliminar Historial";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(51, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(154, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = " Historial de chats";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(27, 252);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Escribir un mensaje";
            // 
            // txtMensaje
            // 
            this.txtMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMensaje.Location = new System.Drawing.Point(30, 275);
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.Size = new System.Drawing.Size(203, 22);
            this.txtMensaje.TabIndex = 5;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Image = ((System.Drawing.Image)(resources.GetObject("btnEnviar.Image")));
            this.btnEnviar.Location = new System.Drawing.Point(182, 303);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(62, 64);
            this.btnEnviar.TabIndex = 4;
            this.btnEnviar.Text = " ";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(179, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "User: Jonathan";
            // 
            // txtHistorial
            // 
            this.txtHistorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHistorial.Location = new System.Drawing.Point(30, 61);
            this.txtHistorial.Multiline = true;
            this.txtHistorial.Name = "txtHistorial";
            this.txtHistorial.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHistorial.Size = new System.Drawing.Size(203, 156);
            this.txtHistorial.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(450, 506);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelConfi);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = " Interfaz de Jonathan";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelConfi.ResumeLayout(false);
            this.panelConfi.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtReceived;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBaud;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxNamePorts;
        private System.Windows.Forms.Button btnUpdateSaved;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelConfi;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtHistorial;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMensaje;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnDetener;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnCrearArchivo;
        private System.Windows.Forms.ProgressBar progresoBarra;
        private System.Windows.Forms.Label txtTamEnviando;
        private System.Windows.Forms.Label txtTamTotal;
        private System.Windows.Forms.Label txtProcentaje;
        //private Form1 f1 = new Form1();


    }
}

