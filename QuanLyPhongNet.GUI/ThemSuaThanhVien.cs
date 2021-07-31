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
    public partial class ThemSuaThanhVien : Form
    {
        ServerManager servermanager;

        public ThemSuaThanhVien(ServerManager x)
        {
            InitializeComponent();
            servermanager = x;
        }

        private void ThemThanhVien_Load(object sender, EventArgs e)
        {
            txtCurrentTime.Text = "0";
            txtPass.Enabled = false;
            txtCurrentTime.Enabled = false;
            txtAddMoney.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Equals(""))
            {
                MessageBox.Show("Chưa nhập tài khoản!", "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Select();
            }
            else if (txtPass.Text.Equals(""))
            {
                MessageBox.Show("Chưa nhập mật khẩu!", "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPass.Select();
            }
            else
            {
                Member member = new Member();
                member.AccountName = txtName.Text;
                member.MemberID = NetRoomReader.Instance.FindIDByAccountName(member.AccountName);
                member.Password = txtPass.Text;
                member.CurrentMoney = float.Parse(txtCurrentTime.Text);
                member.GroupUser = "Hội Viên";
                if (txtAddMoney.Text.Equals("0"))
                {
                    MessageBox.Show("Vui lòng nhập số tiền nạp!", "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                float AddMoney = 0;
                if (txtAddMoney.Text.IndexOf('-') == 0)
                {
                    AddMoney = -float.Parse(txtAddMoney.Text.Substring(1));
                    if (member.CurrentMoney + AddMoney < 0) member.CurrentMoney = 0;
                    else member.CurrentMoney = float.Parse(txtCurrentTime.Text) + AddMoney;
                }
                else
                {
                    AddMoney = float.Parse(txtAddMoney.Text);
                    member.CurrentMoney = float.Parse(txtCurrentTime.Text) + AddMoney;
                }
                if (member.CurrentMoney > 0)
                {
                    member.StatusAccount = "Cho Phép";
                }
                else member.StatusAccount = "Hết Thời Gian";
                member.CurrentTime = NetRoomWritter.Instance.ChangeMoneyToTime((float)member.CurrentMoney);
                if (NetRoomReader.Instance.CheckAccount(txtName.Text))
                {
                    NetRoomWritter.Instance.InsertMember(member);
                    member.MemberID = NetRoomReader.Instance.FindIDByAccountName(member.AccountName);
                    NetRoomWritter.Instance.InsertMemberInfo(member.MemberID, DateTime.Now.Date);
                }
                else
                {
                    MessageBox.Show("Bạn đã nạp " + txtAddMoney.Text + " đồng vào tài khoản " + txtName.Text,
                                    "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NetRoomWritter.Instance.UpdateMember(member);
                }

                TransactionDiary2 td = new TransactionDiary2();
                td.memberID = member.MemberID;
                td.UserID = servermanager.MemberID;
                td.UserName = NetRoomReader.Instance.FindUserNameByID(servermanager.MemberID);
                td.TransacDate = DateTime.Now;
                td.AddTime = NetRoomWritter.Instance.ChangeMoneyToTime(AddMoney);
                td.AddMoney = AddMoney;
                td.Note = txtNote.Text;
                NetRoomWritter.Instance.InsertTransactionDiary(td);
                this.Dispose();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string accountName = txtName.Text;
            int id = NetRoomReader.Instance.FindIDByAccountName(accountName);
            DTO.Member member = NetRoomReader.Instance.GetMemberByID(id);
            if (member != null)
            {
                MessageBox.Show("Tài khoản này đã tồn tại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPass.Text = member.Password.ToString();
                txtCurrentTime.Text = member.CurrentMoney.ToString();
                txtAddMoney.Enabled = true;
                txtAddMoney.Select();
            }
            else
            {
                MessageBox.Show("Tài khoản này chưa tồn tại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPass.Enabled = true;
                txtAddMoney.Enabled = true;
                txtPass.Select();
            }
        }
    }
}
