using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyPhongNet.BUS;

namespace QuanLyPhongNet.GUI
{
    public partial class YeuCauDichVu : Form
    {
        ServerManager servermanager;
        private int _ID;
        private int _Cbbitem;
        private int _leftAmount;
        public delegate void MyDel();
        public MyDel d { get; set; }
        public YeuCauDichVu(ServerManager x, int id, int cbbitem, int leftAmount)
        {
            InitializeComponent();
            servermanager = x;
            _ID = id;
            _Cbbitem = cbbitem;
            _leftAmount = leftAmount;
        }

        private void YeuCauDichVu_Load(object sender, EventArgs e)
        {
            LoadSourceToAllControls();
        }

        private void LoadSourceToAllControls()
        {
            if (_Cbbitem == 0)
            {
                DTO.Food item = NetRoomReader.Instance.GetFoodByID(_ID);
                txtName.Text = item.Name;
                txtPrice.Text = item.PriceUnit.ToString();
                txtUnit.Text = item.UnitLot;
                txtGroupName.Text = item.CategoryName;
                txtInventory.Text = item.InventoryNumber.ToString();
            }
            else if (_Cbbitem == 1)
            {
                DTO.Drink item = NetRoomReader.Instance.GetDrinkByID(_ID);
                txtName.Text = item.Name;
                txtPrice.Text = item.PriceUnit.ToString();
                txtUnit.Text = item.UnitLot;
                txtGroupName.Text = item.CategoryName;
                txtInventory.Text = item.InventoryNumber.ToString();
            }
            else if (_Cbbitem == 2)
            {
                DTO.Card item = NetRoomReader.Instance.GetCardByID(_ID);
                txtName.Text = item.Name;
                txtPrice.Text = item.PriceUnit.ToString();
                txtUnit.Text = item.UnitLot;
                txtGroupName.Text = item.CategoryName;
                txtInventory.Text = item.InventoryNumber.ToString();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (order.Value == 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng đặt!", "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (order.Value > _leftAmount)
            {
                MessageBox.Show("Dịch vụ này chỉ còn lại " + _leftAmount + " trong kho!", "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("Còn lại: " + (Int32.Parse(txtInventory.Text) - (int)order.Value).ToString() + " " + txtUnit.Text,
                "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (_Cbbitem == 0)
            {
                DTO.Food food = new DTO.Food()
                {
                    Name = txtName.Text,
                    PriceUnit = float.Parse(txtPrice.Text),
                    UnitLot = txtUnit.Text,
                    CategoryName = txtGroupName.Text,
                    InventoryNumber = Int32.Parse(txtInventory.Text) - (int)order.Value
                };
                NetRoomWritter.Instance.UpdateFood(_ID, food.Name, food.CategoryName, food.PriceUnit, food.UnitLot,
                    food.InventoryNumber);
            }
            else if (_Cbbitem == 1)
            {
                DTO.Drink drink = new DTO.Drink()
                {
                    Name = txtName.Text,
                    PriceUnit = float.Parse(txtPrice.Text),
                    UnitLot = txtUnit.Text,
                    CategoryName = txtGroupName.Text,
                    InventoryNumber = Int32.Parse(txtInventory.Text) - (int)order.Value
                };
                NetRoomWritter.Instance.UpdateDrink(_ID, drink.Name, drink.CategoryName, drink.PriceUnit, drink.UnitLot, drink.InventoryNumber);
            }
            else if (_Cbbitem == 2)
            {
                DTO.Card card = new DTO.Card()
                {
                    Name = txtName.Text,
                    PriceUnit = float.Parse(txtPrice.Text),
                    UnitLot = txtUnit.Text,
                    CategoryName = txtGroupName.Text,
                    InventoryNumber = Int32.Parse(txtInventory.Text) - (int)order.Value
                };
                NetRoomWritter.Instance.UpdateCard(_ID, card.Name, card.CategoryName, card.PriceUnit, card.UnitLot, card.InventoryNumber);
            }
            DAL.Bill bill = new DAL.Bill();
            bill.FoundedDate = DateTime.Now;
            bill.FoundedUserID = servermanager.MemberID;
            bill.FoundedUserName = NetRoomReader.Instance.FindUserNameByID(servermanager.MemberID);
            bill.PriceTotal = float.Parse(txtPrice.Text) * (int)order.Value;
            NetRoomWritter.Instance.InsertBill(bill);
            d();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
