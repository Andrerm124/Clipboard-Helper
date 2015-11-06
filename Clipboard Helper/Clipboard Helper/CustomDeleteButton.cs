using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard_Helper
{
    public partial class CustomDeleteButton : UserControl
    {
        Boolean mouseOver = false;

        public CustomDeleteButton()
        {
            InitializeComponent();
        }

        private void CustomDeleteButton_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Loaded");
        }

        private void CustomDeleteButton_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine("Painting");

            if(mouseOver)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(75, 255, 130, 0)), ClientRectangle);
        }

        private void CustomDeleteButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Clicked");
            this.ParentForm.Hide();
        }

        private void CustomDeleteButton_MouseEnter(object sender, EventArgs e)
        {
            mouseOver = true;
            this.Invalidate();
        }

        private void CustomDeleteButton_MouseLeave(object sender, EventArgs e)
        {
            mouseOver = false;
            this.Invalidate();
        }
    }
}
