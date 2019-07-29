using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace ClientApp
{
    public partial class Form1 : Form
    {
        private byte[] sendBuffer;
        private Socket senderSocket;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = hostInfo.AddressList[0];
                IPEndPoint endPoint = new IPEndPoint(ipAddress, 8080);

                senderSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                senderSocket.Connect(endPoint);
            }
            catch (Exception ex)
            {
                DialogResult dialog = MessageBox.Show("Connection Failed!", "Retry", MessageBoxButtons.RetryCancel);
                if(dialog == DialogResult.Retry)
                {
                    Button2_Click(sender, e);
                }
                if(dialog == DialogResult.Cancel)
                {
                    Application.Exit();
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            String text = textBox1.Text;

            text += "\n";

            textBox2.AppendText(text);

            if(text != null)
            {
                int stringSize = Encoding.ASCII.GetByteCount(text);

                sendBuffer = new byte[stringSize];

                sendBuffer = Encoding.ASCII.GetBytes(text);

                senderSocket.Send(sendBuffer);

                textBox1.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Do nothing
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (senderSocket != null)
            {
                senderSocket.Shutdown(SocketShutdown.Both);
                senderSocket.Close();
            }
        }
    }
}
