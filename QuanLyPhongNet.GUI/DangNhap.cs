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
using QuanLyPhongNet.DTO;

namespace QuanLyPhongNet.GUI
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (cboUser.Text.Equals(""))
            {
                MessageBox.Show("Chưa chọn loại tài khoản!", "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboUser.Select();
            }
            else if (txtPassword.Text.Equals(""))
            { 
                MessageBox.Show("Chưa nhập mật khẩu!", "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Select();
            }
            else
            {
                try
                {
                    foreach (DTO.User user in NetRoomReader.Instance.GetAllUsers())
                    {
                        if (cboUser.Text.Equals(user.Type) && txtPassword.Text.Equals(user.LoginPass))
                        {
                            ServerManager.MemberID = user.ID;
                            if (user.ID != 0)
                            {
                                GiaoDienChinh frmHome = new GiaoDienChinh();
                                this.Hide();
                                frmHome.ShowDialog();
                                txtPassword.Text = "";
                                this.Show();
                            }
                            else
                            {
                                GiaoDienLuaChon frmHome = new GiaoDienLuaChon();
                                this.Hide();
                                frmHome.ShowDialog();
                                txtPassword.Text = "";
                                this.Show();
                            }
                            return;
                        }
                    }
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    MessageBox.Show("Không kết nối được với Database!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
