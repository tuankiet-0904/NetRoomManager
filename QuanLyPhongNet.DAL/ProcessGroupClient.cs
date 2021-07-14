using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DAL
{
    public class ProcessGroupClient
    {
        static private ProcessGroupClient _instance;
        static public ProcessGroupClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProcessGroupClient();
                return _instance;
            }
            private set { }
        }
        public List<DTO.GroupClient> LoadAllGroupClients()
        {
            using (QuanLyPhongNetDB db = new QuanLyPhongNetDB())
            {
                List<DTO.GroupClient> list = new List<DTO.GroupClient>();
                list = db.GroupClients.Select(groupClient => new DTO.GroupClient
                {
                    GroupClientName = groupClient.GroupName,
                    Description = groupClient.Discription,
                }).ToList();
                return list;
            }

        }

        public void InsertGroupClient(string groupClientName, string description)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                objWriter.GroupClients.Add(new GroupClient
                {
                    GroupName = groupClientName,
                    Discription = description
                });
                objWriter.SaveChanges();
            }
        }

        public void UpdateGroupClient(string groupClientName, string description)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                GroupClient objUpdate;
                objUpdate = objWriter.GroupClients.FirstOrDefault(x => x.GroupName.Equals(groupClientName));
                objUpdate.GroupName = groupClientName;
                objUpdate.Discription = description;
                objWriter.SaveChanges();
            }
        }

        public void DeleteGroupClient(string groupClientName)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                var objDelete = objWriter.GroupClients.Single(x => x.GroupName.Equals(groupClientName));
                objWriter.GroupClients.Remove(objDelete);
                objWriter.SaveChanges();
            }
        }
    }
}
