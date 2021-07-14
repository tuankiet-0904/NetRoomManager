using QuanLyPhongNet.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongNet.GUI
{
    public partial class ThemSuaNhomMay : Form
    {
        public string clientGroupName;
        public ThemSuaNhomMay(string clientGroupName = "")
        {
            this.clientGroupName = clientGroupName;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtClientGroupName.Text != "" || txtNote.Text != "")
            {

                string new_Name = txtClientGroupName.Text;
                if (NetRoomReader.Instance.GetClientGroupByName(new_Name) == null)
                {
                    NetRoomWritter.Instance.InsertClientGroup(txtClientGroupName.Text, txtNote.Text);
                    MessageBox.Show("Thêm máy trạm thành công!", "Thông báo!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    if (MessageBox.Show("Máy trạm này đã tồn tại!\nBạn có chắc muốn cập nhật không?", "Thông báo!",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        NetRoomWritter.Instance.UpdateGroupClient(txtClientGroupName.Text, txtNote.Text);
                        MessageBox.Show("Cập nhật thông tin máy trạm thành công!", "Thông báo!",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                }
            }
            else
            {
                MessageBox.Show("Chưa nhập đầy đủ thông tin!", "Lỗi!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ThemSuaNhomMay_Load(object sender, EventArgs e)
        {
            DTO.GroupClient clientGroup = NetRoomReader.Instance.GetClientGroupByName(clientGroupName);
            if (clientGroup != null)
            {
                txtClientGroupName.Text = clientGroup.GroupClientName;
                txtNote.Text = clientGroup.Description;
            }
        }
    }
}
