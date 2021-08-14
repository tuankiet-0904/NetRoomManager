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
                string Column = "Order".PadRight(20) + "Số lượng".PadRight(10) + "Đơn giá".PadRight(15) + 
                                "Thành tiền".PadRight(15) + "Ghi chú thêm".PadRight(20);
                AllMessageBox.Items.Add(new ListViewItem() { Text = Column, ForeColor = Color.Blue });

                List<string> order = message[1].Split('/').ToList();
                for (int i = 0; i < order.Count-1; i++)
                { 
                    List<string> item = order[i].Split('-').ToList();
                    string res = item[0].PadRight(20) + item[1].PadRight(10) + item[2].PadRight(15) + 
                                 item[3].PadRight(15) + item[4].PadRight(20);
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
