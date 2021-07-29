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
    public partial class QuanLyDanhMuc : Form
    {
        private NetRoomReader objReader;
        private NetRoomWritter objWriter;
        private const int TAB_FOOD = 0;
        private const int TAB_DRINK = 1;
        private const int TAB_CARD = 2;
        private const int TAB_CATEGORY = 3;
        public QuanLyDanhMuc()
        {
            InitializeComponent();
            objReader = new NetRoomReader();
            objWriter = new NetRoomWritter();    
        }

        //************************************************************************************************//

        // Quản lý danh mục
        private void QuanLyDanhMuc_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            cboSearch.Items.AddRange(new CBBItem[]
            {
                new CBBItem(0, "All"),
                new CBBItem(1, "Mã định danh"),
                new CBBItem(2, "Tên món ăn")
            });
            cboSearch.SelectedIndex = 0;
            LoadSourceToDRGV();
            LoadSourceToCBO();
            grbInformation.ForeColor = Color.Blue;
        }

        private void LoadSourceToDRGV()
        {
            switch (tab.SelectedIndex)
            {
                case TAB_FOOD:
                    drgvInformation.DataSource = objReader.GetAllFoods();
                    drgvInformation.Columns[0].HeaderText = "Mã Định Danh";
                    drgvInformation.Columns[0].Width = 100;
                    drgvInformation.Columns[1].HeaderText = "Tên Món Ăn";
                    drgvInformation.Columns[2].HeaderText = "Thuộc Loại";
                    drgvInformation.Columns[3].HeaderText = "Đơn Giá";
                    drgvInformation.Columns[4].HeaderText = "Đơn Vị Tính";
                    drgvInformation.Columns[5].HeaderText = "Số Lượng Tồn";
                    break;
                case TAB_DRINK:
                    drgvInformation.DataSource = objReader.GetAllDrinks();
                    drgvInformation.Columns[0].HeaderText = "Mã Định Danh";
                    drgvInformation.Columns[0].Width = 100;
                    drgvInformation.Columns[1].HeaderText = "Tên Nước Uống";
                    drgvInformation.Columns[2].HeaderText = "Thuộc Loại";
                    drgvInformation.Columns[3].HeaderText = "Đơn Giá";
                    drgvInformation.Columns[4].HeaderText = "Đơn Vị Tính";
                    drgvInformation.Columns[5].HeaderText = "Số Lượng Tồn";
                    break;
                case TAB_CARD:
                    drgvInformation.DataSource = objReader.GetAllCards();
                    drgvInformation.Columns[0].HeaderText = "Mã Định Danh";
                    drgvInformation.Columns[0].Width = 100;
                    drgvInformation.Columns[1].HeaderText = "Tên Thẻ";
                    drgvInformation.Columns[2].HeaderText = "Thuộc Loại";
                    drgvInformation.Columns[3].HeaderText = "Đơn Giá";
                    drgvInformation.Columns[4].HeaderText = "Đơn Vị Tính";
                    drgvInformation.Columns[5].HeaderText = "Số Lượng Tồn";
                    break;
                case TAB_CATEGORY:
                    drgvInformation.DataSource = objReader.GetAllCategorys();
                    drgvInformation.Columns[0].HeaderText = "Tên Loại Danh Mục";
                    drgvInformation.Columns[1].HeaderText = "Loại";
                    break;
            }
            drgvInformation.Refresh();
            LoadSourceToCBO();
            drgvInformation.ClearSelection();
        }

        private void LoadSourceToCBO()
        {
            cboFoodCategory.DataSource = objReader.GetAllFoodCategorys();
            cboFoodCategory.DisplayMember = "CategoryName";
            cboFoodCategory.ValueMember = "CategoryName";
            cboDrinkCategory.DataSource = objReader.GetAllDrinkCategorys();
            cboDrinkCategory.DisplayMember = "CategoryName";
            cboDrinkCategory.ValueMember = "CategoryName";
            cboCardCategory.DataSource = objReader.GetAllCardCategorys();
            cboCardCategory.DisplayMember = "CategoryName";
            cboCardCategory.ValueMember = "CategoryName";
        }

        private void picSearch_Click(object sender, EventArgs e)
        {
            switch (tab.SelectedIndex)
            {
                case TAB_FOOD:
                    drgvInformation.DataSource = objReader.GetListFood(cboSearch.SelectedItem.ToString(), txtSearch.Text);
                    break;
                case TAB_DRINK:
                    drgvInformation.DataSource = objReader.GetListDrink(cboSearch.SelectedItem.ToString(), txtSearch.Text);
                    break;
                case TAB_CARD:
                    drgvInformation.DataSource = objReader.GetListCard(cboSearch.SelectedItem.ToString(), txtSearch.Text);
                    break;
                case TAB_CATEGORY:
                    drgvInformation.DataSource = objReader.GetListCategory(cboSearch.SelectedItem.ToString(), txtSearch.Text);
                    break;
            }
        }

        private void picDelete_Click(object sender, EventArgs e)
        {
            DeleteProcess();
        }

        private void AddOrUpdate_Click(object sender, EventArgs e)
        {
            if (tab.SelectedIndex == TAB_CATEGORY)
            {
                if (!txtCategoryType.Text.Equals("Food") && !txtCategoryType.Text.Equals("Drink") && !txtCategoryType.Text.Equals("Card"))
                {
                    MessageBox.Show("Bạn đã nhập sai loại danh mục!\nVui lòng kiểm tra lại!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (NetRoomReader.Instance.CheckCategory(txtCategoryName.Text))
                {
                    if (MessageBox.Show("Danh mục này chưa tồn tại!\nBạn có muốn thêm danh mục mới không?", "Chú ý!",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        AddProcess();
                    }
                }
                else
                {
                    if (MessageBox.Show("Danh mục này đã tồn tại!\nBạn có muốn cập nhật không?", "Chú ý!",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        UpdateProcess();
                    }
                }
            }
            else
            {
                string name;
                switch (tab.SelectedIndex)
                {
                    case TAB_FOOD:
                        name = txtFoodName.Text;
                        if (NetRoomReader.Instance.CheckFoodName(name))
                        {
                            if (MessageBox.Show("Món ăn này chưa tồn tại!\nBạn có muốn thêm món ăn mới không?", "Chú ý!",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                AddProcess();
                            }
                        }
                        else
                        {
                            if (MessageBox.Show("Món ăn này đã tồn tại!\nBạn có muốn cập nhật không?", "Chú ý!",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                UpdateProcess();
                            }
                        }
                        break;
                    case TAB_DRINK:
                        name = txtDrinkName.Text;
                        if (NetRoomReader.Instance.CheckDrinkName(name))
                        {
                            if (MessageBox.Show("Thức uống này chưa tồn tại!\nBạn có muốn thêm thức uống mới không?", "Chú ý!",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                AddProcess();
                            }
                        }
                        else
                        {
                            if (MessageBox.Show("Thức uống này đã tồn tại!\nBạn có muốn cập nhật không?", "Chú ý!",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                UpdateProcess();
                            }
                        }
                        break;
                    case TAB_CARD:
                        name = txtCardName.Text;
                        if (NetRoomReader.Instance.CheckCardName(name))
                        {
                            if (MessageBox.Show("Thẻ nạp này chưa tồn tại!\nBạn có muốn thêm thẻ nạp mới không?", "Chú ý!",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                AddProcess();
                            }
                        }
                        else
                        {
                            if (MessageBox.Show("Thẻ nạp này đã tồn tại!\nBạn có muốn cập nhật không?", "Chú ý!",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                UpdateProcess();
                            }
                        }
                        break;
                }
            }
        }

        private void picExit_Click(object sender, EventArgs e)
        {
            GiaoDienLuaChon frmOption = new GiaoDienLuaChon();
            this.Hide();
            frmOption.ShowDialog();
        }

        private void AddProcess()
        {
            TabPage tp = tab.SelectedTab;
            if (!CheckValidInput(tp))
            {
                MessageBox.Show("Bạn chưa điền đầy đủ thông tin!", "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            switch (tab.SelectedIndex)
            {
                case TAB_FOOD:
                    objWriter.InsertFood(txtFoodName.Text, cboFoodCategory.Text, float.Parse(txtPriceUnitOfFood.Text), txtUnitLotOfFood.Text, int.Parse(txtInventoryNumberOfFood.Text));
                    break;
                case TAB_DRINK:
                    objWriter.InsertDrink(txtDrinkName.Text, cboDrinkCategory.Text, float.Parse(txtPriceUnitOfDrink.Text), txtUnitLotOfDrink.Text, int.Parse(txtInventoryNumberOfDrink.Text));
                    break;
                case TAB_CARD:
                    objWriter.InsertCard(txtCardName.Text, cboCardCategory.Text, float.Parse(txtPriceUnitOfCard.Text), txtUnitLotOfCard.Text, int.Parse(txtInventoryNumberOfCard.Text));
                    break;
                case TAB_CATEGORY:
                    objWriter.InsertCategory(txtCategoryName.Text, txtCategoryType.Text);
                    break;
            }
            ResetControl(tp);
            LoadSourceToDRGV();
            LoadSourceToCBO();
            MessageBox.Show("Thêm thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void UpdateProcess()
        {
            TabPage tp = tab.SelectedTab;
            if (!CheckValidInput(tp))
            {
                MessageBox.Show("Bạn chưa điền đầy đủ thông tin!", "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            switch (tab.SelectedIndex)
            {
                case TAB_FOOD:
                    objWriter.UpdateFood(int.Parse(drgvInformation.CurrentRow.Cells[0].Value.ToString()), txtFoodName.Text, cboFoodCategory.Text, float.Parse(txtPriceUnitOfFood.Text), txtUnitLotOfFood.Text, int.Parse(txtInventoryNumberOfFood.Text));
                    break;
                case TAB_DRINK:
                    objWriter.UpdateDrink(int.Parse(drgvInformation.CurrentRow.Cells[0].Value.ToString()), txtDrinkName.Text, cboDrinkCategory.Text, float.Parse(txtPriceUnitOfDrink.Text), txtUnitLotOfDrink.Text, int.Parse(txtInventoryNumberOfDrink.Text));
                    break;
                case TAB_CARD:
                    objWriter.UpdateCard(int.Parse(drgvInformation.CurrentRow.Cells[0].Value.ToString()), txtCardName.Text, cboCardCategory.Text, float.Parse(txtPriceUnitOfCard.Text), txtUnitLotOfCard.Text, int.Parse(txtInventoryNumberOfCard.Text));
                    break;
                case TAB_CATEGORY:
                    objWriter.UpdateCategory(drgvInformation.CurrentRow.Cells[0].Value.ToString(), txtCategoryName.Text, txtCategoryType.Text);
                    break;
            }

            TabPage tp = tab.SelectedTab;

            ResetControl(tp);
            LoadSourceToCBO();
            LoadSourceToDRGV();
            MessageBox.Show("Cập nhật thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        public void DeleteProcess()
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mục này?", "Thông Báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                TabPage tp = tab.SelectedTab;
                switch (tab.SelectedIndex)
                {
                    case TAB_FOOD:
                        objWriter.DeleteFood(int.Parse(drgvInformation.CurrentRow.Cells[0].Value.ToString()));
                        MessageBox.Show("Xóa thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case TAB_DRINK:
                        objWriter.DeleteDrink(int.Parse(drgvInformation.CurrentRow.Cells[0].Value.ToString()));
                        MessageBox.Show("Xóa thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case TAB_CARD:
                        objWriter.DeleteCard(int.Parse(drgvInformation.CurrentRow.Cells[0].Value.ToString()));
                        MessageBox.Show("Xóa thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case TAB_CATEGORY:
                        try
                        {
                            objWriter.DeleteCategory(drgvInformation.CurrentRow.Cells[0].Value.ToString());
                            MessageBox.Show("Xóa thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            MessageBox.Show("Bạn không thể xóa danh mục này!\nHãy xóa tất cả các dịch vụ của danh mục này trước!", "Lỗi!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        break;
                }
                ResetControl(tp);
                LoadSourceToDRGV();
                LoadSourceToCBO();
            }
        }

        private void tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cboSearch.Items.Clear();
            switch (tab.SelectedIndex)
            {
                case TAB_CATEGORY:
                    cboSearch.Items.AddRange(new CBBItem[]
                    {
                        new CBBItem(0, "All"),
                        new CBBItem(1, "Tên danh mục"),
                        new CBBItem(2, "Loại danh mục")
                    });
                    break;
                case TAB_FOOD:
                    cboSearch.Items.AddRange(new CBBItem[]
                    {
                        new CBBItem(0, "All"),
                        new CBBItem(1, "Mã định danh"),
                        new CBBItem(2, "Tên món ăn")
                    });
                    break;
                case TAB_DRINK:
                    cboSearch.Items.AddRange(new CBBItem[]
                    {
                        new CBBItem(0, "All"),
                        new CBBItem(1, "Mã định danh"),
                        new CBBItem(2, "Tên thức uống")
                    });
                    break;
                case TAB_CARD:
                    cboSearch.Items.AddRange(new CBBItem[]
                    {
                        new CBBItem(0, "All"),
                        new CBBItem(1, "Mã định danh"),
                        new CBBItem(2, "Tên thẻ cào")
                    });
                    break;
            }
            cboSearch.SelectedIndex = 0;
            LoadSourceToCBO();
            LoadSourceToDRGV();
            TabPage tb = tab.SelectedTab;
            switch (tab.SelectedIndex)
            {
                case TAB_FOOD:
                    grbInformation.ForeColor = Color.Blue;
                    break;
                case TAB_DRINK:
                    grbInformation.ForeColor = Color.Chocolate;
                    break;
                case TAB_CARD:
                    grbInformation.ForeColor = Color.Indigo;
                    break;
                case TAB_CATEGORY:
                    grbInformation.ForeColor = Color.DarkRed;
                    break;
            }
        }

        private void cboSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtSearch.Focus();
        }

        private void ResetControl(TabPage currentTAB)
        {
            foreach (Control c in currentTAB.Controls)
            {
                if (c is TextBox)
                    (c as TextBox).ResetText();
                else if (c is ComboBox)
                {
                    (c as ComboBox).Text = "--Lựa Chọn--";
                    (c as ComboBox).ForeColor = Color.Blue;
                }
            }
        }

        private bool CheckValidInput(TabPage currentTAB)
        {
            int countFilled = currentTAB.Controls.OfType<TextBox>().Count(x => string.IsNullOrEmpty(x.Text));
            if (countFilled > 0)
                return false;
            return true;
        }

        private void drgvInformation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (tab.SelectedIndex)
            {
                case TAB_FOOD:
                    {
                        txtFoodName.Text = drgvInformation.CurrentRow.Cells[1].Value.ToString();
                        cboFoodCategory.Text = drgvInformation.CurrentRow.Cells[2].Value.ToString();
                        txtPriceUnitOfFood.Text = drgvInformation.CurrentRow.Cells[3].Value.ToString();
                        txtInventoryNumberOfFood.Text = drgvInformation.CurrentRow.Cells[5].Value.ToString();
                        txtUnitLotOfFood.Text = drgvInformation.CurrentRow.Cells[4].Value.ToString();
                        break;
                    }
                case TAB_DRINK:
                    {
                        txtDrinkName.Text = drgvInformation.CurrentRow.Cells[1].Value.ToString();
                        cboDrinkCategory.Text = drgvInformation.CurrentRow.Cells[2].Value.ToString();
                        txtPriceUnitOfDrink.Text = drgvInformation.CurrentRow.Cells[3].Value.ToString();
                        txtInventoryNumberOfDrink.Text = drgvInformation.CurrentRow.Cells[5].Value.ToString();
                        txtUnitLotOfDrink.Text = drgvInformation.CurrentRow.Cells[4].Value.ToString();
                        break;
                    }
                case TAB_CARD:
                    {
                        txtCardName.Text = drgvInformation.CurrentRow.Cells[1].Value.ToString();
                        cboCardCategory.Text = drgvInformation.CurrentRow.Cells[2].Value.ToString();
                        txtPriceUnitOfCard.Text = drgvInformation.CurrentRow.Cells[3].Value.ToString();
                        txtInventoryNumberOfCard.Text = drgvInformation.CurrentRow.Cells[5].Value.ToString();
                        txtUnitLotOfCard.Text = drgvInformation.CurrentRow.Cells[4].Value.ToString();
                        break;
                    }
                case TAB_CATEGORY:
                    {
                        txtCategoryName.Text = drgvInformation.CurrentRow.Cells[0].Value.ToString();
                        txtCategoryType.Text = drgvInformation.CurrentRow.Cells[1].Value.ToString();
                        break;
                    }
            }
        }
        
        private void drgvInformation_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TabPage tb = tab.SelectedTab;
            ResetControl(tb);
            drgvInformation.ClearSelection();
        }

        //************************************************************************************************//

        // Mouse Event Handler

        private void SearchMouseHoverEventHandler(object sender, EventArgs e)
        {
            picSearch.BorderStyle = BorderStyle.Fixed3D;
        }

        private void SearchMouseLeaveEventHandler(object sender, EventArgs e)
        {
            picSearch.BorderStyle = BorderStyle.None;
        }

        private void AddMouseHoverEventHandler(object sender, EventArgs e)
        {
            picAdd.BorderStyle = BorderStyle.Fixed3D;
        }

        private void AddMouseLeaveEventHandler(object sender, EventArgs e)
        {
            picAdd.BorderStyle = BorderStyle.None;
        }

        private void UpdateMouseHoverEventHandler(object sender, EventArgs e)
        {
            picUpdate.BorderStyle = BorderStyle.Fixed3D;
        }

        private void UpdateMouseLeaveEventHandler(object sender, EventArgs e)
        {
            picUpdate.BorderStyle = BorderStyle.None;
        }

        private void DeleteMouseHoverEventHandler(object sender, EventArgs e)
        {
            picDelete.BorderStyle = BorderStyle.Fixed3D;
        }

        private void DeleteMouseLeaveEventHandler(object sender, EventArgs e)
        {
            picDelete.BorderStyle = BorderStyle.None;
        }

        private void ExitMouseHoverEventHandler(object sender, EventArgs e)
        {
            picExit.BorderStyle = BorderStyle.Fixed3D;
        }

        private void ExitMouseLeaveEventHandler(object sender, EventArgs e)
        {
            picExit.BorderStyle = BorderStyle.None;
        }   
    }
}
