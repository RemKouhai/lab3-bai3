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
using System.Xml.Linq;

namespace form2
{
    public partial class Form1 : Form
    {

        private TcpClient client = new TcpClient();
        private NetworkStream stream;
        Thread catchMess;
        
        
        
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ConnectToServer()
        {
            try
            {
                
                IPAddress ipa = IPAddress.Parse("127.0.0.1");
                IPEndPoint ipe = new IPEndPoint(ipa, 8080);
                client.Connect(ipe);
                stream = client.GetStream();
                button1.Enabled = true;
                MessageBox.Show("Đã kết nối với server!");
                button2.Invoke((MethodInvoker)delegate{ button2.Enabled = true; }     );          
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xảy ra khi đang kết nối: " + ex.Message);
            }
        }

        private void SendMessage()
        {
            try
            {               
                string message = "Hello server!\n";
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);       
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xảy ra khi đang gởi tin: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            catchMess = new Thread(ConnectToServer);
            catchMess.Start();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendMessage(); 
        }
    }
}
