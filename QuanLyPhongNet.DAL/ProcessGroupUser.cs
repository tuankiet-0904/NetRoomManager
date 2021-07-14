using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DAL
{
    public class ProcessGroupUser
    {
        static private ProcessGroupUser _instance;
        static public ProcessGroupUser Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProcessGroupUser();
                return _instance;
            }
            private set { }
        }
        public List<DTO.GroupUser> LoadAllGroupUsers()
        {
            using (QuanLyPhongNetDB db = new QuanLyPhongNetDB())
            {
                List<DTO.GroupUser> list = new List<DTO.GroupUser>();
                list = db.GroupUsers.Select(groupUser => new DTO.GroupUser
                {
                    GroupUserName = groupUser.GroupName,
                    TypeName = groupUser.TypeName
                }).ToList();
                return list;
            }

        }

        public void UpdateGroupUser(string groupUserName, string typeName)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                GroupUser objUpdate;
                if ((objUpdate = objWriter.GroupUsers.FirstOrDefault(x => x.GroupName.Equals(groupUserName))) == null)
                {
                    objWriter.GroupUsers.Add(new GroupUser
                    {
                        GroupName = groupUserName,
                        TypeName = typeName
                    });
                    objWriter.SaveChanges();
                }
                else
                {
                    objUpdate.GroupName = groupUserName;
                    objUpdate.TypeName = typeName;
                    objWriter.SaveChanges();
                }
            }
        }

        public void DeleteGroupUser(string groupUserName)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                var objDelete = objWriter.GroupUsers.Single(x => x.GroupName.Equals(groupUserName));
                objWriter.GroupUsers.Remove(objDelete);
                objWriter.SaveChanges();
            }
        }
    }
}
