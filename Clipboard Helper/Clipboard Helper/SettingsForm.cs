using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard_Helper
{
    public partial class SettingsForm : Form
    {
        public Boolean beingDragged = false;
        public Point clickOffset = new Point(0, 0);

        public SettingsForm()
        {
            InitializeComponent();

            foreach (Control panels in this.Controls)
            {
                if (panels is CustomPanel)
                {
                    ((CustomPanel)panels).Initialized();

                    foreach (Control c in panels.Controls)
                    {
                        if (c is CustomListBox)
                            ((CustomListBox)c).Initialized();
                        else if (c is CustomPanel)
                            ((CustomPanel)c).Initialized();
                    }
                }
            }

            txtDirectory.Text = MasterForm.resourcePath;
        }

        private void pnlBackground_MouseDown(object sender, MouseEventArgs e)
        {
            beingDragged = true;
            clickOffset.X = e.X;
            clickOffset.Y = e.Y;
        }

        private void pnlBackground_MouseMove(object sender, MouseEventArgs e)
        {
            if (beingDragged)
            {
                Point mousePos = this.PointToScreen(e.Location);

                int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

                int newX = mousePos.X - clickOffset.X;
                int newY = mousePos.Y - clickOffset.Y;

                if (newX < 1)
                    newX = 1;
                else if (newX + this.Width > screenWidth)
                    newX = screenWidth - this.Width - 1;

                if (newY < 1)
                    newY = 1;
                else if (newY + this.Height > screenHeight)
                    newY = screenHeight - this.Height - 1;

                this.Left = newX;
                this.Top = newY;
            }
        }

        private void pnlBackground_MouseUp(object sender, MouseEventArgs e)
        {
            beingDragged = false;
        }

        private void customDeleteButton2_Load(object sender, EventArgs e)
        {
        }

        private void customDeleteButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnlBackground_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = MasterForm.resourcePath;
            DialogResult dialog = folderBrowserDialog.ShowDialog();

            if (dialog == DialogResult.OK)
            {
                Console.WriteLine("Path: " + folderBrowserDialog.SelectedPath);
                MasterForm.UpdateResourcePath(folderBrowserDialog.SelectedPath);
            }
        }
    }
}
