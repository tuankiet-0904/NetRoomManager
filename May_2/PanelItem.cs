using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace May_2
{
    class PanelItem : Panel
    {
        // Directory of Resources folder
        private static String Resources_folder = Directory.GetCurrentDirectory().Replace("bin\\Debug", "Resources");

        public delegate void ValueChangedEventHandler(object sender, EventArgs e, String name, float price);
        public ValueChangedEventHandler ValueChanged;

        public String itemCategory;
        public String itemName;
        public String itemFullName;
        public float itemPrice;
        public int itemInventory;

        public PanelItem(String itemCategory, String itemName, float itemPrice, int itemInventory)
        {
            this.itemCategory = itemCategory;
            this.itemName = itemCategory.Equals("Card") ? itemName.Split(' ')[0] : itemName;
            this.itemFullName = itemName;
            this.itemPrice = itemPrice;
            this.itemInventory = itemInventory;

            // Create Panel
            Size = new Size(250, 100);
            BorderStyle = BorderStyle.FixedSingle;

            PictureBox itemPic = new PictureBox();
            itemPic.Location = new Point(5, 5);
            itemPic.Size = new Size(90, 90);
            itemPic.BorderStyle = BorderStyle.FixedSingle;
            itemPic.Image = Image.FromFile(Resources_folder + "\\" + this.itemCategory + "\\" + this.itemName + ".jpg");
            itemPic.SizeMode = PictureBoxSizeMode.StretchImage;

            Label itemNameLabel = new Label();
            itemNameLabel.Text = itemName;
            itemNameLabel.Location = new Point(100, 5);
            itemNameLabel.Size = new Size(140, 20);

            Label itemPriceLabel = new Label();
            itemPriceLabel.Text = "Giá: " + itemPrice.ToString() + " VND";
            itemPriceLabel.Location = new Point(100, 25);
            itemPriceLabel.Size = new Size(140, 20);

            Label itemInventoryLabel = new Label();
            itemInventoryLabel.Text = "Số lượng: " + itemInventory.ToString();
            itemInventoryLabel.Location = new Point(100, 45);
            itemInventoryLabel.Size = new Size(140, 20);

            NumericUpDown numericBar = new NumericUpDown();
            numericBar.ValueChanged += numeric_ValueChanged;
            numericBar.Value = 0;
            numericBar.Maximum = itemInventory;
            numericBar.Minimum = 0;
            numericBar.TextAlign = HorizontalAlignment.Center;
            numericBar.Location = new Point(100, 70);
            numericBar.Size = new Size(140, 25);

            Controls.Add(itemPic);
            Controls.Add(itemNameLabel);
            Controls.Add(itemPriceLabel);
            Controls.Add(itemInventoryLabel);
            Controls.Add(numericBar);
        }

        void numeric_ValueChanged(object sender, EventArgs e)
        {
            ValueChanged(sender, e, itemFullName, itemPrice);
        }
    }
}
