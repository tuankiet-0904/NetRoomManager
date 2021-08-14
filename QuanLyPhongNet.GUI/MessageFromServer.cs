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

namespace QuanLyPhongNet.GUI
{
    public partial class MessageFromServer : Form
    {
        ServerManager serverManager;

        public MessageFromServer()
        {
            InitializeComponent();
        }
        public MessageFromServer(ServerManager x)
        {
            InitializeComponent();
            serverManager = x;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Start();
            comboBox1.SelectedIndex = 0;
        }

        private void MessageFromServer_Load(object sender, EventArgs e)
        {
            txtSend.Select();
            comboBox1.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadCBBUsingClients();
            if (ServerManager.MessageCode == 1)
            {
                this.Visible = true;
                ServerManager.MessageCode = -1;
                AllMessageBox.Items.Add(new ListViewItem() { Text = ServerManager.MessageFromClient, ForeColor = Color.Blue });
            }
            if (ServerManager.MessageCode == 2) 
            {
                this.Visible = true;
                ServerManager.MessageCode = -1;
                
                List<string> message = ServerManager.MessageFromClient.Split('|').ToList();
                AllMessageBox.Items.Add(new ListViewItem() { Text = message[0], ForeColor = Color.Blue });
                string Column = string.Format("| {0,-30} {1,-10} {2,-15} {3,-15} {4,-30}",
                        "Order", "Số lượng", "Đơn giá", "Thành tiền", "Ghi chú thêm");
                AllMessageBox.Items.Add(new ListViewItem() { Text = Column, ForeColor = Color.Blue });

                List<string> order = message[1].Split('/').ToList();
                for (int i = 0; i < order.Count-1; i++)
                { 
                    List<string> item = order[i].Split('-').ToList();
                    string res = string.Format("| {0,-20} {1,5} {2,15} {3,15} {4,-30}",
                           item[0], item[1], item[2], item[3], item[4]);
                    AllMessageBox.Items.Add(new ListViewItem() { Text = res, ForeColor = Color.Blue });
                }
                AllMessageBox.Items.Add(new ListViewItem() { Text = "Tổng cộng: " + order[order.Count - 1], ForeColor = Color.Blue });
            }
        }

        public void LoadCBBUsingClients()
        {
            if (serverManager.usingClient.Count != 0)
            {
                foreach (DTO.InfoClient client in serverManager.usingClient)
                {
                    if (comboBox1.Items.IndexOf(client.nameClient) == -1 && !client.stateClient.Equals("WAITING"))
                    {
                        comboBox1.Items.Add(client.nameClient);
                    }
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            String time = DateTime.Now.ToString("HH:mm:ss");
            if (txtSend.Text != string.Empty)
                serverManager.SendMessage("[" + time + "] " + "Máy chủ: " + txtSend.Text, comboBox1.Text);
            string sendTo = "To " + comboBox1.Text + ": ";
            AllMessageBox.Items.Add(new ListViewItem() { Text = "[" + time + "] " + sendTo + txtSend.Text, ForeColor = Color.Red });
            txtSend.Clear();
            txtSend.Select();
        }

        private void txtSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtSend.Text != "")
            {
                btnSend.PerformClick();
            }
        }

        public void selectCBBItem(string clientName)
        {
            comboBox1.SelectedItem = clientName;
        }

        private void MessageFromServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }
    }
}
