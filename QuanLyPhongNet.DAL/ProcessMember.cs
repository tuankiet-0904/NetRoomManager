
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyPhongNet.DAL
{
    public class ProcessMember
    {
        static private ProcessMember _instance;
        static public ProcessMember Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProcessMember();
                return _instance;
            }
            private set { }
        }
        public List<DTO.Member> LoadAllMembers()
        {
            using (QuanLyPhongNetDB db = new QuanLyPhongNetDB())
            {
                List<DTO.Member> list = new List<DTO.Member>();
                list = db.Members.Select(member => new DTO.Member
                {
                    ID = member.MemberID,
                    AccountName = member.AccountName,
                    Password = member.Password,
                    CurrentMoney = (float)member.CurrentMoney,
                    GroupUserName = member.GroupUser,
                    Status = member.StatusAccount,
                    TimeInAccount = (TimeSpan)member.CurrentTime,
                }).ToList();
                return list;
            }
        }
        public void InsertMember(Member item)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                objWriter.Members.Add(new Member
                {
                    AccountName = item.AccountName,
                    Password = item.Password,
                    GroupUser = item.GroupUser,
                    CurrentTime = item.CurrentTime,
                    CurrentMoney = item.CurrentMoney,
                    StatusAccount = item.StatusAccount
                });
                objWriter.SaveChanges();
            }
        }

        public void UpdateMember(Member item)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                Member objUpdate;
                objUpdate = objWriter.Members.FirstOrDefault(x => x.AccountName == item.AccountName);
                objUpdate.Password = item.Password;
                objUpdate.GroupUser = item.GroupUser;
                objUpdate.CurrentTime = item.CurrentTime;
                objUpdate.CurrentMoney = item.CurrentMoney;
                objUpdate.StatusAccount = item.StatusAccount;
                objWriter.SaveChanges();
            }
        }

        public void DeleteMember(int memberID)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                var objDelete = objWriter.Members.Single(x => x.MemberID == memberID);
                objWriter.Members.Remove(objDelete);
                objWriter.SaveChanges();
            }
        }
    }
}
