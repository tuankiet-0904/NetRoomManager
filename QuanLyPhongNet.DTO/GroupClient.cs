using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DTO
{
    public class GroupClient
    {
        private string groupClientName;
        private string description;

        public string GroupClientName
        {
            get
            {
                return groupClientName;
            }

            set
            {
                groupClientName = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }
    }
}
