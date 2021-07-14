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
    public partial class ThemSuaNguoiDung : Form
    {
        private int _ID;
        public ThemSuaNguoiDung(int ID = -1)
        {
            InitializeComponent();
            _ID = ID;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "" || txtName.Text != "" || txtPass.Text != "")
            {
                
                int new_ID = int.Parse(txtID.Text);
                if (NetRoomReader.Instance.CheckUserPass(new_ID, txtPass.Text))
                {
                    if (NetRoomReader.Instance.GetUserByID(new_ID) == null)
                    {
                        NetRoomWritter.Instance.InsertUser(new_ID, txtName.Text, txtPass.Text, txtPhoneNumber.Text, txtEmail.Text);
                        MessageBox.Show("Thêm người dùng thành công!", "Thông báo!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                    else
                    {
                        if (MessageBox.Show("Người dùng này đã tồn tại!\nBạn có chắc muốn cập nhật không?", "Thông báo!",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            NetRoomWritter.Instance.UpdateUser(new_ID, txtName.Text, txtPass.Text, txtPhoneNumber.Text, txtEmail.Text);
                            MessageBox.Show("Cập nhật thông tin người dùng thành công!", "Thông báo!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Dispose();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Đã có tài khoản sử dụng mật khẩu này!\nVui lòng nhập mật khẩu khác!", "Lỗi!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ThemSuaNguoiDung_Load(object sender, EventArgs e)
        {
            DTO.User user = NetRoomReader.Instance.GetUserByID(_ID);
            if (user != null)
            {
                txtID.Text = user.ID.ToString();
                txtName.Text = user.UserName;
                txtPass.Text = user.LoginPass;
                txtPhoneNumber.Text = user.PhoneNumber;
                txtEmail.Text = user.Email;
            }
        }
    }
}
