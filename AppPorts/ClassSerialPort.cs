using AppPorts;
using System;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AppPort
{
    internal class ClassSerialPort
    {
        SerialPort serialPort;
        public delegate void MiManejadorDeEventos(object oo, String ss);
        public event MiManejadorDeEventos LlegoMensaje;

        public delegate void MiManejadorDeCreacion(object oo);
        public event MiManejadorDeCreacion LlegoAprobacion;


        public delegate void MiManejadorDeCarpeta(object oo, int s1, int s2,string extension);
        public event MiManejadorDeCarpeta LlegoCarpeta;

        public delegate void MiManejadorDeBarra(object oo,string aa, long g);
        public event MiManejadorDeBarra LlegoBarra;

        string mensajeRecibido;
        string mensajeEnviado;
        byte[] arregloTramaEnvio;
        byte[] tramaCabecera;
        byte[] arregloTramaRelleno;
        byte[] tramaRecibida;
        Boolean bufferExitEmpty;

        private FileStream flowFileToSend;
        private BinaryReader readingFile;
        private ClassFile fileSend;

        private FileStream flowFileToReption;
        private BinaryWriter writeingFile;
        private ClassFile fileReption;

        Thread processSend;
        Thread processVerifyExit;
        Thread processSendFile;

        public ClassSerialPort(String namePort)
        {
            serialPort = new SerialPort();
            serialPort.PortName = namePort;
            serialPort.DataBits = 8;
            serialPort.BaudRate = 115200;

            serialPort.ReceivedBytesThreshold = 1024;
            arregloTramaEnvio = new byte[1024];
            arregloTramaRelleno = new byte[1024];
            tramaRecibida = new byte[1024];
            tramaCabecera = new byte[20];

            for (int i = 0; i < 1024; i++)
                arregloTramaRelleno[i] = 64;

        }
        public ClassSerialPort(String PortName, int DataBits, int BaudRate, int ReceivedBytesThreshold)
        {
            serialPort = new SerialPort();
            serialPort.PortName = PortName;
            serialPort.DataBits = DataBits;
            serialPort.BaudRate = BaudRate;

            serialPort.ReceivedBytesThreshold = ReceivedBytesThreshold;
            arregloTramaEnvio = new byte[getReceivedBytesThreshold()];
            arregloTramaRelleno = new byte[getReceivedBytesThreshold()];
            tramaRecibida = new byte[getReceivedBytesThreshold()];
            tramaCabecera = new byte[20];
            for (int i = 0; i < getReceivedBytesThreshold(); i++)
                arregloTramaRelleno[i] = 64;
        }

        public String getName()
        {
            return serialPort.PortName;
        }
        public void setName(String PortName)
        {
            serialPort.PortName = PortName;
        }

        public int getDataBits()
        {
            return serialPort.DataBits;
        }
        public void setDataBits(int DataBits)
        {
            serialPort.DataBits = DataBits;
        }

        public int getBaudRate()
        {
            return serialPort.BaudRate;
        }
        public void setBaudRate(int BaudRate)
        {
            serialPort.BaudRate = BaudRate;
        }

        public int getReceivedBytesThreshold()
        {
            return serialPort.ReceivedBytesThreshold;
        }
        public void setReceivedBytesThreshold(int ReceivedBytesThreshold)
        {
            serialPort.ReceivedBytesThreshold = ReceivedBytesThreshold;
        }

        public SerialPort getSerialPort()
        {
            return serialPort;
        }

        public ClassFile getFileSend()
        {
            return fileSend;
        }

        public void iniciarConexion()
        {
            serialPort.DataReceived += new SerialDataReceivedEventHandler(sPuerto_DataReceived);
            serialPort.Open();
        }

        public void finalizarSendObject()
        {
            bufferExitEmpty = false;
            processVerifyExit.Abort();
            fileSend = null;
            readingFile.Close();
            flowFileToSend.Close(); 
            processSendFile.Abort();

        }

        public void finalizarReceptionObject()
        {
            fileReption = null;
            flowFileToReption.Close();
            writeingFile.Close();

        }

        public void creationToFile(string name, long size, int idNumber)
        {

            fileReption= new ClassFile();
            flowFileToReption = new FileStream(name, FileMode.Create, FileAccess.Write);
            writeingFile = new BinaryWriter(flowFileToReption);
            fileReption.name = Path.GetFileName(name);
            fileReption.number = idNumber;
            fileReption.size = size;
            fileReption.advance = 0;
        }

        public void confirmarEnvio()
        {
            tramaCabecera = ASCIIEncoding.UTF8.GetBytes("S");
            serialPort.Write(tramaCabecera, 0, 1);
            serialPort.Write(arregloTramaRelleno, 1, 1023);
        }

        public void confirmarPeticionCrearArchivo()
        {
            tramaCabecera = ASCIIEncoding.UTF8.GetBytes("1");
            serialPort.Write(tramaCabecera, 0, 1);
            serialPort.Write(arregloTramaRelleno, 0, 1023);
        }

        public void activarProcesoEnvio()
        {
            tramaCabecera = ASCIIEncoding.UTF8.GetBytes("G");
            serialPort.Write(tramaCabecera, 0, 1);
            serialPort.Write(arregloTramaRelleno, 0, 1023);
        }

        public void denegarPeticionCrearArchivo()
        {
            tramaCabecera = ASCIIEncoding.UTF8.GetBytes("0");
            serialPort.Write(tramaCabecera, 0, 1);
            serialPort.Write(arregloTramaRelleno, 0, 1023);
        }

        public void cerrarFlujos()
        {
            tramaCabecera = ASCIIEncoding.UTF8.GetBytes("F");
            serialPort.Write(tramaCabecera, 0, 1);
            serialPort.Write(arregloTramaRelleno, 0, 1023);
        }


        private void VerifyExit()
        {
            while (true)
            {
                if (serialPort.BytesToWrite > 0)
                    bufferExitEmpty = false;
                else
                    bufferExitEmpty = true;
            }

        }

        public void enviar_send(string msj)
        {
            try
            {
                mensajeEnviado = msj;
                processSend = new Thread(enviandoMensaje);
                processSend.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }
        }

        private void enviandoMensaje()
        {
            try
            {
                int tamanio;
                int tamanioTamanio;
                string ceros;
                tamanio = mensajeEnviado.Length;


                if (tamanio > 1004)
                {
                    int parteEntera = tamanio / 1004;
                    int residuo = tamanio - parteEntera * 1004;
                    byte[] contenido;
                    contenido = ASCIIEncoding.UTF8.GetBytes(mensajeEnviado);
                    MessageBox.Show("TAM: "+contenido.Length.ToString()
                        +"\r\nParte entera: "+parteEntera+
                        "\r\nResiduo: "+residuo);
                    tramaCabecera = ASCIIEncoding.UTF8.GetBytes("M1000000000000001024");

                    for (int i = 0; i < parteEntera; i++)
                    {
                        serialPort.Write(tramaCabecera, 0, 20);
                        serialPort.Write(contenido, i*1004, 1004);
                    }

                    ceros = "M0";
                    tamanioTamanio = residuo.ToString().Length;
                    for (int j = 0; j < 20 - 2 - tamanioTamanio; j++)
                    {
                        ceros = ceros + "0";
                    }
                    tramaCabecera = ASCIIEncoding.UTF8.GetBytes(ceros + residuo.ToString());

                    serialPort.Write(tramaCabecera, 0, 20);
                    serialPort.Write(contenido, parteEntera * 1004, residuo);
                   
                    serialPort.Write(arregloTramaRelleno,0,1024-20-residuo);
                }
                else
                {
                    ceros = "M0";
                    tamanioTamanio = tamanio.ToString().Length;
                    for (int j = 0; j < 20 - 2 - tamanioTamanio; j++)
                    {
                        ceros = ceros + "0";
                    }
                    tramaCabecera = ASCIIEncoding.UTF8.GetBytes(ceros + tamanio.ToString());

                    arregloTramaEnvio = ASCIIEncoding.UTF8.GetBytes(mensajeEnviado);
                    serialPort.Write(tramaCabecera, 0, 20);
                    serialPort.Write(arregloTramaEnvio, 0, mensajeEnviado.Length);
                    serialPort.Write(arregloTramaRelleno, 0, getReceivedBytesThreshold() - 20 - mensajeEnviado.Length);
                    
                }
                ceros = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void IniciadoEnvioArchivo(String name, String path)
        {

            bufferExitEmpty = true;//
            processVerifyExit = new Thread(VerifyExit);//
            processVerifyExit.Start();//

            flowFileToSend = new FileStream(path, FileMode.Open, FileAccess.Read);
            readingFile = new BinaryReader(flowFileToSend);
            fileSend = new ClassFile(1, name, path, flowFileToSend.Length, 0, true);
            enviarTareaCreacionArchivo();
        }

        public void enviarTareaCreacionArchivo()
        {
            //Para la creación del archivo se debe enviar una trama con la tarea C
            //y enviar la información del archivo en la cabecera de la trama, como
            //TAREA+TAMAÑO+NÚMERO+EXTENSIÓN -> tener en cuenta que la cabecera debe
            //ocupar 20 bytes, de otra forma debe complementar los 20 con algún relleno.

            long sizeFile;
            int number;
            String relleno = "", extension = "", textoSizeFile = "";

            sizeFile = fileSend.size;
            extension = Path.GetExtension(fileSend.path);
            number = fileSend.number;
            textoSizeFile = sizeFile.ToString();

            for (int i = 0; i < 9 - textoSizeFile.Length; i++)
            {
                relleno = relleno + "0";
            }
            textoSizeFile = relleno + textoSizeFile;

            relleno = "";
            for (int i = 0; i < 8 - extension.Length; i++)
            {
                relleno = relleno + "@";
            }
            extension += relleno;

            tramaCabecera = ASCIIEncoding.UTF8.GetBytes("C" + textoSizeFile + "01" + extension);
            serialPort.Write(tramaCabecera, 0, 20);
            serialPort.Write(arregloTramaRelleno, 0, 1004);

        }


        public void enviandoArchivo()
        {
            processSendFile = new Thread(sedingFile);
            processSendFile.Start();
        }


        private void sedingFile()
        {
            byte[] tramaSendFile;
            byte[] tramaSendHeadboardFile;

            tramaSendFile = new byte[1004];
            //tramaSendHeadboardFile = new byte[20];
            tramaSendHeadboardFile = ASCIIEncoding.UTF8.GetBytes("AC000000000000000000");
            int v = 0;
            while (fileSend.advance <= fileSend.size - 1004)
            {
                Console.WriteLine(v = v + 1004);

                readingFile.Read(tramaSendFile, 0, 1004);
                fileSend.advance = fileSend.advance + 1004;
                while (bufferExitEmpty == false)
                {//esperamos
                }
                serialPort.Write(tramaSendHeadboardFile, 0, 20);
                serialPort.Write(tramaSendFile, 0, 1004);
                OnLlegoBarra("Enviando", fileSend.size);
            }
            int tamanito = Convert.ToInt16(fileSend.size - fileSend.advance);
            readingFile.Read(tramaSendFile, 0, tamanito);
            while (bufferExitEmpty == false)
            {//esperamos
            }
            serialPort.Write(tramaSendHeadboardFile, 0, 20);
            serialPort.Write(tramaSendFile, 0, tamanito);
            serialPort.Write(arregloTramaRelleno, 0, 1004 - tamanito);
            Console.WriteLine(v = v + tamanito);
            OnLlegoBarra("Enviando",fileSend.size);
            MessageBox.Show("Se envío el archivo "+fileSend.name+" correctamente");
            finalizarSendObject();
            //trama para cerrar procesos.
        }

        public void detener()
        {
            serialPort.Close();

        }


        private void buildingFile()
        {
            // debe realizarse en funcion del tamaño 1004 y la ultima será tamanito

            try
            {
                OnLlegoBarra("Recibiendo", fileReption.size);
                if (fileReption.advance <= fileReption.size - 1004)
                {
                    writeingFile.Write(tramaRecibida, 20, 1004);
                    fileReption.advance = fileReption.advance + 1004;
                }
                else
                {
                    int tamanito = Convert.ToInt16(fileReption.size - fileReption.advance);
                    writeingFile.Write(tramaRecibida, 20, tamanito);
                    MessageBox.Show("Se recibió correctamente el archivo " + fileReption.name);
                    finalizarReceptionObject();
                    //tambien debo señalar al transmisor
                    //cerrarFlujos();


                }

            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }

        }

        private void sPuerto_DataReceived(object o, SerialDataReceivedEventArgs s)
        {


            if (serialPort.BytesToRead >= getReceivedBytesThreshold())
            {
                //Extraer toda la trama que ha llegado
                serialPort.Read(tramaRecibida, 0, getReceivedBytesThreshold());
                //Extraer el primer byte de la trama
                string TAREA = ASCIIEncoding.UTF8.GetString(tramaRecibida, 0, 1);
                //Realizo una tarea según el primer byte
                switch (TAREA)
                {
                    //La tarea es la contrucción de un mensaje.
                    case "M":
                        // Número de caracteres escritos

                        int cantCaracter = Int32.Parse(ASCIIEncoding.UTF8.GetString(tramaRecibida, 2, 18));
                        string x = ASCIIEncoding.UTF8.GetString(tramaRecibida, 1, 1);
                        if (x.Equals("1"))
                        {
                            byte[] bytes = new byte[cantCaracter];
                            string msj = ASCIIEncoding.UTF8.GetString(tramaRecibida, 20, 1004);
                            mensajeRecibido += msj;
                        }
                        else
                        {
                            mensajeRecibido+=mensajeRecibido = ASCIIEncoding.UTF8.GetString(tramaRecibida, 20, cantCaracter);
                            OnLlegoMensaje(mensajeRecibido);
                            mensajeRecibido = "";
                        }
                        
                        break;
                    
                    //La tarea es la pregunta de confirmación para acpetar o no el archivo
                    case "S":
                        DialogResult result = MessageBox.Show("¿Confirmar Archivo?", "Confirmación", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            //Se envía al transmisor una trama, y el primer byte = 1
                            //de confirmación
                            confirmarPeticionCrearArchivo();
                        }
                        else if (result == DialogResult.No)
                        {
                            //Se envía al transmisor una trama, y el primer byte = 0
                            //de no confirmación
                            denegarPeticionCrearArchivo();
                        }
                        break;

                    //La tarea es continuar con el envío, el transmisor sabe que se confirmó el archivo
                    case "1":
                        MessageBox.Show("El receptor acepto el archivo");
                        OnLlegoAprobacion();
                        break;

                    //El transmisor sabe que se no se debe aceptar el archivo
                    case "0":
                        MessageBox.Show("El receptor no acepto el archivo");
                        break;

                    //La tarea es crear en la recepción el archivo fileReception
                    case "C":
                        //Instancia los objects a utilizar en la recepción del archivo
                        //y lo crea 
                        string extensionConEspacios = ASCIIEncoding.UTF8.GetString(tramaRecibida, 12, 8);
                        string extension = extensionConEspacios.Replace("@", "");
                        int size = Int32.Parse(ASCIIEncoding.UTF8.GetString(tramaRecibida, 1, 9));
                        int number = Int32.Parse(ASCIIEncoding.UTF8.GetString(tramaRecibida, 10, 2));
                        OnLlegoCarpeta(size, number,extension);
                        break;
                    case "G":
                    //La tarea es activar el hilo de envío de bytes del archivo 
                        enviandoArchivo();
                        break;
                    //La tarea es enviar partes de bytes del archivo
                    case "A":
                        buildingFile();
                        break;

                    //La tarea es cerrar flujos del trasmisor.
                    case "F":
                        finalizarSendObject();
                        break;

                    //La tarea es identificar una trama sin una tarea.
                    default:
                        MessageBox.Show("TAREA NO RECONOCIDA");
                        break;
                }

            }
        }

        protected virtual void OnLlegoMensaje(string ss)
        {
            if (LlegoMensaje != null)
            {
                LlegoMensaje(this, ss);
            }
        }

        protected virtual void OnLlegoAprobacion()
        {
            if (LlegoAprobacion != null)
            {
                LlegoAprobacion(this);
            }
        }

        protected virtual void OnLlegoCarpeta(int s1, int s2,string aa)
        {
            if (LlegoCarpeta != null)
            {
                LlegoCarpeta(this, s1, s2,aa);
            }
        }

        protected virtual void OnLlegoBarra(string aa,long s1)
        {
            if (LlegoBarra != null)
            {
                LlegoBarra(this,aa, s1);
            }
        }

    }
}

//Principal