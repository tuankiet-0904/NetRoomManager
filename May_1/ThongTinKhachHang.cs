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
    public partial class ThongTinKhachHang : Form
    {
        public ClientManager clientManager;

        public ThongTinKhachHang(ClientManager x)
        {
            this.clientManager = x;
            InitializeComponent();

            timer1.Interval = 500;
            timer1.Enabled = true;
            timer1.Start();

            timer2.Interval = 500;
            timer2.Enabled = true;
        }

        private void ThongTinKhachHang_Load(object sender, EventArgs e)
        {
            clientManager.GetMemberInfo(clientManager.userName);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            List<string> message = clientManager.message2.Split('|').ToList();
            if (message[0].Equals("Member info received!"))
            {
                timer1.Stop();
                txtName.Text = message[1];
                txtFoundedDate.Text = message[2].Split(' ').ToList()[0];
                txtPhoneNumber.Text = message[3];
                txtAddress.Text = message[4];
                txtEmail.Text = message[5];
                clientManager.message2 = "";
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (clientManager.message2.Equals("Update MemberInfo Success!"))
            {
                timer2.Stop();
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clientManager.message2 = "";
            }
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            ChangePassword frmChangePass = new ChangePassword(clientManager);
            frmChangePass.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            timer2.Start();
            string newMemberInfo = txtName.Text + "|" + txtFoundedDate.Text + "|" + txtPhoneNumber.Text + "|" +
                 txtAddress.Text + "|" + txtEmail.Text + "|";
            clientManager.UpdateMemberInfo(clientManager.userName, newMemberInfo);
        }
 
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
