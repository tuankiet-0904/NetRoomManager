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
    public partial class ThemSuaMayTram : Form
    {
        public string clientName;
        public ThemSuaMayTram(string clientName = "")
        {
            this.clientName = clientName;
            InitializeComponent();
        }

        private void LoadCBBClientGroup()
        {
            int value = 0;
            foreach (DTO.GroupClient group in NetRoomReader.Instance.GetAllGroupClients())
            {
                cbbClientGroup.Items.Add(new CBBItem(value, group.GroupClientName));
                value++;
            }
        }

        private void ThemSuaMayTram_Load(object sender, EventArgs e)
        {
            LoadCBBClientGroup();
            DTO.Client client = NetRoomReader.Instance.GetClientByName(clientName);
            if (client != null)
            {
                txtClientName.Text = client.ClientName;
                cbbClientGroup.Text = client.GroupClientName;
                txtNote.Text = client.Note;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtClientName.Text != "" || txtNote.Text != "")
            {

                string new_Name = txtClientName.Text;
                if (NetRoomReader.Instance.GetClientByName(new_Name) == null)
                {
                    NetRoomWritter.Instance.InsertClient(txtClientName.Text, cbbClientGroup.Text, txtNote.Text);
                    MessageBox.Show("Thêm máy trạm thành công!", "Thông báo!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    if (MessageBox.Show("Máy trạm này đã tồn tại!\nBạn có chắc muốn cập nhật không?", "Thông báo!",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        NetRoomWritter.Instance.UpdateClient(txtClientName.Text, cbbClientGroup.Text, txtNote.Text);
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
    }
}
