using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DTO
{
    public class LoginMember
    {
        public int LoginID { get; set; }
        public int MemberID { get; set; }
        public string ClientName { get; set; }
        public DateTime LoginDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan UseTime { get; set; }
        public TimeSpan LeftTime { get; set; }
    }
}
