using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Added libraries
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ServerSide
{
    public partial class Server : Form
    {
        private delegate void UpdateStatusCallback(string _message);

        // Constructor
        public Server()
        {
            InitializeComponent();
        }

        public void ServerStart(string _ipAddress, ushort port, int users)
        {
            IPAddress ipAddress = IPAddress.Parse(_ipAddress);

            textboxLog.AppendText("Starting server: " + ipAddress + ":" + port + "\r \n");
            textboxLog.AppendText("Number of slots: " + users + "\r \n");

            ServerProgram mainServer = new ServerProgram(ipAddress, users);
            ServerProgram.StatusChanged += new StatusChangedEventHandler(mainServer_StatusChanged);

            mainServer.StartListening(port);

            textboxLog.AppendText("Waiting for connections... \r \n");
            textboxLog.AppendText("\n");
        }
            
        public void mainServer_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            this.Invoke(new UpdateStatusCallback(this.UpdateStatus), new object[]
            {
                e.EventMessage
            });
        }

        private void UpdateStatus(string _message)
        {
            textboxLog.AppendText(_message + "\r \n");
        }
    }
}
