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
        ServerManager servermanager;
        public DangNhap()
        {
            InitializeComponent();
            servermanager = new ServerManager();
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
                            servermanager.MemberID = user.ID;
                            if (user.ID != 0)
                            {
                                GiaoDienChinh frmHome = new GiaoDienChinh(servermanager);
                                this.Hide();
                                frmHome.ShowDialog();
                                txtPassword.Text = "";
                                this.Show();
                            }
                            else
                            {
                                GiaoDienLuaChon frmHome = new GiaoDienLuaChon(servermanager);
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

        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (servermanager.usingClient.Count > 0)
            {
                if (MessageBox.Show("Vẫn còn máy con đang hoạt động!\nBạn có chắc muốn thoát?", "Chú ý!",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    foreach (InfoClient i in servermanager.usingClient.ToList())
                    {
                        if (i.stateClient.Equals("MEMBER USING"))
                        {
                            // Save Logout Info
                            string accountUsing = i.nameCustomer;
                            LoginMember loginMember = FindLoginMember(accountUsing);
                            TimeSpan current = DateTime.Now.TimeOfDay;
                            TimeSpan leftTime = TimeSpan.Parse(current.Hours + ":" + current.Minutes + ":" + current.Seconds);
                            TimeSpan useTime = leftTime - loginMember.StartTime;
                            servermanager.SaveLogoutInfo(loginMember.LoginID, useTime, leftTime);
                            // Logout Member
                            string nameClient = i.nameClient;
                            servermanager.ShutDown(nameClient);
                        }
                    }
                    servermanager.CloseSocketConnection();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else servermanager.CloseSocketConnection();
        }

        private void DangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public LoginMember FindLoginMember(string accountUsing)
        {
            Member member = NetRoomReader.Instance.GetMemberByAccountName(accountUsing);
            foreach (LoginMember i in NetRoomReader.Instance.GetAllLoginMembers())
            {
                if (i.MemberID == member.ID && i.LeftTime.Equals(TimeSpan.Zero))
                {
                    return i;
                }
            }
            return null;
        }
    }
}
