using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DTO
{
    public class Member
    {
        public int ID { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set;  }
        public string GroupUserName { get; set; }
        public TimeSpan TimeInAccount { get; set; }
        public float CurrentMoney { get; set; }
        public string Status { get; set; }
    }
}
