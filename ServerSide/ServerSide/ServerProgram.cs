using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Added libraries
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;
using System.Security.Cryptography;
using System.Xml;
using System.Timers;

namespace ServerSide
{
    // This delegate is needed to specify the parameters we're passing with our event
    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);
    public enum MessageType { Unknown, General, Important, Private, PrivateImportant, Server };

    class ServerProgram
    {
        public static int userCount;

        // This hash table stores users and connections (browsable by user)
        public static Hashtable htUsers = new Hashtable(userCount); // stores connections and users (browsable by connection)
        public static Hashtable htConnections = new Hashtable(userCount); // stores the IP address passed to it

        public static ArrayList users = new ArrayList(userCount); // stores connected users

        private IPAddress ipAddress;
        private TcpClient tcpClient;

        // The event and its argument will notify the form when a user has connected, disconnected, send message, etc.
        public static event StatusChangedEventHandler StatusChanged;
        private static StatusChangedEventArgs e;

        // Message Storing
        private static List<Message> messages = new List<Message>();
        private static List<Message> storedMessages = new List<Message>();

        // Timer
        private static System.Timers.Timer timer;

        // Encryption
        private const string initVector = "OasisCitrusPunch";
        private const int keysize = 256;

        // The thread that will hold the connection listener
        private Thread thrListener;

        // The TCP object that listens for connections
        private TcpListener tlsClient;

        // Will tell the while loop to keep monitoring for connections
        bool ServRunning = false;

        private static DateTime deletionDate;

        // The constructor sets the IP address to the one retrieved by the instantiating object
        public ServerProgram(IPAddress address, int numUsers)
        {
            ipAddress = address;
            userCount = numUsers;

            StartTimer();
        }

        // Add the user to the hash tables
        public static void AddUser(TcpClient tcpUser, string strUsername)
        {
            // First add the username and associated connection to both hash tables
            ServerProgram.htUsers.Add(strUsername, tcpUser);
            ServerProgram.htConnections.Add(tcpUser, strUsername);
            ServerProgram.users.Add(strUsername);

            // Tell of the new connection to all other users and to the server form
            SendUserList();
            SendAdminMessage(htConnections[tcpUser] + " has joined the server", true);
        }

        // Remove the user from the hash tables
        public static void RemoveUser(TcpClient tcpUser)
        {
            // If the user is there
            if (htConnections[tcpUser] != null)
            {
                // show the information and tell the other users about the disconnection
                SendAdminMessage(htConnections[tcpUser] + " has left the server", true);

                // Remove the user from the hash table
                ServerProgram.htUsers.Remove(ServerProgram.htConnections[tcpUser]);
                ServerProgram.users.Remove(htConnections[tcpUser].ToString());
                ServerProgram.htConnections.Remove(tcpUser);

                SendUserList();
            }
        }

        // This is called when we want to raise the StatusChanged event
        public static void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChangedEventHandler statusHandler = StatusChanged;
            if (statusHandler != null)
            {
                // Invoke the delegate
                statusHandler(null, e);
            }
        }

        // Send administrative messages
        public static void SendAdminMessage(string Message, bool userConnection)
        {
            StreamWriter swSenderSender;
            string messageToSend = "";
            // First of all, show in our application who says what
            e = new StatusChangedEventArgs("[SERVER] " + Message);
            OnStatusChanged(e);

            addMessage("Server", "Everyone", "[SERVER] " + Message, MessageType.Server);

            // Create an array of TCP clients, the size of the number of users we have
            TcpClient[] tcpClients = new TcpClient[ServerProgram.htUsers.Count];
            // Copy the TcpClient objects into the array
            ServerProgram.htUsers.Values.CopyTo(tcpClients, 0);

            if (userConnection)
            {
                // Loop through the list of TCP clients
                for (int i = 0; i < tcpClients.Length; i++)
                {
                    // Try sending a message to each
                    try
                    {
                        // If the message is blank or the connection is null, break out
                        if (Message.Trim() == "" || tcpClients[i] == null)
                        {
                            continue;
                        }

                        // Send the message to the current user in the loop
                        swSenderSender = new StreamWriter(tcpClients[i].GetStream());

                        messageToSend = "<srv>" + Message;

                        messageToSend = EncryptString(messageToSend, initVector);

                        swSenderSender.WriteLine(messageToSend);
                        swSenderSender.Flush();
                        swSenderSender = null;
                    }
                    catch // If there was a problem, the user is not there anymore, remove him
                    {
                        RemoveUser(tcpClients[i]);
                    }
                }
            }
            else
            {
                // Loop through the list of TCP clients
                for (int i = 0; i < tcpClients.Length; i++)
                {
                    // Try sending a message to each
                    try
                    {
                        // If the message is blank or the connection is null, break out
                        if (Message.Trim() == "" || tcpClients[i] == null)
                        {
                            continue;
                        }
                        // Send the message to the current user in the loop
                        swSenderSender = new StreamWriter(tcpClients[i].GetStream());

                        messageToSend = "<srv>" + Message;

                        messageToSend = EncryptString(messageToSend, initVector);

                        swSenderSender.WriteLine(messageToSend);
                        swSenderSender.Flush();
                        swSenderSender = null;
                    }
                    catch // If there was a problem, the user is not there anymore, remove him
                    {
                        RemoveUser(tcpClients[i]);
                    }
                }
            }
        }

        public static void SendPrivateMessage(string From, string Message)
        {
            // Get the user from the message
            string user = "";
            string search = "";

            int startIndex = 4;

            for (int i = 0; i < Message.Length; i++)
            {
                search = Message.Substring(startIndex, i);

                if (search.Contains(">"))
                {
                    user = Message.Substring(startIndex, i - 1);
                    break;
                }
            }

            startIndex = startIndex + user.Length + 1;
            string newMessage = Message.Substring(startIndex);

            StreamWriter swSenderSender;

            string messageToSend = "";

            // Create an array of TCP clients, the size of the number of users we have
            TcpClient _tcpClient = new TcpClient();

            if (ServerProgram.htUsers.ContainsKey(user))
            {
                _tcpClient = (TcpClient)ServerProgram.htUsers[user];
            }

            if (Message.Trim() != "" || _tcpClient != null)
            {
                try
                {
                    swSenderSender = new StreamWriter(_tcpClient.GetStream());

                    // Send important private message
                    if (Message.Contains("<imp>"))
                    {
                        int index = 5;
                        newMessage = newMessage.Substring(index);

                        messageToSend = "<pm>[" + From + " (IMPORTANT)]:" + newMessage;

                        // Show in our application who says what
                        e = new StatusChangedEventArgs("[IMPORTANT] [PRIVATE " + From + " To " + user + "] " + newMessage);
                        OnStatusChanged(e);

                        addMessage(From, user, "[" + From + "(IMPORTANT)]:" + newMessage, MessageType.PrivateImportant);
                    }
                    // send normal private message
                    else
                    {
                        // Show in our application who says what
                        e = new StatusChangedEventArgs("[PRIVATE " + From + " To " + user + "] " + newMessage);
                        OnStatusChanged(e);

                        messageToSend = "<pm>[" + From + "]:" + newMessage;

                        addMessage(From, user, "[" + From + "]:" + newMessage, MessageType.Private);
                    }

                    messageToSend = EncryptString(messageToSend, initVector);
                    swSenderSender.WriteLine(messageToSend);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                // If there was a problem, the user is not there anymore, remove user
                catch
                {
                    RemoveUser(_tcpClient);
                }
            }
        }

        // Send messages from one user to all the others
        public static void SendMessage(string From, string Message)
        {
            StreamWriter swSenderSender;
            string messageToSend = "";
            bool firstIter = true;

            // Create an array of TCP clients, the size of the number of users we have
            TcpClient[] tcpClients = new TcpClient[ServerProgram.htUsers.Count];

            // Copy the TcpClient objects into the array
            ServerProgram.htUsers.Values.CopyTo(tcpClients, 0);

            // Loop through the list of TCP clients
            for (int i = 0; i < tcpClients.Length; i++)
            {
                // Try sending a message to each
                try
                {
                    // If the message is blank or the connection is null, break out
                    if (Message.Trim() == "" || tcpClients[i] == null)
                    {
                        continue;
                    }

                    swSenderSender = new StreamWriter(tcpClients[i].GetStream());

                    // important message
                    if (Message.Contains("<imp>"))
                    {
                        int startIndex = 5;
                        string newMessage = Message.Substring(startIndex);

                        // Send the important message to the current user in the loop            
                        messageToSend = "<imp>[" + From + " (IMPORTANT)]:" + newMessage;

                        if (firstIter)
                        {
                            // Show in our application who says what
                            e = new StatusChangedEventArgs("[IMPORTANT] [GENERAL " + From + "] " + newMessage);
                            OnStatusChanged(e);

                            addMessage(From, "Everyone", "[" + From + " (IMPORTANT)]:" + newMessage, MessageType.Important);
                        }
                    }
                    // general Message
                    else
                    {
                        // Send the message to the current user in the loop            
                        messageToSend = "[" + From + "]: " + Message;

                        if (firstIter)
                        {
                            // Show in our application who says what
                            e = new StatusChangedEventArgs("[GENERAL " + From + "] " + Message);
                            OnStatusChanged(e);

                            addMessage(From, "Everyone", "[" + From + "]: " + Message, MessageType.General);
                        }

                    }

                    string messageToSendEncrypted = EncryptString(messageToSend, initVector);

                    swSenderSender.WriteLine(messageToSendEncrypted);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch // If there was a problem, the user is not there anymore, remove him
                {
                    RemoveUser(tcpClients[i]);
                }

                firstIter = false;
            }
        }

        private static void addMessage(string sender, string receiver, string message, MessageType type)
        {
            DateTime date = DateTime.Now;

            messages.Add(new Message()
            {
                Sender = sender,
                Receiver = receiver,
                MessageToStore = message,
                MessageType = type,
                Date = date
            });
        }

        // Send messages from one user to all the others
        public static void SendUserList()
        {
            StreamWriter swSenderSender;

            // Create an array of TCP clients, the size of the number of users we have
            TcpClient[] tcpClients = new TcpClient[ServerProgram.htUsers.Count];
            // Copy the TcpClient objects into the array
            ServerProgram.htUsers.Values.CopyTo(tcpClients, 0);

            // Loop through the list of TCP clients
            for (int i = 0; i < tcpClients.Length; i++)
            {
                // Try sending a message to each
                try
                {
                    // If the array is empty or the connection is null, break out
                    if (users.Count == 0 || tcpClients[i] == null)
                    {
                        continue;
                    }

                    // Create message
                    string Message = "UserList-";

                    for (int u = 0; u < users.Count; u++)
                    {
                        Message = Message + users[u] + ",";
                    }

                    // Send the message to the current user in the loop
                    swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                    swSenderSender.WriteLine(Message);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch // If there was a problem, the user is not there anymore, remove him
                {
                    RemoveUser(tcpClients[i]);
                }
            }
        }

        public void StartListening(ushort port)
        {

            // Get the IP of the first network device, however this can prove unreliable on certain configurations
            IPAddress ipaLocal = ipAddress;

            // Create the TCP listener object using the IP of the server and the specified port
            tlsClient = new TcpListener(ipAddress, port);

            // Start the TCP listener and listen for connections
            tlsClient.Start();

            // The while loop will check for true in this before checking for connections
            ServRunning = true;

            // Start the new tread that hosts the listener
            thrListener = new Thread(KeepListening);
            thrListener.Start();
        }

        private void KeepListening()
        {
            // While the server is running
            while (ServRunning == true)
            {
                // Accept a pending connection
                tcpClient = tlsClient.AcceptTcpClient();
                // Create a new instance of Connection
                Connection newConnection = new Connection(tcpClient);
            }
        }

        /* --------------------------------------------------------------------------------------------------
         * 
         *  Encryption Methods
         * 
         * --------------------------------------------------------------------------------------------------
         */

        //Encrypt
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

        private static void StartTimer()
        {
            int minutes = 10;
            timer = new System.Timers.Timer((minutes * 60) * 1000);
            //timer = new System.Timers.Timer(30 * 1000);

            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (messages.Count > 0)
            {
                WriteXML();
            }
        }

        private static void WriteXML()
        {
            deletionDate = new DateTime();
            int id = 1;
            bool xmlRead = false;

            if (File.Exists("MessageLog.xml"))
            {
                ReadXml();
                xmlRead = true;
            }

            using (XmlWriter writer = XmlWriter.Create("MessageLog.xml"))
            {
                writer.WriteStartDocument();

                DateTime localDate = DateTime.Now;

                writer.WriteStartElement("Messages");

                if (storedMessages.Count > 0)
                {
                    foreach (Message msg in storedMessages)
                    {
                        writer.WriteStartElement("Message");

                        writer.WriteElementString("ID", id.ToString());
                        writer.WriteElementString("Sender", EncryptString(msg.Sender, initVector));
                        writer.WriteElementString("Receiver", EncryptString(msg.Receiver, initVector));
                        writer.WriteElementString("MessageType", msg.MessageType.ToString());
                        writer.WriteElementString("MessageStored", EncryptString(msg.MessageToStore, initVector));
                        writer.WriteElementString("Date", msg.Date.ToString());
                        writer.WriteElementString("DeletionDate", msg.DeletionDate.ToString());

                        writer.WriteEndElement();

                        id++;
                    }
                }

                foreach (Message msg in messages)
                {
                    writer.WriteStartElement("Message");

                    writer.WriteElementString("ID", id.ToString());
                    writer.WriteElementString("Sender", EncryptString(msg.Sender, initVector));
                    writer.WriteElementString("Receiver", EncryptString(msg.Receiver, initVector));
                    writer.WriteElementString("MessageType", msg.MessageType.ToString());
                    writer.WriteElementString("MessageStored", EncryptString(msg.MessageToStore, initVector));
                    writer.WriteElementString("Date", msg.Date.ToString());

                    deletionDate = localDate.AddDays(60);
                    writer.WriteElementString("DeletionDate", deletionDate.ToString("dd/MM/yyyy"));

                    writer.WriteEndElement();

                    id++;
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Flush();
            }

            storedMessages.Clear();
            messages.Clear();
        }

        private static void ReadXml()
        {
            bool deleteFile = false;
            int id = 0;
            string sender = null;
            string receiver = null;
            MessageType type = MessageType.Unknown;
            string messageStored = null;
            DateTime date = new DateTime();
            DateTime deletionDate = new DateTime();

            // Create an XML reader for this file.
            using (XmlReader reader = XmlReader.Create("MessageLog.xml"))
            {

                reader.MoveToContent();
                while (reader.Read())
                {
                    // Only detect start elements.
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        // Get element name and switch on it.
                        switch (reader.Name)
                        {
                            case "ID":
                                if (reader.Read())
                                {
                                    id = int.Parse(reader.Value.Trim());
                                }
                                break;
                            case "Sender":
                                if (reader.Read())
                                {
                                    sender = DecryptString(reader.Value.Trim(), initVector);
                                }
                                break;
                            case "Receiver":
                                if (reader.Read())
                                {
                                    receiver = DecryptString(reader.Value.Trim(), initVector);
                                }
                                break;
                            case "MessageType":
                                if (reader.Read())
                                {
                                    string obj = reader.Value.Trim();
                                    type = (MessageType)Enum.Parse(typeof(MessageType), obj, true);
                                }
                                break;
                            case "MessageStored":
                                if (reader.Read())
                                {
                                    messageStored = DecryptString(reader.Value.Trim(), initVector);
                                }
                                break;
                            case "Date":
                                if (reader.Read())
                                {
                                    date = DateTime.Parse(reader.Value.ToString().Trim());
                                }
                                break;
                            case "DeletionDate":
                                if (reader.Read())
                                {
                                    deletionDate = DateTime.Parse(reader.Value.ToString().Trim());
                                }
                                break;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        DateTime localDate = DateTime.Now;
                        if (localDate.Date <= deletionDate.Date || type.Equals(MessageType.Important))
                        {
                            switch (reader.Name)
                            {
                                case "Message":
                                    if (reader.Read())
                                    {
                                        storedMessages.Add(new Message()
                                        {
                                            Sender = sender,
                                            Receiver = receiver,
                                            MessageToStore = messageStored,
                                            MessageType = type,
                                            Date = date,
                                            DeletionDate = deletionDate
                                        });
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

    class Message
    {
        public int Id { get; set; }
        public DateTime DeletionDate { get; set; }
        public MessageType MessageType { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string MessageToStore { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return "Sender: " + Sender + " | Receiver: " + Receiver + " | Type: " + MessageType + " | Message: " + MessageToStore + " | Date: " + Date;
        }
    }

    // This class handels connections; there will be as many instances of it as there will be connected users
    class Connection
    {
        // Encryption
        private const string initVector = "OasisCitrusPunch";

        TcpClient tcpClient;
        // The thread that will send information to the client
        private Thread thrSender;
        private StreamReader srReceiver;
        private StreamWriter swSender;
        private string currUser;
        private string strResponse;

        // The constructor of the class takes in a TCP connection
        public Connection(TcpClient tcpCon)
        {
            tcpClient = tcpCon;
            // The thread that accepts the client and awaits messages
            thrSender = new Thread(AcceptClient);
            // The thread calls the AcceptClient() method
            thrSender.Start();
        }

        private void CloseConnection()
        {
            // Close the currently open objects
            tcpClient.Close();
            srReceiver.Close();
            swSender.Close();
        }

        // Occures when a new client is accepted
        private void AcceptClient()
        {
            srReceiver = new System.IO.StreamReader(tcpClient.GetStream());
            swSender = new System.IO.StreamWriter(tcpClient.GetStream());

            // Read the account information from the client
            currUser = srReceiver.ReadLine();

            // We got a response from the client
            if (currUser != "" && ServerProgram.htUsers.Count < ServerProgram.userCount)
            {
                // Store the user name in the hash table
                if (ServerProgram.htUsers.Contains(currUser) == true)
                {
                    // 0 means not connected
                    swSender.WriteLine("0|This username already exists.");
                    swSender.Flush();
                    CloseConnection();
                    return;
                }
                else if (currUser == "Administrator")
                {
                    // 0 means not connected
                    swSender.WriteLine("0|This username is reserved.");
                    swSender.Flush();
                    CloseConnection();
                    return;
                }
                else
                {
                    // 1 means connected successfully
                    swSender.WriteLine("1");
                    swSender.Flush();

                    // Add the user to the hash tables and start listening for messages from him
                    ServerProgram.AddUser(tcpClient, currUser);
                }
            }
            else
            {
                swSender.WriteLine("0|\nThere are already " + ServerProgram.htUsers.Count + "/" + ServerProgram.userCount + " people in the server");
                swSender.Flush();
                CloseConnection();
                return;
            }

            try
            {
                // Keep waiting for a message from the user
                while ((strResponse = srReceiver.ReadLine()) != "")
                {
                    strResponse = ServerProgram.DecryptString(strResponse, initVector);

                    // If it's invalid, remove the user
                    if (strResponse == null)
                    {
                        ServerProgram.RemoveUser(tcpClient);
                    }
                    else if (strResponse.Contains("<pm "))
                    {
                        ServerProgram.SendPrivateMessage(currUser, strResponse);
                    }
                    else
                    {
                        // Otherwise send the message to all the other users
                        ServerProgram.SendMessage(currUser, strResponse);
                    }
                }
            }
            catch
            {
                // If anything went wrong with this user, disconnect them
                ServerProgram.RemoveUser(tcpClient);
            }
        }
    }
}