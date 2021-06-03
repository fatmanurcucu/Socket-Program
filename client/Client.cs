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
using Microsoft.VisualBasic;

namespace client
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        SimpleTcpClient client;

        private void bttnConnect_Click(object sender, EventArgs e)
        {
            client.Connect(txtHost.Text, Convert.ToInt32(txtPort.Text));
            bttnConnect.Enabled = false;
        }

        private void Host_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtStatus.Invoke((MethodInvoker) delegate ()
            {
                txtStatus.Text += e.MessageString;
            });
        }

        private void bttnSend_Click(object sender, EventArgs e)
        {
            string message = "";
            switch (txtMessage.Text)
            {
                case "ADD":
                    int number =Convert.ToInt32(Interaction.InputBox("Enter a number for ADD?", "Message", ""));
                    message = "ADD" + number.ToString();
                    break;
                case "Faktorial":
                    number = Convert.ToInt32(Interaction.InputBox("Enter a number for ADD?", "Message", ""));
                    message = "FAK" + number.ToString(); break;
                case "MYIP":
                    message = "MYIP";
                    break;
                case "MYPORT":
                    message = "MYPORT";
                    break;
                case "CrNAME":
                    message = "CrNAME";
                    break;
                default:break;

            }
            try
            {
                client.WriteLineAndGetReply(message, TimeSpan.FromSeconds(3));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
