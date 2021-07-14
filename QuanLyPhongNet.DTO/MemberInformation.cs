using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DTO
{
    public class MemberInformation
    {
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public DateTime FoundedDate { get; set; }
        public string PhoneNumber { get; set; }
        public string MemberAddress { get; set; }
        public string Email { get; set; }
    }
}
