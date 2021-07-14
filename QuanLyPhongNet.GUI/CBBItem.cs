using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.GUI
{
    class CBBItem
    {
        
        public int value { get; set; }
        public string Text { get; set; }
        public CBBItem(int value, string text)
        {
            this.value = value;
            this.Text = text;
        }
        public override string ToString()
        {
            return Text;
        }
    }
}
