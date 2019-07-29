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

namespace ServerApp
{
    public partial class Form1 : Form
    {
        private byte[] receivedData = new byte[1024];
        private Socket listner;
        private Socket handler;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 8080);

            listner = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listner.Bind(localEndPoint);
            listner.Listen(10);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            while (true)
            {
                handler = listner.Accept();

                while(handler != null)
                {
                    Array.Clear(receivedData, 0, receivedData.Length);

                    handler.Receive(receivedData);
                                       
                    if (receivedData.Length != 0)
                    {
                        String data = Encoding.ASCII.GetString(receivedData, 0, receivedData.GetUpperBound(0));
                        textBox2.AppendText(data + "\n");
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }
}
