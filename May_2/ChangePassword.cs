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

namespace May_2
{
    public partial class ChangePassword : Form
    {
        public ClientManager clientManager;
        public ChangePassword(ClientManager x)
        {
            this.clientManager = x;
            InitializeComponent();
            timer1.Interval = 500;
            timer1.Enabled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            timer1.Start();
            if (txtOldPass.Text == "" || txtNewPass.Text == "" || txtReEnter.Text == "")
            {
                MessageBox.Show("Mời nhập đầy đủ thông tin!", "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtOldPass.Text.Equals(txtNewPass.Text))
            {
                MessageBox.Show("Mật khẩu mới trùng với mật khẩu cũ!", "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNewPass.Select();
                return;
            }
            if (!txtNewPass.Text.Equals(txtReEnter.Text))
            {
                MessageBox.Show("Nhập lại mật khẩu mới không khớp!", "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtReEnter.Select();
                return;
            }
            clientManager.UpdatePassword(clientManager.userName, txtOldPass.Text, txtNewPass.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (clientManager.message2.Equals("Update Password Success!"))
            {
                timer1.Stop();
                MessageBox.Show("Thay đổi mật khẩu thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clientManager.message2 = "";
                this.Dispose();
            }
            if (clientManager.message2.Equals("Wrong Old Password!"))
            {
                timer1.Stop();
                MessageBox.Show("Sai mật khẩu cũ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clientManager.message2 = "";
                txtOldPass.Select();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
