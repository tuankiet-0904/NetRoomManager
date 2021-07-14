using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DTO
{
    public class GroupUser
    {
        private string groupUserName;
        private string typeName;

        public string GroupUserName
        {
            get
            {
                return groupUserName;
            }

            set
            {
                groupUserName = value;
            }
        }

        public string TypeName
        {
            get
            {
                return typeName;
            }

            set
            {
                typeName = value;
            }
        }
    }
}
