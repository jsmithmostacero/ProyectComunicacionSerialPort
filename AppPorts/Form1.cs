using AppPort;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AppPorts
{
    public partial class Form1 : Form
    {
        //Objeto tipo SerialPort
        ClassSerialPort puertoX;
        //Variable para OFF/ON Cajas, OFF/ON configuración
        Boolean estado, estadoPanel;
        //Variable almacenamiento de items combox
        String cbxnamePuerto;
        //Variables para atributos de SerialPort
        int baud, data, received;
        //Arreglo de nombres puertos
        String[] namePuertos = new string[20];
        //Arreglo de puertos
        ClassSerialPort[] listPuertos;
        //Historial msj
        string historial = "";
        int cont = 0;
        double acumulador=0;
        Boolean off_on=false;
        private OpenFileDialog openFileDialog;

        private void btnUpdateSaved_Click(object sender, EventArgs e)
        {
            if (!off_on)
            {
                estado = !estado;
                if (estado)
                {
                    btnUpdateSaved.Text = "Guardar";
                }
                else
                {
                    editarCajas();
                    btnUpdateSaved.Text = "Editar";
                }
                activarCajas();
            }
            else
            {
                MessageBox.Show("Debe cerrar el Puerto");            }
            
        }

        private void cbxNamePorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (off_on)
            {
                puertoX.detener();
                off_on = false;
            }
            btnUpdateSaved.Enabled = true;
            cbxnamePuerto = cbxNamePorts.Text;
            puertoX = listPuertos[buscarPuerto()];
            puertoX.LlegoMensaje += new ClassSerialPort.MiManejadorDeEventos(this.puertoC_LlegoMensaje);
            puertoX.LlegoAprobacion += new ClassSerialPort.MiManejadorDeCreacion(this.puertoC_LlegoAprobacion);
            puertoX.LlegoCarpeta += new ClassSerialPort.MiManejadorDeCarpeta(this.puertoC_LlegoCarpeta);
            puertoX.LlegoBarra += new ClassSerialPort.MiManejadorDeBarra(this.puertoC_LlegoBarra);

            llenarCajas();
        }

        public Form1()
        {
            InitializeComponent();
            btnUpdateSaved.Enabled = false;
            estado = false;
            activarCajas();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            actualizarPorts();
        }

        public void activarCajas()
        {
            txtBaud.Enabled = estado;
            txtData.Enabled = estado;
            txtReceived.Enabled = estado;
        }

        public void llenarCajas()
        {
            txtBaud.Text = puertoX.getBaudRate().ToString();
            txtData.Text = puertoX.getDataBits().ToString();
            txtReceived.Text = puertoX.getReceivedBytesThreshold().ToString();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (puertoX.getSerialPort().IsOpen)
                {
                    MessageBox.Show("El puerto ya está inicializado", "Error");
                }
                else
                {
                    puertoX.iniciarConexion();
                    off_on = true;
                    MessageBox.Show("Se inició el puerto " + cbxnamePuerto + " correctamente");

                }
            }
            catch (Exception es)
            {
                MessageBox.Show("No se pudo iniciar : "+es);
            }


        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            puertoX.enviar_send(txtMensaje.Text);
            if (off_on)
            {
              
                historial += "\r\nTu: " + txtMensaje.Text;
                txtHistorial.Text = historial;
                txtMensaje.Text = "";

            }
                
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (off_on)
                {
                    puertoX.detener();
                    off_on = false;
                    MessageBox.Show("Se cerró el puerto " + cbxnamePuerto + " correctamente");
                    txtHistorial.Text = "";
                    historial = "";

                }
                else
                {
                    MessageBox.Show("No hay nada que cerrar");

                }
            }
            catch (Exception es)
            {
                MessageBox.Show("No se pudo cerrar : "+es);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelConfi.Visible = estadoPanel;
            estadoPanel = !estadoPanel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            actualizarPorts();
        }

        public void editarCajas()
        {
            //Almacenamos las cajas en las variables locales
            baud = Int32.Parse(txtBaud.Text);
            data = Int32.Parse(txtData.Text);
            received = Int32.Parse(txtReceived.Text);
            String msj = "Errores";

            //Validar datos
            //1-Validar que el número de DaraBits esté entre 5-8
            if (!(data >= 5 && data <= 8))
            {
                msj += "\nLa propiedad DataBist debe ser un número entre 5-8";
            }

            if (msj.Length == 7)
            {
                puertoX.setBaudRate(baud);
                puertoX.setDataBits(data);
                puertoX.setReceivedBytesThreshold(received);
                listPuertos[buscarPuerto()] = puertoX;
                MessageBox.Show("Se actualizó correctamente.");
                llenarCajas();
            }
            else
            {
                MessageBox.Show(msj);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            txtHistorial.Text = "";
            historial = "";
        }


        public void limpiarBarra()
        {
            //limpiar barra
            txtProcentaje.Text = "0 %";
            txtTamEnviando.Text = "Envío";
            txtTamTotal.Text = "Tamaño";
            progresoBarra.Value = 0;
            cont = 0;
            acumulador = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            limpiarBarra();
            puertoX.confirmarEnvio();

        }

        public int buscarPuerto()
        {
            for (int i = 0; i < namePuertos.Length; i++)
            {
                if (namePuertos[i] == cbxnamePuerto)
                {
                    return i;
                }
            }
            return -1;
        }

        private void puertoC_LlegoMensaje(Object oo, string ss)
        {
            if (this.txtHistorial.InvokeRequired)
            {
                // si no estamos en el subproceso de la UI, usamos BeginInvoke para ejecutar el método en el subproceso de la UI
                this.txtHistorial.BeginInvoke(new Action(() =>
                {
                    historial += "\r\n" + "Stefany: " + ss;
                    this.txtHistorial.Text = historial;
                }));
            }
            else
            {
                // si estamos en el subproceso de la UI, actualizamos directamente el control
                historial += "\r\n" + "Stefany: " + ss;
                this.txtHistorial.Text = historial;
            }
        }

        private void puertoC_LlegoBarra(Object oo,string aa, long ss)
        {
            long tamanio = ss;
            if (InvokeRequired)
            {
                Invoke(new Action(() => puertoC_LlegoBarra(oo,aa,ss)));
                return;
            }

            try
            {
                int fijo = 1004;
                double cantidad = tamanio / fijo;
                //cantidad de veces = 58,7
                //58->100%
                //1 ->x%
                //x = (100)/58
                //x = 1.72
                //x = 3%
                //x = 4 %

                double incremento = 100 / cantidad;
                
                acumulador += incremento;
                cont = cont + 1;
                //cont=1
                if (acumulador>=100)
                {
                    progresoBarra.Value = 100;
                    txtProcentaje.Text = "100 %";
                    txtTamEnviando.Text = aa + " " + tamanio.ToString() + " de " + tamanio.ToString() + "  bytes";

                }
                else
                {
                    txtTamTotal.Text = tamanio.ToString() + " bytes";
                    txtTamEnviando.Text = aa + " " + (fijo * cont).ToString() + " de " + tamanio.ToString() + " bytes";
                    txtProcentaje.Text = ((int)acumulador).ToString() + " %";
                    progresoBarra.Value = (int)acumulador;

                    
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un erro " + ex);
            }

        }

        private void puertoC_LlegoAprobacion(Object oo)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => puertoC_LlegoAprobacion(oo)));
                return;
            }
            
            openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            long size;
            
            // Mostrar el cuadro de diálogo y obtener el resultado
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Obtener el archivo seleccionado
                string selectedFile = openFileDialog.FileName;
                //
                FileStream fileStream = new FileStream(selectedFile, FileMode.Open, FileAccess.Read);
                
                size = fileStream.Length;
                string fileName = Path.GetFileName(selectedFile);

                // Saber en qué carpeta se encuentra el archivo
                string directoryPath = Path.GetDirectoryName(selectedFile);
                // Lógica adicional aquí
                // ...

                //puertoX.enviarSolicitud();
                puertoX.IniciadoEnvioArchivo(fileName, selectedFile);
             
            }
        }

        private void puertoC_LlegoCarpeta(Object oo, int size, int number, string extension)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => puertoC_LlegoCarpeta(oo,size,number,extension)));
                return;
            }
            string ruta = "";
            limpiarBarra();
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // Mostrar el cuadro de diálogo para seleccionar una carpeta

            DialogResult result2 = folderBrowserDialog.ShowDialog();

            if (result2 == DialogResult.OK)
            {
                string carpetaSeleccionada = folderBrowserDialog.SelectedPath;
                //ruta = "C:\\Users\\jmostacero\\Documents\\prueba\\";
                int iterador=0;
                ruta = carpetaSeleccionada + "\\" + "prueba" + number + extension;
                //prueba1.pdf

                if (File.Exists(ruta))
                {
                   // MessageBox.Show("Ya existe :" + ruta);
                    //ya existe copia1
                    string iteradorConBasura;
                    int i = 2;
                    while (File.Exists(ruta))
                    {
                        iteradorConBasura = "prueba" + i + extension;
                        ruta = carpetaSeleccionada + "\\" + iteradorConBasura;
                        iteradorConBasura = "";
                        i++;
                    }
                }
                puertoX.creationToFile(ruta, size, iterador);
                puertoX.activarProcesoEnvio();
            }
            else
            {
                Console.WriteLine("No se seleccionó ninguna carpeta.");
            }
        }



        public void actualizarPorts()
        {
            cbxNamePorts.Items.Clear();
            namePuertos = SerialPort.GetPortNames();
            listPuertos = new ClassSerialPort[namePuertos.Length];
            for (int i = 0; i < namePuertos.Length; i++)
            {
                cbxNamePorts.Items.Add(namePuertos[i]);
                puertoX = new ClassSerialPort(namePuertos[i]);
                listPuertos[i] = puertoX;
            }
        }


    }
}
