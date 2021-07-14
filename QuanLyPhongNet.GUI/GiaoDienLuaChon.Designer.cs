
namespace QuanLyPhongNet.GUI
{
    partial class GiaoDienLuaChon
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
            this.label4 = new System.Windows.Forms.Label();
            this.llblSignOut = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.picReport = new System.Windows.Forms.PictureBox();
            this.picHome = new System.Windows.Forms.PictureBox();
            this.picCategory = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCategory)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(120, 326);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 29);
            this.label4.TabIndex = 15;
            this.label4.Text = "Giao diện chính";
            this.label4.UseWaitCursor = true;
            // 
            // llblSignOut
            // 
            this.llblSignOut.AutoSize = true;
            this.llblSignOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Italic);
            this.llblSignOut.ForeColor = System.Drawing.Color.Red;
            this.llblSignOut.LinkColor = System.Drawing.Color.Red;
            this.llblSignOut.Location = new System.Drawing.Point(668, 9);
            this.llblSignOut.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llblSignOut.Name = "llblSignOut";
            this.llblSignOut.Size = new System.Drawing.Size(142, 31);
            this.llblSignOut.TabIndex = 14;
            this.llblSignOut.TabStop = true;
            this.llblSignOut.Text = "Đăng Xuất";
            this.llblSignOut.UseWaitCursor = true;
            this.llblSignOut.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblSignOut_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(312, 624);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 29);
            this.label3.TabIndex = 13;
            this.label3.Text = "Báo cáo, thống kê";
            this.label3.UseWaitCursor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(516, 326);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 29);
            this.label2.TabIndex = 12;
            this.label2.Text = "Quản lý danh mục";
            this.label2.UseWaitCursor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 31);
            this.label1.TabIndex = 11;
            this.label1.Text = "Các tiện ích:";
            this.label1.UseWaitCursor = true;
            // 
            // picReport
            // 
            this.picReport.Image = global::QuanLyPhongNet.GUI.Properties.Resources.pngtree_list_icon_sign_png_image_3569690;
            this.picReport.Location = new System.Drawing.Point(273, 377);
            this.picReport.Margin = new System.Windows.Forms.Padding(4);
            this.picReport.Name = "picReport";
            this.picReport.Size = new System.Drawing.Size(279, 238);
            this.picReport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picReport.TabIndex = 10;
            this.picReport.TabStop = false;
            this.picReport.UseWaitCursor = true;
            this.picReport.Click += new System.EventHandler(this.picReport_Click);
            this.picReport.MouseLeave += new System.EventHandler(this.picReport_MouseLeave);
            this.picReport.MouseHover += new System.EventHandler(this.picReport_MouseHover);
            // 
            // picHome
            // 
            this.picHome.BackColor = System.Drawing.Color.Transparent;
            this.picHome.Image = global::QuanLyPhongNet.GUI.Properties.Resources._261_2619202_home_icon_png_red_transparent_png;
            this.picHome.Location = new System.Drawing.Point(71, 78);
            this.picHome.Margin = new System.Windows.Forms.Padding(4);
            this.picHome.Name = "picHome";
            this.picHome.Size = new System.Drawing.Size(279, 238);
            this.picHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHome.TabIndex = 9;
            this.picHome.TabStop = false;
            this.picHome.UseWaitCursor = true;
            this.picHome.Click += new System.EventHandler(this.picHome_Click);
            this.picHome.MouseLeave += new System.EventHandler(this.picHome_MouseLeave);
            this.picHome.MouseHover += new System.EventHandler(this.picHome_MouseHover);
            // 
            // picCategory
            // 
            this.picCategory.BackColor = System.Drawing.SystemColors.Control;
            this.picCategory.Image = global::QuanLyPhongNet.GUI.Properties.Resources.download;
            this.picCategory.Location = new System.Drawing.Point(476, 78);
            this.picCategory.Margin = new System.Windows.Forms.Padding(4);
            this.picCategory.Name = "picCategory";
            this.picCategory.Size = new System.Drawing.Size(279, 238);
            this.picCategory.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCategory.TabIndex = 8;
            this.picCategory.TabStop = false;
            this.picCategory.UseWaitCursor = true;
            this.picCategory.Click += new System.EventHandler(this.picCategory_Click);
            this.picCategory.MouseLeave += new System.EventHandler(this.picCategory_MouseLeave);
            this.picCategory.MouseHover += new System.EventHandler(this.picCategory_MouseHover);
            // 
            // GiaoDienLuaChon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(832, 658);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.llblSignOut);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picReport);
            this.Controls.Add(this.picHome);
            this.Controls.Add(this.picCategory);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "GiaoDienLuaChon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tiện ích";
            this.Load += new System.EventHandler(this.GiaoDienLuaChon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCategory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel llblSignOut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picReport;
        private System.Windows.Forms.PictureBox picHome;
        private System.Windows.Forms.PictureBox picCategory;
    }
}