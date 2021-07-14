using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyPhongNet.DAL;

namespace QuanLyPhongNet.BUS
{
    public class NetRoomWritter
    {
        static private NetRoomWritter _instance;
        static public NetRoomWritter Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NetRoomWritter();
                return _instance;
            }
            private set { }
        }
        private ProcessFood foodObjectWriter;
        private ProcessDrink drinkObjectWriter;
        private ProcessCard cardObjectWriter;
        private ProcessCategory categoryObjectWriter;
        private ProcessClient clientObjectWriter;
        private ProcessLoginMember loginMemberObjectWriter;
        private ProcessMember memberObjectWriter;
        private ProcessMemberInfo memberInfoObjectWriter;
        private ProcessGroupClient groupClientObjectWriter;
        private ProcessGroupUser groupUserObjectWriter;
        private ProcessUser userObjectWriter;
        private ProcessTD TDObjectWriter;
        private ProcessBill billObjectWriter;

        public NetRoomWritter()
        {
            foodObjectWriter = new ProcessFood();
            drinkObjectWriter = new ProcessDrink();
            cardObjectWriter = new ProcessCard();
            categoryObjectWriter = new ProcessCategory();
            clientObjectWriter = new ProcessClient();
            loginMemberObjectWriter = new ProcessLoginMember();
            memberObjectWriter = new ProcessMember();
            memberInfoObjectWriter = new ProcessMemberInfo();
            groupClientObjectWriter = new ProcessGroupClient();
            groupUserObjectWriter = new ProcessGroupUser();
            userObjectWriter = new ProcessUser();
            billObjectWriter = new ProcessBill();
            TDObjectWriter = new ProcessTD();
        }

        public void InsertClient(string clientName, string groupClientName, string note)
        {
            clientObjectWriter.InsertClient(clientName, groupClientName, note);
        }

        public void InsertClientGroup(string clientGroupName, string description)
        {
            groupClientObjectWriter.InsertGroupClient(clientGroupName, description);
        }

        public void InsertUser(int ID, string name, string loginPass, string phone, string email)
        {
            userObjectWriter.InsertUser(ID, name, loginPass, phone, email);
        }

        public void InsertLoginMember(LoginMember loginMember)
        {
            loginMemberObjectWriter.InsertLoginMember(loginMember);
        }

        public void InsertMember(Member member)
        {
            memberObjectWriter.InsertMember(member);
        }

        public void InsertMemberInfo(int memberID, DateTime foundedDate)
        {
            memberInfoObjectWriter.InsertMemberInfo(memberID, foundedDate);
        }

        public void InsertBill(Bill bill)
        {
            billObjectWriter.CreateNewBill(bill);
        }

        public void InsertFood(string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            foodObjectWriter.InsertFood(name, categoryName, priceUnit, unitLot, inventoryNumber);

        }

        public void InsertDrink(string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            drinkObjectWriter.InsertDrink(name, categoryName, priceUnit, unitLot, inventoryNumber);
        }

        public void InsertCard(string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            cardObjectWriter.InsertCard(name, categoryName, priceUnit, unitLot, inventoryNumber);
        }

        public void InsertCategory(string name, string type)
        {
            categoryObjectWriter.InsertCatergory(name, type);
        }
        public void InsertTransactionDiary(TransactionDiary2 TD)
        {
            TDObjectWriter.InsertTD(TD);
        }

        public void UpdateLoginMember(int loginID, TimeSpan useTime, TimeSpan leftTime)
        {
            loginMemberObjectWriter.UpdateLoginMember(loginID, useTime, leftTime);
        }
 
        public void UpdateMember(Member member)
        {
            memberObjectWriter.UpdateMember(member);
        }

        public void UpdateMemberInfo(MemberInformation memberInfo)
        {
            memberInfoObjectWriter.UpdateMemberInfo(memberInfo);
        }

        public void UpdateFood(int foodID, string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            foodObjectWriter.UpdateFood(foodID, name, categoryName, priceUnit, unitLot, inventoryNumber);
        }

        public void UpdateDrink(int drinkID, string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            drinkObjectWriter.UpdateDrink(drinkID, name, categoryName, priceUnit, unitLot, inventoryNumber);
        }

        public void UpdateCard(int cardID, string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            cardObjectWriter.UpdateCard(cardID, name, categoryName, priceUnit, unitLot, inventoryNumber);
        }

        public void UpdateCategory(string categoryName, string newName, string newType)
        {
            categoryObjectWriter.UpdateCategory(categoryName, newName, newType);
        }
        public void UpdateTransactionDiary(TransactionDiary2 TD)
        {
            TDObjectWriter.UpdateTD(TD);
        }

        public void UpdateClient(string clientName, string groupClientName, string note, string status = "DISCONNET")
        {
            clientObjectWriter.UpdateClient(clientName, groupClientName, note, status);
        }

        public void UpdateGroupClient(string groupClientName, string description)
        {
            groupClientObjectWriter.UpdateGroupClient(groupClientName, description);
        }

        public void UpdateGroupUser(string groupUserName, string typeName)
        {
            groupUserObjectWriter.UpdateGroupUser(groupUserName, typeName);
        }

        public void UpdateUser(int ID, string name, string loginPass, string phone, string email)
        {
            userObjectWriter.UpdateUser(ID, name, loginPass, phone, email);
        }

        public void DeleteMember(int memberID)
        {
            memberObjectWriter.DeleteMember(memberID);
        }

        public void DeleteFood(int foodID)
        {
            foodObjectWriter.DeleteFood(foodID);
        }

        public void DeleteDrink(int drinkID)
        {
            drinkObjectWriter.DeleteDrink(drinkID);
        }

        public void DeleteCard(int cardID)
        {
            cardObjectWriter.DeleteCard(cardID);
        }

        public void DeleteCategory(string categoryName)
        {
            categoryObjectWriter.DeleteCategory(categoryName);
        }

        public void DeleteClient(string clientName)
        {
            clientObjectWriter.DeleteClient(clientName);
        }

        public void DeleteGroupClient(string groupName)
        {
            groupClientObjectWriter.DeleteGroupClient(groupName);
        }

        public void DeleteGroupUser(string groupName)
        {
            groupUserObjectWriter.DeleteGroupUser(groupName);
        }

        public void DeleteUser(int ID)
        {
            userObjectWriter.DeleteUser(ID);
        }
        public TimeSpan ChangeMoneyToTime(float money)
        {
            float timemoney = money / 100;
            TimeSpan time = TimeSpan.Zero;
            if (money >= 0)
            {
                time = TimeSpan.FromMinutes((double)(new decimal(timemoney)));
            }
            return time;
        }
        public void ChangeStatusClient (string clientname, int check)
        {
            
            foreach (DTO.Client i in NetRoomReader.Instance.GetAllClients())
            {
                if (i.ClientName == clientname)
                {
                    if (check == 0)
                    {
                        UpdateClient(i.ClientName, i.GroupClientName, "CONNECT", i.Note);
                    }
                    else if (check == 1)
                    {
                        UpdateClient(i.ClientName, i.GroupClientName, "DISCONNECT", i.Note);
                    }
                }
            }
        }
    }
}
