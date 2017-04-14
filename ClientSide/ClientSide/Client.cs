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
using System.Security.Cryptography;

namespace ClientSide
{
    public partial class Client : Form
    {
        // Variables
        // Network Variables
        private StreamWriter streamWriter;
        private StreamReader streamReader;
        private TcpClient tcpServer;

        // Delegating Threads

        // Messages
        private delegate void UpdateLogRickCallback(string message, string color);

        // Listbox
        private delegate void ClearListbox();
        private delegate void UpdateListbox(string user);
        private delegate void SendPrivateMessage(string user);

        // Connection close
        private delegate void CloseConnectionCallback(string reason);

        // Thread Variable
        private Thread threadMessaging;

        // Connection variables
        private string username = "Anonymous";
        private IPAddress ipAddress;
        private ushort port;
        private bool isConnected;

        // User variables
        private int userCharLimit = 25;
        private int userCount = 0;
        private string[] users;
        private string lastPM = "";

        // Encryption
        private const string initVector = "OasisCitrusPunch";
        private const int keysize = 256;

        // Message
        private bool isPrivate = false;
        private bool isImportant = false;
        private bool timeStamps = false;

        // Constructor
        public Client()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            InitializeComponent();
        }

        /* --------------------------------------------------------------------------------------------------
         * 
         *  Connection / Disconnection Methods
         * 
         * --------------------------------------------------------------------------------------------------
         */

