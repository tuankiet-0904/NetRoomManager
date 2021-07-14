
namespace May_1
{
    partial class HopTienIch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.currentFPS = new System.Windows.Forms.TextBox();
            this.txtFrameRate = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSystemInfo = new System.Windows.Forms.Button();
            this.btnSoundSetting = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTimeSetting = new System.Windows.Forms.Button();
            this.btnKeyboardSetting = new System.Windows.Forms.Button();
            this.btnMouseSetting = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.currentFPS);
            this.panel1.Controls.Add(this.txtFrameRate);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(27, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(408, 384);
            this.panel1.TabIndex = 0;
            // 
            // currentFPS
            // 
            this.currentFPS.BackColor = System.Drawing.SystemColors.Control;
            this.currentFPS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.currentFPS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.currentFPS.Location = new System.Drawing.Point(254, 12);
            this.currentFPS.Name = "currentFPS";
            this.currentFPS.Size = new System.Drawing.Size(70, 19);
            this.currentFPS.TabIndex = 3;
            // 
            // txtFrameRate
            // 
            this.txtFrameRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtFrameRate.FormattingEnabled = true;
            this.txtFrameRate.ItemHeight = 20;
            this.txtFrameRate.Location = new System.Drawing.Point(22, 38);
            this.txtFrameRate.Name = "txtFrameRate";
            this.txtFrameRate.Size = new System.Drawing.Size(355, 284);
            this.txtFrameRate.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnOK.Location = new System.Drawing.Point(22, 330);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(111, 45);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Đồng ý";
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(17, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(234, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tần số quét màn hình hiện tại:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(46, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Độ phân giải hiện tại: 1920 x 1080";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(46, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tiêu chuẩn màu hiện tại: 32bit";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnSystemInfo);
            this.panel2.Controls.Add(this.btnSoundSetting);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.btnTimeSetting);
            this.panel2.Controls.Add(this.btnKeyboardSetting);
            this.panel2.Controls.Add(this.btnMouseSetting);
            this.panel2.Location = new System.Drawing.Point(473, 68);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(287, 427);
            this.panel2.TabIndex = 3;
            // 
            // btnSystemInfo
            // 
            this.btnSystemInfo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSystemInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSystemInfo.Location = new System.Drawing.Point(20, 354);
            this.btnSystemInfo.Name = "btnSystemInfo";
            this.btnSystemInfo.Size = new System.Drawing.Size(248, 60);
            this.btnSystemInfo.TabIndex = 4;
            this.btnSystemInfo.Text = "Tổng quan hệ thống";
            this.btnSystemInfo.UseVisualStyleBackColor = false;
            this.btnSystemInfo.Click += new System.EventHandler(this.btnSystemInfo_Click);
            // 
            // btnSoundSetting
            // 
            this.btnSoundSetting.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSoundSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSoundSetting.Location = new System.Drawing.Point(20, 275);
            this.btnSoundSetting.Name = "btnSoundSetting";
            this.btnSoundSetting.Size = new System.Drawing.Size(248, 60);
            this.btnSoundSetting.TabIndex = 3;
            this.btnSoundSetting.Text = "Điều chỉnh hệ thống âm thanh";
            this.btnSoundSetting.UseVisualStyleBackColor = false;
            this.btnSoundSetting.Click += new System.EventHandler(this.btnSoundSetting_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(76, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Các tiện ích khác";
            // 
            // btnTimeSetting
            // 
            this.btnTimeSetting.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTimeSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnTimeSetting.Location = new System.Drawing.Point(20, 194);
            this.btnTimeSetting.Name = "btnTimeSetting";
            this.btnTimeSetting.Size = new System.Drawing.Size(248, 60);
            this.btnTimeSetting.TabIndex = 2;
            this.btnTimeSetting.Text = "Điều chỉnh thời gian hệ thông";
            this.btnTimeSetting.UseVisualStyleBackColor = false;
            this.btnTimeSetting.Click += new System.EventHandler(this.btnTimeSetting_Click);
            // 
            // btnKeyboardSetting
            // 
            this.btnKeyboardSetting.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnKeyboardSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnKeyboardSetting.Location = new System.Drawing.Point(20, 115);
            this.btnKeyboardSetting.Name = "btnKeyboardSetting";
            this.btnKeyboardSetting.Size = new System.Drawing.Size(248, 60);
            this.btnKeyboardSetting.TabIndex = 1;
            this.btnKeyboardSetting.Text = "Điều chinh thông số bàn phím";
            this.btnKeyboardSetting.UseVisualStyleBackColor = false;
            this.btnKeyboardSetting.Click += new System.EventHandler(this.btnKeyboardSetting_Click);
            // 
            // btnMouseSetting
            // 
            this.btnMouseSetting.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnMouseSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnMouseSetting.Location = new System.Drawing.Point(20, 35);
            this.btnMouseSetting.Name = "btnMouseSetting";
            this.btnMouseSetting.Size = new System.Drawing.Size(248, 60);
            this.btnMouseSetting.TabIndex = 0;
            this.btnMouseSetting.Text = "Điều chỉnh thông số chuột";
            this.btnMouseSetting.UseVisualStyleBackColor = false;
            this.btnMouseSetting.Click += new System.EventHandler(this.btnMouseSetting_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button7.Location = new System.Drawing.Point(685, 501);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 47);
            this.button7.TabIndex = 5;
            this.button7.Text = "Đóng";
            this.button7.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(45, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(289, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hiệu chỉnh độ phân giải của màn hình";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // HopTienIch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "HopTienIch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hộp tiện ích";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSystemInfo;
        private System.Windows.Forms.Button btnSoundSetting;
        private System.Windows.Forms.Button btnTimeSetting;
        private System.Windows.Forms.Button btnKeyboardSetting;
        private System.Windows.Forms.Button btnMouseSetting;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox txtFrameRate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox currentFPS;
    }
}