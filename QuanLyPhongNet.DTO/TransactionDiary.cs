using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DTO
{
    public class TransactionDiary
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public int memberID { get; set; }
        public DateTime TransactionDate { get; set; }
        public TimeSpan AddTime { get; set; }
        public float AddMoney { get; set; }
        public string note { get; set; }
    }
}
