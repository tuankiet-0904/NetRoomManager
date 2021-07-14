using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using May_2;

namespace DoAnSE
{
    public delegate void setInfo(string userName, string timeBalance);
    public class ClientManager
    {
        IPEndPoint iP;
        public Socket client;
        int portCode = 9999;
        const int maxGetByte = 1024 * 4000;
        public static int refreshClient = -1;
        public const int change = -1;
        const int request = 0;
        public static int requestServer = -1;
        public static string MessageFromServer;
        public static int MessageCode = -1;
        const int USECLIENT = 101;
        public const int MEMBERLOGIN = 102;
        public const int PAYMENT = 103;
        public const int SHUTDOWN = 104;
        public LockScreen lockScreen;
        public MessageFromClient messageSend;
        public string userName = "";
        public string message2 = "";
        public int loginID = 0;
        public TimeSpan totalTime;

        public ClientManager()
        {
            iP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portCode);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                client.Connect(iP);
                client.Send(ConvertToByte("ConnectWithMePls!!|" + "MAY-2"));
            }
            catch
            {
                MessageBox.Show("Không kết nối được với máy chủ!");
                return;
            }
            Thread listenSever = new Thread(ReceiveDataFromSever);
            listenSever.IsBackground = true;
            listenSever.Start();
            ClientManager x = this;
            lockScreen = new LockScreen(x);
            lockScreen.Show();
        }

        public void CloseSocketConnection()
        {
            client.Close();
        }

        public void Login(string userName, string passWord)
        {
            client.Send(ConvertToByte("AllowToLogInPls!!|" + userName + "|" + passWord + "|" + "MAY-2|"));
        }

        public void SendMessage (string message)
        {
            client.Send(ConvertToByte("Message!!|" + message + "|" + "May-2|"));
        }

        public void LogoutMember(string userName, TimeSpan remainTime, int loginID, TimeSpan useTime, TimeSpan leftTime)
        {
            message2 = "Log out!!!";
            client.Send(ConvertToByte("LogOutPls!!|" + userName + "|" + remainTime.ToString() + "|" + loginID + "|" + useTime + "|" + leftTime + "|"));
        }

        public void GetMemberInfo(string userName)
        {
            client.Send(ConvertToByte("GetMemberInfo!!|" + userName + "|"));
        }

        public void UpdateMemberInfo(string userName, string newInfo)
        {
            client.Send(ConvertToByte("UpdateMemberInfo!!|" + userName + "|" + newInfo + "|"));
        }

        public void UpdatePassword(string userName, string oldPass, string newPass)
        {
            client.Send(ConvertToByte("UpdatePassword!!|" + userName + "|" + oldPass + "|" + newPass + "|"));
        }

        public void ReceiveDataFromSever()
        {
            try
            {
                while (true)
                {
                    byte[] messageFromClient = new byte[maxGetByte];
                    client.Receive(messageFromClient);
                    string message = CovertToMessage(messageFromClient).ToString();
                    List<string> lstMessage = message.Split('|').ToList();
                    message = "";
                    if (lstMessage[request].Equals("UseClient"))
                    {
                        userName = "Customer";
                        requestServer = USECLIENT;
                        lockScreen.Visible = false;
                    }
                    if (lstMessage[request].Equals("PAYMENT"))
                    {
                        message2 = "PAYMENT";
                        lockScreen.Visible = true;
                        requestServer = PAYMENT;
                    }
                    if (lstMessage[request].Equals("LockClient!"))
                    {
                        message2 = "LockClient!";
                        lockScreen.Visible = true;
                    }
                    if (lstMessage[request].Equals("ShutDown!"))
                    {
                        message2 = "ShutDown!";
                        requestServer = SHUTDOWN;
                        CloseSocketConnection();
                    }
                    if (lstMessage[request].Equals("Message!!"))
                    {
                        MessageCode = 1;
                        MessageFromServer = lstMessage[1];
                    }
                    if (lstMessage[request].Equals("This account is currently being used!"))
                    {
                        message2 = "This account is currently being used!";
                        lockScreen.Visible = true;
                        lockScreen.Show();
                    }
                    if (lstMessage[request].Equals("OkePlayGo"))
                    {
                        message2 = "OkePlayGo";
                        userName = lstMessage[1];
                        totalTime = TimeSpan.Parse(lstMessage[2]);
                        requestServer = MEMBERLOGIN;
                        loginID = Convert.ToInt32(lstMessage[3]);
                        lockScreen.Visible = false;
                        lockScreen.TopMost = false;
                        lockScreen.Hide();
                    }
                    if (lstMessage[request].Equals("Acount not exist !! Or Wrong Username, Password"))
                    {
                        message2 = "Acount not exist !! Or Wrong Username, Password";
                        lockScreen.Visible = true;
                        lockScreen.Show();
                    }
                    if (lstMessage[request].Equals("Your account is exhausted. Recharge to use it!!!"))
                    {
                        message2 = "Your account is exhausted. Recharge to use it!!!";
                        lockScreen.Visible = true;
                        lockScreen.Show();
                    }
                    if (lstMessage[request].Equals("Member Info!"))
                    {
                        message2 = "Member info received!|" + lstMessage[1] + "|" + lstMessage[2] + "|" +
                            lstMessage[3] + "|" + lstMessage[4] + "|" + lstMessage[5] + "|";
                    }
                    if (lstMessage[request].Equals("Update MemberInfo Success!"))
                    {
                        message2 = "Update MemberInfo Success!";
                    }
                    if (lstMessage[request].Equals("Update Password Success!"))
                    {
                        message2 = "Update Password Success!";
                    }
                    if (lstMessage[request].Equals("Wrong Old Password!"))
                    {
                        message2 = "Wrong Old Password!";
                    }
                }
            }
            catch
            { 
            }
        }

        byte[] ConvertToByte(object obj)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, obj);
            return memoryStream.ToArray();
        }
        object CovertToMessage(byte[] messege)
        {
            MemoryStream memoryStream = new MemoryStream(messege);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            return binaryFormatter.Deserialize(memoryStream);
        }
    }
}
