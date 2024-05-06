using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace lab3_bai3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private TcpClient client;

        bool click = false;
        private void button1_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            client.Connect("127.0.0.1", 8080);
            NetworkStream stream = client.GetStream();
            click = true;
            while (click)
            {
                byte[] buffer = Encoding.ASCII.GetBytes("Hello server\n");
                stream.Write(buffer, 0, buffer.Length);
                click = false;
            }
            
            
        }


    }
}
