using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DTO
{
    public class User
    {
        private string userName;
        private string type;
        private string loginPass;
        private string groupUserName;
        private string phoneNumber;
        private string email;
        public int ID { get; set; }

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public string LoginPass
        {
            get
            {
                return loginPass;
            }

            set
            {
                loginPass = value;
            }
        }

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

        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }

            set
            {
                phoneNumber = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }
    }
}
