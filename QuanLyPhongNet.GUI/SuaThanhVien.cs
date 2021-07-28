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
using QuanLyPhongNet.DAL;

namespace QuanLyPhongNet.GUI
{
    public partial class SuaThanhVien : Form
    {
        private int _ID = -1;
        
        public SuaThanhVien(int id)
        {
            InitializeComponent();
            _ID = id;
        }

        private void SuaThongTin_Load(object sender, EventArgs e)
        {
            DTO.Member member = NetRoomReader.Instance.GetMemberByID(_ID);
            txtName.Text = member.AccountName;
            txtPass.Text = member.Password;
            txtCurrentMoney.Text = member.CurrentMoney.ToString();
            cbbGroupUser.SelectedItem = member.GroupUserName.ToString();
            txtCurrentMoney.Enabled = false;
            txtAddMoney.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Member member = new Member();
            member.MemberID = _ID;
            member.AccountName = txtName.Text;
            member.Password = txtPass.Text;
            member.CurrentMoney = float.Parse(txtCurrentMoney.Text);
            member.GroupUser = cbbGroupUser.SelectedItem.ToString();
            if (member.CurrentMoney > 0)
            {
                member.StatusAccount = "Cho Phép";
            }
            else member.StatusAccount = "Hết Thời Gian";
            member.CurrentTime = NetRoomWritter.Instance.ChangeMoneyToTime((float)member.CurrentMoney);
            TransactionDiary2 td = new TransactionDiary2();
            td.UserID = ServerManager.MemberID;
            td.memberID = _ID;
            td.TransacDate = DateTime.Now;
            td.AddMoney = float.Parse(txtAddMoney.Text);
            td.AddTime = NetRoomWritter.Instance.ChangeMoneyToTime(float.Parse(txtAddMoney.Text));
            NetRoomWritter.Instance.InsertTransactionDiary(td);
            NetRoomWritter.Instance.UpdateMember(member);
            MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
