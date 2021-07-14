using DoAnSE;
using System;
using System.Windows.Forms;

namespace May_1
{
    public partial class LockScreen : Form
    {
        public ClientManager clientManager;
        public LockScreen(ClientManager x)
        {
            this.clientManager = x;
            InitializeComponent();
            timer1.Interval = 500;
            timer1.Enabled = true;
        }
        private void LockScreen_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            groupBox1.Visible = true;
            loginStatus.Visible = false;
            txtUserName.Select();
        }

        private void LockScreen_Click(object sender, EventArgs e)
        {
            if (groupBox1.Visible == false)
            {
                txtUserName.Clear();
                txtPassword.Clear();
                groupBox1.Visible = true;
                loginStatus.Visible = false;
                txtUserName.Select();
            }
        }

        private void LockScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (groupBox1.Visible == false)
            {
                txtUserName.Clear();
                txtPassword.Clear();
                groupBox1.Visible = true;
                loginStatus.Visible = false;
                txtUserName.Select();
            }
        }

        private void LoginClickEventHandler(object sender, EventArgs e)
        {
            timer1.Start();

            string userName = txtUserName.Text.ToString();
            string passWord = txtPassword.Text.ToString();
            showLoginStatus("");
            resetTxt();

            clientManager.Login(userName, passWord);
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }
        public void showLoginStatus(string message)
        {
            loginStatus.Text = message;
            loginStatus.Visible = true;
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
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
        public void resetTxt()
        {
            txtUserName.Clear();
            txtPassword.Clear();
            txtUserName.Select();
        }
    }
}
