using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DAL
{
    public class ProcessFood
    {
        static private ProcessFood _instance;
        static public ProcessFood Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProcessFood();
                return _instance;
            }
            private set { }
        }
        public List<DTO.Food> LoadAllFoods()
        {
            using (QuanLyPhongNetDB db = new QuanLyPhongNetDB())
            {
                List<DTO.Food> list = new List<DTO.Food>();
                list = db.Foods.Select(u => new DTO.Food
                {
                    FoodID = u.FoodID,
                    Name = u.FoodName,
                    CategoryName = u.CategoryName,
                    PriceUnit = (float)u.PriceUnit,
                    UnitLot = u.UnitLot,
                    InventoryNumber = (int)u.InventoryNumber,
                }).ToList();
                return list;
            }
    
        }
        public void InsertFood(string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                objWriter.Foods.Add(new Food
                {
                    FoodName = name,
                    CategoryName = categoryName,
                    PriceUnit = priceUnit,
                    UnitLot = unitLot,
                    InventoryNumber = inventoryNumber
                });
                objWriter.SaveChanges();
            }
        }

        public void UpdateFood(int foodID, string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                Food objUpdate;
                objUpdate = objWriter.Foods.FirstOrDefault(x => x.FoodID == foodID);
                objUpdate.FoodName = name;
                objUpdate.CategoryName = categoryName;
                objUpdate.PriceUnit = priceUnit;
                objUpdate.UnitLot = unitLot;
                objUpdate.InventoryNumber = inventoryNumber;
                objWriter.SaveChanges();
            }
        }

        public void DeleteFood(int foodID)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                var objDelete = objWriter.Foods.Single(x => x.FoodID == foodID);
                objWriter.Foods.Remove(objDelete);
                objWriter.SaveChanges();
            }
        }
    }
}
