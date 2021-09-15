using DoAnSE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace May_1
{
    public partial class OrderDichVu : Form
    {
        public ClientManager clientManager;

        public OrderDichVu(ClientManager x)
        {
            InitializeComponent();
            clientManager = x;
        }

        //************************************************************************************************//

        // Order dịch vụ
        private void OrderDichVu_Load(object sender, EventArgs e)
        {
            LoadAllFoods();
            LoadAllDrinks();
            LoadAllCards();
            txtNote.Enabled = false;
        }

        public void numeric_ValueChanged(object sender, EventArgs e, String orderName, float orderPrice)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            int orderIndex = IndexOfOrder(orderName);
            if (orderIndex == -1)
            {
                AddNewOrder(orderName, orderPrice);
            }
            else
            {
                if (numeric.Value == 0)
                {
                    RemoveOrder(orderIndex);
                }
                else
                {
                    EditOrder(orderIndex, Convert.ToInt32(numeric.Value));
                }
            }
            CountTotalPrice();
        }

        //************************************************************************************************//

        // Note
        private void txtNote_TextChanged(object sender, EventArgs e)
        {
            if (DGVOrder.SelectedRows[0].Cells[0].Value != null)
            {
                string note = txtNote.Text;
                DGVOrder.CurrentRow.Cells[4].Value = note;
            }
        }

        //************************************************************************************************//

        // Tab Food
        private void LoadAllFoods()
        {
            foreach (String food in clientManager.FoodData)
            {
                String[] foodInfo = food.Split(':');
                PanelItem foodPanel = new PanelItem("Food", foodInfo[0], float.Parse(foodInfo[1]), Convert.ToInt32(foodInfo[2]));
                foodPanel.ValueChanged += numeric_ValueChanged;
                FoodFlowPanel.Controls.Add(foodPanel);
            }
        }

        //************************************************************************************************//

        // Tab Drink
        private void LoadAllDrinks()
        {
            foreach (String drink in clientManager.DrinkData)
            {
                String[] drinkInfo = drink.Split(':');
                PanelItem drinkPanel = new PanelItem("Drink", drinkInfo[0], float.Parse(drinkInfo[1]), Convert.ToInt32(drinkInfo[2]));
                drinkPanel.ValueChanged += numeric_ValueChanged;
                DrinkFlowPanel.Controls.Add(drinkPanel);
            }
        }

        //************************************************************************************************//

        // Tab Card
        private void LoadAllCards()
        {
            foreach (String card in clientManager.CardData)
            {
                String[] cardInfo = card.Split(':');
                PanelItem cardPanel = new PanelItem("Card", cardInfo[0], float.Parse(cardInfo[1]), Convert.ToInt32(cardInfo[2]));
                cardPanel.ValueChanged += numeric_ValueChanged;
                CardFlowPanel.Controls.Add(cardPanel);
            }
        }

        //************************************************************************************************//

        // DataGridView Ordered
        private void AddNewOrder(string orderName, float price)
        {
            DataGridViewRow newRow = (DataGridViewRow)DGVOrder.Rows[0].Clone();
            newRow.Cells[0].Value = orderName;
            newRow.Cells[1].Value = 1;
            newRow.Cells[2].Value = price;
            newRow.Cells[3].Value = price;
            newRow.Cells[4].Value = "";
            DGVOrder.Rows.Add(newRow);
        }

        public void EditOrder(int orderIndex, int soLuong)
        {
            DGVOrder.Rows[orderIndex].Cells[1].Value = soLuong;
            float price = float.Parse(DGVOrder.Rows[orderIndex].Cells[2].Value.ToString());
            DGVOrder.Rows[orderIndex].Cells[3].Value = soLuong * price;
        }

        private void RemoveOrder(int orderIndex)
        {
            DGVOrder.Rows.Remove(DGVOrder.Rows[orderIndex]);
        }

        private void DGVOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVOrder.CurrentRow.Cells[0].Value != null)
            {
                txtNote.Text = DGVOrder.CurrentRow.Cells[4].Value.ToString();
                txtNote.Enabled = true;
            }
            else
            {
                txtNote.Text = null;
                txtNote.Enabled = false;
            }
        }

        private int IndexOfOrder(string orderName)
        {
            foreach (DataGridViewRow i in DGVOrder.Rows)
            {
                if (i.Cells["Order"].Value == null) continue;
                if (i.Cells["Order"].Value.ToString().Equals(orderName)) return i.Index;
            }
            return -1;
        }

        //************************************************************************************************//

        // Button Order
        private void btnOrder_Click(object sender, EventArgs e)
        {
            try
            {
                string order = "";
                foreach (DataGridViewRow i in DGVOrder.Rows)
                {
                    if (i.Cells[0].Value != null)
                    {
                        order += i.Cells[0].Value.ToString() + "-" + i.Cells[1].Value.ToString() + "-" + i.Cells[2].Value.ToString() + "-"
                               + i.Cells[3].Value.ToString() + "-" + i.Cells[4].Value.ToString() + "/";
                    }
                }
                order += txtTotalPrice.Text;
                clientManager.SendOrder(order);
                MessageBox.Show("Order thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //************************************************************************************************//

        // Button Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //************************************************************************************************//

        // Total Price TextBox
        private string RemoveDot(string input)
        {
            string[] split = input.Split('.');
            string res = "";
            foreach (string i in split)
            {
                res += i;
            }
            return res;
        }

        public string AddDot(string input)
        {
            string res = "";
            int index = input.Length;
            while (index > 3)
            {
                index -= 3;
                string sub = input.Substring(index, 3);
                res = "." + sub + res;
            }
            res = input.Substring(0, index) + res;
            return res;
        }

        private void CountTotalPrice()
        {
            int totalPrice = 0;
            foreach (DataGridViewRow i in DGVOrder.Rows)
            {
                if (i.Cells[0].Value != null)
                {
                    totalPrice += Convert.ToInt32(RemoveDot(i.Cells[3].Value.ToString()));
                }
            }
            txtTotalPrice.Text = AddDot(totalPrice.ToString());
        }
    }
}
