using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace May_2
{
    public partial class OrderDichVu : Form
    {
        public OrderDichVu()
        {
            InitializeComponent();
        }

        private void OrderDichVu_Load(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
        }

        private void picAdd_Click(object sender, EventArgs e)
        {
            if (DGVOrder.CurrentRow.Cells[0].Value != null)
            {
                DGVOrder.CurrentRow.Cells[1].Value = Convert.ToInt32(DGVOrder.CurrentRow.Cells[1].Value.ToString()) + 1;
                CountPrice();
                CountTotalPrice();
            }
        }

        private void picAdd_MouseHover(object sender, EventArgs e)
        {
            picAdd.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picAdd_MouseLeave(object sender, EventArgs e)
        {
            picAdd.BorderStyle = BorderStyle.None;
        }

        private void picSubtract_Click(object sender, EventArgs e)
        {
            if (DGVOrder.CurrentRow.Cells[0].Value != null)
            {
                int value = Convert.ToInt32(DGVOrder.CurrentRow.Cells[1].Value.ToString());
                if (value > 1)
                {
                    DGVOrder.CurrentRow.Cells[1].Value = value - 1;
                    CountPrice();
                    CountTotalPrice();
                }
            }
        }

        private void picSubtract_MouseHover(object sender, EventArgs e)
        {
            picSubtract.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picSubtract_MouseLeave(object sender, EventArgs e)
        {
            picSubtract.BorderStyle = BorderStyle.None;
        }

        private void listFood_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void listFood_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (listFood.SelectedItems.Count > 0)
            {
                string[] order = listFood.SelectedItems[0].Text.Split('|');
                if (listFood.SelectedItems[0].Checked)
                {
                    if (IndexOfOrder(listFood.SelectedItems[0].Text) == -1)
                    {
                        string price = order[1].Split(' ')[1];
                        AddNewOrder(order[0], price, null);
                        CountTotalPrice();
                    }
                }
                else
                {
                    RemoveOrder(order[0]);
                    CountTotalPrice();
                }
            }
        }

        private void listDrink_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (listDrink.SelectedItems.Count > 0)
            {
                string[] order = listDrink.SelectedItems[0].Text.Split('|');
                if (listDrink.SelectedItems[0].Checked)
                {
                    if (IndexOfOrder(listDrink.SelectedItems[0].Text) == -1)
                    {
                        string price = order[1].Split(' ')[1];
                        AddNewOrder(order[0], price, null);
                        CountTotalPrice();
                    }
                }
                else
                {
                    RemoveOrder(order[0]);
                    CountTotalPrice();
                }
            }
        }

        private void listCard_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (listCard.SelectedItems.Count > 0)
            {
                string[] order = listCard.SelectedItems[0].Text.Split('|');
                if (listCard.SelectedItems[0].Checked)
                {
                    if (IndexOfOrder(listCard.SelectedItems[0].Text) == -1)
                    {
                        string cardPrice = "10.000";
                        AddNewOrder(order[0], null, cardPrice);
                        CountTotalPrice();
                    }
                }
                else
                {
                    RemoveOrder(order[0]);
                    CountTotalPrice();
                }
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

        private void AddNewOrder(string orderName, string price, string cardPrice)
        {
            DataGridViewRow newRow = (DataGridViewRow)DGVOrder.Rows[0].Clone();
            newRow.Cells[0].Value = orderName;
            newRow.Cells[1].Value = 1;
            if (cardPrice == null)
            {
                newRow.Cells[2].Value = RemoveDot(price);
                newRow.Cells[4].Value = "";
            }
            else
            {
                newRow.Cells[2].Value = RemoveDot(cardPrice);
                newRow.Cells[4].Value = "Thẻ cào";
            }
            newRow.Cells[3].Value = newRow.Cells[2].Value;
            DGVOrder.Rows.Add(newRow);
        }

        private void RemoveOrder(string orderName)
        {
            int index = IndexOfOrder(orderName);
            if (index != -1)
            {
                DGVOrder.Rows.Remove(DGVOrder.Rows[index]);
            }
        }

        private void DGVOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVOrder.CurrentRow.Cells[0].Value != null)
            {
                txtNote.Text = DGVOrder.CurrentRow.Cells[4].Value.ToString();
            }
            else
            {
                comboBox1.Enabled = false;
                txtNote.Enabled = false;
                return;
            }
            if (DGVOrder.CurrentRow.Cells[4].Value.ToString().Equals("Thẻ cào"))
            {
                comboBox1.Enabled = true;
                comboBox1.SelectedItem = DGVOrder.CurrentRow.Cells[2].Value.ToString();
                txtNote.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox1.SelectedItem = null;
                txtNote.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGVOrder.CurrentRow.Cells[2].Value = RemoveDot(comboBox1.SelectedItem.ToString());
            CountPrice();
            CountTotalPrice();
        }

        private string RemoveDot(string input)
        {
            string[] split = input.Split('.');
            string res = "";
            foreach(string i in split)
            {
                res += i;
            }
            return res;
        }

        private void txtNote_TextChanged(object sender, EventArgs e)
        {
            if (DGVOrder.CurrentRow.Cells[0].Value != null)
            {
                string note = txtNote.Text;
                DGVOrder.CurrentRow.Cells[4].Value = note;
            }
        }

        private void CountPrice()
        {
            DGVOrder.CurrentRow.Cells[3].Value = Convert.ToInt32(RemoveDot(DGVOrder.CurrentRow.Cells[2].Value.ToString())) *
                Convert.ToInt32(RemoveDot(DGVOrder.CurrentRow.Cells[1].Value.ToString()));
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
            txtTotalPrice.Text = totalPrice.ToString(); 
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {

        }
    }
}
