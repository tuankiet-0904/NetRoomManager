using QuanLyPhongNet.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DAL
{
    public class ProcessMemberInfo
    {
        static private ProcessMemberInfo _instance;
        static public ProcessMemberInfo Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProcessMemberInfo();
                return _instance;
            }
            private set { }
        }

        public List<DTO.MemberInformation> LoadAllMemberInfo()
        {
            using (QuanLyPhongNetDB db = new QuanLyPhongNetDB())
            {
                List<DTO.MemberInformation> list = new List<DTO.MemberInformation>();
                list = db.MemberInformations.Select(u => new DTO.MemberInformation
                {
                    MemberID = u.MemberID,
                    MemberName = u.MemberName,
                    FoundedDate = (DateTime)u.FoundedDate,
                    PhoneNumber = u.PhoneNumber,
                    MemberAddress = u.MemberAddress,
                    Email = u.Email
                }).ToList();
                return list;
            }

        }

        public void InsertMemberInfo(int ID, DateTime currentDate)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                objWriter.MemberInformations.Add(new MemberInformation
                {
                    MemberID = ID,
                    MemberName = null,
                    FoundedDate = currentDate,
                    PhoneNumber = null,
                    MemberAddress = null,
                    Email = null
                });
                objWriter.SaveChanges();
            }
        }
        
        public void UpdateMemberInfo(MemberInformation item)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                MemberInformation objUpdate;
                objUpdate = objWriter.MemberInformations.FirstOrDefault(x => x.MemberID == item.MemberID);
                objUpdate.MemberName = item.MemberName;
                objUpdate.FoundedDate = item.FoundedDate;
                objUpdate.PhoneNumber = item.PhoneNumber;
                objUpdate.MemberAddress = item.MemberAddress;
                objUpdate.Email = item.Email;
                objWriter.SaveChanges();
            }
        }
    }
}
