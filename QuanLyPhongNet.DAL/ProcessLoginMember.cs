using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DAL
{
    public class ProcessLoginMember
    {
        static private ProcessLoginMember _instance;
        static public ProcessLoginMember Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProcessLoginMember();
                return _instance;
            }
            private set { }
        }

        public List<DTO.LoginMember> LoadAllLoginMembers()
        {
            using (QuanLyPhongNetDB db = new QuanLyPhongNetDB())
            {
                List<DTO.LoginMember> list = new List<DTO.LoginMember>();
                list = db.LoginMembers.Select(loginMember => new DTO.LoginMember
                {
                    LoginID = loginMember.LoginID,
                    MemberID = (int)loginMember.MemberID,
                    ClientName = loginMember.ClientName,
                    LoginDate = (DateTime)loginMember.LoginDate,
                    StartTime = (TimeSpan)loginMember.StartTime,
                    UseTime = (TimeSpan)loginMember.UseTime,
                    LeftTime = (TimeSpan)loginMember.LeftTime
                }).ToList();
                return list;
            }
        }

        public void InsertLoginMember(LoginMember item)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                objWriter.LoginMembers.Add(new LoginMember
                {
                    LoginID = item.LoginID,
                    MemberID = item.MemberID,
                    ClientName = item.ClientName,
                    LoginDate = item.LoginDate,
                    StartTime = item.StartTime,
                    UseTime = item.UseTime,
                    LeftTime = item.LeftTime
                });
                objWriter.SaveChanges();
            }
        }

        public void UpdateLoginMember(int loginID, TimeSpan useTime, TimeSpan leftTime)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                LoginMember objUpdate;
                objUpdate = objWriter.LoginMembers.FirstOrDefault(x => x.LoginID == loginID);
                objUpdate.UseTime = useTime;
                objUpdate.LeftTime = leftTime;
                objWriter.SaveChanges();
            }
        }
    }
}
