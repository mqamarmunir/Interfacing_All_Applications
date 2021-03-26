using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsApplication5.CommForms
{
    public partial class TCPClient : Form
    {
        public TCPClient()
        {
            InitializeComponent();
        }

        private void btnStartClient_Click(object sender, EventArgs e)
        {
            Int32 port = int.Parse(txtPort.Text.Trim());
            string server = txtIpAddress.Text.Trim();
            TcpClient client = new TcpClient(server, port);
        }
    }
}
