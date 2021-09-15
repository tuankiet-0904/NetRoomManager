using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DTO
{
    public class Bill
    {
        
        private int foundedUserID;
        private string foundedUserName;
        private DateTime foundedDate;
        private float priceTotal;

        public int FoundedUserID
        {
            get
            {
                return foundedUserID;
            }

            set
            {
                foundedUserID = value;
            }
        }

        public string FoundedUserName
        {
            get
            {
                return foundedUserName;
            }

            set
            {
                foundedUserName = value;
            }
        }

        public DateTime FoundedDate
        {
            get
            {
                return foundedDate;
            }

            set
            {
                foundedDate = value;
            }
        }

        public float PriceTotal
        {
            get
            {
                return priceTotal;
            }

            set
            {
                priceTotal = value;
            }
        }
    }
}
