using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace May_1
{
    public partial class HopTienIch : Form
    {
        private Random random;

        // Create the project folder's directory
        private static String Debug_folder = Directory.GetCurrentDirectory(); // Directory of debug folder
        private static String Bin_folder = Directory.GetParent(Debug_folder).FullName; // Directory of bin folder
        // Directory of project folder
        private static String Project_folder = Directory.GetParent(Bin_folder).FullName;
        // Directory of access history file
        private static String Keyboard_path = Project_folder + "\\TienIchShortcut\\Keyboard";
        private static String System_path = Project_folder + "\\TienIchShortcut\\System";

        public HopTienIch()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Start();
            random = new Random();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            int avgFrameRate = random.Next(50, 70);
            txtFrameRate.Items.Add(avgFrameRate.ToString() + "Hz");
            currentFPS.Text = avgFrameRate.ToString() + "Hz";
        }

        private void btnMouseSetting_Click(object sender, EventArgs e)
        {
            Process.Start("C:\\WINDOWS\\system32\\main.cpl");
        }

        private void btnKeyboardSetting_Click(object sender, EventArgs e)
        {
            Process.Start(Keyboard_path);
        }

        private void btnTimeSetting_Click(object sender, EventArgs e)
        {
            Process.Start("C:\\WINDOWS\\system32\\timeDate.cpl");
        }

        private void btnSoundSetting_Click(object sender, EventArgs e)
        {
            Process.Start("C:\\WINDOWS\\system32\\mmsys.cpl");
        }

        private void btnSystemInfo_Click(object sender, EventArgs e)
        {
            Process.Start(System_path);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
