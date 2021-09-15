using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhongNet.DAL;

namespace QuanLyPhongNet.BUS
{
    public class NetRoomReader
    {
        static private NetRoomReader _instance;
        static public NetRoomReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NetRoomReader();
                return _instance;
            }
            private set { }
        }

        //************************************************************************************************//

        // GroupClient
        public List<DTO.GroupClient> GetAllGroupClients()
        {
            return ProcessGroupClient.Instance.LoadAllGroupClients();
        }

        public List<DTO.GroupClient> GetListClientGroup(string searchBy, string searchFor)
        {
            List<DTO.GroupClient> list = new List<DTO.GroupClient>();
            foreach (DTO.GroupClient i in GetAllGroupClients())
            {
                if (searchBy == "All")
                {
                    list.Add(i);
                }
                else
                {
                    switch (searchBy)
                    {
                        case "Nhóm máy":
                            if (searchFor != "")
                            {
                                if (i.GroupClientName.ToLower().Contains(searchFor.ToLower()))
                                {
                                    list.Add(i);
                                }
                            }
                            else list.Add(i);
                            break;
                        case "Mô tả":
                            if (searchFor != "")
                            {
                                if (i.Description.ToLower().Contains(searchFor.ToLower()))
                                {
                                    list.Add(i);
                                }
                            }
                            else list.Add(i);
                            break;
                    }
                }
            }
            return list;
        }

        public DTO.GroupClient GetClientGroupByName(string clientGroupName)
        {
            foreach (DTO.GroupClient clientGroup in GetAllGroupClients())
            {
                if (clientGroup.GroupClientName.Equals(clientGroupName)) return clientGroup;
            }
            return null;
        }

        //************************************************************************************************//

        // GroupUser
        public List<DTO.GroupUser> GetAllGroupUsers()
        {
            return ProcessGroupUser.Instance.LoadAllGroupUsers();
        }

        public List<DTO.GroupUser> GetListUserGroup(string searchBy, string searchFor)
        {
            List<DTO.GroupUser> list = new List<DTO.GroupUser>();
            foreach (DTO.GroupUser i in GetAllGroupUsers())
            {
                if (searchBy == "All")
                {
                    list.Add(i);
                }
                else
                {
                    switch (searchBy)
                    {
                        case "Tên nhóm người dùng":
                            if (searchFor != "")
                            {
                                if (i.GroupUserName.ToLower().Contains(searchFor.ToLower()))
                                {
                                    list.Add(i);
                                }
                            }
                            else list.Add(i);
                            break;
                        case "Loại nhóm người dùng":
                            if (searchFor != "")
                            {
                                if (i.TypeName.ToLower().Contains(searchFor.ToLower()))
                                {
                                    list.Add(i);
                                }
                            }
                            else list.Add(i);
                            break;
                    }
                }
            }
            return list;
        }

        //************************************************************************************************//

        // Category
        public List<DTO.Category> GetAllCategorys()
        {
            return ProcessCategory.Instance.LoadAllCategorys();
        }

        public List<DTO.Category> GetListCategory(string cbbitem, string searchFor)
        {
            List<DTO.Category> list = new List<DTO.Category>();
            foreach (DTO.Category i in GetAllCategorys())
            {
                if (cbbitem == "All")
                {
                    list.Add(i);
                }
                else
                {
                    switch (cbbitem)
                    {
                        case "Tên danh mục":
                            if (searchFor != "")
                            {
                                if (i.CategoryName.ToLower().Contains(searchFor.ToLower()))
                                {
                                    list.Add(i);
                                }
                            }
                            else list.Add(i);
                            break;
                        case "Loại danh mục":
                            if (searchFor != "")
                            {
                                if (i.CategoryType.ToLower().Contains(searchFor.ToLower()))
                                {
                                    list.Add(i);
                                }
                            }
                            else list.Add(i);
                            break;
                    }
                }
            }
            return list;
        }

        public List<DTO.Category> GetAllFoodCategorys()
        {
            List<DTO.Category> list = new List<DTO.Category>();
            foreach (DTO.Category i in GetAllCategorys())
            {
                if (i.CategoryType.Equals("Food"))
                {
                    list.Add(i);
                }
            }
            return list;
        }

        public List<DTO.Category> GetAllDrinkCategorys()
        {
            List<DTO.Category> list = new List<DTO.Category>();
            foreach (DTO.Category i in GetAllCategorys())
            {
                if (i.CategoryType.Equals("Drink"))
                {
                    list.Add(i);
                }
            }
            return list;
        }

        public List<DTO.Category> GetAllCardCategorys()
        {
            List<DTO.Category> list = new List<DTO.Category>();
            foreach (DTO.Category i in GetAllCategorys())
            {
                if (i.CategoryType.Equals("Card"))
                {
                    list.Add(i);
                }
            }
            return list;
        }

        public bool CheckCategory(string name)
        {
            foreach (DTO.Category category in GetAllCategorys())
            {
                if (category.CategoryName.Equals(name))
                {
                    return false;
                }
            }
            return true;
        }

        //************************************************************************************************//

        // Food
        public List<DTO.Food> GetAllFoods()
        {
            return ProcessFood.Instance.LoadAllFoods();
        }

        public List<DTO.Food> GetListFood(string searchBy, string searchFor)
        {
            List<DTO.Food> list = new List<DTO.Food>();
            foreach (DTO.Food i in GetAllFoods())
            {
                switch (searchBy)
                {
                    case "All":
                        list.Add(i);
                        break;
                    case "Mã định danh":
                        if (searchFor != "")
                        {
                            if (i.FoodID == Convert.ToInt32(searchFor))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                    case "Tên món ăn":
                        if (searchFor != "")
                        {
                            if (i.Name.ToLower().Contains(searchFor.ToLower()))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                    case "Loại món ăn":
                        if (searchFor != "")
                        {
                            if (i.CategoryName.ToLower().Contains(searchFor.ToLower()))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                }
            }
            return list;
        }

        public DTO.Food GetFoodByID(int ID)
        {
            foreach (DTO.Food i in GetAllFoods())
            {
                if (ID.Equals(i.FoodID))
                {
                    return i;
                }
            }
            return null;
        }

        public bool CheckFoodName(string name)
        {
            foreach (DTO.Food food in GetAllFoods())
            {
                if (food.Name.Equals(name)) return false;
            }
            return true;
        }

        //************************************************************************************************//

        // Drink
        public List<DTO.Drink> GetAllDrinks()
        {
            return ProcessDrink.Instance.LoadAllDrinks();
        }

        public List<DTO.Drink> GetListDrink(string searchBy, string searchFor)
        {
            List<DTO.Drink> list = new List<DTO.Drink>();
            foreach (DTO.Drink i in GetAllDrinks())
            {
                switch (searchBy)
                {
                    case "All":
                        list.Add(i);
                        break;
                    case "Mã định danh":
                        if (searchFor != "")
                        {
                            if (i.DrinkID == Convert.ToInt32(searchFor))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                    case "Tên thức uống":
                        if (searchFor != "")
                        {
                            if (i.Name.ToLower().Contains(searchFor.ToLower()))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                    case "Loại thức uống":
                        if (searchFor != "")
                        {
                            if (i.CategoryName.ToLower().Contains(searchFor.ToLower()))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                }
            }
            return list;
        }

        public DTO.Drink GetDrinkByID(int ID)
        {
            foreach (DTO.Drink i in GetAllDrinks())
            {
                if (ID.Equals(i.DrinkID))
                {
                    return i;
                }
            }
            return null;
        }

        public bool CheckDrinkName(string name)
        {
            foreach (DTO.Drink drink in GetAllDrinks())
            {
                if (drink.Name.Equals(name)) return false;
            }
            return true;
        }

        //************************************************************************************************//

        // Card
        public List<DTO.Card> GetAllCards()
        {
            return ProcessCard.Instance.LoadAllCards();
        }

        public List<DTO.Card> GetListCard(string searchBy, string searchFor)
        {
            List<DTO.Card> list = new List<DTO.Card>();
            foreach (DTO.Card i in GetAllCards())
            {
                switch (searchBy)
                {
                    case "All":
                        list.Add(i);
                        break;
                    case "Mã định danh":
                        if (searchFor != "")
                        {
                            if (i.CardID == Convert.ToInt32(searchFor))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                    case "Tên thẻ cào":
                        if (searchFor != "")
                        {
                            if (i.Name.ToLower().Contains(searchFor.ToLower()))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                    case "Loại thẻ cào":
                        if (searchFor != "")
                        {
                            if (i.CategoryName.ToLower().Contains(searchFor.ToLower()))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                }
            }
            return list;
        }

        public DTO.Card GetCardByID(int ID)
        {
            foreach (DTO.Card i in GetAllCards())
            {
                if (ID.Equals(i.CardID))
                {
                    return i;
                }
            }
            return null;
        }

        public bool CheckCardName(string name)
        {
            foreach (DTO.Card card in GetAllCards())
            {
                if (card.Name.Equals(name)) return false;
            }
            return true;
        }

        //************************************************************************************************//

        // Member
        public List<DTO.Member> GetAllMembers()
        {
            return ProcessMember.Instance.LoadAllMembers();
        }

        public List<DTO.Member> GetListMembers(string cbbitem, string searchFor)
        {
            List<DTO.Member> list = new List<DTO.Member>();
            foreach (DTO.Member i in GetAllMembers())
            {
                if (cbbitem == "All")
                {
                    list.Add(i);
                }
                else if (cbbitem == "Tên tài khoản")
                {
                    if (searchFor != "")
                    {
                        if (i.AccountName.ToLower().Contains(searchFor.ToLower()))
                        {
                            list.Add(i);
                        }
                    }
                    else list.Add(i);
                }
                else
                {
                    if (searchFor != "")
                    {
                        if (i.GroupUserName.ToLower().Contains(searchFor.ToLower()))
                        {
                            list.Add(i);
                        }
                    }
                    else list.Add(i);
                }
            }
            return list;
        }

        public DTO.Member GetMemberByID(int ID)
        {
            foreach (DTO.Member i in GetAllMembers())
            {
                if (ID == i.ID)
                {
                    return i;
                }
            }
            return null;
        }

        public DTO.Member GetMemberByAccountName(string accountName)
        {
            foreach (DTO.Member i in GetAllMembers())
            {
                if (i.AccountName.Equals(accountName))
                {
                    return i;
                }
            }
            return null;
        }

        public bool CheckAccount(string account)
        {
            foreach (DTO.Member i in GetAllMembers())
            {
                if (i.AccountName.Equals(account)) return false;
            }
            return true;
        }

        public int FindIDByAccountName(string account)
        {
            foreach (DTO.Member i in GetAllMembers())
            {
                if (i.AccountName.Equals(account)) return i.ID;
            }
            return -1;
        }

        //************************************************************************************************//

        // LoginMember
        public List<DTO.LoginMember> GetAllLoginMembers()
        {
            return ProcessLoginMember.Instance.LoadAllLoginMembers();
        }

        public List<DTO.LoginMember> GetListLoginMembers(string searchBy, string searchFor)
        {
            List<DTO.LoginMember> list = new List<DTO.LoginMember>();
            foreach (DTO.LoginMember i in GetAllLoginMembers())
            {
                switch (searchBy)
                {
                    case "All":
                        list.Add(i);
                        break;
                    case "Login ID":
                        if (searchFor != "")
                        {
                            if (i.LoginID == Convert.ToInt32(searchFor))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                    case "ID tài khoản":
                        if (searchFor != "")
                        {
                            if (i.MemberID == Convert.ToInt32(searchFor))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                    case "Tên máy trạm":
                        if (searchFor != "")
                        {
                            if (i.ClientName.ToString().ToLower().Contains(searchFor.ToLower()))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                    case "Ngày đăng nhập":
                        if (searchFor != "")
                        {
                            if (i.LoginDate == DateTime.Parse(searchFor))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                }
            }
            return list;
        }

        //************************************************************************************************//

        // MemberInfo
        public List<DTO.MemberInformation> GetAllMemberInfos()
        {
            return ProcessMemberInfo.Instance.LoadAllMemberInfo();
        }

        public DTO.MemberInformation GetMemberInfoByID(int ID)
        {
            foreach (DTO.MemberInformation i in GetAllMemberInfos())
            {
                if (ID == i.MemberID)
                {
                    return i;
                }
            }
            return null;
        }

        //************************************************************************************************//

        // User
        public List<DTO.User> GetAllUsers()
        {
            return ProcessUser.Instance.LoadAllUsers();
        }

        public List<DTO.User> GetListUsers(string searchBy, string searchFor)
        {
            List<DTO.User> list = new List<DTO.User>();
            foreach (DTO.User i in GetAllUsers())
            {
                if (searchBy == "All")
                {
                    list.Add(i);
                }
                else
                {
                    switch (searchBy)
                    {
                        case "Tên người dùng":
                            if (searchFor != "")
                            {
                                if (i.UserName.ToLower().Contains(searchFor.ToLower()))
                                {
                                    list.Add(i);
                                }
                            }
                            else list.Add(i);
                            break;
                        case "Kiểu người dùng":
                            if (searchFor != "")
                            {
                                if (i.Type.ToLower().Contains(searchFor.ToLower()))
                                {
                                    list.Add(i);
                                }
                            }
                            else list.Add(i);
                            break;
                        case "Nhóm người dùng":
                            if (searchFor != "")
                            {
                                if (i.GroupUserName.ToLower().Contains(searchFor.ToLower()))
                                {
                                    list.Add(i);
                                }
                            }
                            else list.Add(i);
                            break;
                        case "Số điện thoại":
                            if (searchFor != "")
                            {
                                if (i.PhoneNumber.ToLower().Contains(searchFor.ToLower()))
                                {
                                    list.Add(i);
                                }
                            }
                            else list.Add(i);
                            break;
                        case "Địa chỉ Email":
                            if (searchFor != "")
                            {
                                if (i.Email.ToLower().Contains(searchFor.ToLower()))
                                {
                                    list.Add(i);
                                }
                            }
                            else list.Add(i);
                            break;
                    }
                }
            }
            return list;
        }

        public DTO.User GetUserByID(int ID)
        {
            foreach (DTO.User i in GetAllUsers())
            {
                if (ID == i.ID)
                {
                    return i;
                }
            }
            return null;
        }

        public bool CheckUser(int ID)
        {
            foreach (DTO.User i in GetAllUsers())
            {
                if (i.ID == ID) return false;
            }
            return true;
        }

        public bool CheckUserPass(int ID, string password)
        {
            foreach (DTO.User user in GetAllUsers())
            {
                if (user.ID != ID && user.LoginPass == password) return false;
            }
            return true;
        }

        public string FindUserNameByID(int ID)
        {
            foreach (DTO.User i in GetAllUsers())
            {
                if (i.ID == ID) return i.UserName;
            }
            return null;
        }

        //************************************************************************************************//

        // Client
        public List<DTO.Client> GetAllClients()
        {
            return ProcessClient.Instance.LoadAllClients();
        }

        public List<DTO.Client> GetAllDisconnect()
        {
            List<DTO.Client> list = new List<DTO.Client>();
            foreach (DTO.Client i in GetAllClients())
            {
                i.Status = "DISCONNECT";
                list.Add(i);
            }
            return list;
        }

        public DTO.Client GetClientByName(string clientName)
        {
            foreach (DTO.Client client in GetAllClients())
            {
                if (client.ClientName.Equals(clientName))
                {
                    return client;
                }
            }
            return null;
        }

        public bool CheckClientName(string clientName)
        {
            foreach (DTO.Client i in GetAllClients())
            {
                if (i.ClientName.Equals(clientName)) return true;
            }
            return false;
        }

        //************************************************************************************************//

        // TransactionDiary
        public List<DTO.TransactionDiary> GetAllTransactionDiaries()
        {
            return ProcessTD.Instance.LoadAllTDs();
        }

        public List<DTO.TransactionDiary> GetListTransactionDiaries(string searchBy, string searchFor)
        {
            List<DTO.TransactionDiary> list = new List<DTO.TransactionDiary>();
            foreach (DTO.TransactionDiary i in GetAllTransactionDiaries())
            {
                switch (searchBy)
                {
                    case "All":
                        list.Add(i);
                        break;
                    case "ID người dùng":
                        if (searchFor != "")
                        {
                            if (i.userID == Convert.ToInt32(searchFor))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                    case "Tên người dùng":
                        if (searchFor != "")
                        {
                            if (i.userName.ToString().ToLower().Contains(searchFor.ToLower()))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                    case "ID tài khoản":
                        if (searchFor != "")
                        {
                            if (i.memberID == Convert.ToInt32(searchFor))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                    case "Ngày nạp":
                        if (searchFor != "")
                        {
                            if (i.TransactionDate == DateTime.Parse(searchFor))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                }
            }
            return list;
        }

        //************************************************************************************************//

        // Bill
        public List<DTO.Bill> GetAllBills()
        {
            return ProcessBill.Instance.LoadAllBills();
        }

        public List<DTO.Bill> GetListBills(string searchBy, string searchFor)
        {
            List<DTO.Bill> list = new List<DTO.Bill>();
            foreach (DTO.Bill i in GetAllBills())
            {
                switch (searchBy)
                {
                    case "All":
                        list.Add(i);
                        break;
                    case "ID người giao dịch":
                        if (searchFor != "")
                        {
                            if (i.FoundedUserID == Convert.ToInt32(searchFor))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                    case "Tên người giao dịch":
                        if (searchFor != "")
                        {
                            if (i.FoundedUserName.ToString().ToLower().Contains(searchFor.ToLower()))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                    case "Ngày giao dịch":
                        if (searchFor != "")
                        {
                            if (i.FoundedDate == DateTime.Parse(searchFor))
                            {
                                list.Add(i);
                            }
                        }
                        else list.Add(i);
                        break;
                }
            }
            return list;
        }
    }
}
