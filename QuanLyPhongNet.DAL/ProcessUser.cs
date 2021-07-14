using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DAL
{
    public class ProcessUser
    {
        static private ProcessUser _instance;
        static public ProcessUser Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProcessUser();
                return _instance;
            }
            private set { }
        }

        public List<DTO.User> LoadAllUsers()
        {
            using (QuanLyPhongNetDB db = new QuanLyPhongNetDB())
            {
                List<DTO.User> list = new List<DTO.User>();
                list = db.TheUsers.Select(user => new DTO.User
                {
                    ID = (int)user.ID,
                    UserName = user.UserName,
                    Type = user.Type,
                    LoginPass = user.loginPass,
                    GroupUserName = user.GroupUser,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                }).ToList();
                return list;
            }
        }

        public void InsertUser(int id, string name, string loginPass, string phone, string email)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                objWriter.TheUsers.Add(new TheUser
                {
                    ID = id,
                    UserName = name,
                    Type = "Staff",
                    loginPass = loginPass,
                    GroupUser = "Nhân viên",
                    PhoneNumber = phone,
                    Email = email
                });
                objWriter.SaveChanges();
            }
        }

        public void UpdateUser(int ID, string name, string loginPass, string phone, string email)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                TheUser objUpdate = objWriter.TheUsers.FirstOrDefault(x => x.ID == ID);
                objUpdate.UserName = name;
                objUpdate.loginPass = loginPass;
                objUpdate.PhoneNumber = phone;
                objUpdate.Email = email;
                objWriter.SaveChanges();
            }
        }

        public void DeleteUser(int ID)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                var objDelete = objWriter.TheUsers.FirstOrDefault(x => x.ID == ID);
                objWriter.TheUsers.Remove(objDelete);
                objWriter.SaveChanges();
            }
        }
    }
}
