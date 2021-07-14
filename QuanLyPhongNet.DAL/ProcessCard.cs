using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DAL
{
    public class ProcessCard
    {
        static private ProcessCard _instance;
        static public ProcessCard Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProcessCard();
                return _instance;
            }
            private set { }
        }

        public List<DTO.Card> LoadAllCards()
        {
            using (QuanLyPhongNetDB db = new QuanLyPhongNetDB())
            {
                List<DTO.Card> list = new List<DTO.Card>();
                list = db.TheCards.Select(card => new DTO.Card
                {
                    CardID = card.CardID,
                    Name = card.CardName,
                    CategoryName = card.CategoryName,
                    PriceUnit = (float)card.PriceUnit,
                    UnitLot = card.UnitLot,
                    InventoryNumber = (int)card.InventoryNumber,
                }).ToList();
                return list;
            }
        }
        public void InsertCard(string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                objWriter.TheCards.Add(new TheCard
                {
                    CardName = name,
                    CategoryName = categoryName,
                    PriceUnit = priceUnit,
                    UnitLot = unitLot,
                    InventoryNumber = inventoryNumber
                });
                objWriter.SaveChanges();
            }
        }

        public void UpdateCard(int cardID, string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                TheCard objUpdate;
                objUpdate = objWriter.TheCards.FirstOrDefault(x => x.CardID == cardID);
                objUpdate.CardName = name;
                objUpdate.CategoryName = categoryName;
                objUpdate.PriceUnit = priceUnit;
                objUpdate.UnitLot = unitLot;
                objUpdate.InventoryNumber = inventoryNumber;
                objWriter.SaveChanges();
            }
        }

        public void DeleteCard(int cardID)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                var objDelete = objWriter.TheCards.Single(x => x.CardID == cardID);
                objWriter.TheCards.Remove(objDelete);
                objWriter.SaveChanges();
            }
        }
    }
}
