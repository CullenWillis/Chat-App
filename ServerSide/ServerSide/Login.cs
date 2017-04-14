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
using System.Net.Sockets;
using System.Net;

namespace ServerSide
{
    public partial class Login : Form
    {
        // Variables
        private string ipAddress;
        private ushort port;
        private int users = 3;
        // Constructor
        public Login()
        {
            InitializeComponent();
            
            // Gets local IP and displays in textbox
            textboxIPAddress.Text = GetIP();
        }

        // On buttonStart click
        private void buttonStart_Click(object sender, EventArgs e)
        {
            // variables
            bool checkIP = false;
            bool checkPort = false;
            bool checkUsers = false;

            // Check ip
            if (VerifyIP())
            {
                // IF its ok, set ip
                ipAddress = textboxIPAddress.Text;
                checkIP = true;
            }
            else
            {
                // ELSE show error message
                MessageBox.Show("Invalid IP address, please re-enter.");
                textboxIPAddress.Text = GetIP();
                textboxIPAddress.Focus();
            }

            // Check Port
            if (VerifyPort() && checkIP)
            {
                // IF its ok, set ip
                port = ushort.Parse(textboxPort.Text);
                checkPort = true;
            }
            else if (checkIP && !checkPort)
            {
                // ELSE show error message
                MessageBox.Show("Invalid Port, please re-enter.");
                textboxPort.Clear();
                textboxPort.Focus();
            }

            // Check users
            if (VerifyUsers() && checkIP && checkPort)
            {
                // IF its ok, set ip
                users = int.Parse(textboxUsers.Text);
                checkUsers = true;
            }
            else if (checkIP && checkPort && !checkUsers)
            {
                // ELSE show error message
                MessageBox.Show("Invalid number of users, please re-enter a number between 2 - 30.");
                textboxUsers.Clear();
                textboxUsers.Focus();
            }

            // IF the ip and port is ok, Connect
            if (checkIP && checkPort && checkUsers)
            {
                // Instaniate new Server Form
                Server s = new Server();

                // TODO: Send details & start server
                s.ServerStart(ipAddress, port, users);

                // Open form
                s.Show();

                //this.Close();
            }
        }

        // Get local IP
        private string GetIP()
        {
            string ip = "Unknown";

            // Get IP
            IPAddress[] localIP = Dns.GetHostAddresses(Dns.GetHostName());

            // Go through addresses in localIP
            foreach (IPAddress address in localIP)
            {
                // Check if IP matches
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    // Set IP
                    ip = address.ToString();
                }
            }

            return ip;
        }

        // Check ip
        private bool VerifyIP()
        {
            // Set ip to textbox
            String ip = textboxIPAddress.Text;

            // Check if ip is whitespace or null
            if (String.IsNullOrWhiteSpace(ip))
            {
                return false;
            }
            
            // Check if the string contains 4 '.' (like an ip should)
            string[] splitValues = ip.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            // Return true IF parsing is ok!
            byte tempForParsing;
            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        // Check port
        private bool VerifyPort()
        {
            // Check if port is whitespace or null
            if (String.IsNullOrWhiteSpace(textboxPort.Text))
            {
                return false;
            }

            // Try convert into ushort, if works return true, else return false
            try
            {
                ushort.Parse(textboxPort.Text);
            }
            catch
            {
                return false; 
            }

            return true;
        }

        // Check port
        private bool VerifyUsers()
        {
            // Check if port is whitespace or null
            if (String.IsNullOrWhiteSpace(textboxUsers.Text))
            {
                return false;
            }

            // Try convert into ushort, if works return true, else return false
            try
            {
                int users = int.Parse(textboxUsers.Text);

                if(users < 2)
                {
                    return false;
                }

                if (users > 30)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
