using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DTO
{
    public class InfoClient
    {
        public string nameClient { get; set; }
        public string stateClient { get; set; }
        public DateTime startTime { get; set; }
        public string remainTime { get; set; }
        public Socket client { get; set; }
        public string nameCustomer { get; set; }
    }
}
