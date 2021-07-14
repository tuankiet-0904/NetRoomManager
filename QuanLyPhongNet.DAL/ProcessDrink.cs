using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DAL
{
    public class ProcessDrink
    {
        static private ProcessDrink _instance;
        static public ProcessDrink Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProcessDrink();
                return _instance;
            }
            private set { }
        }

        public List<DTO.Drink> LoadAllDrinks()
        {
            using (QuanLyPhongNetDB db = new QuanLyPhongNetDB())
            {
                List<DTO.Drink> list = new List<DTO.Drink>();
                list = db.Drinks.Select(drink => new DTO.Drink
                {
                    DrinkID = drink.DrinkID,
                    Name = drink.DrinkName,
                    CategoryName = drink.CategoryName,
                    PriceUnit = (float)drink.PriceUnit,
                    UnitLot = drink.UnitLot,
                    InventoryNumber = (int)drink.InventoryNumber,
                }).ToList();
                return list;
            }

        }
        public void InsertDrink(string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                objWriter.Drinks.Add(new Drink
                {
                    DrinkName = name,
                    CategoryName = categoryName,
                    PriceUnit = priceUnit,
                    UnitLot = unitLot,
                    InventoryNumber = inventoryNumber
                });
                objWriter.SaveChanges();
            }
        }

        public void UpdateDrink(int drinkID, string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                Drink objUpdate;
                objUpdate = objWriter.Drinks.FirstOrDefault(x => x.DrinkID == drinkID);
                objUpdate.DrinkName = name;
                objUpdate.CategoryName = categoryName;
                objUpdate.PriceUnit = priceUnit;
                objUpdate.UnitLot = unitLot;
                objUpdate.InventoryNumber = inventoryNumber;
                objWriter.SaveChanges();
            }
        }
        public void DeleteDrink(int drinkID)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                var objDelete = objWriter.Drinks.Single(x => x.DrinkID == drinkID);
                objWriter.Drinks.Remove(objDelete);
                objWriter.SaveChanges();
            }
        }
    }
}
