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
                this.TopMost = true;
                this.Visible = true;
                ServerManager.MessageCode = -1;
                String time = DateTime.Now.ToString("HH:mm:ss");
                AllMessageBox.Items.Add(new ListViewItem() { Text = ServerManager.MessageFromClient, ForeColor = Color.Blue });
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
