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
    public partial class GiaoDienLuaChon : Form
    {
        public GiaoDienLuaChon()
        {
            InitializeComponent();
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            GiaoDienChinh frmHome = new GiaoDienChinh();
            this.Hide();
            frmHome.ShowDialog();
            this.Show();
        }

        private void picCategory_Click(object sender, EventArgs e)
        {
            QuanLyDanhMuc frmCategory = new QuanLyDanhMuc();
            this.Hide();
            frmCategory.ShowDialog();
            this.Show();
        }

        private void picReport_Click(object sender, EventArgs e)
        {
            BaoCao frmReport = new BaoCao();
            this.Hide();
            frmReport.ShowDialog();
            this.Show();
        }

        private void picHome_MouseHover(object sender, EventArgs e)
        {
            picHome.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picHome_MouseLeave(object sender, EventArgs e)
        {
            picHome.BorderStyle = BorderStyle.None;
        }

        private void picCategory_MouseHover(object sender, EventArgs e)
        {
            picCategory.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picCategory_MouseLeave(object sender, EventArgs e)
        {
            picCategory.BorderStyle = BorderStyle.None;
        }

        private void picReport_MouseHover(object sender, EventArgs e)
        {
            picReport.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picReport_MouseLeave(object sender, EventArgs e)
        {
            picReport.BorderStyle = BorderStyle.None;
        }

        private void GiaoDienLuaChon_Load(object sender, EventArgs e)
        {

        }

        private void llblSignOut_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Dispose();
        }
    }
}
