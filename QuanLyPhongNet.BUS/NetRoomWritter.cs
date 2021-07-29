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

        //************************************************************************************************//

        // GroupClient
        public void InsertClientGroup(string clientGroupName, string description)
        {
            groupClientObjectWriter.InsertGroupClient(clientGroupName, description);
        }

        public void UpdateGroupClient(string groupClientName, string description)
        {
            groupClientObjectWriter.UpdateGroupClient(groupClientName, description);
        }

        public void DeleteGroupClient(string groupName)
        {
            groupClientObjectWriter.DeleteGroupClient(groupName);
        }

        //************************************************************************************************//

        // GroupUser
        public void UpdateGroupUser(string groupUserName, string typeName)
        {
            groupUserObjectWriter.UpdateGroupUser(groupUserName, typeName);
        }

        public void DeleteGroupUser(string groupName)
        {
            groupUserObjectWriter.DeleteGroupUser(groupName);
        }

        //************************************************************************************************//

        // Category
        public void InsertCategory(string name, string type)
        {
            categoryObjectWriter.InsertCatergory(name, type);
        }

        public void UpdateCategory(string categoryName, string newName, string newType)
        {
            categoryObjectWriter.UpdateCategory(categoryName, newName, newType);
        }

        public void DeleteCategory(string categoryName)
        {
            categoryObjectWriter.DeleteCategory(categoryName);
        }

        //************************************************************************************************//

        // Food
        public void InsertFood(string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            foodObjectWriter.InsertFood(name, categoryName, priceUnit, unitLot, inventoryNumber);

        }

        public void UpdateFood(int foodID, string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            foodObjectWriter.UpdateFood(foodID, name, categoryName, priceUnit, unitLot, inventoryNumber);
        }

        public void DeleteFood(int foodID)
        {
            foodObjectWriter.DeleteFood(foodID);
        }

        //************************************************************************************************//

        // Drink
        public void InsertDrink(string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            drinkObjectWriter.InsertDrink(name, categoryName, priceUnit, unitLot, inventoryNumber);
        }

        public void UpdateDrink(int drinkID, string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            drinkObjectWriter.UpdateDrink(drinkID, name, categoryName, priceUnit, unitLot, inventoryNumber);
        }

        public void DeleteDrink(int drinkID)
        {
            drinkObjectWriter.DeleteDrink(drinkID);
        }


        //************************************************************************************************//

        // Card
        public void InsertCard(string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            cardObjectWriter.InsertCard(name, categoryName, priceUnit, unitLot, inventoryNumber);
        }

        public void UpdateCard(int cardID, string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            cardObjectWriter.UpdateCard(cardID, name, categoryName, priceUnit, unitLot, inventoryNumber);
        }

        public void DeleteCard(int cardID)
        {
            cardObjectWriter.DeleteCard(cardID);
        }

        //************************************************************************************************//

        // Member
        public void InsertMember(Member member)
        {
            memberObjectWriter.InsertMember(member);
        }

        public void UpdateMember(Member member)
        {
            memberObjectWriter.UpdateMember(member);
        }

        public void DeleteMember(int memberID)
        {
            memberObjectWriter.DeleteMember(memberID);
        }

        //************************************************************************************************//

        // LoginMember
        public void InsertLoginMember(LoginMember loginMember)
        {
            loginMemberObjectWriter.InsertLoginMember(loginMember);
        }

        public void UpdateLoginMember(int loginID, TimeSpan useTime, TimeSpan leftTime)
        {
            loginMemberObjectWriter.UpdateLoginMember(loginID, useTime, leftTime);
        }

        //************************************************************************************************//

        // MemberInfo
        public void InsertMemberInfo(int memberID, DateTime foundedDate)
        {
            memberInfoObjectWriter.InsertMemberInfo(memberID, foundedDate);
        }

        public void UpdateMemberInfo(MemberInformation memberInfo)
        {
            memberInfoObjectWriter.UpdateMemberInfo(memberInfo);
        }

        //************************************************************************************************//

        // User
        public void InsertUser(int ID, string name, string loginPass, string phone, string email)
        {
            userObjectWriter.InsertUser(ID, name, loginPass, phone, email);
        }

        public void UpdateUser(int ID, string name, string loginPass, string phone, string email)
        {
            userObjectWriter.UpdateUser(ID, name, loginPass, phone, email);
        }

        public void DeleteUser(int ID)
        {
            userObjectWriter.DeleteUser(ID);
        }

        //************************************************************************************************//

        // Client
        public void InsertClient(string clientName, string groupClientName, string note)
        {
            clientObjectWriter.InsertClient(clientName, groupClientName, note);
        }

        public void UpdateClient(string clientName, string groupClientName, string note, string status = "DISCONNET")
        {
            clientObjectWriter.UpdateClient(clientName, groupClientName, note, status);
        }

        public void DeleteClient(string clientName)
        {
            clientObjectWriter.DeleteClient(clientName);
        }

        public void ChangeStatusClient(string clientname, int check)
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

        //************************************************************************************************//

        // TransactionDiary
        public void InsertTransactionDiary(TransactionDiary2 TD)
        {
            TDObjectWriter.InsertTD(TD);
        }

        public void UpdateTransactionDiary(TransactionDiary2 TD)
        {
            TDObjectWriter.UpdateTD(TD);
        }

        //************************************************************************************************//

        // Bill
        public void InsertBill(Bill bill)
        {
            billObjectWriter.CreateNewBill(bill);
        }

        //************************************************************************************************//

        // Các hàm khác
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
    }
}
