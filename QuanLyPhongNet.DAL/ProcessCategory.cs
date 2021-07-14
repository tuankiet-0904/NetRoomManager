using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DAL
{
    public class ProcessCategory
    {
        static private ProcessCategory _instance;
        static public ProcessCategory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProcessCategory();
                return _instance;
            }
            private set { }
        }

        public List<DTO.Category> LoadAllCategorys()
        {
            using (QuanLyPhongNetDB db = new QuanLyPhongNetDB())
            {
                List<DTO.Category> list = new List<DTO.Category>();
                list = db.Categories.Select(category => new DTO.Category
                {
                    CategoryName = category.CategoryName,
                    CategoryType = category.CategotyType
                }).ToList();
                return list;
            }
        }
        public void InsertCatergory(string name, string type)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                objWriter.Categories.Add(new Category
                {
                    CategoryName = name,
                    CategotyType = type
                });
                objWriter.SaveChanges();
            }
        }

        public void UpdateCategory(string name, string newName, string newType)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                Category objUpdate;
                objUpdate = objWriter.Categories.FirstOrDefault(x => x.CategoryName.Equals(name));
                objUpdate.CategoryName = newName;
                objUpdate.CategotyType = newType;
                objWriter.SaveChanges();
            }
        }

        public void DeleteCategory(string name)
        {
            using (  QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                var objDelete = objWriter.Categories.Single(x => x.CategoryName.Equals(name));
                objWriter.Categories.Remove(objDelete);
                objWriter.SaveChanges();
            }
        }
    }
}
