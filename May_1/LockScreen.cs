using DoAnSE;
using System;
using System.Windows.Forms;

namespace May_1
{
    public partial class LockScreen : Form
    {
        public ClientManager clientManager;
        public bool lockMode;
        private string password = "";
        public delegate void MyDel();
        public MyDel ShutDown;

        public LockScreen(ClientManager x)
        {
            this.clientManager = x;
            InitializeComponent();
            timer1.Interval = 500;
            timer1.Enabled = true;
            this.lockMode = false;
        }

        //************************************************************************************************//

        // LockScreen
        private void LockScreen_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            groupBox1.Visible = true;
            loginStatus.Visible = false;
            txtUserName.Select();
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void LockScreen_Click(object sender, EventArgs e)
        {
            if (groupBox1.Visible == false)
            {
                resetTxt();
                groupBox1.Visible = true;
                loginStatus.Visible = false;
            }
        }

        private void LockScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (groupBox1.Visible == false)
            {
                resetTxt();
                groupBox1.Visible = true;
                loginStatus.Visible = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (clientManager.message2.Equals("Acount not exist !! Or Wrong Username, Password"))
            {
                timer1.Stop();
                showLoginStatus("Sai tài khoản hoặc mật khẩu!");
                clientManager.message2 = "";
                resetTxt();
            }
            if (clientManager.message2.Equals("Your account is exhausted. Recharge to use it!!!"))
            {
                timer1.Stop();
                showLoginStatus("Tài khoản đã hết tiền!");
                txtUserName.Select();
                clientManager.message2 = "";
                resetTxt();
            }
            if (clientManager.message2.Equals("This account is currently being used!"))
            {
                timer1.Stop();
                showLoginStatus("Tài khoản này đang được sử dụng!");
                txtUserName.Select();
                clientManager.message2 = "";
                resetTxt();
            }
            if (clientManager.message2.Equals("OkePlayGo"))
            {
                password = txtPassword.Text;
                clientManager.message2 = "";
                resetTxt();
            }
        }

        private void LockScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        //************************************************************************************************//

        // Buttons Click
        private void LoginClickEventHandler(object sender, EventArgs e)
        {
            if (lockMode)
            {
                if (txtUserName.Text.Equals(clientManager.userName) && txtPassword.Text.Equals(password))
                {
                    this.lockMode = false;
                    this.Visible = false;
                    this.TopMost = false;
                }
                else
                {
                    showLoginStatus("Sai tài khoản hoặc mật khẩu!");
                }
                resetTxt();
            }
            else
            {
                timer1.Start();

                string userName = txtUserName.Text.ToString();
                string passWord = txtPassword.Text.ToString();
                showLoginStatus("");

                clientManager.Login(userName, passWord);
            }
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void btnShutDown_Click(object sender, EventArgs e)
        {
            ShutDown();
        }

        //************************************************************************************************//

        // Orther functions
        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        public void showLoginStatus(string message)
        {
            loginStatus.Text = message;
            loginStatus.Visible = true;
        }

        public void resetTxt()
        {
            txtUserName.Clear();
            txtPassword.Clear();
            txtUserName.Select();
        }

        private void btnShutDown_MouseHover(object sender, EventArgs e)
        {
            btnShutDown.BorderStyle = BorderStyle.Fixed3D;
        }

        private void btnShutDown_MouseLeave(object sender, EventArgs e)
        {
            btnShutDown.BorderStyle = BorderStyle.None;
        }
    }
}