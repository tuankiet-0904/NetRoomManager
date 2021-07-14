using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DAL
{
    public class ProcessBill
    {
        static private ProcessBill _instance;
        static public ProcessBill Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProcessBill();
                return _instance;
            }
            private set { }
        }

        public void CreateNewBill(Bill item)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                objWriter.Bills.Add(new Bill
                {
                    FoundedUserID = item.FoundedUserID,
                    FoundedUserName = item.FoundedUserName,
                    FoundedDate = item.FoundedDate,
                    PriceTotal = item.PriceTotal,
                });
                objWriter.SaveChanges();
            }
        }
        public List<DTO.Bill> LoadAllBills()
        {
            using (QuanLyPhongNetDB db = new QuanLyPhongNetDB())
            {
                List<DTO.Bill> list = new List<DTO.Bill>();
                list = db.Bills.Select(bill => new DTO.Bill
                {
                    FoundedUserID = (int)bill.FoundedUserID,
                    FoundedUserName = bill.FoundedUserName,
                    FoundedDate = bill.FoundedDate.Value,
                    PriceTotal = (float)bill.PriceTotal
                }).ToList();
                return list;
            }
        }
    }
}
