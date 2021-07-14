using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DTO
{
    public class Category
    {
        private string categoryName;
        private string categoryType;

        public string CategoryName
        {
            get
            {
                return categoryName;
            }

            set
            {
                categoryName = value;
            }
        }
        public string CategoryType
        {
            get
            {
                return categoryType;
            }

            set
            {
                categoryType = value;
            }
        }
    }
}
