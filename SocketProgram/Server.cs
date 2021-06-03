using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;

namespace SocketProgram
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        SimpleTcpServer server;
        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13; //enter
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtStatus.Invoke((MethodInvoker)delegate ()
            {
                string request = e.MessageString.Remove(e.MessageString.Length - 1);
                string message = "";
                int number;
                txtStatus.Text += request;
                if (e.MessageString.StartsWith("ADD"))
                {
                    number = Convert.ToInt32(request.Remove(0, 3));
                    int sum;
                    do
                    {
                        sum = 0;
                        while (number != 0)
                        {
                            sum += number % 10;
                            number /= 10;
                        }
                        number = sum;
                    } while (sum > 9);
                    message = "ADD:" + sum.ToString();
                }
                else if (e.MessageString.StartsWith("FAK"))
                {
                    number = Convert.ToInt32(request.Remove(0, 3));
                    int sum=1;
                    while (number != 1)
                    {
                        sum *= number;
                        number--;
                    }
                    message = "FAK:" + sum.ToString();
                }
                else if (request.StartsWith("MYIP"))
                {
                    message = txtHost.Text;
                }else if (request.StartsWith("MYPORT"))
                {
                    message = txtPort.Text;
                }else if (request.StartsWith("CrNAME"))
                {
                    message = "Fatma Nur CÜCÜ 170504057 cucufatmanur@gmail.com";
                }
                e.ReplyLine(string.Format("Server Response : {0}\n", message));
            });
        }

        private void bttnStart_Click(object sender, EventArgs e)
        {
            txtStatus.Text += "Server Starting...\n";
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(txtHost.Text);
            server.Start(ip, Convert.ToInt32(txtPort.Text));
        }

        private void bttnStop_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
                server.Stop();
        }
    }

}
      

