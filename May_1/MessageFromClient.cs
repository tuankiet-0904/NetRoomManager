using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAnSE;

namespace May_1
{
    public partial class MessageFromClient : Form
    {
        ClientManager clientManager;
        public MessageFromClient()
        {
            InitializeComponent();
        }
        public MessageFromClient(ClientManager x)
        {
            InitializeComponent();
            clientManager = x;
            timer1.Interval = 500;
            timer1.Enabled = true;
            timer1.Start();
        }

        //************************************************************************************************//

        // ChatBox
        private void MessageFromClient_Load(object sender, EventArgs e)
        {
            txtSend.Select();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ClientManager.MessageCode == 1)
            {
                this.Visible = true;
                ClientManager.MessageCode = -1;
                AllMessageBox.Items.Add(new ListViewItem() { Text = ClientManager.MessageFromServer, ForeColor = Color.Red });
            }
        }

        private void MessageFromClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }

        //************************************************************************************************//

        // Buttons Click
        private void btnSend_Click(object sender, EventArgs e)
        {
            String time = DateTime.Now.ToString("HH:mm:ss");
            if (txtSend.Text != string.Empty)
                clientManager.SendMessage("[" + time + "] " + clientManager.userName + ": " + txtSend.Text);
            AllMessageBox.Items.Add(new ListViewItem() { Text = "[" + time + "] " + clientManager.userName + ": " + txtSend.Text, ForeColor = Color.Blue });
            txtSend.Clear();
            txtSend.Select();
        }

        //************************************************************************************************//

        // Other functions
        private void txtSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend.PerformClick();
            }
        }

        private void txtSend_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtSend.Text != "")
            {
                btnSend.PerformClick();
            }
        }

        
    }
}