        // Properly close threads if user closes via other methods
        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (isConnected)
            {
                isConnected = false;

                // Close network streams/threads
                streamWriter.Close();
                streamReader.Close();
                tcpServer.Close();
            }
        }

        // When users connects
        public bool Connect(string _username, string _ipAddress, ushort _port)
        {
            // IF server is online
            if (!isConnected)
            {
                // Set Variables
                username = _username;
                ipAddress = IPAddress.Parse(_ipAddress);
                port = _port;

                // Connect to server
                if (IntializeConnection())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                // Close
                CloseConnection("Connection request denied!.");
            }

            return false;
        }

        // When user disconnects by button
        private void CloseConnection(string reason)
        {
            textboxLog.AppendText(reason + "\r \n");

            // Close the objects
            isConnected = false;
            streamWriter.Close();
            streamReader.Close();
            tcpServer.Close();
        }

        // Connect user to the server
        private bool IntializeConnection()
        {
            try
            {
                // Start a new connection
                tcpServer = new TcpClient();
                tcpServer.Connect(ipAddress, port);
                isConnected = true;

                // Send username to the server
                streamWriter = new StreamWriter(tcpServer.GetStream());
                streamWriter.WriteLine(username);
                streamWriter.Flush();

                // Start the thread for receiving messages
                threadMessaging = new Thread(new ThreadStart(ReceiveMessages));
                threadMessaging.Start();

                return true;
            }
            catch
            {
                MessageBox.Show("There is no available server under: " + ipAddress + ":" + port);
                this.Close();

                return false;
            }
        }

        /* --------------------------------------------------------------------------------------------------
         * 
         *  Sending / Receiving Messages
         * 
         * --------------------------------------------------------------------------------------------------
         */

        // Receive Messages 
        private void ReceiveMessages()
        {
            // Intialize stream on new StreamReader
            streamReader = new StreamReader(tcpServer.GetStream());

            // set the connectionResponse
            string connectionResponse = streamReader.ReadLine();

            // IF the first character is 1, then connection was successful
            if (connectionResponse[0] == '1')
            {
                // Update log with connection message
                this.Invoke(new UpdateLogRickCallback(this.AppendRichtext), new object[]
                {
                    "Connection was successful!", "Cyan"
                });
            }
            // IF the first character isnt 1, then connection was not successful
            else
            {
                string reason = "Connection was not successful!";

                // connectionResponse reason starts at line 3 (hence the Substring)
                reason += connectionResponse.Substring(2, connectionResponse.Length - 2);

                // Update the form with reason why we couldn't connect to the server
                this.Invoke(new UpdateLogRickCallback(this.AppendRichtext), new object[]
                {
                    reason, "Cyan"
                });

                return;
            }

            try
            {
                while (isConnected)
                {
                    string message = streamReader.ReadLine();

                    //Check if message is UserList -
                    if (message.Contains("UserList-"))
                    {
                        userCount = 0;
                        string[] tempUsers = new string[30];

                        string user = "";

                        int stringLength = message.Length;
                        int startIndex = 9;

                        for (int i = 0; i < 30; i++)
                        {
                            string search = "";

                            if (startIndex >= stringLength)
                            {
                                break;
                            }

                            for (int u = 1; u < userCharLimit + 1; u++)
                            {
                                search = message.Substring(startIndex, u);

                                if (search.Contains(","))
                                {
                                    user = message.Substring(startIndex, u - 1);
                                    tempUsers[userCount] = user;

                                    userCount++;
                                    startIndex += u;

                                    break;
                                }
                            }
                        }

                        users = new string[userCount];

                        for (int s = 0; s < userCount; s++)
                        {
                            users[s] = tempUsers[s];
                        }

                        PopulateListbox();
                    }
                    // else send a normal message
                    else
                    {
                        Console.WriteLine("Encrypted message: " + message);

                        message = DecryptString(message, initVector);

                        Console.WriteLine("Decrypted message: " + message + "\n");

                        // IF MESSAGE has <pm> (private message) set color to purple
                        if (message.Contains("<pm>"))
                        {
                            int startIndex = 4;
                            string newMessage = message.Substring(startIndex);

                            // Set color of textbox
                            this.Invoke(new UpdateLogRickCallback(this.AppendRichtext), new object[]
                            {
                                newMessage, "Orchid"
                            });

                            // Set lastPM to user in message
                            string search = "";
                            string user = "";
                            startIndex = 1;

                            for (int i = 0; i < 25; i++)
                            {
                                search = newMessage.Substring(startIndex, i);

                                if (search.Contains("]"))
                                {
                                    user = newMessage.Substring(startIndex, i - 1);
                                    lastPM = user;

                                    break;
                                }
                            }
                        }
                        // IF MESSAGE has <srv> (server) set color to blue and bold??
                        else if (message.Contains("<srv>"))
                        {
                            int startIndex = 5;
                            string newMessage = message.Substring(startIndex);

                            this.Invoke(new UpdateLogRickCallback(this.AppendRichtext), new object[]
                            {
                                newMessage, "Cyan"
                            });
                        }
                        // IF MESSAGE has <imp> (Important) set color to red
                        else if (message.Contains("<imp>"))
                        {
                            int startIndex = 5;
                            string newMessage = message.Substring(startIndex);

                            this.Invoke(new UpdateLogRickCallback(this.AppendRichtext), new object[]
                            {
                            newMessage, "OrangeRed"
                            });
                        }
                        // ELSE normal message
                        else
                        {
                            this.Invoke(new UpdateLogRickCallback(this.AppendRichtext), new object[]
                            {
                            message, "White"
                            });
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Server has shut down");
                this.Close();
            }
        }

        // Send message to server
        private void SendMessage()
        {
            isPrivate = false;
            string message = textboxMessage.Text;
            string user = "";

            if (message.Contains("/msg"))
            {
                user = FindUser(message);

                if (user.Equals(null))
                {
                    MessageBox.Show("Invalid username");
                    textboxMessage.Clear();
                    textboxMessage.Focus();
                }
                else
                {
                    int startIndex = 5 + user.Length + 1;
                    message = message.Substring(startIndex);
                    isPrivate = true;
                }
            }
            else if (message.Contains("/r"))
            {
                int startIndex = 3;

                message = message.Substring(startIndex);
                user = lastPM;

                isPrivate = true;
            }

            // IF texbox isnt null
            if (textboxMessage.Lines.Length >= 1)
            {
                if (isImportant)
                {
                    if (isPrivate)
                    {
                        // Send message to client 
                        this.Invoke(new UpdateLogRickCallback(this.AppendRichtext), new object[]
                        {
                            "To [" + user + " (IMPORTANT)]: " + message, "Lime"
                        });

                        // Send message to stream
                        message = "<pm " + user + "><imp>" + message;
                    }
                    else
                    {
                        // Send message to stream
                        message = "<imp>" + message;
                    }
                    
                }
                else if (!isImportant)
                {
                    if (isPrivate)
                    {
                        // Send message to client
                        this.Invoke(new UpdateLogRickCallback(this.AppendRichtext), new object[]
                        {
                            "To [" + user + "]: " + message, "Lime"
                        });

                        // Send message to stream
                        message = "<pm " + user + ">" + message;
                    }
                }

                message = EncryptString(message, initVector);

                streamWriter.WriteLine(message);
                streamWriter.Flush();

                textboxMessage.Lines = null;
                textboxMessage.Text = "";
            }
        }

        /* --------------------------------------------------------------------------------------------------
         * 
         *  Data Gathering Methods
         * 
         * --------------------------------------------------------------------------------------------------
         */

        private void PopulateListbox()
        {
            this.Invoke(new ClearListbox(this.ClearUserList));

            for (int i = 0; i < users.Length; i++)
            {
                if (users[i] != null)
                {
                    if (!username.Equals(users[i]))
                    {
                        this.Invoke(new UpdateListbox(this.UpdateUserList), new object[]
                        {
                            users[i]
                        });
                    }
                }
            }
        }

        private string FindUser(string message)
        {
            string user = null;
            string tempUser = null;
            string search = "";
            int startIndex = 5;

            // Finds the user from the messafe
            for (int i = 0; i < 25; i++)
            {
                search = message.Substring(startIndex, i);

                if (search.Contains(":"))
                {
                    tempUser = message.Substring(startIndex, i - 1);

                    break;
                }
            }

            // Checks if user is in the system
            for (int s = 0; s < users.Length; s++)
            {
                if (tempUser.Equals(users[s]))
                {
                    user = tempUser;

                    break;
                }
            }

            return user;
        }

        /* --------------------------------------------------------------------------------------------------
         * 
         *  Threading Methods
         * 
         * --------------------------------------------------------------------------------------------------
         */

        // Change color of the textbox
        private void AppendRichtext(string message, string color)
        {
            string timestamp = DateTime.Now.ToShortTimeString();
            Color textColor = Color.FromName(color);

            textboxLog.SelectionStart = textboxLog.TextLength;
            textboxLog.SelectionLength = timestamp.Length + message.Length;
            textboxLog.SelectionColor = textColor;

            if (timeStamps)
            {
                textboxLog.AppendText(timestamp + " - " + message + "\n");
            }
            else
            {
                textboxLog.AppendText(message + "\n");
            }

            textboxLog.ScrollToCaret();
        }

        // Clear the listbox
        private void ClearUserList()
        {
            listboxUsers.Items.Clear();
        }

        // Update the listbox in the form with a user
        private void UpdateUserList(string _user)
        {
            listboxUsers.Items.Add(_user);
        }

        // make '/msg user' appear
        private void PrivateMessage(string _user)
        {
            textboxMessage.Text = "/msg " + _user + ": ";
        }

        /* --------------------------------------------------------------------------------------------------
         * 
         *  Encryption Methods
         * 
         * --------------------------------------------------------------------------------------------------
         */

        public static string EncryptString(string plainText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }
        
        //Decrypt
        public static string DecryptString(string cipherText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        /* --------------------------------------------------------------------------------------------------
         * 
         *  Event Methods
         * 
         * --------------------------------------------------------------------------------------------------
         */

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            CloseConnection("Disconnected at user's request.");

            //this.Close();
        }

        private void listboxUsers_DoubleClick(object sender, EventArgs e)
        {
            this.Invoke(new SendPrivateMessage(this.PrivateMessage), new object[]
            {
                listboxUsers.SelectedItem.ToString()
            });

            textboxMessage.Focus();
        }

        private void checkboxImportant_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxImportant.Checked == true)
            {
                isImportant = true;
            }
            else if (checkboxImportant.Checked == false)
            {
                isImportant = false;
            }
        }

        private void checkboxTimeStamps_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxTimeStamps.Checked == true)
            {
                timeStamps = true;
            }
            else if (checkboxTimeStamps.Checked == false)
            {
                timeStamps = false;
            }
        }

        private void textboxMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textboxMessage.Text = "";
                e.Handled = true;
            }
        }

        private void textboxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                // Send message
                SendMessage();
                checkboxImportant.Checked = false;
            }

        }
    }
}
