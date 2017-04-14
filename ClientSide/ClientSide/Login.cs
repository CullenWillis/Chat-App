using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSide
{
    public partial class Login : Form
    {
        private string username;
        private string ipAddress;
        private ushort port;

        // Constructor
        public Login()
        {
            InitializeComponent();
        }

        // When buttonConnect is clicked
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            // variables
            bool checkIP = false;
            bool checkPort = false;

            // Check ip
            if (verifyIP())
            {
                // IF its ok, set ip
                ipAddress = textboxIPAddress.Text;
                checkIP = true;
            }
            else
            {
                // ELSE show error message
                MessageBox.Show("Invalid IP address, please re-enter.");
                textboxIPAddress.Clear();
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

            // IF username is not null
            if (textboxUsername.Text != "")
            {
                username = textboxUsername.Text;
            }
            else
            {
                username = "Anonymous";
            }

            // IF the ip and port is ok, Connect
            if (checkIP && checkPort)
            {
                // Instaniate new Server Form
                Client c = new Client();

                // Send details & connect to server
                if(c.Connect(username, ipAddress, port))
                {
                    // Open form
                    c.Show();

                    // Close current form
                    //this.Close();
                }
            }
        }

        // Check ip
        private bool verifyIP()
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

        // Check rt
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
    }
}
