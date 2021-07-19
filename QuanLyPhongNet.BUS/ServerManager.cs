using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using QuanLyPhongNet.DTO;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace QuanLyPhongNet.BUS
{
    public class ServerManager
    {
           
        static private ServerManager _instance;
        static public ServerManager Instance
        {
            get
            {
                if (_instance == null)
                _instance = new ServerManager();
                return _instance;
            }
        private set { }
        }
            
        IPEndPoint iP;
        Socket socketServer;
        public List<InfoClient> usingClient = new List<InfoClient>();
        public static int MemberID;
        const int portCode = 9999;
        const int maxGetByte = 1024 * 5000;
        public static int MessageCode = -1;
        public static int refreshClient = -1;
        public const int change = -1;
        const int request = 0;
        public static string MessageFromClient = "";
        const string wait = "WAITING";
        const string USING = "USING";
        public TimeSpan totalTime;
        public static string clientsend;

        public ServerManager()
        {
            iP = new IPEndPoint(IPAddress.Any, portCode);
            socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            socketServer.Bind(iP);
            Thread OkeFine = new Thread(StartAceptClient);
            OkeFine.IsBackground = true;
            OkeFine.Start();
        }

        public void CloseSocketConnection()
        {
            socketServer.Close();
        } 

        public void StartAceptClient()
        {
            while (!socketServer.Connected)
            {
                try
                {
                    while (true)
                    {
                        socketServer.Listen(100);
                        Socket newClient = socketServer.Accept();
                        Thread listenClient = new Thread(ReceiveDataFromClient);
                        listenClient.IsBackground = true;
                        listenClient.Start(newClient);
                    }
                }
                catch
                {

                }
            }
        }

        public void ReceiveDataFromClient(object obj)
        {
            Socket currentClient = obj as Socket;
            try
            {
                while (true)
                {
                    byte[] messegeFromClient = new byte[1024 * 5000];
                    currentClient.Receive(messegeFromClient);

                    string messege = (string)CovertToMessege(messegeFromClient);
                    List<string> lstMessege = messege.Split('|').ToList();
                    if (lstMessege[request].Equals("ConnectWithMePls!!"))
                    {                  
                        usingClient.Add(
                        new InfoClient
                        {
                            stateClient = wait,
                            client = currentClient,
                            nameClient = lstMessege[1],
                            nameCustomer = ""
                        });
                        refreshClient = 9999;
                    }
                    if (lstMessege[request].Equals("AllowToLogInPls!!"))
                    {
                        switch (CheckLogin(lstMessege[1], lstMessege[2], 0))
                        {
                            case -1:
                                currentClient.Send(ConvertToByte("This account is currently being used!|" + lstMessege[1] + "|" + totalTime + "|"));
                                break;
                            case 0:
                                int loginID = NetRoomReader.Instance.GetAllLoginMembers().Count + 1;
                                int memberID = NetRoomReader.Instance.FindIDByAccountName(lstMessege[1]);
                                currentClient.Send(ConvertToByte("OkePlayGo|" + lstMessege[1] + "|" + totalTime + "|" + loginID + "|"));
                                SaveLoginInfo(loginID, memberID, lstMessege[3], DateTime.Now.Date, DateTime.Now.TimeOfDay);
                                ChangeStateClient(currentClient, "MEMBER USING", lstMessege[1]);
                                refreshClient = 9999;
                                break;
                            case 1:
                                currentClient.Send(ConvertToByte("Your account is exhausted. Recharge to use it!!!|" + lstMessege[1] + "|" + totalTime + "|"));
                                ChangeStateClient(currentClient, "WAITING", lstMessege[1]);
                                break;
                            case 2:
                                currentClient.Send(ConvertToByte("Acount not exist !! Or Wrong Username, Password|" + lstMessege[1] + "|" + totalTime + "|"));
                                ChangeStateClient(currentClient, "WAITING", lstMessege[1]);
                                break;
                        }
                    }
                    if (lstMessege[request].Equals("LogOutPls!!"))
                    {
                        UpdateRemainTime(lstMessege[1], TimeSpan.Parse(lstMessege[2]));
                        ChangeStateClient(currentClient, "WAITING", lstMessege[1]);
                        refreshClient = 9999;
                        SaveLogoutInfo(Convert.ToInt32(lstMessege[3]), TimeSpan.Parse(lstMessege[4]), TimeSpan.Parse(lstMessege[5]));
                    }
                    if (lstMessege[request].Equals("Message!!"))
                    {
                        MessageFromClient = lstMessege[2] + ": " + lstMessege[1];
                        MessageCode = 1;
                        clientsend = lstMessege[2];
                    }
                    if (lstMessege[request].Equals("GetMemberInfo!!"))
                    {
                        DTO.MemberInformation memberInfo = GetMemberInfo(lstMessege[1]);
                        string sendBackInfo = memberInfo.MemberName + "|" + memberInfo.FoundedDate.ToString() + "|" +
                            memberInfo.PhoneNumber + "|" + memberInfo.MemberAddress + "|" + memberInfo.Email + "|";
                        currentClient.Send(ConvertToByte("Member Info!|" + sendBackInfo));
                    }
                    if (lstMessege[request].Equals("UpdateMemberInfo!!"))
                    {
                        UpdateMemberInfo(lstMessege[1], lstMessege[2], lstMessege[3], lstMessege[4], lstMessege[5], lstMessege[6]);
                        currentClient.Send(ConvertToByte("Update MemberInfo Success!|"));
                    }
                    if (lstMessege[request].Equals("UpdatePassword!!"))
                    {
                        switch (CheckLogin(lstMessege[1], lstMessege[2], 1))
                        {
                            case 0:
                            case 1:
                                UpdatePassword(lstMessege[1], lstMessege[3]);
                                currentClient.Send(ConvertToByte("Update Password Success!|"));
                                break;
                            case 2:
                                currentClient.Send(ConvertToByte("Wrong Old Password!|"));
                                break;
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public void SendMessage(string message, string sendtoclient)
        {
            if (sendtoclient.Equals("All"))
            {
                foreach (InfoClient client in usingClient)
                {
                    client.client.Send(ConvertToByte("Message!!|" + message + "|"));
                }
            }
            else
            {
                try
                {
                    InfoClient client = FindClient(sendtoclient);
                    client.client.Send(ConvertToByte("Message!!|" + message + "|"));
                }
                catch
                {
                    MessageBox.Show("Máy trạm này đã ngắt kết nối!", "Lỗi!", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        public InfoClient FindClient(string sendtoclient)
        {
            foreach (InfoClient client in usingClient)
            {
                if (client.nameClient.ToUpper().Equals(sendtoclient.ToUpper())) return client;
            }
            return null;
        }

        public void UpdateRemainTime(string userName, TimeSpan remainTime)
        {
            userName = userName.ToUpper();
            List<DTO.Member> lstMember = new NetRoomReader().GetAllMembers();

            foreach (DTO.Member m in lstMember)
            {
                m.AccountName = m.AccountName.ToUpper();
                if (m.AccountName.Equals(userName))
                {
                    float money =  ChangeUseTimeToMinutes(remainTime.ToString()) * 100;
                    DAL.Member member = new DAL.Member
                    {
                        MemberID = m.ID,
                        AccountName = m.AccountName,
                        Password = m.Password,
                        GroupUser = m.GroupUserName,
                        CurrentTime = remainTime,
                        CurrentMoney = money,
                        StatusAccount = m.Status,
                    };
                    NetRoomWritter.Instance.UpdateMember(member);
                }
            }
        }

        private int CheckLogin(string userName, string pass, int opt)
        {
            userName = userName.ToUpper();
            if (opt == 0)
            {
                foreach (DTO.InfoClient client in this.usingClient)
                {
                    if (client.nameCustomer.ToUpper().Equals(userName)) return -1;
                }
            }
            List<DTO.Member> lstMember = new NetRoomReader().GetAllMembers();
            foreach (DTO.Member m in lstMember)
            {
                if (m.AccountName.ToUpper().Equals(userName) && m.Password.Equals(pass))
                {
                    if (ChangeUseTimeToMinutes(m.TimeInAccount.ToString()) > 0)
                    {
                        totalTime = m.TimeInAccount;
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            return 2;
        }

        private float ChangeUseTimeToMinutes(String useTime)
        {
            float minutes = 0;
            string[] arrListStr = useTime.Split(':');
            if (float.Parse(arrListStr[1]) > 0)
            {
                minutes = minutes + float.Parse(arrListStr[1]);
            }
            if (float.Parse(arrListStr[0]) > 0)
            {
                minutes = minutes + float.Parse(arrListStr[0]) * 60;
            }
            return minutes;
        }
        public float TotalPrice(int index)
        {
            InfoClient client = usingClient[index];
            if (client.stateClient == "MEMBER USING")
            {
                return 0;
            }
            
            TimeSpan time = DateTime.Now.Subtract(client.startTime);
            
            client.stateClient = "WAITING";
            client.client.Send(ConvertToByte("PAYMENT"));
            float total = 0;
            string useTime = time.ToString();
            float priceperminutes = 100;
            float minutes = ChangeUseTimeToMinutes(useTime);
            if (minutes == 0 || minutes < 20)
                return 2000;
            total = total + minutes * priceperminutes;
            return total;
        }

        public void ShutDown(string nameClient)
        {
            InfoClient client = new InfoClient();
            foreach (InfoClient i in usingClient)
            {
                if (i.nameClient.Equals(nameClient))
                {
                    client = i;
                }
            }
            usingClient.Remove(client);
            client.client.Send(ConvertToByte("ShutDown!"));
        }

        public void LockClient(int index)
        {
            InfoClient client = usingClient[index];
            client.stateClient = "WAITING";
            client.nameClient = "";
            client.client.Send(ConvertToByte("LockClient!"));
        }

        public void ChangeStateClient(Socket client, string state, string userName)
        {
            foreach (InfoClient cli in usingClient)
            {
                if (cli.client == client)
                {
                    cli.stateClient = state;
                    if (state.Equals("WAITING")) cli.nameCustomer = "";
                    else cli.nameCustomer = userName;
                }
            }
        }

        public void UpdateMemberInfo(string userName, string name, string foundedDate, string phoneNumber, string address, string email)
        {
            DTO.MemberInformation memberInfo = GetMemberInfo(userName);
            DAL.MemberInformation newMemberInfo = new DAL.MemberInformation();
            newMemberInfo.MemberID = memberInfo.MemberID;
            newMemberInfo.MemberName = name;
            newMemberInfo.FoundedDate = Convert.ToDateTime(foundedDate).Date;
            newMemberInfo.PhoneNumber = phoneNumber;
            newMemberInfo.MemberAddress = address;
            newMemberInfo.Email = email;
            NetRoomWritter.Instance.UpdateMemberInfo(newMemberInfo);
        }

        public void SaveLoginInfo(int loginID, int memberID, string clientName, DateTime loginDate, TimeSpan loginTime)
        {
            loginTime = TimeSpan.Parse(loginTime.Hours + ":" + loginTime.Minutes + ":" + loginTime.Seconds);
            DAL.LoginMember loginInfo = new DAL.LoginMember
            {
                LoginID = loginID,
                MemberID = memberID,
                ClientName = clientName,
                LoginDate = loginDate,
                StartTime = loginTime,
                UseTime = TimeSpan.Zero,
                LeftTime = TimeSpan.Zero
            };
            NetRoomWritter.Instance.InsertLoginMember(loginInfo);
        }

        public void SaveLogoutInfo(int loginID, TimeSpan useTime, TimeSpan leftTime)
        {
            NetRoomWritter.Instance.UpdateLoginMember(loginID, useTime, leftTime);
        }

        public void UpdatePassword(string account, string newPassword)
        {
            DTO.Member member = NetRoomReader.Instance.GetMemberByAccountName(account);
            DAL.Member newPass = new DAL.Member();
            newPass.MemberID = member.ID;
            newPass.AccountName = member.AccountName;
            newPass.Password = newPassword;
            newPass.GroupUser = member.GroupUserName;
            newPass.CurrentTime = member.TimeInAccount;
            newPass.CurrentMoney = member.CurrentMoney;
            newPass.StatusAccount = member.Status;
            NetRoomWritter.Instance.UpdateMember(newPass);
        }

        byte[] ConvertToByte(object obj)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, obj);
            return memoryStream.ToArray();
        }
        object CovertToMessege(byte[] messege)
        {
            MemoryStream memoryStream = new MemoryStream(messege);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            return binaryFormatter.Deserialize(memoryStream);
        }

        public InfoClient GetInfoClient(string nameClient, string contraint)
        {
            foreach (InfoClient infoClient in usingClient)
            {
                if (infoClient.nameClient.Equals(nameClient) && infoClient.stateClient.Equals(contraint))
                {
                    return infoClient;
                }
            }
            return null;
        }

        public DTO.MemberInformation GetMemberInfo(string accountName)
        {
            int memberID = 0;
            foreach (DTO.Member i in NetRoomReader.Instance.GetAllMembers())
            {
                if (i.AccountName.Equals(accountName))
                {
                    memberID = i.ID;
                    break;
                }
            }
            foreach (DTO.MemberInformation i in NetRoomReader.Instance.GetAllMemberInfos())
            {
                if (i.MemberID == memberID)
                {
                    return i;
                }
            }
            return null;
        }

        public bool UsingWithGuess(string nameClient)
        {
            InfoClient currentClient = GetInfoClient(nameClient, wait);
            if (currentClient == null)
                return false;
            try
            {
                currentClient.client.Send(ConvertToByte("UseClient"));
                currentClient.stateClient = USING;
                currentClient.startTime = DateTime.Now;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool PaymentNetFee(string nameClient)
        {
            InfoClient currentClient = GetInfoClient(nameClient, USING);
            if (currentClient == null)
                return false;
            try
            {
                currentClient.client.Send(ConvertToByte("Payment"));
                currentClient.stateClient = wait;
                currentClient.startTime = new DateTime();
                return true;
            }
            catch
            {
                return false;
            }
        }

        
    }
}
