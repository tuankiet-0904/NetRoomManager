using System;
using System.Drawing;
using System.Windows.Forms;
using DoAnSE;

namespace May_2
{
    public partial class Timing : Form
    {
        ClientManager clientManager;
        const int USECLIENT = 101;
        const int MEMBERLOGIN = 102;
        const int PAYMENT = 103;
        const int SHUTDOWN = 104;
        int hour = 0;
        int min = 0;
        int sec = 0;
        double money = 0;
        TimeSpan total;
        TimeSpan use;
        TimeSpan remain;
        string userName = "";
        MessageFromClient chatBox;

        public Timing()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            
            clientManager = new ClientManager();
            timerProgram.Interval = 1000;
            timerProgram.Enabled = true;
            timerProgram.Start();
            chatBox = new MessageFromClient(this.clientManager);
            chatBox.Show();
            chatBox.Visible = false;
        }

        //************************************************************************************************//

        // Timing
        private void Timing_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Right - this.Width + 10, 0);
        }

        private void timerProgram_Tick(object sender, EventArgs e)
        {
            if (ClientManager.requestServer == USECLIENT)
            {
                lblUserName.Text = "UserName: Customer";
                txtTotalTime.Text = "Unlimited";
                txtRemainTime.Text = "Unlimited";
                ClientManager.requestServer = -1;
                min = 0;
                ResetTime();
            }
            else if (ClientManager.requestServer == MEMBERLOGIN)
            {
                lblUserName.Text = "UserName: " + clientManager.userName;
                userName = clientManager.userName;
                txtTotalTime.Text = clientManager.totalTime.ToString();
                ClientManager.requestServer = -2;
                min = 0;
                txtUseTimeFee.Text = "0";
                ResetTime();

            }
            else if (ClientManager.requestServer == SHUTDOWN)
            {
                clientManager.CloseSocketConnection();
                this.Dispose();
            }

            else if (ClientManager.requestServer == PAYMENT)
            {
                ResetTime();
            }

            TimeCount();

            if (ClientManager.requestServer == -2)
            {
                total = TimeSpan.Parse(txtTotalTime.Text.ToString());
                use = TimeSpan.Parse(txtUseTime.Text.ToString());
                remain = total - use;
                txtRemainTime.Text = remain.ToString();
                txtUseTimeFee.Text = "0";
            }

            MoneyCount(txtUseTime.Text.ToString());
            if (ClientManager.refreshClient == MEMBERLOGIN)
            {
                TimeSpan useTime = TimeSpan.Parse(txtUseTime.Text.ToString());
                TimeSpan totalTime = TimeSpan.Parse(txtTotalTime.Text.ToString());
                if (useTime > totalTime)
                {
                    // Logout khỏi tài khoản
                    TimeSpan current = DateTime.Now.TimeOfDay;
                    TimeSpan leftTime = TimeSpan.Parse(current.Hours + ":" + current.Minutes + ":" + current.Seconds);
                    clientManager.LogoutMember(userName, remain, clientManager.loginID, use, leftTime);
                    clientManager.lockScreen.Visible = true;
                    clientManager.lockScreen.resetTxt();
                    clientManager.lockScreen.TopMost = true;
                    clientManager.lockScreen.showLoginStatus("");
                }
            }
        }

        private void Timing_FormClosing(object sender, FormClosingEventArgs e)
        {
            clientManager.CloseSocketConnection();
            Application.Exit();
        }

        //************************************************************************************************//

        // Counting Time
        private void TimeCount()
        {
            txtUseTime.Text = hour.ToString("D2") + ":" + min.ToString("D2") + ":" + sec.ToString("D2");
            sec++;
            if (sec == 60)
            {
                min++;
                sec = 00;
            }

            if (min == 60)
            {
                hour++;
                min = 00;
            }

            if (hour > 99)
            {
                timerProgram.Stop();
                timerProgram.Enabled = true;
            }

        }
        
        private int ChangeUseTimeToMinutes(String useTime)
        {
            int minutes = 0;
            string[] arrListStr = useTime.Split(':');
            if (int.Parse(arrListStr[1]) > 0)
            {
                minutes = minutes + int.Parse(arrListStr[1]);
            }

            if (int.Parse(arrListStr[0]) > 0)
            {
                minutes = minutes + int.Parse(arrListStr[0]) * 60;
            }

            return minutes;
        }
        
        private void MoneyCount(String useTime)
        {
            int time = ChangeUseTimeToMinutes(useTime);
            txtUseTimeFee.Text = money.ToString();
            if (time < 20)
            {
                money = 2000;
            }

            else
                money = money + 1.5;
        }

        private void ResetTime()
        {
            min = 0;
            sec = 0;
            hour = 0;
            money = 0;
        }

        //************************************************************************************************//

        // Buttons Click
        private void picMessege_Click(object sender, EventArgs e)
        {
            chatBox.Visible = true;
        }

        private void picService_Click(object sender, EventArgs e)
        {
            OrderDichVu frmOrder = new OrderDichVu();
            frmOrder.ShowDialog();
        }

        private void LogoutClickEventHandler(object sender, EventArgs e)
        {
            TimeSpan current = DateTime.Now.TimeOfDay;
            TimeSpan leftTime = TimeSpan.Parse(current.Hours + ":" + current.Minutes + ":" + current.Seconds);
            clientManager.LogoutMember(userName, remain, clientManager.loginID, use, leftTime);
            clientManager.lockScreen.Visible = true;
            clientManager.lockScreen.resetTxt();
            clientManager.lockScreen.TopMost = true;
            clientManager.lockScreen.showLoginStatus("");
        }

        private void picPassword_Click(object sender, EventArgs e)
        {
            ThongTinKhachHang clientInfo = new ThongTinKhachHang(clientManager);
            clientInfo.ShowDialog();
        }

        private void picLock_Click(object sender, EventArgs e)
        {
            // Vẫn trừ tiền, chỉ có thể đăng nhập lại bằng tài khoản đang tính tiền
            clientManager.lockScreen.lockMode = true;
            clientManager.lockScreen.Visible = true;
            clientManager.lockScreen.TopMost = true;
            clientManager.lockScreen.showLoginStatus("Chế độ chờ - Mời đăng nhập lại để tiếp tục!");
            clientManager.lockScreen.resetTxt();
        } 

        private void picUnitity_Click(object sender, EventArgs e)
        {
            HopTienIch toolBox = new HopTienIch();
            toolBox.ShowDialog();
        }
    }
}