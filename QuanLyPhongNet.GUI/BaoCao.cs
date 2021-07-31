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
    public partial class BaoCao : Form
    {
        private float[] totalIncome;
        public BaoCao()
        {
            InitializeComponent();
            totalIncome = new float[13];
        }

        //************************************************************************************************//

        // Giao diện Báo cáo
        private void BaoCao_Load(object sender, EventArgs e)
        {
            CalculateIncome();
            LoadDGVNKHT();
            LoadDGVNKGD();
            LoadChartThongKe();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    LoadDGVNKHT();
                    break;
                case 1:
                    LoadDGVNKGD();
                    break;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //************************************************************************************************//

        // Nạp Tiền Tab
        private void LoadDGVNKHT()
        {
            drgvNKHT.DataSource = NetRoomReader.Instance.GetAllTransactionDiaries();
            drgvNKHT.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            drgvNKHT.Columns[0].HeaderText = "ID tài khoản";
            drgvNKHT.Columns[1].HeaderText = "Người sử dụng";
            drgvNKHT.Columns[2].HeaderText = "Ngày nạp";
            drgvNKHT.Columns[3].HeaderText = "Tiền nạp thêm";
            drgvNKHT.Columns[4].HeaderText = "Giờ nạp thêm";
            drgvNKHT.Columns[5].HeaderText = "Ghi Chú";
        }

        //************************************************************************************************//

        // Giao Dịch Tab
        private void LoadDGVNKGD()
        {   
            drgvNKGD.DataSource = NetRoomReader.Instance.GetAllBills();
            drgvNKGD.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            drgvNKGD.Columns[0].HeaderText = "ID tài khoản";
            drgvNKGD.Columns[1].HeaderText = "Người Giao Dịch";
            drgvNKGD.Columns[2].HeaderText = "Ngày Giao Dịch";
            drgvNKGD.Columns[3].HeaderText = "Tổng tiền";
        }

        //************************************************************************************************//

        // Thống Kê Tab
        private void LoadChartThongKe()
        {
            chartThongKe.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chartThongKe.ChartAreas["ChartArea1"].AxisX.Title = "Tháng";
            chartThongKe.ChartAreas["ChartArea1"].AxisY.Title = "VND";
            chartThongKe.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font("Microsoft Sans Serif", 12);
            chartThongKe.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12);
            for (int i = 1; i <= 12; i++)
            {
                chartThongKe.Series["ThongKe"].Points.AddXY(i, totalIncome[i]);
            }
        }


        //************************************************************************************************//

        // Các hàm khác
        private void CalculateIncome()
        {
            foreach (DTO.TransactionDiary i in NetRoomReader.Instance.GetAllTransactionDiaries())
            {
                int month = i.TransactionDate.Month;
                totalIncome[month] += i.AddMoney;
            }
            foreach (DTO.Bill i in NetRoomReader.Instance.GetAllBills())
            {
                int month = i.FoundedDate.Month;
                totalIncome[month] += i.PriceTotal;
            }
        }
        
    }
}
