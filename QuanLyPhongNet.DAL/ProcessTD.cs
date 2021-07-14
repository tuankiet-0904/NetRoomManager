using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhongNet.DTO;

namespace QuanLyPhongNet.DAL
{
    public class ProcessTD
    {
        static private ProcessTD _instance;
        static public ProcessTD Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProcessTD();
                return _instance;
            }
            private set { }
        }
        public List<DTO.TransactionDiary> LoadAllTDs()
        {
            using (QuanLyPhongNetDB db = new QuanLyPhongNetDB())
            {
                List<DTO.TransactionDiary> list = new List<DTO.TransactionDiary>();
                list = db.TransactionDiary2.Select(td => new DTO.TransactionDiary
                {
                    userID = (int)td.UserID,
                    userName = td.UserName,
                    memberID = (int)td.memberID,
                    AddMoney = (float)td.AddMoney,
                    AddTime = (TimeSpan)td.AddTime,
                    TransactionDate = (DateTime)td.TransacDate,
                    note = td.Note,
                }).ToList();
                return list;
            }
        }

        public void InsertTD(TransactionDiary2 item)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                objWriter.TransactionDiary2.Add(new TransactionDiary2
                {
                    memberID = item.memberID,
                    UserID = item.UserID,
                    UserName = item.UserName,
                    TransacDate = item.TransacDate,
                    AddTime = item.AddTime,
                    AddMoney = item.AddMoney,
                    Note = item.Note
                });
                objWriter.SaveChanges();
            }
        }

        public void UpdateTD(TransactionDiary2 item)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                TransactionDiary2 objUpdate;
                objUpdate = objWriter.TransactionDiary2.FirstOrDefault(x => x.TD_ID == item.TD_ID);
                objUpdate.memberID = item.memberID;
                objUpdate.UserID = item.UserID;
                objUpdate.TransacDate = item.TransacDate;
                objUpdate.AddMoney = item.AddMoney;
                objUpdate.AddTime = item.AddTime;
                objUpdate.Note = item.Note;
                objWriter.SaveChanges();
            }
        }

        public void DeleteTD(int TD_ID)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                var objDelete = objWriter.TransactionDiary2.Single(x => x.TD_ID == TD_ID);
                objWriter.TransactionDiary2.Remove(objDelete);
                objWriter.SaveChanges();
            }
        }
    }
}
