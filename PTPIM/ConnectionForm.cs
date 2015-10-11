using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace PTPIM
{
    public partial class ConnectionForm : Form
    {
        private UdpClient sendClient;
        private UdpClient recvClient;
        private String targetIP;
        public ConnectionForm()
        {
            InitializeComponent();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            targetIP = IPBox.Text;
            IPEndPoint localIpep = new IPEndPoint(
                IPAddress.Parse("127.0.0.1"), 12345); // 本机IP，指定的端口号
            sendClient = new UdpClient(localIpep);

            Thread thrSend = new Thread(new ParameterizedThreadStart(SendMessage));
            thrSend.Start("shake");
        }

        void SendMessage(Object obj)
        {
            string message = (string)obj;
            byte[] sendbytes = Encoding.Unicode.GetBytes(message);

            IPEndPoint remoteIpep = new IPEndPoint(
                IPAddress.Parse(this.targetIP), 8848); // 发送到的IP地址和端口号

            sendClient.Send(sendbytes, sendbytes.Length, remoteIpep);
            sendClient.Close();
        }
    }
}
