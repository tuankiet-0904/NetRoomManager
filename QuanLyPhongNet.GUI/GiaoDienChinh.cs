using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyPhongNet.BUS;
using QuanLyPhongNet.DTO;

namespace QuanLyPhongNet.GUI
{
    public partial class GiaoDienChinh : Form
    {
        ServerManager servermanager;
        private NetRoomReader objReader;
        private NetRoomWritter objWriter;
        MessageFromServer chatBox;
        public GiaoDienChinh()
        {
            InitializeComponent();
            servermanager = new ServerManager();
            CheckForIllegalCrossThreadCalls = false;
            timerHome.Interval = 200;
            timerHome.Enabled = true;
            timerHome.Start();
            objReader = new NetRoomReader();
            objWriter = new NetRoomWritter();
        }

        //************************************************************************************************//

        // Giao diện chính
        private void GiaoDienChinh_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-10, 0);
            LoadCBBDichVu();
            this.cbbSearchMember.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;
            this.comboBox3.SelectedIndex = 0;
            this.cbbNhomMay.SelectedIndex = 0;
            this.cbbUserGroup.SelectedIndex = 0;
            this.cbbSearchUser.SelectedIndex = 0;
            LoadSourceToDRGV();
            chatBox = new MessageFromServer(this.servermanager);
            chatBox.Show();
            chatBox.Visible = false;
        }

        private void LoadSourceToDRGV()
        {
            ShowCategory();
            LoadDRGVUsingClient();
            LoadDRGVMember();
            LoadDRGVUser();
            LoadDRGVNKHT();
            LoadDRGVNKGD();
            LoadDRGClientGroup();
            LoadDRGUserGroup();
            LoadDRGVNKDN();
        }

        private void timerHome_Tick(object sender, EventArgs e)
        {
            if (ServerManager.refreshClient != -1)
            {
                if (ServerManager.refreshClient != servermanager.usingClient.Count)
                {
                    ServerManager.refreshClient = servermanager.usingClient.Count;
                    if (servermanager.usingClient.Count > 0) LoadClient();
                }
            }
            if (servermanager.clientDisconnect != "")
            {
                string clientName = servermanager.clientDisconnect;
                foreach (InfoClient i in servermanager.usingClient.ToList())
                {
                    if (i.nameClient.Equals(clientName))
                    {
                        if (i.stateClient.Equals("MEMBER USING"))
                        {
                            // Save Logout Info
                            string accountUsing = i.nameCustomer;
                            LoginMember loginMember = FindLoginMember(accountUsing);
                            TimeSpan current = DateTime.Now.TimeOfDay;
                            TimeSpan leftTime = TimeSpan.Parse(current.Hours + ":" + current.Minutes + ":" + current.Seconds);
                            TimeSpan useTime = leftTime - loginMember.StartTime;
                            servermanager.SaveLogoutInfo(loginMember.LoginID, useTime, leftTime);
                        }
                        servermanager.usingClient.RemoveAt(servermanager.usingClient.IndexOf(i));
                        servermanager.clientDisconnect = "";
                        if (servermanager.usingClient.Count > 0)
                        {
                            LoadClient();
                        }
                        else
                        {
                            LoadDRGVUsingClient();
                        }
                        break;
                    }
                }
                LoadDRGVNKDN();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            servermanager.CloseSocketConnection();
            this.Dispose();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl.SelectedTab.Text)
            {
                case "Các Máy Trạm":
                    if (servermanager.usingClient.Count == 0)
                    {
                        LoadDRGVUsingClient();
                    }
                    break;
                case "Tài Khoản":
                    tabControl1.SelectedIndex = 0;
                    LoadDRGVMember();
                    cbbSearchMember.SelectedIndex = 0;
                    txtSearchMember.Text = "";
                    break;
                case "Nhật Ký Hệ Thống":
                    LoadDRGVNKHT();
                    comboBox2.SelectedIndex = 0;
                    textBox2.Text = "";
                    break;
                case "Nhật Ký Giao Dịch":
                    LoadDRGVNKGD();
                    comboBox3.SelectedIndex = 0;
                    textBox3.Text = "";
                    break;
                case "Nhóm Máy":
                    LoadDRGClientGroup();
                    cbbNhomMay.SelectedIndex = 0;
                    txtNhomMay.Text = "";
                    break;
                case "Dịch Vụ":
                    tabCategory.SelectedIndex = 0;
                    drgvFood.DataSource = NetRoomReader.Instance.GetAllFoods();
                    cbbDichVu.SelectedIndex = 0;
                    txtSearchService.Text = "";
                    break;
                case "Nhóm Người Sử Dụng":
                    cbbUserGroup.SelectedIndex = 0;
                    txtUserGroup.Text = "";
                    break;
                case "Nhật Ký Đăng Nhập":
                    LoadDRGVNKDN();
                    cbbNKDN.SelectedIndex = 0;
                    txtNKDN.Text = "";
                    break;
            }
        }

        private void picSendMessage_Click(object sender, EventArgs e)
        {
            if (servermanager.usingClient.Count == 0)
            {
                if (servermanager.usingClient.Count != 0)
                {
                    MessageBox.Show("Máy này chưa được kết nối!", "Lỗi!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (drgvUsingClient.SelectedRows.Count == 1)
            {
                string state = drgvUsingClient.CurrentRow.Cells[2].Value.ToString();
                if (state.Equals("MEMBER USING") || state.Equals("USING"))
                {
                    string clientName = drgvUsingClient.CurrentRow.Cells[0].Value.ToString();
                    chatBox.Visible = true;
                    chatBox.selectCBBItem(clientName);
                }
                else
                {
                    MessageBox.Show("Máy này hiện đang không có người dùng!", "Lỗi!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GiaoDienChinh_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (servermanager.usingClient.Count > 0)
            {
                if (MessageBox.Show("Vẫn còn máy con đang hoạt động!\nBạn có chắc muốn thoát?", "Chú ý!",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    foreach (InfoClient i in servermanager.usingClient.ToList())
                    {
                        if (i.stateClient.Equals("MEMBER USING"))
                        {
                            // Save Logout Info
                            string accountUsing = i.nameCustomer;
                            LoginMember loginMember = FindLoginMember(accountUsing);
                            TimeSpan current = DateTime.Now.TimeOfDay;
                            TimeSpan leftTime = TimeSpan.Parse(current.Hours + ":" + current.Minutes + ":" + current.Seconds);
                            TimeSpan useTime = leftTime - loginMember.StartTime;
                            servermanager.SaveLogoutInfo(loginMember.LoginID, useTime, leftTime);
                            // Logout Member
                            string nameClient = i.nameClient;
                            servermanager.ShutDown(nameClient);
                        }
                    }
                    servermanager.CloseSocketConnection();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else servermanager.CloseSocketConnection();
        }

        //************************************************************************************************//

        // Các Máy Trạm Tab
        public void LoadClient()
        {
            drgvUsingClient.DataSource = (from client in servermanager.usingClient
                                          select new
                                          {
                                              ComputerName = client.nameClient,
                                              UsingAccount = client.nameCustomer,
                                              State = client.stateClient,
                                              StartTime = client.startTime
                                          }).ToArray();
        }

        private void LoadDRGVUsingClient()
        {
            drgvUsingClient.DataSource = NetRoomReader.Instance.GetAllDisconnect();
            drgvUsingClient.Columns[0].HeaderText = "Tên máy con";
            drgvUsingClient.Columns[1].HeaderText = "Nhóm máy";
            drgvUsingClient.Columns[2].HeaderText = "Trạng thái";
            drgvUsingClient.Columns[3].HeaderText = "Ghi chú";
        }

        private void picOpenClient_Click(object sender, EventArgs e)
        {
            if (drgvUsingClient.SelectedRows.Count < 1) return;
            if (drgvUsingClient.SelectedRows[0].Cells[2].Value.ToString().Equals("DISCONNECT"))
            {
                MessageBox.Show("Máy này chưa được kết nối!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string state = drgvUsingClient.SelectedRows[0].Cells[2].Value.ToString();
            if (state.Equals("MEMBER USING") || state.Equals("USING"))
            {
                MessageBox.Show("Máy này đang được sử dụng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index = drgvUsingClient.SelectedRows[0].Index;
            servermanager.UsingWithGuess(servermanager.usingClient[index].nameClient);
            servermanager.usingClient[index].stateClient = "USING";
            LoadClient();
        }

        private void picCalculateMoney_Click(object sender, EventArgs e)
        {
            if (drgvUsingClient.SelectedRows.Count < 1) return;
            if (drgvUsingClient.SelectedRows[0].Cells[2].Value.ToString().Equals("DISCONNECT"))
            {
                MessageBox.Show("Máy này chưa được kết nối!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int index = drgvUsingClient.SelectedRows[0].Index;
            if (servermanager.usingClient[index].stateClient != "USING")
            {
                MessageBox.Show("Không thể tính tiền máy này!", "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtTotalPrice.Text = servermanager.TotalPrice(index).ToString();
            servermanager.usingClient[index].stateClient = "WAITING";
            servermanager.usingClient[index].startTime = new DateTime();
            LoadClient();
            DAL.Bill bill = new DAL.Bill();
            bill.FoundedDate = DateTime.Now;
            bill.FoundedUserID = ServerManager.MemberID;
            bill.PriceTotal = float.Parse(txtTotalPrice.Text);
            NetRoomWritter.Instance.InsertBill(bill);
            LoadDRGVNKGD();
        }

        private void picLockClient_Click(object sender, EventArgs e)
        {
            if (drgvUsingClient.SelectedRows.Count < 1) return;
            if (drgvUsingClient.SelectedRows[0].Cells[2].Value.ToString().Equals("DISCONNECT"))
            {
                MessageBox.Show("Máy này chưa được kết nối!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string accountUsing = drgvUsingClient.SelectedRows[0].Cells["UsingAccount"].Value.ToString();
            if (!accountUsing.Equals(""))
            {
                LoginMember loginMember = FindLoginMember(accountUsing);
                TimeSpan current = DateTime.Now.TimeOfDay;
                TimeSpan leftTime = TimeSpan.Parse(current.Hours + ":" + current.Minutes + ":" + current.Seconds);
                TimeSpan useTime = leftTime - loginMember.StartTime;
                servermanager.SaveLogoutInfo(loginMember.LoginID, useTime, leftTime);
            }
            int index = drgvUsingClient.SelectedRows[0].Index;
            servermanager.LockClient(index);
            servermanager.usingClient[index].stateClient = "WAITING";
            servermanager.usingClient[index].startTime = new DateTime();
            LoadClient();
        }

        private void picShutdownClient_Click(object sender, EventArgs e)
        {
            if (drgvUsingClient.SelectedRows.Count < 1) return;
            if (servermanager.usingClient.Count == 0)
            {
                MessageBox.Show("Máy này chưa được kết nối!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string accountUsing = drgvUsingClient.SelectedRows[0].Cells[1].Value.ToString();
            if (!accountUsing.Equals(""))
            {
                LoginMember loginMember = FindLoginMember(accountUsing);
                TimeSpan current = DateTime.Now.TimeOfDay;
                TimeSpan leftTime = TimeSpan.Parse(current.Hours + ":" + current.Minutes + ":" + current.Seconds);
                TimeSpan useTime = leftTime - loginMember.StartTime;
                servermanager.SaveLogoutInfo(loginMember.LoginID, useTime, leftTime);
            }
            string clientName = drgvUsingClient.SelectedRows[0].Cells[0].Value.ToString();
            servermanager.ShutDown(clientName);
            if (servermanager.usingClient.Count == 0)
            {
                LoadDRGVUsingClient();
            }
            else
            {
                LoadClient();
            }
        }

        private void picAddMay_Click(object sender, EventArgs e)
        {
            if (servermanager.usingClient.Count != 0)
            {
                MessageBox.Show("Hiện tại chức năng này không thực hiện được!", "Lỗi!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ServerManager.MemberID != 0)
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Lỗi!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ThemSuaMayTram frmMayTram = new ThemSuaMayTram();
                frmMayTram.ShowDialog();
                LoadDRGVUsingClient();
            }
        }

        private void picXoaMay_Click(object sender, EventArgs e)
        {
            if (servermanager.usingClient.Count != 0)
            {
                MessageBox.Show("Hiện tại chức năng này không thực hiện được!", "Lỗi!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ServerManager.MemberID != 0)
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Lỗi!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa máy con này?", "Thông Báo!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string clientName = drgvUsingClient.CurrentRow.Cells[0].Value.ToString();
                    NetRoomWritter.Instance.DeleteClient(clientName);
                    MessageBox.Show("Xóa máy con thành công!", "Thông báo!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDRGVUsingClient();
                }
            }
        }

        private void picUpdateMay_Click(object sender, EventArgs e)
        {
            if (servermanager.usingClient.Count != 0)
            {
                MessageBox.Show("Hiện tại chức năng này không thực hiện được!", "Lỗi!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ServerManager.MemberID != 0)
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Lỗi!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string clientName = drgvUsingClient.CurrentRow.Cells[0].Value.ToString();
                ThemSuaMayTram frmMayTram = new ThemSuaMayTram(clientName);
                frmMayTram.ShowDialog();
                LoadDRGVUsingClient();
            }
        }

        //************************************************************************************************//

        // Tài Khoản Tab
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Text)
            {
                case "Hội viên":
                    LoadDRGVMember();
                    cbbSearchMember.SelectedIndex = 0;
                    txtSearchMember.Text = "";
                    break;
                case "Người dùng":
                    LoadDRGVUser();
                    cbbSearchUser.SelectedIndex = 0;
                    txtSearchUser.Text = "";
                    break;
            }
        }

            // Member Tab
            private void LoadDRGVMember()
                        {
                            drgvMember.DataSource = NetRoomReader.Instance.GetAllMembers();
                            drgvMember.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            drgvMember.Columns[0].HeaderText = "ID";
                            drgvMember.Columns[1].HeaderText = "Tên tài khoản";
                            drgvMember.Columns[2].HeaderText = "Mật khẩu";
                            drgvMember.Columns[3].HeaderText = "Thuộc nhóm";
                            drgvMember.Columns[4].HeaderText = "Thời gian hiện có";
                            drgvMember.Columns[5].HeaderText = "Số tiền hiện có";
                            drgvMember.Columns[6].HeaderText = "Trạng thái";
                        }
        
            private void LoadDRGVClient(string searchBy, string searchFor)
        {
            drgvMember.DataSource = NetRoomReader.Instance.GetListMembers(searchBy, searchFor);
        }

            private void drgvMember_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
            {
                SuaThanhVien f = new SuaThanhVien((int)drgvMember.CurrentRow.Cells[0].Value);
                f.ShowDialog();
                LoadDRGVNKHT();
            }

            private void cbbSearchMember_SelectedIndexChanged(object sender, EventArgs e)
            {
                txtSearchMember.Text = "";
                txtSearchMember.Focus();
            }

            private void picAddMember_Click(object sender, EventArgs e)
        {
            ThemSuaThanhVien f = new ThemSuaThanhVien();
            f.ShowDialog();
            LoadDRGVMember();
            LoadDRGVNKHT();
        }

            private void picUpdateMember_Click(object sender, EventArgs e)
        {
            SuaThanhVien f = new SuaThanhVien((int)drgvMember.CurrentRow.Cells[0].Value);
            f.ShowDialog();
            LoadDRGVMember();
            LoadDRGVNKHT();
        }

            private void btnSearchMember_Click(object sender, EventArgs e)
        {
            string cbbitem = cbbSearchMember.SelectedItem.ToString();
            string name = txtSearchMember.Text;
            LoadDRGVClient(cbbitem, name);
        }

            private void picMemberInfo_Click(object sender, EventArgs e)
            {
                if (drgvMember.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Bạn chưa chọn tài khoản hội viên nào!", "Thông báo!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (drgvMember.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Bạn chọn quá nhiều tài khoản hội viên!", "Thông báo!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int memberID = Convert.ToInt32(drgvMember.SelectedRows[0].Cells["ID"].Value);
                ThongTinKhachHang memberInfo = new ThongTinKhachHang(memberID);
                memberInfo.ShowDialog();
            }

            // User Tab
            private void LoadDRGVUser()
                        {
                            drgvUser.DataSource = NetRoomReader.Instance.GetAllUsers();
                            drgvUser.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            drgvUser.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            drgvUser.Columns[0].HeaderText = "ID";
                            drgvUser.Columns[1].HeaderText = "Tên người dùng";
                            drgvUser.Columns[3].HeaderText = "Mật khẩu";
                            drgvUser.Columns[2].HeaderText = "Kiểu người dùng";
                            drgvUser.Columns[4].HeaderText = "Nhóm người dùng";
                            drgvUser.Columns[5].HeaderText = "Số điện thoại";
                            drgvUser.Columns[6].HeaderText = "Địa chỉ Email";
                            if (ServerManager.MemberID != 0)
                            {
                                drgvUser.Columns[3].Visible = false;
                            }
                        }

            private void LoadDRGVUser(string searchBy, string searchFor)
            {
                drgvUser.DataSource = NetRoomReader.Instance.GetListUsers(searchBy, searchFor);
                if (ServerManager.MemberID != 0)
                {
                    drgvUser.Columns[3].Visible = false;
                }
            }

            private void picAddUser_Click(object sender, EventArgs e)
        {
            if (ServerManager.MemberID != 0)
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Lỗi!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ThemSuaNguoiDung frmAddUser = new ThemSuaNguoiDung();
                frmAddUser.ShowDialog();
                LoadDRGVUser();
            }
        }

            private void picUpdateUser_Click(object sender, EventArgs e)
            {
                if (ServerManager.MemberID != 0)
                {
                    MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Lỗi!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    int ID = int.Parse(drgvUser.CurrentRow.Cells[0].Value.ToString());
                    int numUser = drgvUser.Rows.Count - 1;
                    ThemSuaNguoiDung frmUpdateUser = new ThemSuaNguoiDung(ID);
                    frmUpdateUser.ShowDialog();
                    LoadDRGVUser();
                }
            }

            private void picDeleteUser_Click(object sender, EventArgs e)
            {
                if (ServerManager.MemberID != 0)
                {
                    MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Lỗi!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa mục này?", "Thông Báo!",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int ID = int.Parse(drgvUser.CurrentRow.Cells[0].Value.ToString());
                        NetRoomWritter.Instance.DeleteUser(ID);
                        MessageBox.Show("Xóa người dùng thành công!", "Thông báo!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDRGVUser();
                    }
                }
            }

            private void cbbSearchUser_SelectedIndexChanged(object sender, EventArgs e)
            {
                txtSearchUser.Text = "";
                txtSearchUser.Focus();
            }

            private void picSearchUser_Click(object sender, EventArgs e)
            {
                string searchBy = cbbSearchUser.SelectedItem.ToString();
                string searchFor = txtSearchUser.Text;
                LoadDRGVUser(searchBy, searchFor);
            }

        //************************************************************************************************//

        // Nhật Ký Hệ Thống Tab
        private void LoadDRGVNKHT()
        {
            List<TransactionDiary> data = NetRoomReader.Instance.GetAllTransactionDiaries();
            drgvNKHT.DataSource = data;
            drgvNKHT.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            drgvNKHT.Columns[0].HeaderText = "ID người dùng";
            drgvNKHT.Columns[1].HeaderText = "Tên người dùng";
            drgvNKHT.Columns[2].HeaderText = "ID tài khoản";
            drgvNKHT.Columns[3].HeaderText = "Ngày nạp";
            drgvNKHT.Columns[4].HeaderText = "Giờ nạp thêm";
            drgvNKHT.Columns[5].HeaderText = "Tiền nạp thêm";
            drgvNKHT.Columns[6].HeaderText = "Ghi Chú";

            List<DateTime> date = new List<DateTime>();
            foreach (TransactionDiary i in data)
            {
                date.Add(i.TransactionDate);
            }
            dateTimePicker1.Value = FindMinDate(date);
            dateTimePicker2.Value = FindMaxDate(date);
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            string searchBy = comboBox2.SelectedItem.ToString();
            string searchFor = textBox2.Text;
            List<TransactionDiary> data = NetRoomReader.Instance.GetListTransactionDiaries(searchBy, searchFor);
            drgvNKHT.DataSource = data;

            List<DateTime> date = new List<DateTime>();
            foreach (TransactionDiary i in data)
            {
                date.Add(i.TransactionDate);
            }
            dateTimePicker1.Value = FindMinDate(date);
            dateTimePicker2.Value = FindMaxDate(date);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.Focus();
        }

        //************************************************************************************************//

        // Nhật ký Giao dịch Tab
        private void LoadDRGVNKGD()
        {
            List<Bill> data = NetRoomReader.Instance.GetAllBills();
            drgvNKGD.DataSource = data;
            drgvNKGD.Columns[0].HeaderText = "ID người giao dịch";
            drgvNKGD.Columns[1].HeaderText = "Tên người giao dịch";
            drgvNKGD.Columns[2].HeaderText = "Ngày giao dịch";
            drgvNKGD.Columns[3].HeaderText = "Tổng tiền";

            List<DateTime> date = new List<DateTime>();
            foreach (Bill i in data)
            {
                date.Add(i.FoundedDate);
            }
            dateTimePicker3.Value = FindMinDate(date);
            dateTimePicker4.Value = FindMaxDate(date);
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            string searchBy = comboBox3.SelectedItem.ToString();
            string searchFor = textBox3.Text;
            List<Bill> data = NetRoomReader.Instance.GetListBills(searchBy, searchFor);
            drgvNKGD.DataSource = data;

            List<DateTime> date = new List<DateTime>();
            foreach (Bill i in data)
            {
                date.Add(i.FoundedDate);
            }
            dateTimePicker3.Value = FindMinDate(date);
            dateTimePicker4.Value = FindMaxDate(date);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox3.Focus();
        }

        //************************************************************************************************//

        // Nhật ký Đăng nhập Tab
        private void LoadDRGVNKDN()
        {
            List<LoginMember> data = NetRoomReader.Instance.GetAllLoginMembers();
            drgvNKDN.DataSource = data;
            drgvNKDN.Columns[0].HeaderText = "Login ID";
            drgvNKDN.Columns[1].HeaderText = "ID tài khoản";
            drgvNKDN.Columns[2].HeaderText = "Tên máy trạm";
            drgvNKDN.Columns[3].HeaderText = "Ngày đăng nhập";
            drgvNKDN.Columns[4].HeaderText = "Thời điểm đăng nhập";
            drgvNKDN.Columns[5].HeaderText = "Thời gian sử dụng";
            drgvNKDN.Columns[6].HeaderText = "Thời điểm đăng xuất";

            List<DateTime> date = new List<DateTime>();
            foreach (LoginMember i in data)
            {
                date.Add(i.LoginDate);
            }
            dateTimePicker5.Value = FindMinDate(date);
            dateTimePicker6.Value = FindMaxDate(date);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            string searchBy = cbbNKDN.SelectedItem.ToString();
            string searchFor = txtNKDN.Text;
            List<LoginMember> data = NetRoomReader.Instance.GetListLoginMembers(searchBy, searchFor);
            drgvNKDN.DataSource = data;

            List<DateTime> date = new List<DateTime>();
            foreach (LoginMember i in data)
            {
                date.Add(i.LoginDate);
            }
            dateTimePicker5.Value = FindMinDate(date);
            dateTimePicker6.Value = FindMaxDate(date);
        }

        private void cbbNKDN_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNKDN.Text = "";
            txtNKDN.Focus();
        }

        //************************************************************************************************//

        // Dịch vụ Tab
        public void ShowCategory()
        {
            drgvFood.DataSource = NetRoomReader.Instance.GetAllFoods();
            drgvFood.Columns[0].HeaderText = "Mã Định Danh";
            drgvFood.Columns[1].HeaderText = "Tên Món Ăn";
            drgvFood.Columns[2].HeaderText = "Thuộc Loại";
            drgvFood.Columns[3].HeaderText = "Đơn Giá";
            drgvFood.Columns[4].HeaderText = "Đơn Vị Tính";
            drgvFood.Columns[5].HeaderText = "Số Lượng Tồn";
            drgvDrink.DataSource = NetRoomReader.Instance.GetAllDrinks();
            drgvDrink.Columns[0].HeaderText = "Mã Định Danh";
            drgvDrink.Columns[1].HeaderText = "Tên Món Ăn";
            drgvDrink.Columns[2].HeaderText = "Thuộc Loại";
            drgvDrink.Columns[3].HeaderText = "Đơn Giá";
            drgvDrink.Columns[4].HeaderText = "Đơn Vị Tính";
            drgvDrink.Columns[5].HeaderText = "Số Lượng Tồn";
            drgvCard.DataSource = NetRoomReader.Instance.GetAllCards();
            drgvCard.Columns[0].HeaderText = "Mã Định Danh";
            drgvCard.Columns[1].HeaderText = "Tên Món Ăn";
            drgvCard.Columns[2].HeaderText = "Thuộc Loại";
            drgvCard.Columns[3].HeaderText = "Đơn Giá";
            drgvCard.Columns[4].HeaderText = "Đơn Vị Tính";
            drgvCard.Columns[5].HeaderText = "Số Lượng Tồn";
        }

        private void LoadCBBDichVu()
        {
            cbbDichVu.Items.Clear();
            cbbDichVu.Items.AddRange(new CBBItem[]{
                new CBBItem(0, "All"),
                new CBBItem(1, "Mã định danh")
            });
            switch (tabCategory.SelectedTab.Text)
            {
                case "Thức ăn":
                    cbbDichVu.Items.AddRange(new CBBItem[]{
                        new CBBItem(2, "Tên món ăn"),
                        new CBBItem(3, "Loại món ăn")
                    });
                    break;
                case "Nước uống":
                    cbbDichVu.Items.AddRange(new CBBItem[]{
                        new CBBItem(2, "Tên thức uống"),
                        new CBBItem(3, "Loại thức uống")
                    });
                    break;
                case "Thẻ cào":
                    cbbDichVu.Items.AddRange(new CBBItem[]{
                        new CBBItem(2, "Tên thẻ cào"),
                        new CBBItem(3, "Loại thẻ cào")
                    });
                    break;
            }
            this.cbbDichVu.SelectedIndex = 0;
        }

        private void tabCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCBBDichVu();
            switch (tabCategory.SelectedTab.Text)
            {
                case "Thức ăn":
                    drgvFood.DataSource = NetRoomReader.Instance.GetAllFoods();
                    break;
                case "Nước uống":
                    drgvDrink.DataSource = NetRoomReader.Instance.GetAllDrinks();
                    break;
                case "Thẻ cào":
                    drgvCard.DataSource = NetRoomReader.Instance.GetAllCards();
                    break;
            }
            txtSearchService.Text = "";
        }

        private void picOrder_Click(object sender, EventArgs e)
        {
            order();
            LoadDRGVNKGD();
        }

        public void order()
        {
            if (tabCategory.SelectedIndex == 0)
            {
                YeuCauDichVu frmOrder = new YeuCauDichVu(Int32.Parse(drgvFood.CurrentRow.Cells[0].Value.ToString()),
                                                    tabCategory.SelectedIndex);
                frmOrder.d += new YeuCauDichVu.MyDel(ShowCategory);
                frmOrder.ShowDialog();
                Show();
            }
            else if (tabCategory.SelectedIndex == 1)
            {
                YeuCauDichVu frmOrder = new YeuCauDichVu(Int32.Parse(drgvDrink.CurrentRow.Cells[0].Value.ToString()),
                                                    tabCategory.SelectedIndex);
                frmOrder.d += new YeuCauDichVu.MyDel(ShowCategory);
                frmOrder.ShowDialog();
                Show();
            }
            else if (tabCategory.SelectedIndex == 2)
            {
                YeuCauDichVu frmOrder = new YeuCauDichVu(Int32.Parse(drgvCard.CurrentRow.Cells[0].Value.ToString()),
                                                    tabCategory.SelectedIndex);
                frmOrder.d += new YeuCauDichVu.MyDel(ShowCategory);
                frmOrder.ShowDialog();
                Show();
            }
        }

        private void picSearch_Click(object sender, EventArgs e)
        {
            if (tabCategory.SelectedIndex == 0)
            {
                drgvFood.DataSource = NetRoomReader.Instance.GetListFood(cbbDichVu.Text, txtSearchService.Text);
            }
            else if (tabCategory.SelectedIndex == 1)
            {
                drgvDrink.DataSource = NetRoomReader.Instance.GetListDrink(cbbDichVu.Text, txtSearchService.Text);
            }
            else if (tabCategory.SelectedIndex == 2)
            {
                drgvCard.DataSource = NetRoomReader.Instance.GetListCard(cbbDichVu.Text, txtSearchService.Text);
            }
        }

        private void drgvFood_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            order();
        }

        private void drgvDrink_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            order();
        }

        private void drgvCard_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            order();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearchService.Text = "";
            txtSearchService.Focus();
        }

        //************************************************************************************************//

        // Nhóm Máy Tab
        private void LoadDRGClientGroup()
        {
            drgvClientGroup.DataSource = NetRoomReader.Instance.GetAllGroupClients();
            drgvClientGroup.Columns[0].HeaderText = "Tên nhóm máy";
            drgvClientGroup.Columns[1].HeaderText = "Mô tả";
        }
        private void LoadDRGVClientGroup(string searchBy, string searchFor)
        {
            drgvClientGroup.DataSource = NetRoomReader.Instance.GetListClientGroup(searchBy, searchFor);
        }

        private void picSearchNhomMay_Click(object sender, EventArgs e)
        {
            string searchBy = cbbNhomMay.SelectedItem.ToString();
            string searchFor = txtNhomMay.Text;
            LoadDRGVClientGroup(searchBy, searchFor);
        }

        private void picAddGroupClient_Click(object sender, EventArgs e)
        {
            if (ServerManager.MemberID != 0)
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Lỗi!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ThemSuaNhomMay frmNhomMay = new ThemSuaNhomMay();
                frmNhomMay.ShowDialog();
                LoadDRGClientGroup();
            }
        }

        private void picDeleteGroupClient_Click(object sender, EventArgs e)
        {
            if (ServerManager.MemberID != 0)
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Lỗi!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhóm máy này?", "Thông Báo!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string clientGroupName = drgvClientGroup.CurrentRow.Cells[0].Value.ToString();
                    try
                    {
                        NetRoomWritter.Instance.DeleteGroupClient(clientGroupName);
                        MessageBox.Show("Xóa nhóm máy thành công!", "Thông báo!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDRGClientGroup();
                    }
                    catch
                    {
                        MessageBox.Show("Bạn không thể xóa nhóm máy này!\nHãy xóa tất cả các máy con thuộc nhóm máy này trước!", "Lỗi!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void picUpdateGroupClient_Click(object sender, EventArgs e)
        {
            if (ServerManager.MemberID != 0)
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Lỗi!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string clientGroupName = drgvClientGroup.CurrentRow.Cells[0].Value.ToString();
                ThemSuaNhomMay frmNhomMay = new ThemSuaNhomMay(clientGroupName);
                frmNhomMay.ShowDialog();
                LoadDRGClientGroup();
            }
        }

        //************************************************************************************************//

        // Nhóm người sử dụng Tab
        private void LoadDRGUserGroup()
        {
            drgvUserGroup.DataSource = NetRoomReader.Instance.GetAllGroupUsers();
            drgvUserGroup.Columns[0].HeaderText = "Tên nhóm người dùng";
            drgvUserGroup.Columns[1].HeaderText = "Loại nhóm người dùng";
        }

        private void LoadDRGVUserGroup(string searchBy, string searchFor)
        {
            drgvUserGroup.DataSource = NetRoomReader.Instance.GetListUserGroup(searchBy, searchFor);
        }

        private void picSearchUserGroup_Click(object sender, EventArgs e)
        {
            string searchBy = cbbUserGroup.SelectedItem.ToString();
            string searchFor = txtUserGroup.Text;
            LoadDRGVUserGroup(searchBy, searchFor);
        }

        //************************************************************************************************//

        // Các hàm khác
        public LoginMember FindLoginMember(string accountUsing)
        {
            Member member = NetRoomReader.Instance.GetMemberByAccountName(accountUsing);
            foreach (LoginMember i in NetRoomReader.Instance.GetAllLoginMembers())
            {
                if (i.MemberID == member.ID && i.LeftTime.Equals(TimeSpan.Zero))
                {
                    return i;
                }
            }
            return null;
        }

        private DateTime FindMinDate(List<DateTime> list)
        {
            if (list.Count == 0) return DateTime.Today;
            DateTime minDate = list[0];
            foreach (DateTime i in list)
            {
                if (i.CompareTo(minDate) < 0) minDate = i;
            }
            return minDate;
        }

        private DateTime FindMaxDate(List<DateTime> list)
        {
            if (list.Count == 0) return DateTime.Today;
            DateTime maxDate = DateTime.MinValue;
            foreach (DateTime i in list)
            {
                if (i.CompareTo(maxDate) > 0) maxDate = i;
            }
            return maxDate;
        }

        //************************************************************************************************//

        // Các hàm xử lý sự kiện trỏ chuột
        private void btnSearch_MouseHover(object sender, EventArgs e)
        {
            btnSearchMember.BorderStyle = BorderStyle.Fixed3D;
        }

        private void btnSearch_MouseLeave(object sender, EventArgs e)
        {
            btnSearchMember.BorderStyle = BorderStyle.None;
        }
        
        private void picAddMember_MouseHover(object sender, EventArgs e)
        {
            picAddMember.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picAddMember_MouseLeave(object sender, EventArgs e)
        {
            picAddMember.BorderStyle = BorderStyle.None;
        }

        private void picUpdateMember_MouseHover(object sender, EventArgs e)
        {
            picUpdateMember.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picUpdateMember_MouseLeave(object sender, EventArgs e)
        {
            picUpdateMember.BorderStyle = BorderStyle.None;
        }
    
        private void picCalculateMoney_MouseHover(object sender, EventArgs e)
        {
            picCalculateMoney.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picCalculateMoney_MouseLeave(object sender, EventArgs e)
        {
            picCalculateMoney.BorderStyle = BorderStyle.None;
        }   

        private void picOpenClient_MouseHover(object sender, EventArgs e)
        {
            picOpenClient.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picOpenClient_MouseLeave(object sender, EventArgs e)
        {
            picOpenClient.BorderStyle = BorderStyle.None;
        }

        private void picLockClient_MouseHover(object sender, EventArgs e)
        {
            picLockClient.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picLockClient_MouseLeave(object sender, EventArgs e)
        {
            picLockClient.BorderStyle = BorderStyle.None;
        }

        private void picShutdownClient_MouseHover(object sender, EventArgs e)
        {
            picShutdownClient.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picShutdownClient_MouseLeave(object sender, EventArgs e)
        {
            picShutdownClient.BorderStyle = BorderStyle.None;
        }

        private void picAddUser_MouseHover(object sender, EventArgs e)
        {
            picAddUser.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picAddUser_MouseLeave(object sender, EventArgs e)
        {
            picAddUser.BorderStyle = BorderStyle.None;
        }

        private void picDeleteUser_MouseHover(object sender, EventArgs e)
        {
            picDeleteUser.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picDeleteUser_MouseLeave(object sender, EventArgs e)
        {
            picDeleteUser.BorderStyle = BorderStyle.None;
        }

        private void picUpdateUser_MouseHover(object sender, EventArgs e)
        {
            picUpdateUser.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picUpdateUser_MouseLeave(object sender, EventArgs e)
        {
            picUpdateUser.BorderStyle = BorderStyle.None;
        }

        private void picSearchUser_MouseHover(object sender, EventArgs e)
        {
            picSearchUser.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picSearchUser_MouseLeave(object sender, EventArgs e)
        {
            picSearchUser.BorderStyle = BorderStyle.None;
        }

        private void pictureBox18_MouseHover(object sender, EventArgs e)
        {
            pictureBox18.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox18_MouseLeave(object sender, EventArgs e)
        {
            pictureBox18.BorderStyle = BorderStyle.None;
        }

        private void pictureBox19_MouseHover(object sender, EventArgs e)
        {
            pictureBox19.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox19_MouseLeave(object sender, EventArgs e)
        {
            pictureBox19.BorderStyle = BorderStyle.None;
        }

        private void picSearchNhomMay_MouseHover(object sender, EventArgs e)
        {
            picSearchNhomMay.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picSearchNhomMay_MouseLeave(object sender, EventArgs e)
        {
            picSearchNhomMay.BorderStyle = BorderStyle.None;
        }

        private void picSearch_MouseHover(object sender, EventArgs e)
        {
            picSearch.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picSearch_MouseLeave(object sender, EventArgs e)
        {
            picSearch.BorderStyle = BorderStyle.None;
        }

        private void picOrder_MouseHover(object sender, EventArgs e)
        {
            picOrder.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picOrder_MouseLeave(object sender, EventArgs e)
        {
            picOrder.BorderStyle = BorderStyle.None;
        }

        private void picSearchUserGroup_MouseHover(object sender, EventArgs e)
        {
            picSearchUserGroup.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picSearchUserGroup_MouseLeave(object sender, EventArgs e)
        {
            picSearchUserGroup.BorderStyle = BorderStyle.None;
        }

        private void picAddMay_MouseHover(object sender, EventArgs e)
        {
            picAddMay.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picAddMay_MouseLeave(object sender, EventArgs e)
        {
            picAddMay.BorderStyle = BorderStyle.None;
        }

        private void picXoaMay_MouseHover(object sender, EventArgs e)
        {
            picXoaMay.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picXoaMay_MouseLeave(object sender, EventArgs e)
        {
            picXoaMay.BorderStyle = BorderStyle.None;
        }

        private void picUpdateMay_MouseHover(object sender, EventArgs e)
        {
            picUpdateMay.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picUpdateMay_MouseLeave(object sender, EventArgs e)
        {
            picUpdateMay.BorderStyle = BorderStyle.None;
        }

        private void picAddGroupClient_MouseHover(object sender, EventArgs e)
        {
            picAddGroupClient.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picAddGroupClient_MouseLeave(object sender, EventArgs e)
        {
            picAddGroupClient.BorderStyle = BorderStyle.None;
        }

        private void picDeleteGroupClient_MouseHover(object sender, EventArgs e)
        {
            picDeleteGroupClient.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picDeleteGroupClient_MouseLeave(object sender, EventArgs e)
        {
            picDeleteGroupClient.BorderStyle = BorderStyle.None;
        }

        private void picUpdateGroupClient_MouseHover(object sender, EventArgs e)
        {
            picUpdateGroupClient.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picUpdateGroupClient_MouseLeave(object sender, EventArgs e)
        {
            picUpdateGroupClient.BorderStyle = BorderStyle.None;
        }
       
        private void picSendMessage_MouseHover(object sender, EventArgs e)
        {
            picSendMessage.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picSendMessage_MouseLeave(object sender, EventArgs e)
        {
            picSendMessage.BorderStyle = BorderStyle.None;
        }

        private void picMemberInfo_MouseHover(object sender, EventArgs e)
        {
            picMemberInfo.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picMemberInfo_MouseLeave(object sender, EventArgs e)
        {
            picMemberInfo.BorderStyle = BorderStyle.None;
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BorderStyle = BorderStyle.None;
        }

        //************************************************************************************************//
    }
}