using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace lab3_bai3
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        

        

        private void Form1_Shown(object sender, EventArgs e)
        {
            
            
        }

       



        private void Form1_Load(object sender, EventArgs e)
        {
            
           
        }
        private bool isThreadRunning = true;
        private bool isListening = false;
        private Thread serverThread = null;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isThreadRunning = false;
        }
        private void StartListen(object sender, EventArgs e)
        {

        }
        private StringBuilder receivedData = new StringBuilder();


        void StartUnsafeThread()
        {
            int bytesReceived = 0;
            byte[] buffer = new byte[1];
            Socket serverSocket;
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            listenerSocket.Bind(ipepServer);
            listenerSocket.Listen(-1);
            serverSocket = listenerSocket.Accept();
            textBox1.Text = "New client connected\r";


            while (serverSocket.Connected)
            {
                string text = "";


                do
                {
                    bytesReceived = serverSocket.Receive(buffer);
                    text += Encoding.ASCII.GetString(buffer);
                    receivedData.Append(text);
                } while (text[text.Length - 1] != '\n');

                if (text.EndsWith("\n"))
                {
                    string message = text.ToString();
                    UpdateTextBox("\n" + message);
                }



            }
            listenerSocket.Close();
        }

        private void UpdateTextBox(string message)
        {
            try
            {
                if (textBox1.InvokeRequired && isThreadRunning)
                {
                    textBox1.Invoke(new Action<string>(UpdateTextBox), message);
                }
                else
                {
                    textBox1.AppendText(message + Environment.NewLine);
                }
            }
            catch { }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isListening && (serverThread == null || !serverThread.IsAlive))
            {
               
                isListening = true;

                textBox1.Text = "Listening\r";
                CheckForIllegalCrossThreadCalls = false;
                Thread serverThread = new Thread(new ThreadStart(StartUnsafeThread));
                serverThread.Start();
            }
        }
    }
}
