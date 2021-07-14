using QuanLyPhongNet.BUS;
using QuanLyPhongNet.DTO;
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
    public partial class ThongTinKhachHang : Form
    {
        private int memberID;
        public ThongTinKhachHang(int memberID = 0)
        {
            this.memberID = memberID;
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ThongTinKhachHang_Load(object sender, EventArgs e)
        {
            MemberInformation memberInfo = NetRoomReader.Instance.GetMemberInfoByID(memberID);
            txtName.Text = memberInfo.MemberName;
            txtFoundedDate.Text = memberInfo.FoundedDate.ToString().Split(' ')[0];
            txtPhoneNumber.Text = memberInfo.PhoneNumber;
            txtAddress.Text = memberInfo.MemberAddress;
            txtEmail.Text = memberInfo.Email;
            btnExit.Focus();
        }
    }
}
